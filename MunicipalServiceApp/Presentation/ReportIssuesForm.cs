using MunicipalServiceApp.Application.Abstractions;
using MunicipalServiceApp.Domain;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MunicipalServiceApp.Presentation
{
    public partial class ReportIssuesForm : Form
    {
        // Reduce flicker on resize/scroll (optional, safe for WinForms)
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }
        private readonly IIssueService _issueService;
        private readonly IGeocodingService _geo;
        private string _attachedPath = string.Empty;

        // prevents live progress updates from fighting the submit animation
        private bool _isSubmitting = false;

        public ReportIssuesForm() : this(null!, null!) { }

        public ReportIssuesForm(IIssueService issueService, IGeocodingService geo)
        {
            _issueService = issueService ?? throw new ArgumentNullException(nameof(issueService));
            _geo = geo ?? throw new ArgumentNullException(nameof(geo));

            InitializeComponent();
            Text = "Report Issues";
            StartPosition = FormStartPosition.CenterScreen;

            // Open maximized but allow resize/move when restored
            FormBorderStyle = FormBorderStyle.Sizable;
            MinimizeBox = MaximizeBox = true;
            MinimumSize = new System.Drawing.Size(1000, 700);

            // double-buffering
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();

            // Maximize on first show (Designer-safe), then keep card centered on resize
            Shown += (_, __) =>
            {
                if (!DesignMode)
                    WindowState = FormWindowState.Maximized;

                CenterCard();
            };
            SizeChanged += (_, __) => CenterCard();

            // wire live-progress events right after InitializeComponent()
            WireProgressEvents();
        }

        private void ReportIssuesForm_Load(object? sender, EventArgs e)
        {
            PopulateCategories();

            prgEngagement.Minimum = 0;
            prgEngagement.Maximum = 100;
            prgEngagement.Value = 0;

            lblAttachmentPath.Text = "No file selected";
            lblStatus.Text = "Awaiting submission…";

            // ensure initial progress reflects empty form
            RefreshProgress();
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

        private void WireProgressEvents()
        {
            // Update progress whenever the user types or selects anything
            txtLocation.TextChanged += OnAnyInputChanged;
            rtbDescription.TextChanged += OnAnyInputChanged;
            cmbCategory.SelectedIndexChanged += OnAnyInputChanged;
        }

        private void OnAnyInputChanged(object? sender, EventArgs e) => RefreshProgress();

        /// <summary>
        /// Computes and applies the current progress % based on filled inputs.
        /// </summary>
        private void RefreshProgress()
        {
            if (_isSubmitting) return;

            int p = 0;

            // Weighting (adds to 100)
            if (!string.IsNullOrWhiteSpace(txtLocation.Text)) p += 30;          // Location
            if (cmbCategory.SelectedIndex >= 0) p += 20;                         // Category
            if ((rtbDescription.Text?.Trim().Length ?? 0) >= 10) p += 30;        // Description
            if (!string.IsNullOrEmpty(_attachedPath)) p += 20;                   // Attachment

            p = Math.Max(0, Math.Min(100, p));
            prgEngagement.Value = p;

            // Helpful hint text
            lblStatus.Text = p switch
            {
                < 30 => "Tip: Enter the location to get started.",
                < 50 => "Tip: Choose a category.",
                < 80 => "Tip: Add a short description (10+ characters).",
                < 100 => "Optional: Attach a photo/doc to reach 100%.",
                _ => "Ready to submit."
            };
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
                RefreshProgress();
            }
        }

        // Address validation (geocoding) happens BEFORE saving
        private async void btnSubmit_Click(object? sender, EventArgs e)
        {
            _isSubmitting = true;

            // Make sure the live % is reflected, then animate to 100 on submit
            var startPct = prgEngagement.Value;
            lblStatus.Text = "Validating address…";

            var rawAddress = txtLocation.Text?.Trim() ?? string.Empty;
            var addr = await _geo.ValidateAsync(rawAddress);
            if (!addr.Success)
            {
                MessageBox.Show(addr.ErrorMessage, "Invalid Address",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _isSubmitting = false;
                RefreshProgress(); // show live progress again
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
                _isSubmitting = false;
                RefreshProgress();
                return;
            }

            lblStatus.Text = "Submitting…";
            await AnimateProgressBarAsync(startPct, 100, 600);

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

            _isSubmitting = false;
            RefreshProgress();
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

        // Helper to keep the card centered in the body panel
        private void CenterCard()
        {
            if (body == null || pnlCard == null) return;

            // center horizontally within the scrollable body
            var x = Math.Max(0, (body.ClientSize.Width - pnlCard.Width) / 2);
            pnlCard.Left = x;

            // keep a comfortable margin from the top of the body area
            pnlCard.Top = 16;
        }
    }
}
