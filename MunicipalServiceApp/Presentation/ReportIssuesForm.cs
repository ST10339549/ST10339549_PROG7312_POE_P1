using MunicipalServiceApp.Application.Abstractions;
using MunicipalServiceApp.Domain;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MunicipalServiceApp.Presentation
{
    public partial class ReportIssuesForm : Form
    {
        private readonly IIssueService _issueService;
        private readonly IGeocodingService _geo;
        private string _attachedPath = string.Empty;

        public ReportIssuesForm() : this(null!, null!) { }

        public ReportIssuesForm(IIssueService issueService, IGeocodingService geo)
        {
            _issueService = issueService ?? throw new ArgumentNullException(nameof(issueService));
            _geo = geo ?? throw new ArgumentNullException(nameof(geo));

            InitializeComponent();
            Text = "Report Issues";
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void ReportIssuesForm_Load(object? sender, EventArgs e)
        {
            PopulateCategories();

            lblStatus.Text = "Awaiting submission…";
            prgEngagement.Minimum = 0;
            prgEngagement.Maximum = 100;
            prgEngagement.Value = 0;

            lblAttachmentPath.Text = "No file selected";
        }

        private void PopulateCategories()
        {
            cmbCategory.DataSource = null;
            cmbCategory.Items.Clear();

            foreach (var c in Domain.Categories.All())
                cmbCategory.Items.Add(c);

            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            if (cmbCategory.Items.Count > 0)
                cmbCategory.SelectedIndex = 0;
        }

        private void btnAttach_Click(object? sender, EventArgs e)
        {
            using var dlg = new OpenFileDialog
            {
                Title = "Attach a photo or document",
                Filter = "Images or PDF|*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.pdf|All Files|*.*",
                Multiselect = false
            };
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                _attachedPath = dlg.FileName;
                lblAttachmentPath.Text = _attachedPath;
            }
        }

        // Address validation (geocoding) happens BEFORE saving
        private async void btnSubmit_Click(object? sender, EventArgs e)
        {
            prgEngagement.Value = 0;
            lblStatus.Text = "Validating address…";

            var rawAddress = txtLocation.Text?.Trim() ?? string.Empty;
            var addr = await _geo.ValidateAsync(rawAddress);
            if (!addr.Success)
            {
                MessageBox.Show(addr.ErrorMessage, "Invalid Address",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lblStatus.Text = "Awaiting submission…";
                txtLocation.Focus();
                return;
            }

            // Build the issue with normalized address + coords
            var issue = new Issue
            {
                Location = addr.NormalizedAddress,
                Latitude = addr.Latitude,
                Longitude = addr.Longitude,
                Category = cmbCategory.SelectedItem?.ToString() ?? string.Empty,
                Description = rtbDescription.Text?.Trim() ?? string.Empty,
                AttachmentPath = _attachedPath
            };

            lblStatus.Text = "Validating…";
            var result = _issueService.CreateIssue(issue);
            if (!result.Success)
            {
                MessageBox.Show(result.ErrorMessage, "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lblStatus.Text = "Awaiting submission…";
                return;
            }

            lblStatus.Text = "Submitting…";
            await AnimateProgressBarAsync(0, 100, 600);

            var token = result.Value ?? "(unavailable)";
            lblStatus.Text = $"Issue submitted. Tracking #: {token}";

            try { Clipboard.SetText(token); } catch { /* ignore clipboard errors */ }

            MessageBox.Show(
                $"Your report has been logged successfully.\n\nTracking #: {token}\n\n" +
                "The tracking number has been copied to your clipboard.",
                "Submitted",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            // Reset inputs for the next report
            txtLocation.Clear();
            rtbDescription.Clear();
            if (cmbCategory.Items.Count > 0) cmbCategory.SelectedIndex = 0;
            _attachedPath = string.Empty;
            lblAttachmentPath.Text = "No file selected";
            prgEngagement.Value = 0;
            txtLocation.Focus();
        }

        private async Task AnimateProgressBarAsync(int from, int to, int durationMs)
        {
            if (to < from) (from, to) = (to, from);
            int steps = 20;
            int delay = Math.Max(1, durationMs / steps);
            for (int i = 0; i <= steps; i++)
            {
                int val = from + (int)((to - from) * (i / (double)steps));
                prgEngagement.Value = Math.Min(prgEngagement.Maximum, Math.Max(prgEngagement.Minimum, val));
                await Task.Delay(delay);
            }
        }

        private void btnBack_Click(object? sender, EventArgs e) => Close();
    }
}
