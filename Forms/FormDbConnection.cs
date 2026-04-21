using AppGym.Helpers;

namespace AppGym.Forms
{
    public class FormDbConnection : Form
    {
        private readonly ComboBox _cboServer = new();
        private readonly TextBox _txtDatabase = new();
        private readonly CheckBox _chkWindows = new();
        private readonly TextBox _txtUser = new();
        private readonly TextBox _txtPass = new();
        private readonly Button _btnCreate = new();
        private readonly Button _btnTest = new();
        private readonly Button _btnSave = new();
        private readonly Button _btnCancel = new();
        private readonly Label _lblStatus = new();

        public DbConfig.Settings Result { get; private set; } = new();

        public FormDbConnection(DbConfig.Settings? initial = null)
        {
            Text = "Cấu hình kết nối SQL Server";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false; MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(520, 330);
            BackColor = Color.FromArgb(245, 246, 250);
            Font = new Font("Segoe UI", 10F);

            var s = initial ?? DbConfig.Load();

            AddLabel("Server / Instance:", 20, 23);
            _cboServer.Location = new Point(170, 20);
            _cboServer.Size = new Size(320, 27);
            _cboServer.Items.AddRange(new object[]
            {
                @"(local)\SQLEXPRESS",
                @".\SQLEXPRESS",
                @"(localdb)\MSSQLLocalDB",
                "localhost",
                "."
            });
            _cboServer.Text = s.Server;
            Controls.Add(_cboServer);

            AddLabel("Database:", 20, 63);
            _txtDatabase.Location = new Point(170, 60);
            _txtDatabase.Size = new Size(320, 27);
            _txtDatabase.Text = s.Database;
            Controls.Add(_txtDatabase);

            _chkWindows.Text = "Dùng Windows Authentication";
            _chkWindows.AutoSize = true;
            _chkWindows.Location = new Point(170, 100);
            _chkWindows.Checked = s.IntegratedSecurity;
            _chkWindows.CheckedChanged += (_, _) => UpdateAuthState();
            Controls.Add(_chkWindows);

            AddLabel("User ID:", 20, 143);
            _txtUser.Location = new Point(170, 140);
            _txtUser.Size = new Size(320, 27);
            _txtUser.Text = s.UserId ?? string.Empty;
            Controls.Add(_txtUser);

            AddLabel("Password:", 20, 183);
            _txtPass.Location = new Point(170, 180);
            _txtPass.Size = new Size(320, 27);
            _txtPass.UseSystemPasswordChar = true;
            _txtPass.Text = s.Password ?? string.Empty;
            Controls.Add(_txtPass);

            _lblStatus.AutoSize = false;
            _lblStatus.Location = new Point(20, 220);
            _lblStatus.Size = new Size(470, 40);
            _lblStatus.ForeColor = Color.FromArgb(120, 130, 148);
            _lblStatus.Text = "Mẹo: bạn có thể dùng (localdb)\\MSSQLLocalDB nếu máy đã cài Visual Studio.";
            Controls.Add(_lblStatus);

            _btnTest.Text = "Kiểm tra";
            _btnTest.Location = new Point(20, 275);
            _btnTest.Size = new Size(100, 36);
            _btnTest.FlatStyle = FlatStyle.Flat;
            _btnTest.BackColor = Color.FromArgb(52, 152, 219);
            _btnTest.ForeColor = Color.White;
            _btnTest.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _btnTest.Click += (_, _) => DoTest();
            Controls.Add(_btnTest);

            _btnCreate.Text = "Tạo DB mới";
            _btnCreate.Location = new Point(130, 275);
            _btnCreate.Size = new Size(110, 36);
            _btnCreate.FlatStyle = FlatStyle.Flat;
            _btnCreate.BackColor = Color.FromArgb(155, 89, 182);
            _btnCreate.ForeColor = Color.White;
            _btnCreate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _btnCreate.Click += (_, _) => DoCreate();
            Controls.Add(_btnCreate);

            _btnSave.Text = "Lưu & Tiếp tục";
            _btnSave.Location = new Point(250, 275);
            _btnSave.Size = new Size(150, 36);
            _btnSave.FlatStyle = FlatStyle.Flat;
            _btnSave.BackColor = Color.FromArgb(39, 174, 96);
            _btnSave.ForeColor = Color.White;
            _btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _btnSave.Click += (_, _) => DoSave();
            Controls.Add(_btnSave);

            _btnCancel.Text = "Thoát";
            _btnCancel.Location = new Point(410, 275);
            _btnCancel.Size = new Size(80, 36);
            _btnCancel.FlatStyle = FlatStyle.Flat;
            _btnCancel.BackColor = Color.FromArgb(231, 76, 60);
            _btnCancel.ForeColor = Color.White;
            _btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _btnCancel.DialogResult = DialogResult.Cancel;
            Controls.Add(_btnCancel);

            UpdateAuthState();
            AcceptButton = _btnSave;
            CancelButton = _btnCancel;
        }

