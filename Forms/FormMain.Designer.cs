namespace AppGym.Forms
{
    partial class FormMain
    {
        private System.ComponentModel.IContainer components = null;

        private Panel panelSidebar;
        private Panel panelLogo;
        private Label lblLogo;
        private Label lblLogoSub;
        private Button btnLogout;
        private Panel panelTopBar;
        private Label lblPageTitle;
        private Label lblUserName;
        private Panel panelSeparator;
        private Panel panelContent;
        private ContextMenuStrip userMenu;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            panelSidebar = new Panel();
            btnLogout = new Button();
            panelLogo = new Panel();
            lblLogoSub = new Label();
            lblLogo = new Label();
            panelTopBar = new Panel();
            lblUserName = new Label();
            lblPageTitle = new Label();
            panelSeparator = new Panel();
            panelContent = new Panel();
            userMenu = new ContextMenuStrip(components);
            panelSidebar.SuspendLayout();
            panelLogo.SuspendLayout();
            panelTopBar.SuspendLayout();
            SuspendLayout();
            // 
            // panelSidebar
            // 
            panelSidebar.BackColor = Color.FromArgb(16, 24, 40);
            panelSidebar.Controls.Add(btnLogout);
            panelSidebar.Controls.Add(panelLogo);
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Location = new Point(0, 0);
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Size = new Size(270, 780);
            panelSidebar.TabIndex = 0;
            // 
            // btnLogout
            // 
            btnLogout.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnLogout.BackColor = Color.FromArgb(22, 32, 52);
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnLogout.ForeColor = Color.FromArgb(233, 235, 240);
            btnLogout.Location = new Point(20, 704);
            btnLogout.Name = "btnLogout";
            btnLogout.Padding = new Padding(16, 0, 0, 0);
            btnLogout.Size = new Size(228, 48);
            btnLogout.TabIndex = 1;
            btnLogout.Text = "  \u2726   \u0110\u0103ng xu\u1EA5t";
            btnLogout.TextAlign = ContentAlignment.MiddleLeft;
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += BtnLogout_Click;
            // 
            // panelLogo
            // 
            panelLogo.BackColor = Color.FromArgb(11, 18, 33);
            panelLogo.Controls.Add(lblLogoSub);
            panelLogo.Controls.Add(lblLogo);
            panelLogo.Dock = DockStyle.Top;
            panelLogo.Location = new Point(0, 0);
            panelLogo.Name = "panelLogo";
            panelLogo.Size = new Size(270, 126);
            panelLogo.TabIndex = 0;
            // 
            // lblLogoSub
            // 
            lblLogoSub.AutoSize = true;
            lblLogoSub.Font = new Font("Segoe UI", 9.5F);
            lblLogoSub.ForeColor = Color.FromArgb(183, 191, 206);
            lblLogoSub.Location = new Point(30, 78);
            lblLogoSub.Name = "lblLogoSub";
            lblLogoSub.Size = new Size(144, 21);
            lblLogoSub.TabIndex = 1;
            lblLogoSub.Text = "Studio qu\u1EA3n l\u00FD gym";
            // 
            // lblLogo
            // 
            lblLogo.AutoSize = true;
            lblLogo.Font = new Font("Segoe UI Semibold", 19.2F, FontStyle.Bold);
            lblLogo.ForeColor = Color.FromArgb(232, 169, 84);
            lblLogo.Location = new Point(28, 30);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(177, 45);
            lblLogo.TabIndex = 0;
            lblLogo.Text = "Gym Flow";
            // 
            // panelTopBar
            // 
            panelTopBar.BackColor = Color.FromArgb(250, 250, 252);
            panelTopBar.Controls.Add(lblUserName);
            panelTopBar.Controls.Add(lblPageTitle);
            panelTopBar.Dock = DockStyle.Top;
            panelTopBar.Location = new Point(270, 0);
            panelTopBar.Name = "panelTopBar";
            panelTopBar.Padding = new Padding(28, 0, 28, 0);
            panelTopBar.Size = new Size(1080, 78);
            panelTopBar.TabIndex = 1;
            // 
            // lblUserName
            // 
            lblUserName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblUserName.AutoSize = true;
            lblUserName.BackColor = Color.FromArgb(246, 238, 225);
            lblUserName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUserName.ForeColor = Color.FromArgb(64, 75, 92);
            lblUserName.Location = new Point(842, 26);
            lblUserName.Name = "lblUserName";
            lblUserName.Padding = new Padding(14, 8, 14, 8);
            lblUserName.Size = new Size(28, 39);
            lblUserName.TabIndex = 1;
            lblUserName.Text = "";
            // 
            // lblPageTitle
            // 
            lblPageTitle.AutoSize = true;
            lblPageTitle.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
            lblPageTitle.ForeColor = Color.FromArgb(29, 37, 51);
            lblPageTitle.Location = new Point(28, 18);
            lblPageTitle.Name = "lblPageTitle";
            lblPageTitle.Size = new Size(164, 46);
            lblPageTitle.TabIndex = 0;
            lblPageTitle.Text = "T\u1ED5ng quan";
            // 
            // panelSeparator
            // 
            panelSeparator.BackColor = Color.FromArgb(230, 233, 239);
            panelSeparator.Dock = DockStyle.Top;
            panelSeparator.Location = new Point(270, 78);
            panelSeparator.Name = "panelSeparator";
            panelSeparator.Size = new Size(1080, 1);
            panelSeparator.TabIndex = 2;
            // 
            // panelContent
            // 
            panelContent.AutoScroll = true;
            panelContent.BackColor = Color.FromArgb(243, 245, 249);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(270, 79);
            panelContent.Name = "panelContent";
            panelContent.Padding = new Padding(22);
            panelContent.Size = new Size(1080, 701);
            panelContent.TabIndex = 3;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(243, 245, 249);
            ClientSize = new Size(1350, 780);
            Controls.Add(panelContent);
            Controls.Add(panelSeparator);
            Controls.Add(panelTopBar);
            Controls.Add(panelSidebar);
            MinimumSize = new Size(1024, 640);
            Name = "FormMain";
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
            Text = "GYM MANAGER - Qu\u1EA3n l\u00FD ph\u00F2ng t\u1EADp";
            panelSidebar.ResumeLayout(false);
            panelLogo.ResumeLayout(false);
            panelLogo.PerformLayout();
            panelTopBar.ResumeLayout(false);
            panelTopBar.PerformLayout();
            ResumeLayout(false);
        }
    }
}
