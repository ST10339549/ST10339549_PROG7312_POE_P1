using MunicipalServiceApp.Application.Abstractions;
using MunicipalServiceApp.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MunicipalServiceApp.Presentation
{
    public partial class MyReportedIssuesForm : Form
    {
        private readonly IIssueService _issueService;

        public MyReportedIssuesForm(IIssueService issueService)
        {
            _issueService = issueService ?? throw new ArgumentNullException(nameof(issueService));
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            Text = "My Submitted Reports";

            // Window chrome
            FormBorderStyle = FormBorderStyle.Sizable;
            MinimizeBox = MaximizeBox = true;
            MinimumSize = new Size(1000, 700);

            // Smooth painting
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();

            // Open maximized but keep responsive
            Shown += (_, __) =>
            {
                if (!DesignMode)
                    WindowState = FormWindowState.Maximized;

                CenterCard();
            };
            SizeChanged += (_, __) => CenterCard();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }

        private void CenterCard()
        {
            if (body == null || card == null) return;

            var x = Math.Max(0, (body.ClientSize.Width - card.Width) / 2);
            card.Left = x;
            card.Top  = 16;
        }

        private void MyReportedIssuesForm_Load(object? sender, EventArgs e)
        {
            Populate();
        }

        private void Populate()
        {
            lvReports.BeginUpdate();
            lvReports.Items.Clear();

            // Pull from memory (custom DS underneath)
            foreach (var issue in _issueService.All())
            {
                var created = issue.CreatedAt == default ? DateTime.Now : issue.CreatedAt;

                var item = new ListViewItem(issue.TrackingNumber ?? "-");
                item.SubItems.Add(issue.Location ?? "-");
                item.SubItems.Add(issue.Category ?? "-");
                item.SubItems.Add(Shorten(issue.Description, 80));
                item.SubItems.Add(string.IsNullOrWhiteSpace(issue.AttachmentPath) ? "-" : "Open file");
                item.SubItems.Add(created.ToString("yyyy-MM-dd HH:mm"));

                // keep full attachment path in Tag so we can open it
                item.Tag = issue;
                lvReports.Items.Add(item);
            }

            lvReports.EndUpdate();
        }

        private static string Shorten(string? s, int max)
        {
            if (string.IsNullOrEmpty(s)) return "-";
            if (s.Length <= max) return s;
            return s.Substring(0, max - 1) + "…";
        }

        private void btnBack_Click(object? sender, EventArgs e) => Close();

        private void btnRefresh_Click(object? sender, EventArgs e) => Populate();

        private void btnOpenAttachment_Click(object? sender, EventArgs e) => OpenSelectedAttachment();

        private void lvReports_DoubleClick(object? sender, EventArgs e) => OpenSelectedAttachment();

        private void OpenSelectedAttachment()
        {
            if (lvReports.SelectedItems.Count == 0) return;
            var issue = lvReports.SelectedItems[0].Tag as Issue;
            if (issue == null) return;

            var path = issue.AttachmentPath;
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                MessageBox.Show("No attachment found for this report.", "Attachment",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not open the attachment.\n\n" + ex.Message,
                    "Attachment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
