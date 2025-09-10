using MunicipalServiceApp.Application.Abstractions;
using System;
using System.Windows.Forms;

namespace MunicipalServiceApp.Presentation
{
    public partial class MainMenuForm : Form
    {
        private readonly IIssueService _issueService;
        private readonly IGeocodingService _geo;

        public MainMenuForm() : this(null!, null!) { }

        public MainMenuForm(IIssueService issueService, IGeocodingService geo)
        {
            _issueService = issueService ?? throw new ArgumentNullException(nameof(issueService));
            _geo = geo ?? throw new ArgumentNullException(nameof(geo));

            InitializeComponent();
            Text = "Municipal Services - Main Menu";
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void MainMenuForm_Load(object? sender, EventArgs e)
        {
            // styling or layout hooks if needed
        }

        private void btnReportIssues_Click(object? sender, EventArgs e)
        {
            using var frm = new ReportIssuesForm(_issueService, _geo);
            Hide();
            frm.ShowDialog(this);
            Show();
        }

        private void btnMyReports_Click(object? sender, EventArgs e)
        {
            using var frm = new MyReportedIssuesForm(_issueService);
            Hide();
            frm.ShowDialog(this);
            Show();
        }

        private void btnLocalEvents_Click(object? sender, EventArgs e)
        {
            // Disabled in Part 1
        }

        private void btnServiceStatus_Click(object? sender, EventArgs e)
        {
            // Disabled in Part 1
        }
    }
}
