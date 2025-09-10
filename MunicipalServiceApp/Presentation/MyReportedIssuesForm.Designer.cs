namespace MunicipalServiceApp.Presentation
{
    partial class MyReportedIssuesForm
    {
        private System.ComponentModel.IContainer? components = null;

        private Panel header = null!;
        private Label lblTitle = null!;
        private Label lblSub = null!;
        private Panel body = null!;
        private Panel card = null!;
        private Label lblHeading = null!;
        private ListView lvReports = null!;
        private Button btnBack = null!;
        private Button btnRefresh = null!;
        private Button btnOpenAttachment = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            // Header
            header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = Color.FromArgb(92, 71, 173),
                Padding = new Padding(24, 16, 24, 18)
            };
            lblTitle = new Label
            {
                Text = "Municipal Services",
                ForeColor = Color.White,
                AutoSize = true,
                Font = new Font("Segoe UI Semibold", 22f),
                Margin = new Padding(0, 0, 0, 2)
            };
            lblSub = new Label
            {
                Text = "Report issues • Discover local events • Track service requests",
                ForeColor = Color.White,
                AutoSize = true,
                Font = new Font("Segoe UI", 11f),
                Margin = new Padding(0)
            };
            var headerStack = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            headerStack.Controls.Add(lblTitle);
            headerStack.Controls.Add(lblSub);
            header.Controls.Add(headerStack);

            // Body
            body = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(247, 248, 250),
                AutoScroll = true
            };

            // Card
            card = new Panel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(24),
                MinimumSize = new Size(900, 0)
            };
            body.Controls.Add(card);

            // Heading
            lblHeading = new Label
            {
                Text = "My Submitted Reports",
                Font = new Font("Segoe UI Semibold", 14f),
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 12)
            };

            // ListView
            lvReports = new ListView
            {
                View = View.Details,
                FullRowSelect = true,
                HideSelection = false,
                GridLines = false,
                Dock = DockStyle.Top,
                Height = 360
            };
            lvReports.Columns.Add("Tracking #", 160);
            lvReports.Columns.Add("Location", 220);
            lvReports.Columns.Add("Category", 120);
            lvReports.Columns.Add("Description", 260);
            lvReports.Columns.Add("Attachment", 120);
            lvReports.Columns.Add("Created", 140);

            lvReports.DoubleClick += lvReports_DoubleClick;

            // Buttons
            var btnRow = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.RightToLeft,
                Margin = new Padding(0, 8, 0, 0),
                WrapContents = false
            };

            btnBack = new Button { Text = "Back to Main Menu", Width = 160, Height = 36, Margin = new Padding(6, 0, 0, 0) };
            btnBack.Click += btnBack_Click;

            btnOpenAttachment = new Button { Text = "Open Attachment", Width = 150, Height = 36, Margin = new Padding(6, 0, 0, 0) };
            btnOpenAttachment.Click += btnOpenAttachment_Click;

            btnRefresh = new Button { Text = "Refresh", Width = 100, Height = 36, Margin = new Padding(6, 0, 0, 0) };
            btnRefresh.Click += btnRefresh_Click;

            btnRow.Controls.Add(btnBack);
            btnRow.Controls.Add(btnOpenAttachment);
            btnRow.Controls.Add(btnRefresh);

            // Add to card
            card.Controls.Add(btnRow);
            card.Controls.Add(lvReports);
            card.Controls.Add(lblHeading);

            // Center card horizontally
            body.Resize += (_, __) =>
            {
                var x = (body.ClientSize.Width - card.Width) / 2;
                if (x < 0) x = 0;
                card.Left = x;
                card.Top = 16;
            };

            // Form
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(247, 248, 250);
            MinimumSize = new Size(1100, 650);

            Controls.Add(body);
            Controls.Add(header);

            Load += MyReportedIssuesForm_Load;
        }
    }
}