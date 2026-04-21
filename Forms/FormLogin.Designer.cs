namespace AppGym.Forms
{
    partial class FormLogin
    {
        private System.ComponentModel.IContainer components = null;

        private Panel panelLeft;
        private Panel panelFeatureOne;
        private Panel panelFeatureTwo;
        private Panel panelFeatureThree;
        private Panel panelRight;
        private Panel panelLoginCard;
        private Label lblBrand;
        private Label lblAppName;
        private Label lblSlogan;
        private Label lblHeroTag;
        private Label lblFeatureOneTitle;
        private Label lblFeatureOneText;
        private Label lblFeatureTwoTitle;
        private Label lblFeatureTwoText;
        private Label lblFeatureThreeTitle;
        private Label lblFeatureThreeText;
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
        private Label lblQuickTitle;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panelLeft = new Panel();
            panelFeatureThree = new Panel();
            lblFeatureThreeText = new Label();
            lblFeatureThreeTitle = new Label();
            panelFeatureTwo = new Panel();
            lblFeatureTwoText = new Label();
            lblFeatureTwoTitle = new Label();
            panelFeatureOne = new Panel();
            lblFeatureOneText = new Label();
            lblFeatureOneTitle = new Label();
            lblSlogan = new Label();
            lblAppName = new Label();
            lblBrand = new Label();
            lblHeroTag = new Label();
            panelRight = new Panel();
            panelLoginCard = new Panel();
            lblQuickTitle = new Label();
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
            panelFeatureThree.SuspendLayout();
            panelFeatureTwo.SuspendLayout();
            panelFeatureOne.SuspendLayout();
            panelRight.SuspendLayout();
            panelLoginCard.SuspendLayout();
            SuspendLayout();
            // 
            // panelLeft
            // 
            panelLeft.BackColor = Color.FromArgb(18, 27, 45);
            panelLeft.Controls.Add(panelFeatureThree);
            panelLeft.Controls.Add(panelFeatureTwo);
            panelLeft.Controls.Add(panelFeatureOne);
            panelLeft.Controls.Add(lblSlogan);
            panelLeft.Controls.Add(lblAppName);
            panelLeft.Controls.Add(lblBrand);
            panelLeft.Controls.Add(lblHeroTag);
            panelLeft.Dock = DockStyle.Left;
            panelLeft.Location = new Point(0, 0);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new Size(520, 760);
            panelLeft.TabIndex = 0;
            panelLeft.MouseDown += Form_MouseDown;
            // 
            // panelFeatureThree
            // 
            panelFeatureThree.BackColor = Color.FromArgb(34, 46, 73);
            panelFeatureThree.Controls.Add(lblFeatureThreeText);
            panelFeatureThree.Controls.Add(lblFeatureThreeTitle);
            panelFeatureThree.Location = new Point(58, 580);
            panelFeatureThree.Name = "panelFeatureThree";
            panelFeatureThree.Size = new Size(394, 92);
            panelFeatureThree.TabIndex = 6;
            panelFeatureThree.MouseDown += Form_MouseDown;
            // 
            // lblFeatureThreeText
            // 
            lblFeatureThreeText.AutoSize = true;
            lblFeatureThreeText.Font = new Font("Segoe UI", 9.5F);
            lblFeatureThreeText.ForeColor = Color.FromArgb(193, 201, 217);
            lblFeatureThreeText.Location = new Point(18, 44);
            lblFeatureThreeText.Name = "lblFeatureThreeText";
            lblFeatureThreeText.Size = new Size(273, 21);
            lblFeatureThreeText.TabIndex = 1;
            lblFeatureThreeText.Text = "B\u00E1o c\u00E1o thanh to\u00E1n v\u00E0 \u0111\u0103ng k\u00FD tr\u1EF1c quan";
            lblFeatureThreeText.MouseDown += Form_MouseDown;
            // 
            // lblFeatureThreeTitle
            // 
            lblFeatureThreeTitle.AutoSize = true;
            lblFeatureThreeTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblFeatureThreeTitle.ForeColor = Color.White;
            lblFeatureThreeTitle.Location = new Point(18, 16);
            lblFeatureThreeTitle.Name = "lblFeatureThreeTitle";
            lblFeatureThreeTitle.Size = new Size(148, 25);
            lblFeatureThreeTitle.TabIndex = 0;
            lblFeatureThreeTitle.Text = "T\u1ED5ng quan ngay";
            lblFeatureThreeTitle.MouseDown += Form_MouseDown;
            // 
            // panelFeatureTwo
            // 
            panelFeatureTwo.BackColor = Color.FromArgb(30, 42, 67);
            panelFeatureTwo.Controls.Add(lblFeatureTwoText);
            panelFeatureTwo.Controls.Add(lblFeatureTwoTitle);
            panelFeatureTwo.Location = new Point(58, 476);
            panelFeatureTwo.Name = "panelFeatureTwo";
            panelFeatureTwo.Size = new Size(394, 92);
            panelFeatureTwo.TabIndex = 5;
            panelFeatureTwo.MouseDown += Form_MouseDown;
            // 
            // lblFeatureTwoText
            // 
            lblFeatureTwoText.AutoSize = true;
            lblFeatureTwoText.Font = new Font("Segoe UI", 9.5F);
            lblFeatureTwoText.ForeColor = Color.FromArgb(193, 201, 217);
            lblFeatureTwoText.Location = new Point(18, 44);
            lblFeatureTwoText.Name = "lblFeatureTwoText";
            lblFeatureTwoText.Size = new Size(293, 21);
            lblFeatureTwoText.TabIndex = 1;
            lblFeatureTwoText.Text = "Theo d\u00F5i h\u1ECDc vi\u00EAn, HLV, g\u00F3i t\u1EADp v\u00E0 h\u00F3a \u0111\u01A1n";
            lblFeatureTwoText.MouseDown += Form_MouseDown;
            // 
            // lblFeatureTwoTitle
            // 
            lblFeatureTwoTitle.AutoSize = true;
            lblFeatureTwoTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblFeatureTwoTitle.ForeColor = Color.White;
            lblFeatureTwoTitle.Location = new Point(18, 16);
            lblFeatureTwoTitle.Name = "lblFeatureTwoTitle";
            lblFeatureTwoTitle.Size = new Size(169, 25);
            lblFeatureTwoTitle.TabIndex = 0;
            lblFeatureTwoTitle.Text = "Qu\u1EA3n l\u00FD t\u1EADp trung";
            lblFeatureTwoTitle.MouseDown += Form_MouseDown;
            // 
            // panelFeatureOne
            // 
            panelFeatureOne.BackColor = Color.FromArgb(26, 38, 61);
            panelFeatureOne.Controls.Add(lblFeatureOneText);
            panelFeatureOne.Controls.Add(lblFeatureOneTitle);
            panelFeatureOne.Location = new Point(58, 372);
            panelFeatureOne.Name = "panelFeatureOne";
            panelFeatureOne.Size = new Size(394, 92);
            panelFeatureOne.TabIndex = 4;
            panelFeatureOne.MouseDown += Form_MouseDown;
            // 
            // lblFeatureOneText
            // 
            lblFeatureOneText.AutoSize = true;
            lblFeatureOneText.Font = new Font("Segoe UI", 9.5F);
            lblFeatureOneText.ForeColor = Color.FromArgb(193, 201, 217);
            lblFeatureOneText.Location = new Point(18, 44);
            lblFeatureOneText.Name = "lblFeatureOneText";
            lblFeatureOneText.Size = new Size(281, 21);
            lblFeatureOneText.TabIndex = 1;
            lblFeatureOneText.Text = "Thi\u1EBFt k\u1EBF cho lu\u1ED3ng v\u1EADn h\u00E0nh ph\u00F2ng gym";
            lblFeatureOneText.MouseDown += Form_MouseDown;
            // 
            // lblFeatureOneTitle
            // 
            lblFeatureOneTitle.AutoSize = true;
            lblFeatureOneTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblFeatureOneTitle.ForeColor = Color.White;
            lblFeatureOneTitle.Location = new Point(18, 16);
            lblFeatureOneTitle.Name = "lblFeatureOneTitle";
            lblFeatureOneTitle.Size = new Size(159, 25);
            lblFeatureOneTitle.TabIndex = 0;
            lblFeatureOneTitle.Text = "V\u1EADn h\u00E0nh m\u01B0\u1EE3t";
            lblFeatureOneTitle.MouseDown += Form_MouseDown;
            // 
            // lblSlogan
            // 
            lblSlogan.AutoSize = true;
            lblSlogan.Font = new Font("Segoe UI", 13F);
            lblSlogan.ForeColor = Color.FromArgb(205, 212, 224);
            lblSlogan.Location = new Point(58, 250);
            lblSlogan.MaximumSize = new Size(380, 0);
            lblSlogan.Name = "lblSlogan";
            lblSlogan.Size = new Size(371, 90);
            lblSlogan.TabIndex = 3;
            lblSlogan.Text = "\u0110i\u1EC1u h\u00E0nh ph\u00F2ng gym v\u1EDBi m\u1ED9t m\u00E0n h\u00ECnh r\u00F5 r\u00E0ng, tr\u1EF1c quan v\u00E0 s\u1EB5n s\u00E0ng cho b\u00E1o c\u00E1o, \u0111\u0103ng k\u00FD, thanh to\u00E1n.";
            lblSlogan.MouseDown += Form_MouseDown;
            // 
            // lblAppName
            // 
            lblAppName.AutoSize = true;
            lblAppName.Font = new Font("Segoe UI Semibold", 31.8F, FontStyle.Bold);
            lblAppName.ForeColor = Color.White;
            lblAppName.Location = new Point(58, 165);
            lblAppName.Name = "lblAppName";
            lblAppName.Size = new Size(307, 71);
            lblAppName.TabIndex = 2;
            lblAppName.Text = "Gym Flow";
            lblAppName.MouseDown += Form_MouseDown;
            // 
            // lblBrand
            // 
            lblBrand.AutoSize = true;
            lblBrand.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
            lblBrand.ForeColor = Color.FromArgb(232, 169, 84);
            lblBrand.Location = new Point(58, 115);
            lblBrand.Name = "lblBrand";
            lblBrand.Size = new Size(237, 40);
            lblBrand.TabIndex = 1;
            lblBrand.Text = "APPGYM STUDIO";
            lblBrand.MouseDown += Form_MouseDown;
            // 
            // lblHeroTag
            // 
            lblHeroTag.AutoSize = true;
            lblHeroTag.BackColor = Color.FromArgb(44, 58, 88);
            lblHeroTag.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblHeroTag.ForeColor = Color.FromArgb(240, 220, 181);
            lblHeroTag.Location = new Point(58, 62);
            lblHeroTag.Name = "lblHeroTag";
            lblHeroTag.Padding = new Padding(12, 6, 12, 6);
            lblHeroTag.Size = new Size(153, 33);
            lblHeroTag.TabIndex = 0;
            lblHeroTag.Text = "B\u1EA2NG \u0110I\u1EC0U KHI\u1EC2N";
            lblHeroTag.MouseDown += Form_MouseDown;
            // 
            // panelRight
            // 
            panelRight.BackColor = Color.FromArgb(243, 245, 249);
            panelRight.Controls.Add(panelLoginCard);
            panelRight.Controls.Add(lblClose);
            panelRight.Dock = DockStyle.Fill;
            panelRight.Location = new Point(520, 0);
            panelRight.Name = "panelRight";
            panelRight.Size = new Size(640, 760);
            panelRight.TabIndex = 1;
            panelRight.MouseDown += Form_MouseDown;
            // 
            // panelLoginCard
            // 
            panelLoginCard.BackColor = Color.White;
            panelLoginCard.Controls.Add(lblQuickTitle);
            panelLoginCard.Controls.Add(lblHint);
            panelLoginCard.Controls.Add(btnLogin);
            panelLoginCard.Controls.Add(lblError);
            panelLoginCard.Controls.Add(txtPassword);
            panelLoginCard.Controls.Add(lblPass);
            panelLoginCard.Controls.Add(txtUsername);
            panelLoginCard.Controls.Add(lblUser);
            panelLoginCard.Controls.Add(lblSub);
            panelLoginCard.Controls.Add(lblTitle);
            panelLoginCard.Location = new Point(86, 114);
            panelLoginCard.Name = "panelLoginCard";
            panelLoginCard.Size = new Size(468, 514);
            panelLoginCard.TabIndex = 1;
            panelLoginCard.MouseDown += Form_MouseDown;
            // 
            // lblQuickTitle
            // 
            lblQuickTitle.AutoSize = true;
            lblQuickTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblQuickTitle.ForeColor = Color.FromArgb(90, 102, 124);
            lblQuickTitle.Location = new Point(52, 393);
            lblQuickTitle.Name = "lblQuickTitle";
            lblQuickTitle.Size = new Size(192, 21);
            lblQuickTitle.TabIndex = 9;
            lblQuickTitle.Text = "T\u00E0i kho\u1EA3n d\u00F9ng th\u1EED nhanh";
            // 
            // lblHint
            // 
            lblHint.AutoSize = true;
            lblHint.Font = new Font("Segoe UI", 10F);
            lblHint.ForeColor = Color.FromArgb(29, 37, 51);
            lblHint.Location = new Point(52, 422);
            lblHint.Name = "lblHint";
            lblHint.Size = new Size(259, 23);
            lblHint.TabIndex = 8;
            lblHint.Text = "admin / 123";
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(232, 169, 84);
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnLogin.ForeColor = Color.FromArgb(19, 28, 46);
            btnLogin.Location = new Point(52, 327);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(364, 50);
            btnLogin.TabIndex = 7;
            btnLogin.Text = "V\u00E0o b\u1EA3ng \u0111i\u1EC1u khi\u1EC3n";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += BtnLogin_Click;
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.Font = new Font("Segoe UI", 9.5F);
            lblError.ForeColor = Color.FromArgb(210, 84, 84);
            lblError.Location = new Point(52, 285);
            lblError.MaximumSize = new Size(364, 0);
            lblError.Name = "lblError";
            lblError.Size = new Size(0, 21);
            lblError.TabIndex = 6;
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.FromArgb(249, 250, 252);
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Segoe UI", 12F);
            txtPassword.Location = new Point(52, 234);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(364, 34);
            txtPassword.TabIndex = 5;
            txtPassword.UseSystemPasswordChar = true;
            txtPassword.KeyDown += TxtPassword_KeyDown;
            // 
            // lblPass
            // 
            lblPass.AutoSize = true;
            lblPass.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPass.ForeColor = Color.FromArgb(29, 37, 51);
            lblPass.Location = new Point(52, 203);
            lblPass.Name = "lblPass";
            lblPass.Size = new Size(86, 23);
            lblPass.TabIndex = 4;
            lblPass.Text = "M\u1EADt kh\u1EA9u";
            // 
            // txtUsername
            // 
            txtUsername.BackColor = Color.FromArgb(249, 250, 252);
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Font = new Font("Segoe UI", 12F);
            txtUsername.Location = new Point(52, 153);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(364, 34);
            txtUsername.TabIndex = 3;
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUser.ForeColor = Color.FromArgb(29, 37, 51);
            lblUser.Location = new Point(52, 122);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(128, 23);
            lblUser.TabIndex = 2;
            lblUser.Text = "T\u00EAn \u0111\u0103ng nh\u1EADp";
            // 
            // lblSub
            // 
            lblSub.AutoSize = true;
            lblSub.Font = new Font("Segoe UI", 11F);
            lblSub.ForeColor = Color.FromArgb(104, 115, 134);
            lblSub.Location = new Point(52, 69);
            lblSub.Name = "lblSub";
            lblSub.Size = new Size(299, 25);
            lblSub.TabIndex = 1;
            lblSub.Text = "\u0110\u0103ng nh\u1EADp \u0111\u1EC3 ti\u1EBFp t\u1EE5c l\u00E0m vi\u1EC7c h\u00F4m nay";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(19, 28, 46);
            lblTitle.Location = new Point(52, 18);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(224, 54);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "\u0110\u0103ng nh\u1EADp";
            // 
            // lblClose
            // 
            lblClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblClose.AutoSize = true;
            lblClose.Cursor = Cursors.Hand;
            lblClose.Font = new Font("Segoe UI", 14F);
            lblClose.ForeColor = Color.FromArgb(120, 130, 145);
            lblClose.Location = new Point(586, 22);
            lblClose.Name = "lblClose";
            lblClose.Size = new Size(34, 32);
            lblClose.TabIndex = 0;
            lblClose.Text = "\u2715";
            lblClose.Click += BtnClose_Click;
            // 
            // FormLogin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(243, 245, 249);
            ClientSize = new Size(1160, 760);
            Controls.Add(panelRight);
            Controls.Add(panelLeft);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GYM MANAGER - \u0110\u0103ng nh\u1EADp";
            panelLeft.ResumeLayout(false);
            panelLeft.PerformLayout();
            panelFeatureThree.ResumeLayout(false);
            panelFeatureThree.PerformLayout();
            panelFeatureTwo.ResumeLayout(false);
            panelFeatureTwo.PerformLayout();
            panelFeatureOne.ResumeLayout(false);
            panelFeatureOne.PerformLayout();
            panelRight.ResumeLayout(false);
            panelRight.PerformLayout();
            panelLoginCard.ResumeLayout(false);
            panelLoginCard.PerformLayout();
            ResumeLayout(false);
        }
    }
}