        private void AddLabel(string text, int x, int y)
        {
            Controls.Add(new Label
            {
                Text = text,
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(x, y)
            });
        }

        private void UpdateAuthState()
        {
            bool win = _chkWindows.Checked;
            _txtUser.Enabled = !win;
            _txtPass.Enabled = !win;
        }

        private DbConfig.Settings Collect() => new()
        {
            Server = _cboServer.Text.Trim(),
            Database = string.IsNullOrWhiteSpace(_txtDatabase.Text) ? "GymManagementDB" : _txtDatabase.Text.Trim(),
            IntegratedSecurity = _chkWindows.Checked,
            UserId = _chkWindows.Checked ? null : _txtUser.Text,
            Password = _chkWindows.Checked ? null : _txtPass.Text
        };

        private void DoTest()
        {
            var s = Collect();
            _lblStatus.ForeColor = Color.FromArgb(120, 130, 148);
            _lblStatus.Text = "Đang kiểm tra...";
            Application.DoEvents();
            var (ok, err) = DbConfig.TryConnect(s);
            if (ok)
            {
                _lblStatus.ForeColor = Color.FromArgb(39, 174, 96);
                _lblStatus.Text = "✓ Kết nối thành công.";
                return;
            }
            // Maybe server is reachable but DB doesn't exist yet.
            var (srvOk, _) = DbConfig.TryConnectServer(s);
            if (srvOk && !DbConfig.DatabaseExists(s))
            {
                _lblStatus.ForeColor = Color.FromArgb(230, 126, 34);
                _lblStatus.Text = $"Kết nối được server, nhưng database \"{s.Database}\" chưa tồn tại.\nBấm \"Tạo DB mới\" để tạo tự động.";
                return;
            }
            _lblStatus.ForeColor = Color.FromArgb(231, 76, 60);
            _lblStatus.Text = "✗ " + err;
        }

        private void DoCreate()
        {
            var s = Collect();
            var (srvOk, srvErr) = DbConfig.TryConnectServer(s);
            if (!srvOk)
            {
                _lblStatus.ForeColor = Color.FromArgb(231, 76, 60);
                _lblStatus.Text = "✗ Không kết nối được server: " + srvErr;
                return;
            }
            if (MessageBox.Show(
                    $"Tạo database \"{s.Database}\" trên server \"{s.Server}\" và nạp dữ liệu mẫu?\n\n" +
                    "Tài khoản admin mặc định sau khi tạo: admin / admin",
                    "Xác nhận tạo database",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) != DialogResult.OK) return;

            _lblStatus.ForeColor = Color.FromArgb(120, 130, 148);
            _lblStatus.Text = "Đang tạo database, vui lòng chờ...";
            Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            var (ok, err) = DbConfig.RunSetupScript(s);
            Cursor = Cursors.Default;
            if (!ok)
            {
                _lblStatus.ForeColor = Color.FromArgb(231, 76, 60);
                _lblStatus.Text = "✗ " + err;
                return;
            }
            _lblStatus.ForeColor = Color.FromArgb(39, 174, 96);
            _lblStatus.Text = "✓ Đã tạo database. Bấm \"Lưu & Tiếp tục\" để vào ứng dụng.";
        }

        private void DoSave()
        {
            var s = Collect();
            var (ok, err) = DbConfig.TryConnect(s);
            if (!ok)
            {
                _lblStatus.ForeColor = Color.FromArgb(231, 76, 60);
                _lblStatus.Text = "✗ " + err;
                return;
            }
            DbConfig.Save(s);
            DataAccess.DatabaseHelper.ConnectionString = DbConfig.BuildConnectionString(s);
            Result = s;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
