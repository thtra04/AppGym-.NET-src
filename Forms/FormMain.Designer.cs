namespace AppGym.Forms
{
    partial class FormMain
    {
        private System.ComponentModel.IContainer components = null;

        private Panel panelSidebar;
        private Panel panelLogo;
        private Label lblLogo;
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
            this.panelSidebar   = new Panel();
            this.panelLogo      = new Panel();
            this.lblLogo        = new Label();
            this.btnLogout      = new Button();
            this.panelTopBar    = new Panel();
            this.lblPageTitle   = new Label();
            this.lblUserName    = new Label();
            this.panelSeparator = new Panel();
            this.panelContent   = new Panel();
            this.userMenu       = new ContextMenuStrip();

            this.panelSidebar.SuspendLayout();
            this.panelLogo.SuspendLayout();
            this.panelTopBar.SuspendLayout();
            this.SuspendLayout();

            // ?? panelSidebar ???????????????????????????????
            this.panelSidebar.BackColor = Color.FromArgb(30, 39, 73);
            this.panelSidebar.Controls.Add(this.btnLogout);
            this.panelSidebar.Controls.Add(this.panelLogo);
            this.panelSidebar.Dock = DockStyle.Left;
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Width = 240;

            // panelLogo
            this.panelLogo.BackColor = Color.FromArgb(20, 28, 58);
            this.panelLogo.Controls.Add(this.lblLogo);
            this.panelLogo.Dock = DockStyle.Top;
            this.panelLogo.Height = 80;
            this.panelLogo.Name = "panelLogo";

            // lblLogo
            this.lblLogo.AutoSize = false;
            this.lblLogo.Dock = DockStyle.Fill;
            this.lblLogo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblLogo.ForeColor = Color.FromArgb(230, 126, 34);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Text = "GYM MANAGER";
            this.lblLogo.TextAlign = ContentAlignment.MiddleCenter;

            // btnLogout
            this.btnLogout.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.btnLogout.BackColor = Color.FromArgb(30, 39, 73);
            this.btnLogout.FlatStyle = FlatStyle.Flat;
            this.btnLogout.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnLogout.ForeColor = Color.FromArgb(200, 200, 220);
            this.btnLogout.Location = new Point(0, 560);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Padding = new Padding(15, 0, 0, 0);
            this.btnLogout.Size = new Size(240, 48);
            this.btnLogout.Text = "  \u2716   \u0110\u0103ng xu\u1EA5t";
            this.btnLogout.TextAlign = ContentAlignment.MiddleLeft;
            this.btnLogout.Click += new EventHandler(this.BtnLogout_Click);

            // ?? panelTopBar ????????????????????????????????
            this.panelTopBar.BackColor = Color.White;
            this.panelTopBar.Controls.Add(this.lblUserName);
            this.panelTopBar.Controls.Add(this.lblPageTitle);
            this.panelTopBar.Dock = DockStyle.Top;
            this.panelTopBar.Height = 60;
            this.panelTopBar.Name = "panelTopBar";
            this.panelTopBar.Padding = new Padding(20, 0, 20, 0);

            // lblPageTitle
            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblPageTitle.ForeColor = Color.FromArgb(44, 62, 80);
            this.lblPageTitle.Location = new Point(20, 16);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Text = "Tổng quan";

            // userMenu
            this.userMenu.Font = new Font("Segoe UI", 10F);
            this.userMenu.ShowImageMargin = false;
            var tsAccount = new ToolStripMenuItem("Quản lý tài khoản");
            tsAccount.Padding = new Padding(8, 6, 8, 6);
            tsAccount.Click += TsAccount_Click;
            var tsSep = new ToolStripSeparator();
            var tsLogout = new ToolStripMenuItem("Đăng xuất");
            tsLogout.Padding = new Padding(8, 6, 8, 6);
            tsLogout.ForeColor = Color.FromArgb(220, 53, 69);
            tsLogout.Click += (s, e) => BtnLogout_Click(s, e);
            this.userMenu.Items.AddRange(new ToolStripItem[] { tsAccount, tsSep, tsLogout });

            // lblUserName
            this.lblUserName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new Font("Segoe UI", 10F);
            this.lblUserName.ForeColor = Color.FromArgb(80, 80, 80);
            this.lblUserName.Location = new Point(900, 20);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Text = "";
            this.lblUserName.Cursor = Cursors.Hand;
            this.lblUserName.Click += LblUserName_Click;
            this.lblUserName.MouseEnter += (s, e) => { lblUserName.ForeColor = Color.FromArgb(52, 152, 219); lblUserName.Font = new Font("Segoe UI", 10F, FontStyle.Underline); };
            this.lblUserName.MouseLeave += (s, e) => { lblUserName.ForeColor = Color.FromArgb(80, 80, 80); lblUserName.Font = new Font("Segoe UI", 10F); };

            // ?? panelSeparator ?????????????????????????????
            this.panelSeparator.BackColor = Color.FromArgb(220, 220, 220);
            this.panelSeparator.Dock = DockStyle.Top;
            this.panelSeparator.Height = 1;
            this.panelSeparator.Name = "panelSeparator";

            // ?? panelContent ???????????????????????????????
            this.panelContent.AutoScroll = true;
            this.panelContent.BackColor = Color.FromArgb(245, 246, 250);
            this.panelContent.Dock = DockStyle.Fill;
            this.panelContent.Name = "panelContent";
            this.panelContent.Padding = new Padding(20);

            // ?? FormMain ???????????????????????????????????
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(245, 246, 250);
            this.ClientSize = new Size(1300, 750);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelSeparator);
            this.Controls.Add(this.panelTopBar);
            this.Controls.Add(this.panelSidebar);
            this.MinimumSize = new Size(1100, 650);
            this.Name = "FormMain";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "GYM MANAGER - Quản lý phòng tập";

            this.panelSidebar.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            this.panelTopBar.ResumeLayout(false);
            this.panelTopBar.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
