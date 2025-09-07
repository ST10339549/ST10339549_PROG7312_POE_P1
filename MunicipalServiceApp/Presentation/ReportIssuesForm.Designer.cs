namespace MunicipalServiceApp.Presentation
{
    partial class ReportIssuesForm
    {
        private System.ComponentModel.IContainer? components = null;
        private Label lblHeading = null!;
        private Label lblLocation = null!;
        private TextBox txtLocation = null!;
        private Label lblCategory = null!;
        private ComboBox cmbCategory = null!;
        private Label lblDescription = null!;
        private RichTextBox rtbDescription = null!;
        private Button btnAttach = null!;
        private Label lblAttachmentPath = null!;
        private Button btnSubmit = null!;
        private Button btnBack = null!;
        private ProgressBar prgEngagement = null!;
        private Label lblStatus = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            lblHeading = new Label();
            lblLocation = new Label();
            txtLocation = new TextBox();
            lblCategory = new Label();
            cmbCategory = new ComboBox();
            lblDescription = new Label();
            rtbDescription = new RichTextBox();
            btnAttach = new Button();
            lblAttachmentPath = new Label();
            btnSubmit = new Button();
            btnBack = new Button();
            prgEngagement = new ProgressBar();
            lblStatus = new Label();

            // Heading
            lblHeading.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblHeading.Location = new Point(20, 15);
            lblHeading.Name = "lblHeading";
            lblHeading.Size = new Size(400, 32);
            lblHeading.Text = "Report an Issue";

            // Location
            lblLocation.Location = new Point(22, 60);
            lblLocation.Size = new Size(120, 23);
            lblLocation.Text = "Location";
            txtLocation.Location = new Point(25, 82);
            txtLocation.Name = "txtLocation";
            txtLocation.Size = new Size(430, 27);

            // Category
            lblCategory.Location = new Point(22, 118);
            lblCategory.Size = new Size(120, 23);
            lblCategory.Text = "Category";
            cmbCategory.Location = new Point(25, 140);
            cmbCategory.Size = new Size(430, 28);
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;

            // Description
            lblDescription.Location = new Point(22, 176);
            lblDescription.Size = new Size(120, 23);
            lblDescription.Text = "Description";
            rtbDescription.Location = new Point(25, 198);
            rtbDescription.Size = new Size(430, 120);

            // Attach
            btnAttach.Location = new Point(25, 330);
            btnAttach.Size = new Size(170, 34);
            btnAttach.Text = "Attach Photo/Doc…";
            btnAttach.UseVisualStyleBackColor = true;
            btnAttach.Click += btnAttach_Click;

            lblAttachmentPath.Location = new Point(205, 336);
            lblAttachmentPath.AutoSize = true;
            lblAttachmentPath.Text = "No file selected";

            // Status + Progress
            lblStatus.Location = new Point(25, 368);
            lblStatus.AutoSize = true;
            lblStatus.Text = "Awaiting submission…";

            prgEngagement.Location = new Point(25, 388);
            prgEngagement.Size = new Size(430, 20);
            prgEngagement.Minimum = 0;
            prgEngagement.Maximum = 100;
            prgEngagement.Value = 0;

            // Buttons
            btnBack.Location = new Point(25, 420);
            btnBack.Size = new Size(170, 36);
            btnBack.Text = "Back to Main Menu";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;

            btnSubmit.Location = new Point(285, 420);
            btnSubmit.Size = new Size(170, 36);
            btnSubmit.Text = "Submit";
            btnSubmit.UseVisualStyleBackColor = true;
            btnSubmit.Click += btnSubmit_Click;

            // Form
            AutoScaleDimensions = new SizeF(7F, 15F);
            ClientSize = new Size(484, 481);
            Controls.AddRange(new Control[]
            {
                lblHeading,
                lblLocation, txtLocation,
                lblCategory, cmbCategory,
                lblDescription, rtbDescription,
                btnAttach, lblAttachmentPath,
                lblStatus, prgEngagement,
                btnBack, btnSubmit
            });
            Name = "ReportIssuesForm";
            Text = "Report Issues";
            Load += ReportIssuesForm_Load;
        }
    }
}