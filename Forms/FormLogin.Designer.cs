using AppGym.Helpers;

namespace AppGym.Forms
{
    partial class FormLogin
    {
        private System.ComponentModel.IContainer components = null;

        // Left panel
        private Panel panelLeft;
        private Label lblBrand;
        private Label lblAppName;
        private Label lblSlogan;

        // Right panel
        private Panel panelRight;
        private Label lblClose;
        private Label lblTitle;
        private Label lblSub;
        private Label lblUser;
        private TextBox txtUsername;
        private Label lblPass;
        private TextBox txtPassword;
        private Label lblError;
        private Button btnLogin;
        private Label lblHint;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panelLeft = new Panel();
            lblSlogan = new Label();
            lblAppName = new Label();
            lblBrand = new Label();
            panelRight = new Panel();
            lblHint = new Label();
            btnLogin = new Button();
            lblError = new Label();
            txtPassword = new TextBox();
            lblPass = new Label();
            txtUsername = new TextBox();
            lblUser = new Label();
            lblSub = new Label();
            lblTitle = new Label();
            lblClose = new Label();
            panelLeft.SuspendLayout();
            panelRight.SuspendLayout();
            SuspendLayout();
            // 
            // panelLeft
            // 
            panelLeft.BackColor = Color.FromArgb(30, 39, 73);
            panelLeft.Controls.Add(lblSlogan);
            panelLeft.Controls.Add(lblAppName);
            panelLeft.Controls.Add(lblBrand);
            panelLeft.Dock = DockStyle.Left;
            panelLeft.Location = new Point(0, 0);
            panelLeft.Margin = new Padding(3, 4, 3, 4);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new Size(457, 733);
            panelLeft.TabIndex = 1;
            panelLeft.MouseDown += Form_MouseDown;
            // 
            // lblSlogan
            // 
            lblSlogan.AutoSize = true;
            lblSlogan.Font = new Font("Segoe UI", 11F);
            lblSlogan.ForeColor = Color.FromArgb(180, 180, 200);
            lblSlogan.Location = new Point(69, 347);
            lblSlogan.Name = "lblSlogan";
            lblSlogan.Size = new Size(249, 50);
            lblSlogan.TabIndex = 0;
            lblSlogan.Text = "Hệ thống quản lý phòng tập\nchuyên nghiệp  hiện đại";
            // 
            // lblAppName
            // 
            lblAppName.AutoSize = true;
            lblAppName.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblAppName.ForeColor = Color.White;
            lblAppName.Location = new Point(74, 267);
            lblAppName.Name = "lblAppName";
            lblAppName.Size = new Size(279, 46);
            lblAppName.TabIndex = 1;
            lblAppName.Text = "GYM MANAGER";
            lblAppName.MouseDown += Form_MouseDown;
            // 
            // lblBrand
            // 
            lblBrand.AutoSize = true;
            lblBrand.Font = new Font("Segoe UI", 60F);
            lblBrand.ForeColor = Color.FromArgb(230, 126, 34);
            lblBrand.Location = new Point(124, 106);
            lblBrand.Name = "lblBrand";
            lblBrand.Size = new Size(194, 133);
            lblBrand.TabIndex = 2;
            lblBrand.Text = "🏋️";
            lblBrand.MouseDown += Form_MouseDown;
            // 
            // panelRight
            // 
            panelRight.BackColor = Color.FromArgb(245, 246, 250);
            panelRight.Controls.Add(lblHint);
            panelRight.Controls.Add(btnLogin);
            panelRight.Controls.Add(lblError);
            panelRight.Controls.Add(txtPassword);
            panelRight.Controls.Add(lblPass);
            panelRight.Controls.Add(txtUsername);
            panelRight.Controls.Add(lblUser);
            panelRight.Controls.Add(lblSub);
            panelRight.Controls.Add(lblTitle);
            panelRight.Controls.Add(lblClose);
            panelRight.Dock = DockStyle.Fill;
            panelRight.Location = new Point(457, 0);
            panelRight.Margin = new Padding(3, 4, 3, 4);
            panelRight.Name = "panelRight";
            panelRight.Size = new Size(572, 733);
            panelRight.TabIndex = 0;
            // 
            // lblHint
            // 
            lblHint.AutoSize = true;
            lblHint.Font = new Font("Segoe UI", 9F);
            lblHint.ForeColor = Color.Gray;
            lblHint.Location = new Point(131, 560);
            lblHint.Name = "lblHint";
            lblHint.Size = new Size(157, 20);
            lblHint.TabIndex = 0;
            lblHint.Text = "Mặc định: admin / 123";
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(230, 126, 34);
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(69, 473);
            btnLogin.Margin = new Padding(3, 4, 3, 4);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(411, 64);
            btnLogin.TabIndex = 1;
            btnLogin.Text = "ĐĂNG NHẬP";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += BtnLogin_Click;
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.Font = new Font("Segoe UI", 9.5F);
            lblError.ForeColor = Color.FromArgb(231, 76, 60);
            lblError.Location = new Point(69, 433);
            lblError.Name = "lblError";
            lblError.Size = new Size(0, 21);
            lblError.TabIndex = 2;
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.White;
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Segoe UI", 12F);
            txtPassword.Location = new Point(69, 373);
            txtPassword.Margin = new Padding(3, 4, 3, 4);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(411, 34);
            txtPassword.TabIndex = 3;
            txtPassword.UseSystemPasswordChar = true;
            txtPassword.KeyDown += TxtPassword_KeyDown;
            // 
            // lblPass
            // 
            lblPass.AutoSize = true;
            lblPass.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPass.ForeColor = Color.FromArgb(44, 62, 80);
            lblPass.Location = new Point(69, 340);
            lblPass.Name = "lblPass";
            lblPass.Size = new Size(86, 23);
            lblPass.TabIndex = 4;
            lblPass.Text = "Mật khẩu";
            // 
            // txtUsername
            // 
            txtUsername.BackColor = Color.White;
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Font = new Font("Segoe UI", 12F);
            txtUsername.Location = new Point(69, 267);
            txtUsername.Margin = new Padding(3, 4, 3, 4);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(411, 34);
            txtUsername.TabIndex = 5;
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUser.ForeColor = Color.FromArgb(44, 62, 80);
            lblUser.Location = new Point(69, 233);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(128, 23);
            lblUser.TabIndex = 6;
            lblUser.Text = "Tên đăng nhập";
            // 
            // lblSub
            // 
            lblSub.AutoSize = true;
            lblSub.Font = new Font("Segoe UI", 11F);
            lblSub.ForeColor = Color.Gray;
            lblSub.Location = new Point(69, 160);
            lblSub.Name = "lblSub";
            lblSub.Size = new Size(273, 25);
            lblSub.TabIndex = 7;
            lblSub.Text = "Vui lòng đăng nhập để tiếp tục";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 26F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitle.Location = new Point(69, 93);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(251, 60);
            lblTitle.TabIndex = 8;
            lblTitle.Text = "Đăng nhập";
            // 
            // lblClose
            // 
            lblClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblClose.AutoSize = true;
            lblClose.Cursor = Cursors.Hand;
            lblClose.Font = new Font("Segoe UI", 14F);
            lblClose.ForeColor = Color.Gray;
            lblClose.Location = new Point(846, 20);
            lblClose.Name = "lblClose";
            lblClose.Size = new Size(34, 32);
            lblClose.TabIndex = 9;
            lblClose.Text = "✕";
            lblClose.Click += BtnClose_Click;
            // 
            // FormLogin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 246, 250);
            ClientSize = new Size(1029, 733);
            Controls.Add(panelRight);
            Controls.Add(panelLeft);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "FormLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GYM MANAGER - Đăng nhập";
            panelLeft.ResumeLayout(false);
            panelLeft.PerformLayout();
            panelRight.ResumeLayout(false);
            panelRight.PerformLayout();
            ResumeLayout(false);
        }
    }
}
