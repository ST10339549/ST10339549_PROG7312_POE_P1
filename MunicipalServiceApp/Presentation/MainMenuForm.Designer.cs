namespace MunicipalServiceApp.Presentation
{
    partial class MainMenuForm
    {
        private System.ComponentModel.IContainer? components = null;

        private Panel header = null!;
        private Label lblTitle = null!;
        private Label lblSub = null!;
        private Panel pnlMenu = null!;
        private Button btnOpenReport = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            // --- Header banner ---
            header = new Panel
            {
                Height = 100,
                Dock = DockStyle.Top,
                BackColor = System.Drawing.Color.FromArgb(92, 71, 173)
            };

            lblTitle = new Label
            {
                Text = "Municipal Services",
                ForeColor = System.Drawing.Color.White,
                AutoSize = true,
                Font = new System.Drawing.Font("Segoe UI Semibold", 22f),
                Left = 24,
                Top = 20
            };

            lblSub = new Label
            {
                Text = "Report issues • Discover local events • Track service requests",
                ForeColor = System.Drawing.Color.White,
                AutoSize = true,
                Font = new System.Drawing.Font("Segoe UI", 11f),
                Left = 26,
                Top = 62
            };

            header.Controls.Add(lblTitle);
            header.Controls.Add(lblSub);
            Controls.Add(header);

            // --- Centered content area ---
            pnlMenu = new Panel
            {
                Width = 820,
                Height = 300
            };
            Controls.Add(pnlMenu);

            // --- Card 1: Report Issues ---
            var card1 = new Panel
            {
                Width = 250,
                Height = 220,
                Left = 10,
                Top = 30,
                BackColor = System.Drawing.Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            var lblH1 = new Label
            {
                Text = "Report Issues",
                Font = new System.Drawing.Font("Segoe UI Semibold", 14f),
                AutoSize = false,
                Width = 230,
                Left = 10,
                Top = 12
            };
            var lblB1 = new Label
            {
                Text = "Log potholes, water leaks, streetlights and more.\nVerified addresses using maps.",
                Font = new System.Drawing.Font("Segoe UI", 9.25f),
                AutoSize = false,
                Width = 230,
                Height = 110,
                Left = 10,
                Top = 46
            };
            btnOpenReport = new Button
            {
                Text = "Open",
                Width = 100,
                Height = 34,
                Left = 10,
                Top = 165
            };
            btnOpenReport.Click += btnReportIssues_Click;
            card1.Controls.Add(lblH1);
            card1.Controls.Add(lblB1);
            card1.Controls.Add(btnOpenReport);

            // --- Card 2: Local Events (disabled for Part 1) ---
            var card2 = new Panel
            {
                Width = 250,
                Height = 220,
                Left = 285,
                Top = 30,
                BackColor = System.Drawing.Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            var lblH2 = new Label
            {
                Text = "Local Events (Part 2)",
                Font = new System.Drawing.Font("Segoe UI Semibold", 14f),
                AutoSize = false,
                Width = 230,
                Left = 10,
                Top = 12
            };
            var lblB2 = new Label
            {
                Text = "Browse community events and announcements.\nComing soon.",
                Font = new System.Drawing.Font("Segoe UI", 9.25f),
                AutoSize = false,
                Width = 230,
                Height = 110,
                Left = 10,
                Top = 46
            };
            var btn2 = new Button
            {
                Text = "Open",
                Width = 100,
                Height = 34,
                Left = 10,
                Top = 165,
                Enabled = false
            };
            card2.Controls.Add(lblH2);
            card2.Controls.Add(lblB2);
            card2.Controls.Add(btn2);

            // --- Card 3: Service Request Status (disabled for Part 1) ---
            var card3 = new Panel
            {
                Width = 250,
                Height = 220,
                Left = 560,
                Top = 30,
                BackColor = System.Drawing.Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            var lblH3 = new Label
            {
                Text = "Service Request Status (Part 3)",
                Font = new System.Drawing.Font("Segoe UI Semibold", 14f),
                AutoSize = false,
                Width = 230,
                Left = 10,
                Top = 12
            };
            var lblB3 = new Label
            {
                Text = "Track the progress of your submitted requests.\nComing soon.",
                Font = new System.Drawing.Font("Segoe UI", 9.25f),
                AutoSize = false,
                Width = 230,
                Height = 110,
                Left = 10,
                Top = 46
            };
            var btn3 = new Button
            {
                Text = "Open",
                Width = 100,
                Height = 34,
                Left = 10,
                Top = 165,
                Enabled = false
            };
            card3.Controls.Add(lblH3);
            card3.Controls.Add(lblB3);
            card3.Controls.Add(btn3);

            pnlMenu.Controls.Add(card1);
            pnlMenu.Controls.Add(card2);
            pnlMenu.Controls.Add(card3);

            // --- Form properties ---
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(980, 560);
            Name = "MainMenuForm";
            Text = "Main Menu";

            // Center pnlMenu on load & resize
            Load += MainMenuForm_Load;
            Resize += (_, __) =>
            {
                pnlMenu.Left = System.Math.Max(0, (ClientSize.Width - pnlMenu.Width) / 2);
                pnlMenu.Top = System.Math.Max(header.Bottom + 16, (ClientSize.Height - pnlMenu.Height) / 2);
            };
            Shown += (_, __) =>
            {
                this.ResizeRedraw = true;
                // trigger initial centering
                pnlMenu.Left = System.Math.Max(0, (ClientSize.Width - pnlMenu.Width) / 2);
                pnlMenu.Top = System.Math.Max(header.Bottom + 16, (ClientSize.Height - pnlMenu.Height) / 2);
            };
        }
    }
}
