using MunicipalServiceApp.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MunicipalServiceApp.Presentation
{
    public partial class MainMenuForm : Form
    {
        private readonly IIssueService _issueService;

        // Designer-friendly parameterless constructor (for VS Designer)
        public MainMenuForm() : this(null!) { }

        public MainMenuForm(IIssueService issueService)
        {
            _issueService = issueService ?? throw new ArgumentNullException(nameof(issueService));
            InitializeComponent();
            Text = "Municipal Services - Main Menu";
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void MainMenuForm_Load(object? sender, EventArgs e)
        {
            // Nothing yet
        }

        private void btnReportIssues_Click(object? sender, EventArgs e)
        {
            using var frm = new ReportIssuesForm(_issueService);
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
