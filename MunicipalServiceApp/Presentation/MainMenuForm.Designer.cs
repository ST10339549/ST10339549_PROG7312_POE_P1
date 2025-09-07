namespace MunicipalServiceApp.Presentation
{
    partial class MainMenuForm
    {
        private System.ComponentModel.IContainer? components = null;
        private Label lblTitle = null!;
        private Button btnReportIssues = null!;
        private Button btnLocalEvents = null!;
        private Button btnServiceStatus = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            lblTitle = new Label();
            btnReportIssues = new Button();
            btnLocalEvents = new Button();
            btnServiceStatus = new Button();

            // lblTitle
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(24, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(460, 40);
            lblTitle.Text = "Municipal Services";

            // btnReportIssues
            btnReportIssues.Location = new Point(30, 90);
            btnReportIssues.Name = "btnReportIssues";
            btnReportIssues.Size = new Size(260, 44);
            btnReportIssues.Text = "Report Issues";
            btnReportIssues.UseVisualStyleBackColor = true;
            btnReportIssues.Click += btnReportIssues_Click;

            // btnLocalEvents
            btnLocalEvents.Location = new Point(30, 144);
            btnLocalEvents.Name = "btnLocalEvents";
            btnLocalEvents.Size = new Size(260, 44);
            btnLocalEvents.Text = "Local Events";
            btnLocalEvents.Enabled = false;
            btnLocalEvents.UseVisualStyleBackColor = true;
            btnLocalEvents.Click += btnLocalEvents_Click;

            // btnServiceStatus
            btnServiceStatus.Location = new Point(30, 198);
            btnServiceStatus.Name = "btnServiceStatus";
            btnServiceStatus.Size = new Size(260, 44);
            btnServiceStatus.Text = "Service Request Status";
            btnServiceStatus.Enabled = false;
            btnServiceStatus.UseVisualStyleBackColor = true;
            btnServiceStatus.Click += btnServiceStatus_Click;

            // MainMenuForm
            AutoScaleDimensions = new SizeF(7F, 15F);
            ClientSize = new Size(520, 270);
            Controls.Add(lblTitle);
            Controls.Add(btnReportIssues);
            Controls.Add(btnLocalEvents);
            Controls.Add(btnServiceStatus);
            Name = "MainMenuForm";
            Text = "Main Menu";
            Load += MainMenuForm_Load;
        }
    }
}