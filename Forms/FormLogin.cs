using AppGym.DataAccess;
using AppGym.Helpers;
using AppGym.Models;
using System.Drawing.Drawing2D;

namespace AppGym.Forms
{
    public partial class FormLogin : Form
    {
        public TaiKhoan? LoggedInUser { get; private set; }

        private Point _dragOffset;

        public FormLogin()
        {
            InitializeComponent();
            ApplyVisualStyle();
        }

        private void ApplyVisualStyle()
        {
            DoubleBuffered = true;
            AcceptButton = btnLogin;

            panelLeft.Paint += PanelLeft_Paint;
            panelRight.Paint += PanelRight_Paint;

            foreach (var control in new Control[]
            {
                panelLoginCard, panelFeatureOne, panelFeatureTwo, panelFeatureThree, btnLogin, txtUsername, txtPassword
            })
            {
                ApplyRounded(control);
                control.Resize += (_, _) => ApplyRounded(control);
            }

            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatAppearance.MouseOverBackColor = ThemeColors.AccentHover;
            btnLogin.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(ThemeColors.Accent, 0.08f);

            Shown += (_, _) => txtUsername.Focus();
        }

        private void ApplyRounded(Control control)
        {
            var radius = control == panelLoginCard ? 28 : control == btnLogin ? 18 : 22;
            UIHelper.RoundControl(control, radius);
        }

        private void PanelLeft_Paint(object? sender, PaintEventArgs e)
        {
            using var brush = new LinearGradientBrush(panelLeft.ClientRectangle,
                Color.FromArgb(10, 18, 32),
                Color.FromArgb(35, 49, 77),
                LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(brush, panelLeft.ClientRectangle);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using var glowOne = new SolidBrush(Color.FromArgb(46, 232, 169, 84));
            using var glowTwo = new SolidBrush(Color.FromArgb(32, 86, 121, 255));
            e.Graphics.FillEllipse(glowOne, new Rectangle(300, 40, 190, 190));
            e.Graphics.FillEllipse(glowTwo, new Rectangle(-60, 560, 220, 220));
        }

        private void PanelRight_Paint(object? sender, PaintEventArgs e)
        {
            using var brush = new LinearGradientBrush(panelRight.ClientRectangle,
                Color.FromArgb(248, 249, 252),
                Color.FromArgb(238, 241, 246),
                LinearGradientMode.ForwardDiagonal);
            e.Graphics.FillRectangle(brush, panelRight.ClientRectangle);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using var circleOne = new SolidBrush(Color.FromArgb(70, 232, 169, 84));
            using var circleTwo = new SolidBrush(Color.FromArgb(50, 29, 48, 86));
            e.Graphics.FillEllipse(circleOne, new Rectangle(420, 520, 170, 170));
            e.Graphics.FillEllipse(circleTwo, new Rectangle(35, 24, 100, 100));
        }

        private void Form_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _dragOffset = new Point(e.X, e.Y);
                ((Control)sender!).MouseMove += Form_MouseMove;
                ((Control)sender!).MouseUp += Form_MouseUp;
            }
        }

        private void Form_MouseMove(object? sender, MouseEventArgs e)
        {
            var screen = ((Control)sender!).PointToScreen(new Point(e.X, e.Y));
            Location = new Point(screen.X - _dragOffset.X - 10, screen.Y - _dragOffset.Y - 10);
        }

        private void Form_MouseUp(object? sender, MouseEventArgs e)
        {
            ((Control)sender!).MouseMove -= Form_MouseMove;
            ((Control)sender!).MouseUp -= Form_MouseUp;
        }

        private void BtnLogin_Click(object? sender, EventArgs e)
        {
            lblError.Text = "";
            var username = txtUsername.Text.Trim();
            var password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblError.Text = "Vui l\u00F2ng nh\u1EADp \u0111\u1EA7y \u0111\u1EE7 th\u00F4ng tin.";
                return;
            }

            try
            {
                var dao = new TaiKhoanDAO();
                var user = dao.Login(username, password);
                if (user != null)
                {
                    LoggedInUser = user;
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    var existingUser = dao.GetByUsername(username);
                    lblError.Text = existingUser != null && !existingUser.HoatDong
                        ? "Tài khoản này đang bị tạm khóa."
                        : "T\u00EAn \u0111\u0103ng nh\u1EADp ho\u1EB7c m\u1EADt kh\u1EA9u kh\u00F4ng \u0111\u00FAng.";
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "L\u1ED7i k\u1EBFt n\u1ED1i: " + ex.Message;
            }
        }

        private void BtnClose_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }

        private void TxtPassword_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) BtnLogin_Click(sender, e);
        }
    }
}
