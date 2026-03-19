using AppGym.DataAccess;
using AppGym.Helpers;
using AppGym.Models;

namespace AppGym.Forms
{
    public partial class FormLogin : Form
    {
        public TaiKhoan? LoggedInUser { get; private set; }

        private Point _dragOffset;

        public FormLogin()
        {
            InitializeComponent();
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
                lblError.Text = "⚠ Vui lòng nhập đầy đủ thông tin!";
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
                    lblError.Text = "Tên đăng nhập hoặc mật khẩu không đúng!";
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Lỗi kết nối: " + ex.Message;
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
