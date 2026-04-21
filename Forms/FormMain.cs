using AppGym.DataAccess;
using AppGym.Helpers;
using AppGym.Models;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace AppGym.Forms
{
    public partial class FormMain : Form
    {
        private TaiKhoan _currentUser;
        private Button? _activeButton;
        private readonly List<System.Windows.Forms.Timer> _dashboardTimers = new();
        private PermissionService _perm;
        private bool IsAdmin => _perm.IsAdmin;

        public FormMain(TaiKhoan user)
        {
            _currentUser = user;
            _perm = new PermissionService(user);
            InitializeComponent();
            lblUserName.Text = $"{_currentUser.HoTen} ({_currentUser.VaiTro})";
            ApplyShellStyling();

            // Setup user dropdown menu
            userMenu.Font = new Font("Segoe UI", 10F);
            userMenu.ShowImageMargin = false;
            var tsAccount = new ToolStripMenuItem("Đổi mật khẩu");
            tsAccount.Padding = new Padding(8, 6, 8, 6);
            tsAccount.Click += TsAccount_Click;
            var tsSep = new ToolStripSeparator();
            var tsLogout = new ToolStripMenuItem("Đăng xuất");
            tsLogout.Padding = new Padding(8, 6, 8, 6);
            tsLogout.ForeColor = ThemeColors.Danger;
            tsLogout.Click += (s, e) => BtnLogout_Click(s, e);
            userMenu.Items.AddRange(new ToolStripItem[] { tsAccount, tsSep, tsLogout });

            // lblUserName click & hover
            lblUserName.Cursor = Cursors.Hand;
            lblUserName.Click += LblUserName_Click;
            lblUserName.MouseEnter += (s, e) => lblUserName.BackColor = Color.FromArgb(240, 228, 205);
            lblUserName.MouseLeave += (s, e) => lblUserName.BackColor = Color.FromArgb(246, 238, 225);

            BuildMenuButtons();
            Load += (s, e) =>
            {
                UpdateShellLayout();
                if (_perm.CanView(Permissions.TongQuan)) ShowDashboard();
                else ShowFirstAvailablePage();
            };
            Resize += (s, e) => UpdateShellLayout();
        }

        private void ApplyShellStyling()
        {
            UIHelper.RoundControl(btnLogout, 18);
            UIHelper.RoundControl(lblUserName, 18);
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatAppearance.MouseOverBackColor = ThemeColors.SidebarHover;
            btnLogout.FlatAppearance.MouseDownBackColor = ThemeColors.PrimaryLight;
        }

        private void UpdateShellLayout()
        {
            lblUserName.Location = new Point(panelTopBar.Width - lblUserName.Width - 30, 20);
            UIHelper.RoundControl(lblUserName, 18);
            UIHelper.RoundControl(btnLogout, 18);
        }

        private void BuildMenuButtons()
        {
            // visible: function that returns whether this menu should be shown given current user permissions.
            var allMenuItems = new (string text, string icon, Action action, Func<bool> visible)[]
            {
                ("Tổng quan",        "\u25A3", ShowDashboard,      () => _perm.CanView(Permissions.TongQuan)),
                ("Học viên",         "\u263A", ShowHocVien,         () => _perm.CanView(Permissions.HocVien)),
                ("Huấn luyện viên",  "\u2605", ShowHuanLuyenVien,   () => _perm.CanView(Permissions.HuanLuyenVien)),
                ("Gói tập",          "\u25C6", ShowGoiTap,          () => _perm.CanView(Permissions.GoiTap)),
                ("Đăng ký gói",      "\u25B6", ShowDangKyGoi,       () => _perm.CanView(Permissions.DangKyGoi)),
                ("Phân công PT",     "\u2611", ShowPhanCong,        () => _perm.CanView(Permissions.PhanCong)),
                ("Hóa đơn",          "\u25B2", ShowHoaDon,          () => _perm.CanView(Permissions.HoaDon)),
                ("Ca làm",           "\u25CB", ShowCaLam,           () => _perm.CanView(Permissions.CaLam)),
                ("Quản lý tài khoản", "\u26C2", ShowAccountManagement, () => _perm.CanManageAccounts),
                ("Phân quyền",       "\u2756", ShowPhanQuyen,       () => _perm.CanAssignPermissions),
            };

            int yPos = panelLogo.Bottom + 28;
            foreach (var item in allMenuItems)
            {
                if (!item.visible()) continue;

                var btn = new Button
                {
                    Text      = $"  {item.icon}   {item.text}",
                    Size      = new Size(228, 46),
                    Location  = new Point(20, yPos),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = ThemeColors.SidebarBg,
                    ForeColor = Color.FromArgb(216, 220, 228),
                    Font      = new Font("Segoe UI Semibold", 10.5f, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Padding   = new Padding(18, 0, 0, 0),
                    Cursor    = Cursors.Hand,
                    Tag       = item.action
                };
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatAppearance.MouseOverBackColor = ThemeColors.SidebarHover;
                UIHelper.RoundControl(btn, 16);
                btn.Resize += (s, e) => UIHelper.RoundControl(btn, 16);
                btn.Click += MenuButton_Click;
                panelSidebar.Controls.Add(btn);
                yPos += 54;
            }
        }

        private void MenuButton_Click(object? sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is Action action)
            {
                SetActiveButton(btn);
                action();
            }
        }

        private void ShowFirstAvailablePage()
        {
            foreach (Control c in panelSidebar.Controls)
            {
                if (c is Button btn && btn.Tag is Action action)
                {
                    SetActiveButton(btn);
                    action();
                    return;
                }
            }
            SetPageTitle("Không có quyền truy cập");
        }

        private void SetActiveButton(Button btn)
        {
            if (_activeButton != null)
            {
                _activeButton.BackColor = ThemeColors.SidebarBg;
                _activeButton.ForeColor = Color.FromArgb(216, 220, 228);
            }
            _activeButton = btn;
            _activeButton.BackColor = ThemeColors.SidebarHover;
            _activeButton.ForeColor = ThemeColors.Accent;
        }

        private void BtnLogout_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DialogResult = DialogResult.Retry;
                Close();
            }
        }

        private void LblUserName_Click(object? sender, EventArgs e)
        {
            userMenu.Show(lblUserName, new Point(0, lblUserName.Height));
        }

        private void TsAccount_Click(object? sender, EventArgs e)
        {
            // User avatar menu → directly open change-password dialog (profile page removed from UI).
            BtnChangePass_Click(sender, e);
        }

        private void BtnChangePass_Click(object? sender, EventArgs e)
        {
            using var dlg = new Form
            {
                Text = "Đổi mật khẩu",
                Size = new Size(520, 360),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = ThemeColors.Background
            };

            var card = UIHelper.CreateCard(468, 292);
            card.Location = new Point(18, 18);
            UIHelper.RoundControl(card, 24);
            dlg.Controls.Add(card);

            card.Controls.Add(new Label
            {
                Text = "Cập nhật mật khẩu",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = ThemeColors.TextPrimary,
                AutoSize = true,
                Location = new Point(22, 18)
            });
            card.Controls.Add(new Label
            {
                Text = "Nhập lại mật khẩu cũ để xác nhận quyền thay đổi.",
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = ThemeColors.TextSecondary,
                AutoSize = true,
                Location = new Point(22, 48)
            });

            var lblOld = new Label { Text = "Mật khẩu cũ", Location = new Point(22, 82), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = ThemeColors.TextPrimary };
            var txtOld = new TextBox { Location = new Point(22, 106), Size = new Size(424, 32), Font = new Font("Segoe UI", 11), UseSystemPasswordChar = true };
            var lblNew = new Label { Text = "Mật khẩu mới", Location = new Point(22, 146), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = ThemeColors.TextPrimary };
            var txtNew = new TextBox { Location = new Point(22, 170), Size = new Size(424, 32), Font = new Font("Segoe UI", 11), UseSystemPasswordChar = true };
            var lblConfirm = new Label { Text = "Xác nhận mật khẩu", Location = new Point(22, 210), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = ThemeColors.TextPrimary };
            var txtConfirm = new TextBox { Location = new Point(22, 234), Size = new Size(424, 32), Font = new Font("Segoe UI", 11), UseSystemPasswordChar = true };

            var btnSave = UIHelper.CreateButton("Lưu thay đổi", ThemeColors.Success, Color.White, 140, 38);
            btnSave.Location = new Point(306, 276);

            btnSave.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtOld.Text) || string.IsNullOrWhiteSpace(txtNew.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtNew.Text != txtConfirm.Text)
                {
                    MessageBox.Show("Mật khẩu xác nhận không khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Verify old password
                var verify = new TaiKhoanDAO().Login(_currentUser.TenDangNhap, txtOld.Text);
                if (verify == null)
                {
                    MessageBox.Show("Mật khẩu cũ không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    new TaiKhoanDAO().ChangePassword(_currentUser.MaTK, txtNew.Text);
                    MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dlg.Close();
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            };

            card.Controls.AddRange(new Control[] { lblOld, txtOld, lblNew, txtNew, lblConfirm, txtConfirm });
            dlg.Controls.Add(btnSave);
            dlg.ShowDialog();
        }

        private void ShowAccountManagement()
        {
            ClearContent();
            SetPageTitle(_perm.IsAdmin ? "Quản lý tài khoản hệ thống" : "Quản lý tài khoản nhân viên");
            panelContent.AutoScroll = true;
            panelContent.Padding = new Padding(24);

            int availableWidth = Math.Max(panelContent.ClientSize.Width - 48, 1040);
            int contentWidth = Math.Min(availableWidth, 1320);
            int contentLeft = Math.Max((panelContent.ClientSize.Width - contentWidth) / 2, 10);

            AddAdminAccountManagementSection(contentWidth, 10, contentLeft);
        }

        private void AddAdminAccountManagementSection(int contentWidth, int top, int left)
        {
            var dao = new TaiKhoanDAO();
            int sectionHeight = Math.Max(panelContent.ClientSize.Height - top - 20, 480);

            var adminCard = UIHelper.CreateCard(contentWidth, sectionHeight);
            adminCard.Location = new Point(left, top);
            adminCard.Padding = Padding.Empty;
            UIHelper.RoundControl(adminCard, 24);
            adminCard.Resize += (s, e) => UIHelper.RoundControl(adminCard, 24);
            panelContent.Controls.Add(adminCard);

            string headerText = _perm.IsAdmin ? "Tất cả tài khoản hệ thống" : "Danh sách nhân viên";
            string subText = _perm.IsAdmin
                ? "Admin có thể theo dõi, tạo mới, cập nhật và đặt lại mật khẩu cho mọi tài khoản."
                : "Quản lý chỉ được thao tác với tài khoản vai trò Nhân viên.";

            adminCard.Controls.Add(new Label
            {
                Text = headerText,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = ThemeColors.TextPrimary,
                AutoSize = true,
                Location = new Point(22, 18)
            });
            adminCard.Controls.Add(new Label
            {
                Text = subText,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = ThemeColors.TextSecondary,
                AutoSize = false,
                Size = new Size(adminCard.Width - 44, 20),
                Location = new Point(22, 50)
            });

            int buttonRight = adminCard.Width - 22;
            // Place CRUD buttons in their own row below the title/subtitle so they
            // never sit on top of the heading text on narrower widths.
            const int actionRowTop = 84;
            Button PlaceActionButton(Button button)
            {
                buttonRight -= button.Width;
                button.Location = new Point(buttonRight, actionRowTop);
                button.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                adminCard.Controls.Add(button);
                buttonRight -= 10;
                return button;
            }

            var btnDelete = PlaceActionButton(UIHelper.CreateButton("Xóa", ThemeColors.Danger, Color.White, 86, 36));
            var btnResetPassword = PlaceActionButton(UIHelper.CreateButton("Đặt lại mật khẩu", ThemeColors.Warning, Color.White, 172, 36));
            var btnEdit = PlaceActionButton(UIHelper.CreateButton("Sửa", ThemeColors.Info, Color.White, 86, 36));
            var btnAdd = PlaceActionButton(UIHelper.CreateButton("+ Thêm mới", ThemeColors.Success, Color.White, 128, 36));

            var searchWrap = new Panel
            {
                BackColor = Color.FromArgb(247, 248, 251),
                Location = new Point(22, actionRowTop + 1),
                Size = new Size(332, 36)
            };
            searchWrap.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(223, 229, 238));
                e.Graphics.DrawRectangle(pen, 0, 0, searchWrap.Width - 1, searchWrap.Height - 1);
            };
            UIHelper.RoundControl(searchWrap, 18);
            searchWrap.Resize += (s, e) => UIHelper.RoundControl(searchWrap, 18);
            adminCard.Controls.Add(searchWrap);

            var txtSearch = UIHelper.CreateSearchBox(searchWrap.Width - 30);
            txtSearch.BorderStyle = BorderStyle.None;
            txtSearch.BackColor = searchWrap.BackColor;
            txtSearch.Font = new Font("Segoe UI", 10.5F);
            txtSearch.Location = new Point(14, 9);
            txtSearch.Size = new Size(searchWrap.Width - 28, 20);
            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSearch.PlaceholderText = "Tìm theo mã, tên đăng nhập, họ tên, vai trò...";
            searchWrap.Controls.Add(txtSearch);

            adminCard.Controls.Add(new Label
            {
                Text = "Double-click để chỉnh sửa nhanh tài khoản được chọn.",
                Font = new Font("Segoe UI", 9.2F),
                ForeColor = ThemeColors.TextSecondary,
                AutoSize = false,
                Size = new Size(Math.Max(adminCard.Width - searchWrap.Right - 44 - 502, 240), 22),
                Location = new Point(searchWrap.Right + 18, actionRowTop + 8),
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            });

            var dgv = new DataGridView
            {
                Location = new Point(22, 134),
                Size = new Size(adminCard.Width - 44, adminCard.Height - 156),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };
            UIHelper.StyleDataGridView(dgv);
            adminCard.Controls.Add(dgv);

            void LoadData(string keyword = "")
            {
                try
                {
                    var list = string.IsNullOrWhiteSpace(keyword) ? dao.GetAll() : dao.Search(keyword);
                    if (!_perm.IsAdmin)
                    {
                        list = list.Where(x => string.Equals(x.VaiTro, "NhanVien", StringComparison.OrdinalIgnoreCase)).ToList();
                    }
                    dgv.DataSource = list;
                    ConfigureTaiKhoanGrid(dgv);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            void OpenSelectedEditor()
            {
                if (dgv.CurrentRow?.DataBoundItem is not TaiKhoan taiKhoan) return;
                if (!_perm.CanManageAccount(taiKhoan))
                {
                    MessageBox.Show("Bạn không có quyền thao tác trên tài khoản này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                OpenAccountEditorDialog(taiKhoan, ShowAccountManagement);
            }

            void ResetSelectedPassword()
            {
                if (dgv.CurrentRow?.DataBoundItem is not TaiKhoan taiKhoan) return;
                if (!_perm.CanManageAccount(taiKhoan))
                {
                    MessageBox.Show("Bạn không có quyền thao tác trên tài khoản này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                OpenAccountPasswordResetDialog(taiKhoan, ShowAccountManagement);
            }

            LoadData();
            txtSearch.TextChanged += (s, e) => LoadData(txtSearch.Text.Trim());
            btnAdd.Click += (s, e) => OpenAccountEditorDialog(null, ShowAccountManagement);
            btnEdit.Click += (s, e) => OpenSelectedEditor();
            btnResetPassword.Click += (s, e) => ResetSelectedPassword();
            btnDelete.Click += (s, e) =>
            {
                if (dgv.CurrentRow?.DataBoundItem is not TaiKhoan taiKhoan) return;
                if (!_perm.CanManageAccount(taiKhoan))
                {
                    MessageBox.Show("Bạn không có quyền thao tác trên tài khoản này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (taiKhoan.MaTK == _currentUser.MaTK)
                {
                    MessageBox.Show("Không thể xóa tài khoản đang đăng nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (taiKhoan.VaiTro == "Admin" && !dao.HasOtherAdmin(taiKhoan.MaTK))
                {
                    MessageBox.Show("Hệ thống phải còn ít nhất một tài khoản Admin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (taiKhoan.HoatDong && taiKhoan.VaiTro == "Admin" && !dao.HasOtherActiveAdmin(taiKhoan.MaTK))
                {
                    MessageBox.Show("Hệ thống phải còn ít nhất một Admin đang hoạt động.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dao.HasLinkedOperations(taiKhoan.MaTK))
                {
                    MessageBox.Show("Tài khoản này đã phát sinh đăng ký gói hoặc hóa đơn. Hãy chuyển sang trạng thái tạm khóa thay vì xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show($"Xóa tài khoản \"{taiKhoan.TenDangNhap}\"?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                {
                    return;
                }

                try
                {
                    dao.Delete(taiKhoan.MaTK);
                    MessageBox.Show("Xóa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowAccountManagement();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            dgv.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    OpenSelectedEditor();
                }
            };
        }

        private void ConfigureTaiKhoanGrid(DataGridView dgv)
        {
            if (dgv.Columns.Contains("MaTK")) dgv.Columns["MaTK"].HeaderText = "Mã TK";
            if (dgv.Columns.Contains("TenDangNhap")) dgv.Columns["TenDangNhap"].HeaderText = "Tên đăng nhập";
            if (dgv.Columns.Contains("HoTen")) dgv.Columns["HoTen"].HeaderText = "Họ tên";
            if (dgv.Columns.Contains("VaiTro")) dgv.Columns["VaiTro"].HeaderText = "Vai trò";
            if (dgv.Columns.Contains("HoatDong")) dgv.Columns["HoatDong"].Visible = false;
            if (dgv.Columns.Contains("TrangThaiText")) dgv.Columns["TrangThaiText"].HeaderText = "Trạng thái";
            if (dgv.Columns.Contains("TaoLuc"))
            {
                dgv.Columns["TaoLuc"].HeaderText = "Ngày tạo";
                dgv.Columns["TaoLuc"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }
        }

        private void OpenAccountEditorDialog(TaiKhoan? taiKhoan, Action onSaved)
        {
            bool isCreate = taiKhoan == null;
            bool isEditingCurrent = taiKhoan?.MaTK == _currentUser.MaTK;

            using var dlg = new Form
            {
                Text = isCreate ? "Thêm tài khoản" : "Cập nhật tài khoản",
                Size = new Size(560, isCreate ? 690 : 520),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = ThemeColors.Background
            };

            var card = UIHelper.CreateCard(508, isCreate ? 610 : 440);
            card.Location = new Point(18, 18);
            UIHelper.RoundControl(card, 24);
            dlg.Controls.Add(card);

            card.Controls.Add(new Label
            {
                Text = isCreate ? "Tạo tài khoản mới" : "Cập nhật thông tin tài khoản",
                Font = new Font("Segoe UI", 17F, FontStyle.Bold),
                ForeColor = ThemeColors.TextPrimary,
                AutoSize = true,
                Location = new Point(22, 18)
            });
            card.Controls.Add(new Label
            {
                Text = isCreate
                    ? "Thiết lập tên đăng nhập, họ tên, vai trò và mật khẩu khởi tạo."
                    : "Cập nhật thông tin nhận diện của tài khoản được chọn.",
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = ThemeColors.TextSecondary,
                AutoSize = true,
                Location = new Point(22, 48)
            });

            var lblUsername = new Label { Text = "Tên đăng nhập", Location = new Point(22, 88), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = ThemeColors.TextPrimary };
            var txtUsername = new TextBox { Location = new Point(22, 112), Size = new Size(464, 32), Font = new Font("Segoe UI", 11) };
            var lblHoTen = new Label { Text = "Họ tên", Location = new Point(22, 154), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = ThemeColors.TextPrimary };
            var txtHoTen = new TextBox { Location = new Point(22, 178), Size = new Size(464, 32), Font = new Font("Segoe UI", 11) };
            var lblVaiTro = new Label { Text = "Vai trò", Location = new Point(22, 220), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = ThemeColors.TextPrimary };
            var cboVaiTro = new ComboBox
            {
                Location = new Point(22, 244),
                Size = new Size(464, 32),
                Font = new Font("Segoe UI", 10.5F),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            if (_perm.IsAdmin)
            {
                cboVaiTro.Items.AddRange(new object[] { "Admin", "QuanLy", "NhanVien" });
            }
            else
            {
                // QuanLy can only create or modify NhanVien accounts.
                cboVaiTro.Items.Add("NhanVien");
                cboVaiTro.Enabled = false;
            }
            cboVaiTro.SelectedIndex = cboVaiTro.Items.Count - 1;
            var lblTrangThai = new Label { Text = "Trạng thái", Location = new Point(22, 286), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = ThemeColors.TextPrimary };
            var cboTrangThai = new ComboBox
            {
                Location = new Point(22, 310),
                Size = new Size(464, 32),
                Font = new Font("Segoe UI", 10.5F),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboTrangThai.Items.AddRange(new object[] { "Đang hoạt động", "Tạm khóa" });
            cboTrangThai.SelectedIndex = 0;

            card.Controls.AddRange(new Control[] { lblUsername, txtUsername, lblHoTen, txtHoTen, lblVaiTro, cboVaiTro, lblTrangThai, cboTrangThai });

            var lblSelfHint = new Label
            {
                Text = "Tài khoản đang đăng nhập không đổi vai trò hoặc trạng thái tại màn quản trị này.",
                Font = new Font("Segoe UI", 9F),
                ForeColor = ThemeColors.Warning,
                AutoSize = true,
                Location = new Point(22, 352),
                Visible = isEditingCurrent
            };
            card.Controls.Add(lblSelfHint);

            TextBox? txtPassword = null;
            TextBox? txtConfirm = null;
            if (isCreate)
            {
                var lblPassword = new Label { Text = "Mật khẩu khởi tạo", Location = new Point(22, 362), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = ThemeColors.TextPrimary };
                txtPassword = new TextBox { Location = new Point(22, 386), Size = new Size(464, 32), Font = new Font("Segoe UI", 11), UseSystemPasswordChar = true };
                var lblConfirm = new Label { Text = "Xác nhận mật khẩu", Location = new Point(22, 426), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = ThemeColors.TextPrimary };
                txtConfirm = new TextBox { Location = new Point(22, 450), Size = new Size(464, 32), Font = new Font("Segoe UI", 11), UseSystemPasswordChar = true };
                card.Controls.AddRange(new Control[] { lblPassword, txtPassword, lblConfirm, txtConfirm });
            }

            if (!isCreate && taiKhoan != null)
            {
                txtUsername.Text = taiKhoan.TenDangNhap;
                txtHoTen.Text = taiKhoan.HoTen;
                cboVaiTro.SelectedItem = taiKhoan.VaiTro;
                cboTrangThai.SelectedItem = taiKhoan.HoatDong ? "Đang hoạt động" : "Tạm khóa";
            }

            if (isEditingCurrent)
            {
                cboVaiTro.Enabled = false;
                cboTrangThai.Enabled = false;
            }

            var btnSave = UIHelper.CreateButton(isCreate ? "Tạo tài khoản" : "Lưu thay đổi", ThemeColors.Success, Color.White, 148, 38);
            btnSave.Location = new Point(card.Width - 170, card.Height - 60);
            card.Controls.Add(btnSave);
            dlg.AcceptButton = btnSave;

            btnSave.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtHoTen.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên đăng nhập và họ tên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cboVaiTro.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn vai trò!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (isCreate)
                {
                    if (string.IsNullOrWhiteSpace(txtPassword?.Text) || string.IsNullOrWhiteSpace(txtConfirm?.Text))
                    {
                        MessageBox.Show("Vui lòng nhập đầy đủ mật khẩu khởi tạo!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (txtPassword!.Text != txtConfirm!.Text)
                    {
                        MessageBox.Show("Mật khẩu xác nhận không khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                var payload = taiKhoan ?? new TaiKhoan();
                payload.TenDangNhap = txtUsername.Text.Trim();
                payload.HoTen = txtHoTen.Text.Trim();
                payload.VaiTro = cboVaiTro.SelectedItem?.ToString() ?? "NhanVien";
                payload.HoatDong = cboTrangThai.SelectedItem?.ToString() != "Tạm khóa";

                if (!isCreate && taiKhoan != null && taiKhoan.VaiTro == "Admin" && payload.VaiTro != "Admin" && !new TaiKhoanDAO().HasOtherAdmin(taiKhoan.MaTK))
                {
                    MessageBox.Show("Hệ thống phải còn ít nhất một tài khoản Admin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!isCreate &&
                    taiKhoan != null &&
                    taiKhoan.VaiTro == "Admin" &&
                    taiKhoan.HoatDong &&
                    (!payload.HoatDong || payload.VaiTro != "Admin") &&
                    !new TaiKhoanDAO().HasOtherActiveAdmin(taiKhoan.MaTK))
                {
                    MessageBox.Show("Hệ thống phải còn ít nhất một Admin đang hoạt động.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    var taiKhoanDao = new TaiKhoanDAO();
                    bool ok = isCreate
                        ? taiKhoanDao.Insert(payload, txtPassword!.Text)
                        : taiKhoanDao.Update(payload);

                    if (!ok)
                    {
                        MessageBox.Show("Không thể lưu tài khoản.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (!isCreate && payload.MaTK == _currentUser.MaTK)
                    {
                        var refreshed = taiKhoanDao.GetById(payload.MaTK);
                        if (refreshed != null)
                        {
                            _currentUser = refreshed;
                            _perm = new PermissionService(_currentUser);
                            lblUserName.Text = $"{_currentUser.HoTen} ({_currentUser.VaiTro})";
                            UpdateShellLayout();
                        }
                    }

                    MessageBox.Show(isCreate ? "Đã tạo tài khoản thành công!" : "Đã cập nhật tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dlg.Close();
                    onSaved();
                }
                catch (Exception ex)
                {
                    var message = ex.Message.Contains("UNIQUE", StringComparison.OrdinalIgnoreCase)
                        ? "Tên đăng nhập đã tồn tại."
                        : "Lỗi: " + ex.Message;
                    MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            dlg.ShowDialog();
        }

        private void OpenAccountPasswordResetDialog(TaiKhoan taiKhoan, Action onSaved)
        {
            using var dlg = new Form
            {
                Text = "Đặt lại mật khẩu",
                Size = new Size(520, 330),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = ThemeColors.Background
            };

            var card = UIHelper.CreateCard(468, 258);
            card.Location = new Point(18, 18);
            UIHelper.RoundControl(card, 24);
            dlg.Controls.Add(card);

            card.Controls.Add(new Label
            {
                Text = $"Đặt lại mật khẩu cho @{taiKhoan.TenDangNhap}",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = ThemeColors.TextPrimary,
                AutoSize = true,
                Location = new Point(22, 18)
            });
            card.Controls.Add(new Label
            {
                Text = "Nhập mật khẩu mới và xác nhận để cập nhật ngay cho tài khoản được chọn.",
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = ThemeColors.TextSecondary,
                AutoSize = false,
                Size = new Size(424, 22),
                Location = new Point(22, 48)
            });

            var lblNew = new Label { Text = "Mật khẩu mới", Location = new Point(22, 88), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = ThemeColors.TextPrimary };
            var txtNew = new TextBox { Location = new Point(22, 112), Size = new Size(424, 32), Font = new Font("Segoe UI", 11), UseSystemPasswordChar = true };
            var lblConfirm = new Label { Text = "Xác nhận mật khẩu", Location = new Point(22, 152), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = ThemeColors.TextPrimary };
            var txtConfirm = new TextBox { Location = new Point(22, 176), Size = new Size(424, 32), Font = new Font("Segoe UI", 11), UseSystemPasswordChar = true };
            var btnSave = UIHelper.CreateButton("Cập nhật mật khẩu", ThemeColors.Warning, Color.White, 164, 38);
            btnSave.Location = new Point(282, 214);

            btnSave.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtNew.Text) || string.IsNullOrWhiteSpace(txtConfirm.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ mật khẩu mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtNew.Text != txtConfirm.Text)
                {
                    MessageBox.Show("Mật khẩu xác nhận không khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    new TaiKhoanDAO().ChangePassword(taiKhoan.MaTK, txtNew.Text);
                    MessageBox.Show("Đặt lại mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dlg.Close();
                    onSaved();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            card.Controls.AddRange(new Control[] { lblNew, txtNew, lblConfirm, txtConfirm, btnSave });
            dlg.AcceptButton = btnSave;
            dlg.ShowDialog();
        }

        private void SetPageTitle(string title) => lblPageTitle.Text = title;

        private void ClearContent()
        {
            StopDashboardAnimations();
            panelContent.Controls.Clear();
            panelContent.AutoScroll = false;
            panelContent.Padding = Padding.Empty;
        }

        // ==================== DASHBOARD ====================
        private void ShowDashboard()
        {
            ClearContent();
            SetPageTitle("Tổng quan");
            panelContent.AutoScroll = true;
            panelContent.Padding = new Padding(20);

            int hvCount = 0, hlvCount = 0, goiCount = 0, dkCount = 0;
            decimal revenue = 0;
            decimal outstanding = 0;
            var dangKyList = new List<DangKyGoi>();
            var hoaDonList = new List<HoaDon>();
            try
            {
                hvCount = new HocVienDAO().Count();
                hlvCount = new HuanLuyenVienDAO().Count();
                goiCount = new GoiTapDAO().Count();
                dangKyList = new DangKyGoiDAO().GetAll();
                hoaDonList = new HoaDonDAO().GetAll();
                dkCount = dangKyList.Count;
                revenue = hoaDonList.Sum(x => x.SoTien ?? 0);
                outstanding = dangKyList.Sum(x => Math.Max(x.ConThieu, 0m));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Dashboard data load failed: {ex}");
            }

            var paidEnoughCount = dangKyList.Count(x => x.DaThanhToan > 0 && x.ConThieu <= 0);
            var paymentRate = dkCount == 0 ? 0 : (int)Math.Round((double)paidEnoughCount / dkCount * 100);
            var invoiceToday = hoaDonList.Count(x => x.NgayThanhToan?.Date == DateTime.Today);
            var monthStart = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var newThisMonth = dangKyList.Count(x => x.NgayBatDau >= monthStart);

            int availableWidth = Math.Max(panelContent.ClientSize.Width - 40, 1120);
            int contentW = Math.Min(availableWidth, 1360);
            int contentLeft = Math.Max((panelContent.ClientSize.Width - contentW) / 2, 10);
            int yPos = 10;

            var hero = CreateDashboardHero(contentW, invoiceToday, paymentRate, newThisMonth, out var lblHeroRevenue);
            hero.Location = new Point(contentLeft, yPos);
            panelContent.Controls.Add(hero);
            RunDashboardAnimation(22, 15, progress =>
            {
                var currentRevenue = revenue * (decimal)progress;
                lblHeroRevenue.Text = FormatCompactCurrency(currentRevenue);
            });

            yPos = hero.Bottom + 18;
            var cards = new (string title, string subtitle, decimal value, string icon, Color accent, bool isCurrency)[]
            {
                ("Học viên", "Tổng hội viên đang quản lý", hvCount, "☺", Color.FromArgb(54, 133, 225), false),
                ("Huấn luyện viên", "Nguồn lực phụ trách hiện tại", hlvCount, "★", Color.FromArgb(39, 174, 96), false),
                ("Gói tập", "Danh mục gói tập đang bán", goiCount, "◆", Color.FromArgb(142, 68, 173), false),
                ("Đăng ký", "Lượt đăng ký hiệu lực", dkCount, "▶", Color.FromArgb(235, 149, 50), false),
                ("Còn thiếu", "Giá trị cần thu thêm", outstanding, "●", Color.FromArgb(217, 83, 79), true)
            };

            int cardGap = 14;
            int cardWidth = (contentW - cardGap * 4) / 5;
            int cardHeight = 142;
            int xPos = contentLeft;
            foreach (var card in cards)
            {
                var metricCard = CreateDashboardMetricCard(card.title, card.subtitle, card.icon, card.accent, new Size(cardWidth, cardHeight), out var valueLabel);
                metricCard.Location = new Point(xPos, yPos);
                panelContent.Controls.Add(metricCard);
                RunDashboardAnimation(18, 16, progress =>
                {
                    var currentValue = card.value * (decimal)progress;
                    valueLabel.Text = card.isCurrency ? FormatCompactCurrency(currentValue) : Math.Round(currentValue).ToString("#,##0");
                });
                xPos += cardWidth + cardGap;
            }

            yPos += cardHeight + 26;
            var lblCharts = new Label
            {
                Text = "■  Phân tích trực quan",
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = ThemeColors.TextPrimary,
                AutoSize = true,
                Location = new Point(contentLeft, yPos)
            };
            panelContent.Controls.Add(lblCharts);

            var lblChartsHint = new Label
            {
                Text = "Bộ số liệu được làm mới từ hóa đơn và đăng ký hiện có.",
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = ThemeColors.TextSecondary,
                AutoSize = true,
                Location = new Point(contentLeft, yPos + 26)
            };
            panelContent.Controls.Add(lblChartsHint);

            int chartTop = yPos + 54;
            int dashboardChartGap = 20;
            int chartWidth = (contentW - dashboardChartGap) / 2;
            int chartHeight = 318;
            int paymentChartHeight = 304;

            var pnlRevenue = CreateChartCard("Doanh thu 6 tháng gần nhất", "Theo dõi nhịp thu trong nửa năm qua", new Point(contentLeft, chartTop), new Size(chartWidth, chartHeight), out var revenueBody);
            panelContent.Controls.Add(pnlRevenue);
            PopulateRevenueChart(revenueBody, hoaDonList);

            var pnlPackages = CreateChartCard("Top gói tập được đăng ký", "So sánh nhu cầu theo từng gói", new Point(contentLeft + chartWidth + dashboardChartGap, chartTop), new Size(chartWidth, chartHeight), out var packageBody);
            panelContent.Controls.Add(pnlPackages);
            PopulatePackageChart(packageBody, dangKyList);

            var pnlPaymentStatus = CreateChartCard("Tình trạng thanh toán đăng ký", "Nhìn nhanh mức độ hoàn thành thanh toán", new Point(contentLeft, chartTop + chartHeight + dashboardChartGap), new Size(contentW, paymentChartHeight), out var paymentBody);
            panelContent.Controls.Add(pnlPaymentStatus);
            PopulatePaymentStatusChart(paymentBody, dangKyList);

            yPos = pnlPaymentStatus.Bottom + 26;
            var lblSection = new Label
            {
                Text = "☑  Đăng ký gần đây",
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = ThemeColors.TextPrimary,
                AutoSize = true,
                Location = new Point(contentLeft, yPos)
            };
            panelContent.Controls.Add(lblSection);

            var lblRecentHint = new Label
            {
                Text = "Danh sách 10 đăng ký mới nhất để theo dõi nhanh tình trạng bán gói.",
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = ThemeColors.TextSecondary,
                AutoSize = true,
                Location = new Point(contentLeft, yPos + 26)
            };
            panelContent.Controls.Add(lblRecentHint);

            var dgv = new DataGridView { Location = new Point(contentLeft, yPos + 58), Size = new Size(contentW, 320), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
            UIHelper.StyleDataGridView(dgv);
            panelContent.Controls.Add(dgv);

            try
            {
                dgv.DataSource = dangKyList.Take(10).ToList();
                ConfigureDangKyGoiGrid(dgv);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Dashboard recent registrations bind failed: {ex}");
            }
        }

        private void StopDashboardAnimations()
        {
            foreach (var timer in _dashboardTimers.ToList())
            {
                timer.Stop();
                timer.Dispose();
            }

            _dashboardTimers.Clear();
        }

        private void RunDashboardAnimation(int frames, int interval, Action<double> updateAction)
        {
            var timer = new System.Windows.Forms.Timer { Interval = interval };
            _dashboardTimers.Add(timer);

            int currentStep = 0;
            timer.Tick += (s, e) =>
            {
                currentStep++;
                var progress = Math.Min(1d, currentStep / (double)frames);
                var easedProgress = 1d - Math.Pow(1d - progress, 3d);
                updateAction(easedProgress);

                if (progress >= 1d)
                {
                    timer.Stop();
                    timer.Dispose();
                    _dashboardTimers.Remove(timer);
                }
            };

            timer.Start();
        }

        private Panel CreateDashboardHero(int width, int invoiceToday, int paymentRate, int newThisMonth, out Label revenueLabel)
        {
            var panel = new Panel
            {
                Size = new Size(width, 188),
                BackColor = ThemeColors.Primary,
                Padding = new Padding(28)
            };
            UIHelper.RoundControl(panel, 28);
            panel.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using var gradientBrush = new LinearGradientBrush(panel.ClientRectangle, ThemeColors.PrimaryDark, ThemeColors.PrimaryLight, 14f);
                e.Graphics.FillRectangle(gradientBrush, panel.ClientRectangle);

                using var accentBrush = new SolidBrush(Color.FromArgb(36, ThemeColors.Accent));
                using var softBrush = new SolidBrush(Color.FromArgb(24, Color.White));
                e.Graphics.FillEllipse(accentBrush, panel.Width - 230, -58, 220, 220);
                e.Graphics.FillEllipse(softBrush, panel.Width - 336, 54, 110, 110);
                e.Graphics.FillEllipse(softBrush, panel.Width - 170, 92, 36, 36);
            };

            var lblKicker = new Label
            {
                Text = "APPGYM OVERVIEW",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = ThemeColors.Accent,
                AutoSize = true,
                Location = new Point(28, 24),
                BackColor = Color.Transparent
            };
            panel.Controls.Add(lblKicker);

            int reservedRight = 286; // revenue card width + gap
            int textBlockWidth = Math.Max(width - 60 - reservedRight, 320);

            var lblTitle = new Label
            {
                Text = "Bảng điều khiển vận hành",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = ThemeColors.TextOnDark,
                AutoSize = false,
                Size = new Size(textBlockWidth, 36),
                Location = new Point(28, 50),
                BackColor = Color.Transparent
            };
            panel.Controls.Add(lblTitle);

            var lblSubtitle = new Label
            {
                Text = "Theo dõi doanh thu, nhịp bán gói và tình trạng thanh toán trên cùng một màn hình.",
                Font = new Font("Segoe UI", 10.5F),
                ForeColor = Color.FromArgb(220, 226, 236),
                AutoSize = false,
                Size = new Size(textBlockWidth, 38),
                Location = new Point(28, 92),
                BackColor = Color.Transparent
            };
            panel.Controls.Add(lblSubtitle);

            var revenueCard = new Panel
            {
                Size = new Size(246, 120),
                Location = new Point(width - 274, 28),
                BackColor = Color.FromArgb(34, Color.White)
            };
            UIHelper.RoundControl(revenueCard, 24);
            panel.Controls.Add(revenueCard);

            revenueCard.Controls.Add(new Label
            {
                Text = "Doanh thu ghi nhận",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = ThemeColors.TextOnDark,
                AutoSize = true,
                Location = new Point(18, 16),
                BackColor = Color.Transparent
            });

            revenueLabel = new Label
            {
                Text = FormatCompactCurrency(0),
                Font = new Font("Segoe UI", 24F, FontStyle.Bold),
                ForeColor = ThemeColors.TextOnDark,
                AutoSize = false,
                Size = new Size(210, 44),
                Location = new Point(18, 38),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleLeft
            };
            revenueCard.Controls.Add(revenueLabel);

            var lblRevenueCaption = new Label
            {
                Text = "Cập nhật từ hóa đơn đã lưu trong hệ thống.",
                Font = new Font("Segoe UI", 9.2F),
                ForeColor = Color.FromArgb(224, 230, 238),
                AutoSize = false,
                Size = new Size(210, 32),
                Location = new Point(18, 80),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.TopLeft
            };
            revenueCard.Controls.Add(lblRevenueCaption);

            var chipInvoice = CreateHeroMetricChip("Hóa đơn hôm nay", invoiceToday.ToString(), ThemeColors.Accent, Color.FromArgb(255, 247, 235));
            chipInvoice.Location = new Point(28, 136);
            panel.Controls.Add(chipInvoice);

            var chipPayment = CreateHeroMetricChip("Thu đủ", paymentRate + "%", Color.FromArgb(39, 174, 96), Color.FromArgb(234, 250, 241));
            chipPayment.Location = new Point(chipInvoice.Right + 12, 136);
            panel.Controls.Add(chipPayment);

            var chipNew = CreateHeroMetricChip("Đăng ký tháng này", newThisMonth.ToString(), Color.FromArgb(52, 152, 219), Color.FromArgb(235, 245, 255));
            chipNew.Location = new Point(chipPayment.Right + 12, 136);
            panel.Controls.Add(chipNew);

            return panel;
        }

        private Panel CreateHeroMetricChip(string title, string value, Color accent, Color valueBackColor)
        {
            int titleWidth = TextRenderer.MeasureText(title, new Font("Segoe UI", 9F, FontStyle.Bold)).Width;
            int valueWidth = Math.Max(TextRenderer.MeasureText(value, new Font("Segoe UI", 10F, FontStyle.Bold)).Width + 20, 54);
            int chipWidth = Math.Max(titleWidth + valueWidth + 40, 166);

            var chip = new Panel
            {
                Size = new Size(chipWidth, 40),
                BackColor = Color.FromArgb(26, Color.White)
            };
            UIHelper.RoundControl(chip, 18);

            var valuePill = new Panel
            {
                Size = new Size(valueWidth, 26),
                Location = new Point(chip.Width - valueWidth - 10, 7),
                BackColor = valueBackColor
            };
            UIHelper.RoundControl(valuePill, 13);
            chip.Controls.Add(valuePill);

            valuePill.Controls.Add(new Label
            {
                Text = value,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = accent,
                BackColor = Color.Transparent
            });

            chip.Controls.Add(new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = ThemeColors.TextOnDark,
                AutoSize = true,
                Location = new Point(14, 11),
                BackColor = Color.Transparent
            });

            return chip;
        }

        private Panel CreateDashboardMetricCard(string title, string subtitle, string icon, Color accent, Size size, out Label valueLabel)
        {
            var panel = new Panel
            {
                Size = size,
                BackColor = Color.White,
                Padding = new Padding(18)
            };
            UIHelper.RoundControl(panel, 24);
            panel.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using var borderPen = new Pen(Color.FromArgb(228, 233, 240));
                using var accentBrush = new SolidBrush(Color.FromArgb(18, accent));
                using var accentPen = new Pen(accent, 5);
                e.Graphics.FillRectangle(accentBrush, new Rectangle(12, 12, panel.Width - 24, 42));
                e.Graphics.DrawLine(accentPen, 20, panel.Height - 12, panel.Width - 20, panel.Height - 12);
                e.Graphics.DrawRectangle(borderPen, 0, 0, panel.Width - 1, panel.Height - 1);
            };

            var iconWrap = new Panel
            {
                Size = new Size(42, 42),
                Location = new Point(16, 16),
                BackColor = Color.FromArgb(22, accent)
            };
            UIHelper.RoundControl(iconWrap, 21);
            panel.Controls.Add(iconWrap);

            iconWrap.Controls.Add(new Label
            {
                Text = icon,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI Symbol", 16F, FontStyle.Bold),
                ForeColor = accent,
                BackColor = Color.Transparent
            });

            panel.Controls.Add(new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = ThemeColors.TextPrimary,
                AutoSize = false,
                Size = new Size(size.Width - 90, 24),
                Location = new Point(66, 18),
                BackColor = Color.Transparent
            });

            valueLabel = new Label
            {
                Text = "0",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = accent,
                AutoSize = false,
                Size = new Size(size.Width - 32, 38),
                Location = new Point(16, 60),
                BackColor = Color.Transparent
            };
            panel.Controls.Add(valueLabel);

            panel.Controls.Add(new Label
            {
                Text = subtitle,
                Font = new Font("Segoe UI", 9.2F),
                ForeColor = ThemeColors.TextSecondary,
                AutoSize = false,
                Size = new Size(size.Width - 32, 32),
                Location = new Point(16, 102),
                BackColor = Color.Transparent
            });

            return panel;
        }

        private Panel CreateChartCard(string title, string subtitle, Point location, Size size, out Panel body)
        {
            var panel = new Panel
            {
                Size = size,
                BackColor = ThemeColors.CardBackground,
                Padding = new Padding(16)
            };
            panel.Location = location;
            UIHelper.RoundControl(panel, 24);
            panel.Paint += (s, e) =>
            {
                using var borderPen = new Pen(Color.FromArgb(228, 233, 240));
                e.Graphics.DrawRectangle(borderPen, 0, 0, panel.Width - 1, panel.Height - 1);
            };

            var lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 10.8F, FontStyle.Bold),
                ForeColor = ThemeColors.TextPrimary,
                AutoSize = true,
                Location = new Point(16, 12)
            };
            panel.Controls.Add(lblTitle);

            var lblSubtitle = new Label
            {
                Text = subtitle,
                Font = new Font("Segoe UI", 9.2F),
                ForeColor = ThemeColors.TextSecondary,
                AutoSize = false,
                Size = new Size(size.Width - 32, 20),
                Location = new Point(16, 34)
            };
            panel.Controls.Add(lblSubtitle);

            body = new Panel
            {
                Location = new Point(16, 62),
                Size = new Size(size.Width - 32, size.Height - 78),
                BackColor = Color.White,
                AutoScroll = false
            };
            UIHelper.RoundControl(body, 16);
            panel.Controls.Add(body);
            return panel;
        }

        private void PopulateRevenueChart(Panel body, List<HoaDon> hoaDonList)
        {
            body.Controls.Clear();
            var monthlyRevenue = hoaDonList
                .Where(x => x.NgayThanhToan.HasValue)
                .GroupBy(x => new DateTime(x.NgayThanhToan!.Value.Year, x.NgayThanhToan.Value.Month, 1))
                .ToDictionary(g => g.Key, g => g.Sum(x => x.SoTien ?? 0m));

            var values = new List<(string Label, decimal Value)>();
            var startMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-5);
            for (int i = 0; i < 6; i++)
            {
                var month = startMonth.AddMonths(i);
                monthlyRevenue.TryGetValue(month, out var value);
                values.Add((month.ToString("MM/yyyy"), value));
            }

            var totalRevenue = values.Sum(x => x.Value);
            var bestMonth = values.OrderByDescending(x => x.Value).First();
            body.Controls.Add(new Label
            {
                Text = "Tổng 6 tháng",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = ThemeColors.TextSecondary,
                AutoSize = true,
                Location = new Point(14, 14)
            });
            body.Controls.Add(new Label
            {
                Text = FormatCompactCurrency(totalRevenue),
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = ThemeColors.TextPrimary,
                AutoSize = true,
                Location = new Point(14, 32)
            });

            body.Controls.Add(new Label
            {
                Text = $"Tháng cao nhất: {bestMonth.Label}",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = ThemeColors.TextSecondary,
                AutoSize = false,
                Size = new Size(180, 18),
                Location = new Point(body.Width - 194, 14),
                TextAlign = ContentAlignment.MiddleRight
            });
            body.Controls.Add(new Label
            {
                Text = FormatCompactCurrency(bestMonth.Value),
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = ThemeColors.Info,
                AutoSize = false,
                Size = new Size(180, 24),
                Location = new Point(body.Width - 194, 30),
                TextAlign = ContentAlignment.MiddleRight
            });

            var maxValue = Math.Max(values.Max(x => x.Value), 1);
            var chartTop = 68;
            var chartHeight = Math.Max(body.Height - 102, 120);
            var columnWidth = Math.Max((body.Width - 30) / values.Count - 10, 42);

            for (int guide = 0; guide < 4; guide++)
            {
                int y = chartTop + guide * (chartHeight / 3);
                body.Controls.Add(new Panel
                {
                    BackColor = Color.FromArgb(238, 241, 246),
                    Size = new Size(body.Width - 24, 1),
                    Location = new Point(12, y)
                });
            }

            for (int i = 0; i < values.Count; i++)
            {
                var item = values[i];
                var x = 14 + i * (columnWidth + 10);
                var targetBarHeight = (int)Math.Round((double)(item.Value / maxValue * (chartHeight - 28)));
                var finalBarHeight = Math.Max(targetBarHeight, item.Value > 0 ? 10 : 0);

                var track = new Panel
                {
                    BackColor = Color.FromArgb(234, 242, 252),
                    Size = new Size(columnWidth, chartHeight),
                    Location = new Point(x, chartTop)
                };
                UIHelper.RoundControl(track, 16);
                body.Controls.Add(track);

                var bar = new Panel
                {
                    BackColor = ThemeColors.Info,
                    Size = new Size(columnWidth, 1),
                    Location = new Point(0, track.Height - 1)
                };
                track.Controls.Add(bar);
                RunDashboardAnimation(18 + i * 2, 15, progress =>
                {
                    int currentHeight = Math.Max((int)Math.Round(finalBarHeight * progress), item.Value > 0 ? 1 : 0);
                    bar.Size = new Size(columnWidth, currentHeight);
                    bar.Location = new Point(0, track.Height - currentHeight);
                });

                var lblMonth = new Label
                {
                    Text = item.Label,
                    Font = new Font("Segoe UI", 8.5F),
                    ForeColor = ThemeColors.TextSecondary,
                    TextAlign = ContentAlignment.MiddleCenter,
                    AutoSize = false,
                    Size = new Size(columnWidth, 22),
                    Location = new Point(x, chartTop + chartHeight + 4)
                };
                body.Controls.Add(lblMonth);
            }
        }

        private void PopulatePackageChart(Panel body, List<DangKyGoi> dangKyList)
        {
            body.Controls.Clear();
            var packageData = dangKyList
                .GroupBy(x => string.IsNullOrWhiteSpace(x.TenGoi) ? "Chưa rõ" : x.TenGoi)
                .Select(g => new { TenGoi = g.Key, SoLuong = g.Count() })
                .OrderByDescending(x => x.SoLuong)
                .Take(5)
                .ToList();

            if (packageData.Count == 0)
            {
                body.Controls.Add(new Label
                {
                    Text = "Chưa có dữ liệu đăng ký",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                    ForeColor = ThemeColors.TextSecondary
                });
            }
            else
            {
                var maxCount = packageData.Max(x => x.SoLuong);
                var totalCount = packageData.Sum(x => x.SoLuong);
                body.Controls.Add(new Label
                {
                    Text = $"{totalCount} lượt đăng ký trong top 5 gói",
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    ForeColor = ThemeColors.TextPrimary,
                    AutoSize = true,
                    Location = new Point(14, 14)
                });

                int rowTop = 40;
                for (int index = 0; index < packageData.Count; index++)
                {
                    var item = packageData[index];
                    var percent = totalCount == 0 ? 0 : (decimal)item.SoLuong / totalCount * 100;

                    var lblName = new Label
                    {
                        Text = item.TenGoi,
                        Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                        ForeColor = ThemeColors.TextPrimary,
                        AutoSize = false,
                        Size = new Size(body.Width - 128, 22),
                        Location = new Point(14, rowTop)
                    };
                    body.Controls.Add(lblName);

                    var lblValue = new Label
                    {
                        Text = $"{item.SoLuong} ({percent:0.#}%)",
                        Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                        ForeColor = Color.FromArgb(142, 68, 173),
                        TextAlign = ContentAlignment.MiddleRight,
                        AutoSize = false,
                        Size = new Size(110, 22),
                        Location = new Point(body.Width - 124, rowTop)
                    };
                    body.Controls.Add(lblValue);

                    var track = new Panel
                    {
                        BackColor = Color.FromArgb(244, 237, 250),
                        Size = new Size(body.Width - 28, 10),
                        Location = new Point(14, rowTop + 22)
                    };
                    UIHelper.RoundControl(track, 8);
                    body.Controls.Add(track);

                    var fill = new Panel
                    {
                        BackColor = Color.FromArgb(142, 68, 173),
                        Size = new Size(1, track.Height),
                        Location = new Point(0, 0)
                    };
                    track.Controls.Add(fill);

                    var fillWidth = maxCount == 0 ? 0 : (int)Math.Round((double)item.SoLuong / maxCount * track.Width);
                    var finalWidth = Math.Max(fillWidth, item.SoLuong > 0 ? 12 : 0);
                    RunDashboardAnimation(18 + index * 2, 15, progress =>
                    {
                        fill.Width = Math.Max((int)Math.Round(finalWidth * progress), item.SoLuong > 0 ? 1 : 0);
                    });

                    rowTop += 36;
                }
            }
        }

        private void PopulatePaymentStatusChart(Panel body, List<DangKyGoi> dangKyList)
        {
            body.Controls.Clear();
            var paidCount = dangKyList.Count(x => x.DaThanhToan > 0 && x.ConThieu <= 0);
            var partialCount = dangKyList.Count(x => x.DaThanhToan > 0 && x.ConThieu > 0);
            var unpaidCount = dangKyList.Count(x => x.DaThanhToan <= 0);
            var total = Math.Max(dangKyList.Count, 1);
            var items = new[]
            {
                new { Label = "Đã thanh toán đủ", Value = paidCount, Color = Color.FromArgb(39, 174, 96) },
                new { Label = "Thanh toán một phần", Value = partialCount, Color = Color.FromArgb(243, 156, 18) },
                new { Label = "Chưa thanh toán", Value = unpaidCount, Color = Color.FromArgb(231, 76, 60) }
            };

            var summaryWrap = new Panel
            {
                Size = new Size(body.Width - 24, 60),
                Location = new Point(12, 12),
                BackColor = Color.FromArgb(248, 250, 253)
            };
            UIHelper.RoundControl(summaryWrap, 16);
            body.Controls.Add(summaryWrap);

            summaryWrap.Controls.Add(new Label
            {
                Text = $"Tổng đăng ký: {dangKyList.Count}",
                Font = new Font("Segoe UI", 11.5F, FontStyle.Bold),
                ForeColor = ThemeColors.TextPrimary,
                AutoSize = true,
                Location = new Point(16, 6)
            });

            summaryWrap.Controls.Add(new Label
            {
                Text = $"Đã thu: {paidCount} | Một phần: {partialCount} | Chưa thu: {unpaidCount}",
                Font = new Font("Segoe UI", 9F),
                ForeColor = ThemeColors.TextSecondary,
                AutoSize = true,
                Location = new Point(16, 36)
            });

            var stackedBar = new Panel
            {
                BackColor = Color.FromArgb(236, 240, 241),
                Size = new Size(body.Width - 24, 20),
                Location = new Point(12, 82)
            };
            UIHelper.RoundControl(stackedBar, 10);
            body.Controls.Add(stackedBar);

            int currentX = 0;
            int itemIndex = 0;
            foreach (var item in items.Where(x => x.Value > 0))
            {
                var segmentWidth = (int)Math.Round((double)item.Value / total * stackedBar.Width);
                var segment = new Panel
                {
                    BackColor = item.Color,
                    Size = new Size(1, stackedBar.Height),
                    Location = new Point(currentX, 0)
                };
                stackedBar.Controls.Add(segment);
                int finalWidth = Math.Max(segmentWidth, 1);
                RunDashboardAnimation(18 + itemIndex * 2, 15, progress =>
                {
                    segment.Width = Math.Max((int)Math.Round(finalWidth * progress), 1);
                });
                currentX += segmentWidth;
                itemIndex++;
            }

            int rowTop = 110;
            for (int index = 0; index < items.Length; index++)
            {
                var item = items[index];
                var percent = dangKyList.Count == 0 ? 0 : (decimal)item.Value / dangKyList.Count * 100;

                var colorBox = new Panel
                {
                    BackColor = item.Color,
                    Size = new Size(14, 14),
                    Location = new Point(12, rowTop + 5)
                };
                body.Controls.Add(colorBox);

                body.Controls.Add(new Label
                {
                    Text = item.Label,
                    Font = new Font("Segoe UI", 9.6F, FontStyle.Bold),
                    ForeColor = ThemeColors.TextPrimary,
                    AutoSize = true,
                    Location = new Point(34, rowTop + 1)
                });

                body.Controls.Add(new Label
                {
                    Text = $"{item.Value} đăng ký ({percent:0.#}%)",
                    Font = new Font("Segoe UI", 9.6F),
                    ForeColor = ThemeColors.TextSecondary,
                    AutoSize = true,
                    Location = new Point(Math.Max(body.Width - 170, 210), rowTop + 1)
                });

                var progressTrack = new Panel
                {
                    BackColor = Color.FromArgb(244, 246, 247),
                    Size = new Size(body.Width - 24, 10),
                    Location = new Point(12, rowTop + 22)
                };
                UIHelper.RoundControl(progressTrack, 7);
                body.Controls.Add(progressTrack);

                var fill = new Panel
                {
                    BackColor = item.Color,
                    Size = new Size(1, progressTrack.Height),
                    Location = new Point(0, 0)
                };
                progressTrack.Controls.Add(fill);
                int finalWidth = (int)Math.Round((double)item.Value / total * progressTrack.Width);
                RunDashboardAnimation(18 + index * 2, 15, progress =>
                {
                    fill.Width = Math.Max((int)Math.Round(finalWidth * progress), item.Value > 0 ? 1 : 0);
                });

                rowTop += 38;
            }
        }

        private void ConfigureDangKyGoiGrid(DataGridView dgv)
        {
            if (dgv.Columns.Contains("MaHV")) dgv.Columns["MaHV"].Visible = false;
            if (dgv.Columns.Contains("MaGoi")) dgv.Columns["MaGoi"].Visible = false;
            if (dgv.Columns.Contains("MaNguoiLap")) dgv.Columns["MaNguoiLap"].Visible = false;
            if (dgv.Columns.Contains("MaDK")) dgv.Columns["MaDK"].HeaderText = "Mã ĐK";
            if (dgv.Columns.Contains("TenHV")) dgv.Columns["TenHV"].HeaderText = "Học viên";
            if (dgv.Columns.Contains("TenGoi")) dgv.Columns["TenGoi"].HeaderText = "Gói tập";
            if (dgv.Columns.Contains("TenNguoiLap")) dgv.Columns["TenNguoiLap"].HeaderText = "Người lên gói";
            if (dgv.Columns.Contains("GiaGoi"))
            {
                dgv.Columns["GiaGoi"].HeaderText = "Giá gói";
                dgv.Columns["GiaGoi"].DefaultCellStyle.Format = "#,##0";
            }
            if (dgv.Columns.Contains("DaThanhToan"))
            {
                dgv.Columns["DaThanhToan"].HeaderText = "Đã thanh toán";
                dgv.Columns["DaThanhToan"].DefaultCellStyle.Format = "#,##0";
            }
            if (dgv.Columns.Contains("ConThieu"))
            {
                dgv.Columns["ConThieu"].HeaderText = "Còn thiếu";
                dgv.Columns["ConThieu"].DefaultCellStyle.Format = "#,##0";
            }
            if (dgv.Columns.Contains("TrangThaiThanhToan")) dgv.Columns["TrangThaiThanhToan"].HeaderText = "Trạng thái TT";
            if (dgv.Columns.Contains("NgayBatDau")) dgv.Columns["NgayBatDau"].HeaderText = "Ngày BĐ";
            if (dgv.Columns.Contains("NgayHetHan")) dgv.Columns["NgayHetHan"].HeaderText = "Ngày HH";
            if (dgv.Columns.Contains("GhiChu")) dgv.Columns["GhiChu"].HeaderText = "Ghi chú";
        }

        private static string FormatCompactCurrency(decimal revenue)
        {
            if (revenue >= 1_000_000_000) return (revenue / 1_000_000_000m).ToString("#,##0.#") + " t\u1EF7";
            if (revenue >= 1_000_000) return (revenue / 1_000_000m).ToString("#,##0.#") + " tr";
            if (revenue >= 1_000) return (revenue / 1_000m).ToString("#,##0") + "K";
            return revenue.ToString("#,##0") + "\u0111";
        }

        // ==================== HOC VIEN ====================
        private void ShowHocVien()
        {
            ClearContent();
            SetPageTitle("Quản lý Học viên");
            BuildCrudPage("Tìm kiếm học viên...", out var dgv, out var txtSearch, out var btnAdd, out var btnEdit, out var btnDelete, out var btnRefresh);
            var dao = new HocVienDAO();
            void LoadData(string kw = "")
            {
                try
                {
                    dgv.DataSource = string.IsNullOrEmpty(kw) ? dao.GetAll() : dao.Search(kw);
                    dgv.Columns["MaHV"].HeaderText = "Mã HV"; dgv.Columns["HoTen"].HeaderText = "Họ tên";
                    dgv.Columns["GioiTinh"].HeaderText = "Giới tính"; dgv.Columns["NgaySinh"].HeaderText = "Ngày sinh";
                    dgv.Columns["SDT"].HeaderText = "SĐT"; dgv.Columns["Email"].HeaderText = "Email";
                    dgv.Columns["NgayDangKy"].HeaderText = "Ngày ĐK";
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
            LoadData();
            txtSearch.TextChanged += (s, e) => LoadData(txtSearch.Text.Trim());
            btnRefresh.Click  += (s, e) => { txtSearch.Clear(); LoadData(); };
            btnAdd.Click      += (s, e) => { if (new FormHocVienDetail(null).ShowDialog() == DialogResult.OK) LoadData(); };
            btnEdit.Click     += (s, e) => { if (dgv.CurrentRow == null) return; var hv = (HocVien)dgv.CurrentRow.DataBoundItem; if (new FormHocVienDetail(hv).ShowDialog() == DialogResult.OK) LoadData(); };
            btnDelete.Click   += (s, e) => { if (dgv.CurrentRow == null) return; var hv = (HocVien)dgv.CurrentRow.DataBoundItem; if (MessageBox.Show($"Xóa \"{hv.HoTen}\"?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) { try { dao.Delete(hv.MaHV); LoadData(); } catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); } } };
            ApplyPermissions(Permissions.HocVien, btnAdd, btnEdit, btnDelete);
        }

        // ==================== HUAN LUYEN VIEN ====================
        private void ShowHuanLuyenVien()
        {
            ClearContent();
            SetPageTitle("Quản lý Huấn luyện viên");
            BuildCrudPage("Tìm kiếm HLV...", out var dgv, out var txtSearch, out var btnAdd, out var btnEdit, out var btnDelete, out var btnRefresh);
            var dao = new HuanLuyenVienDAO();
            void LoadData(string kw = "")
            {
                try
                {
                    dgv.DataSource = string.IsNullOrEmpty(kw) ? dao.GetAll() : dao.Search(kw);
                    dgv.Columns["MaHLV"].HeaderText = "Mã HLV"; dgv.Columns["HoTen"].HeaderText = "Họ tên";
                    dgv.Columns["GioiTinh"].HeaderText = "Giới tính"; dgv.Columns["SDT"].HeaderText = "SĐT";
                    dgv.Columns["ChuyenMon"].HeaderText = "Chuyên môn";
                    dgv.Columns["Luong"].HeaderText = "Lương"; dgv.Columns["Luong"].DefaultCellStyle.Format = "#,##0";
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
            LoadData();
            txtSearch.TextChanged += (s, e) => LoadData(txtSearch.Text.Trim());
            btnRefresh.Click += (s, e) => { txtSearch.Clear(); LoadData(); };
            btnAdd.Click     += (s, e) => { if (new FormHLVDetail(null).ShowDialog() == DialogResult.OK) LoadData(); };
            btnEdit.Click    += (s, e) => { if (dgv.CurrentRow == null) return; var hlv = (HuanLuyenVien)dgv.CurrentRow.DataBoundItem; if (new FormHLVDetail(hlv).ShowDialog() == DialogResult.OK) LoadData(); };
            btnDelete.Click  += (s, e) => { if (dgv.CurrentRow == null) return; var hlv = (HuanLuyenVien)dgv.CurrentRow.DataBoundItem; if (MessageBox.Show($"Xóa \"{hlv.HoTen}\"?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) { try { dao.Delete(hlv.MaHLV); LoadData(); } catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); } } };
            ApplyPermissions(Permissions.HuanLuyenVien, btnAdd, btnEdit, btnDelete);
        }

        // ==================== GOI TAP ====================
        private void ShowGoiTap()
        {
            ClearContent();
            SetPageTitle("Quản lý Gói tập");
            BuildCrudPage("Tìm theo mã, tên gói, mô tả...", out var dgv, out var txtSearch, out var btnAdd, out var btnEdit, out var btnDelete, out var btnRefresh);
            var dao = new GoiTapDAO();
            void LoadData(string kw = "")
            {
                try
                {
                    dgv.DataSource = string.IsNullOrWhiteSpace(kw) ? dao.GetAll() : dao.Search(kw);
                    dgv.Columns["MaGoi"].HeaderText = "Mã Gói"; dgv.Columns["TenGoi"].HeaderText = "Tên gói";
                    dgv.Columns["ThoiHan"].HeaderText = "Thời hạn (ngày)";
                    dgv.Columns["Gia"].HeaderText = "Giá"; dgv.Columns["Gia"].DefaultCellStyle.Format = "#,##0";
                    dgv.Columns["MoTa"].HeaderText = "Mô tả";
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
            LoadData();
            txtSearch.TextChanged += (s, e) => LoadData(txtSearch.Text.Trim());
            btnRefresh.Click += (s, e) => { txtSearch.Clear(); LoadData(); };
            btnAdd.Click     += (s, e) => { if (new FormGoiTapDetail(null).ShowDialog() == DialogResult.OK) LoadData(); };
            btnEdit.Click    += (s, e) => { if (dgv.CurrentRow == null) return; var gt = (GoiTap)dgv.CurrentRow.DataBoundItem; if (new FormGoiTapDetail(gt).ShowDialog() == DialogResult.OK) LoadData(); };
            btnDelete.Click  += (s, e) => { if (dgv.CurrentRow == null) return; var gt = (GoiTap)dgv.CurrentRow.DataBoundItem; if (MessageBox.Show($"Xóa \"{gt.TenGoi}\"?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) { try { dao.Delete(gt.MaGoi); LoadData(); } catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); } } };
            ApplyPermissions(Permissions.GoiTap, btnAdd, btnEdit, btnDelete);
        }

        // ==================== DANG KY GOI ====================
        private void ShowDangKyGoi()
        {
            ClearContent();
            SetPageTitle("Quản lý Đăng ký gói");
            BuildCrudPage("Tìm theo học viên, gói tập, trạng thái...", out var dgv, out var txtSearch, out var btnAdd, out var btnEdit, out var btnDelete, out var btnRefresh);
            var dao = new DangKyGoiDAO();
            void OpenSelectedDangKyEditor()
            {
                if (dgv.CurrentRow?.DataBoundItem is not DangKyGoi dk) return;
                if (new FormDangKyGoiDetail(dk, _currentUser).ShowDialog() == DialogResult.OK)
                {
                    LoadData(txtSearch.Text.Trim());
                }
            }

            void LoadData(string keyword = "")
            {
                try
                {
                    var data = dao.GetAll();
                    if (!string.IsNullOrWhiteSpace(keyword))
                    {
                        data = data.Where(x =>
                                x.MaDK.ToString().Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                x.TenHV.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                x.TenGoi.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                x.TenNguoiLap.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                x.TrangThaiThanhToan.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                            .ToList();
                    }

                    dgv.DataSource = data;
                    ConfigureDangKyGoiGrid(dgv);
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
            LoadData();
            txtSearch.TextChanged += (s, e) => LoadData(txtSearch.Text.Trim());
            btnRefresh.Click += (s, e) => { txtSearch.Clear(); LoadData(); };
            btnAdd.Click     += (s, e) => { if (new FormDangKyGoiDetail(null, _currentUser).ShowDialog() == DialogResult.OK) LoadData(); };
            btnEdit.Click    += (s, e) => OpenSelectedDangKyEditor();
            btnDelete.Click  += (s, e) => { if (dgv.CurrentRow == null) return; var dk = (DangKyGoi)dgv.CurrentRow.DataBoundItem; if (MessageBox.Show($"Xóa đăng ký #{dk.MaDK}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) { try { dao.Delete(dk.MaDK); LoadData(); } catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); } } };
            dgv.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) OpenSelectedDangKyEditor(); };
            ApplyPermissions(Permissions.DangKyGoi, btnAdd, btnEdit, btnDelete);
        }

        // ==================== PHAN CONG ====================
        private void ShowPhanCong()
        {
            ClearContent();
            SetPageTitle("Quản lý Phân công PT");
            BuildCrudPage("Tìm theo HLV, học viên, ca làm...", out var dgv, out var txtSearch, out var btnAdd, out var btnEdit, out var btnDelete, out var btnRefresh);
            var dao = new PhanCongDAO();
            void LoadData(string kw = "")
            {
                try
                {
                    dgv.DataSource = string.IsNullOrWhiteSpace(kw) ? dao.GetAll() : dao.Search(kw);
                    dgv.Columns["MaHLV"].Visible = false; dgv.Columns["MaDK"].Visible = false; dgv.Columns["MaCa"].Visible = false;
                    dgv.Columns["MaPC"].HeaderText = "Mã PC"; dgv.Columns["TenHLV"].HeaderText = "HLV";
                    dgv.Columns["TenHV"].HeaderText = "Học viên"; dgv.Columns["TenCa"].HeaderText = "Ca làm";
                    dgv.Columns["NgayBatDau"].HeaderText = "Ngày BĐ"; dgv.Columns["NgayKetThuc"].HeaderText = "Ngày KT";
                    dgv.Columns["GhiChu"].HeaderText = "Ghi chú";
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
            LoadData();
            txtSearch.TextChanged += (s, e) => LoadData(txtSearch.Text.Trim());
            btnRefresh.Click += (s, e) => { txtSearch.Clear(); LoadData(); };
            btnAdd.Click     += (s, e) => { if (new FormPhanCongDetail(null).ShowDialog() == DialogResult.OK) LoadData(); };
            btnEdit.Click    += (s, e) => { if (dgv.CurrentRow == null) return; var pc = (PhanCong)dgv.CurrentRow.DataBoundItem; if (new FormPhanCongDetail(pc).ShowDialog() == DialogResult.OK) LoadData(); };
            btnDelete.Click  += (s, e) => { if (dgv.CurrentRow == null) return; var pc = (PhanCong)dgv.CurrentRow.DataBoundItem; if (MessageBox.Show($"Xóa phân công #{pc.MaPC}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) { try { dao.Delete(pc.MaPC); LoadData(); } catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); } } };
            ApplyPermissions(Permissions.PhanCong, btnAdd, btnEdit, btnDelete);

            // Extra toolbar button: show students/registrations that are not yet assigned a PT.
            if (btnAdd.Parent is Panel tb)
            {
                var btnUnassigned = UIHelper.CreateButton("Chưa phân công", Color.FromArgb(243, 156, 18), Color.White, 158, 36);
                btnUnassigned.Location = new Point(btnAdd.Left - btnUnassigned.Width - 10, btnAdd.Top);
                btnUnassigned.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                tb.Controls.Add(btnUnassigned);
                btnUnassigned.Click += (_, _) => ShowUnassignedDangKyDialog(() => LoadData(txtSearch.Text.Trim()));
            }
        }

        private void ShowUnassignedDangKyDialog(Action onChanged)
        {
            using var dlg = new Form
            {
                Text = "Học viên / đăng ký chưa phân công PT",
                Size = new Size(820, 520),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.Sizable,
                MinimumSize = new Size(640, 400),
                BackColor = Color.FromArgb(245, 246, 250)
            };

            var lbl = new Label
            {
                Text = "Danh sách đăng ký gói hiện chưa được phân công huấn luyện viên",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Location = new Point(16, 12),
                AutoSize = true
            };
            dlg.Controls.Add(lbl);

            var grid = new DataGridView
            {
                Location = new Point(16, 44),
                Size = new Size(dlg.ClientSize.Width - 32, dlg.ClientSize.Height - 110),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoGenerateColumns = true
            };
            UIHelper.StyleDataGridView(grid);
            dlg.Controls.Add(grid);

            var btnAssign = UIHelper.CreateButton("Phân công ngay", Color.FromArgb(39, 174, 96), Color.White, 160, 36);
            btnAssign.Location = new Point(dlg.ClientSize.Width - 176, dlg.ClientSize.Height - 52);
            btnAssign.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            dlg.Controls.Add(btnAssign);

            var btnClose = UIHelper.CreateButton("Đóng", Color.FromArgb(149, 165, 166), Color.White, 100, 36);
            btnClose.Location = new Point(btnAssign.Left - 110, btnAssign.Top);
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClose.Click += (_, _) => dlg.Close();
            dlg.Controls.Add(btnClose);

            void ReloadGrid()
            {
                try
                {
                    var data = new PhanCongDAO().GetDangKyChuaPhanCong();
                    grid.DataSource = data;
                    if (grid.Columns.Contains("MaHV")) grid.Columns["MaHV"].Visible = false;
                    if (grid.Columns.Contains("MaGoi")) grid.Columns["MaGoi"].Visible = false;
                    if (grid.Columns.Contains("MaNguoiLap")) grid.Columns["MaNguoiLap"].Visible = false;
                    if (grid.Columns.Contains("TenNguoiLap")) grid.Columns["TenNguoiLap"].Visible = false;
                    if (grid.Columns.Contains("GiaGoi")) grid.Columns["GiaGoi"].Visible = false;
                    if (grid.Columns.Contains("DaThanhToan")) grid.Columns["DaThanhToan"].Visible = false;
                    if (grid.Columns.Contains("ConThieu")) grid.Columns["ConThieu"].Visible = false;
                    if (grid.Columns.Contains("TrangThaiThanhToan")) grid.Columns["TrangThaiThanhToan"].Visible = false;
                    if (grid.Columns.Contains("MaDK")) grid.Columns["MaDK"].HeaderText = "Mã ĐK";
                    if (grid.Columns.Contains("TenHV")) grid.Columns["TenHV"].HeaderText = "Học viên";
                    if (grid.Columns.Contains("TenGoi")) grid.Columns["TenGoi"].HeaderText = "Gói tập";
                    if (grid.Columns.Contains("NgayBatDau")) grid.Columns["NgayBatDau"].HeaderText = "Ngày BĐ";
                    if (grid.Columns.Contains("NgayHetHan")) grid.Columns["NgayHetHan"].HeaderText = "Ngày HH";
                    if (grid.Columns.Contains("GhiChu")) grid.Columns["GhiChu"].HeaderText = "Ghi chú";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            btnAssign.Click += (_, _) =>
            {
                if (grid.CurrentRow?.DataBoundItem is not DangKyGoi dk) return;
                var pc = new PhanCong { MaDK = dk.MaDK };
                if (new FormPhanCongDetail(pc).ShowDialog(dlg) == DialogResult.OK)
                {
                    ReloadGrid();
                    onChanged();
                }
            };

            grid.CellDoubleClick += (_, e) =>
            {
                if (e.RowIndex < 0) return;
                btnAssign.PerformClick();
            };

            ReloadGrid();
            dlg.ShowDialog(this);
        }

        // ==================== HOA DON ====================
        private void ShowHoaDon()
        {
            ClearContent();
            SetPageTitle("Hóa đơn & thanh toán");
            panelContent.AutoScroll = true;
            panelContent.Padding = new Padding(18);
            var dao = new HoaDonDAO();
            List<HoaDon> invoiceItems = new();
            List<DangKyUnpaidItem> unpaidItems = new();

            int availableWidth = Math.Max(panelContent.ClientSize.Width - 20, 900);
            int contentWidth = Math.Min(availableWidth, 1380);
            int contentLeft = Math.Max((panelContent.ClientSize.Width - contentWidth) / 2, 10);
            bool compactSingleRow = contentWidth >= 1620;

            var filterCard = UIHelper.CreateCard(contentWidth, compactSingleRow ? 78 : 94);
            filterCard.Location = new Point(contentLeft, 10);
            filterCard.Padding = Padding.Empty;
            UIHelper.RoundControl(filterCard, 22);
            filterCard.Resize += (s, e) => UIHelper.RoundControl(filterCard, 22);
            panelContent.Controls.Add(filterCard);

            int buttonTop = compactSingleRow ? 20 : 14;
            int actionRight = filterCard.Width - 16;
            Button PlaceToolbarButton(Button button)
            {
                actionRight -= button.Width;
                button.Location = new Point(actionRight, buttonTop);
                button.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                filterCard.Controls.Add(button);
                actionRight -= 10;
                return button;
            }

            var btnExportReport = PlaceToolbarButton(UIHelper.CreateButton("Xuất báo cáo", Color.FromArgb(39, 174, 96), Color.White, 128, 35));
            var btnExportInvoice = PlaceToolbarButton(UIHelper.CreateButton("Xuất HĐ", Color.FromArgb(52, 73, 94), Color.White, 96, 35));
            var btnRefresh = PlaceToolbarButton(UIHelper.CreateButton("Làm mới", Color.FromArgb(52, 152, 219), Color.White, 96, 35));
            var btnDelete = PlaceToolbarButton(UIHelper.CreateButton("Xóa", Color.FromArgb(231, 76, 60), Color.White, 86, 35));
            var btnPreview = PlaceToolbarButton(UIHelper.CreateButton("In hóa đơn", Color.FromArgb(41, 128, 185), Color.White, 112, 35));
            var btnAdd = PlaceToolbarButton(UIHelper.CreateButton("+ Thêm mới", Color.FromArgb(39, 174, 96), Color.White, 118, 35));

            var searchWrap = new Panel
            {
                BackColor = Color.FromArgb(247, 248, 251),
                Location = new Point(16, compactSingleRow ? 20 : 48),
                Size = new Size(compactSingleRow ? 292 : 320, 36)
            };
            searchWrap.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(223, 229, 238));
                e.Graphics.DrawRectangle(pen, 0, 0, searchWrap.Width - 1, searchWrap.Height - 1);
            };
            UIHelper.RoundControl(searchWrap, 18);
            searchWrap.Resize += (s, e) => UIHelper.RoundControl(searchWrap, 18);
            filterCard.Controls.Add(searchWrap);

            var txtKeyword = UIHelper.CreateSearchBox(300);
            txtKeyword.BorderStyle = BorderStyle.None;
            txtKeyword.BackColor = searchWrap.BackColor;
            txtKeyword.Location = new Point(14, 8);
            txtKeyword.Size = new Size(searchWrap.Width - 28, 20);
            txtKeyword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtKeyword.PlaceholderText = "Tìm theo học viên, gói tập, mã hóa đơn...";
            searchWrap.Controls.Add(txtKeyword);

            var chkNoInvoice = new CheckBox
            {
                Text = "Chưa có hóa đơn",
                Checked = true,
                Location = new Point(searchWrap.Right + 18, compactSingleRow ? 27 : 54),
                Font = new Font("Segoe UI", 10F),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            filterCard.Controls.Add(chkNoInvoice);

            var chkUnderpaid = new CheckBox
            {
                Text = "Chưa thanh toán đủ",
                Checked = true,
                Location = new Point(chkNoInvoice.Right + 18, compactSingleRow ? 27 : 54),
                Font = new Font("Segoe UI", 10F),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            filterCard.Controls.Add(chkUnderpaid);

            var lblPaid = new Label
            {
                Text = "Danh sách hóa đơn",
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                AutoSize = true,
                Location = new Point(contentLeft, filterCard.Bottom + 12)
            };
            panelContent.Controls.Add(lblPaid);

            var lblPaidHint = new Label
            {
                Text = "Double-click để sửa nhanh. Bấm In hóa đơn để mở bản xem trước PDF và in.",
                Font = new Font("Segoe UI", 9.2F),
                ForeColor = ThemeColors.TextSecondary,
                AutoSize = true,
                Location = new Point(contentLeft, lblPaid.Bottom + 2)
            };
            panelContent.Controls.Add(lblPaidHint);

            var dgvPaid = new DataGridView
            {
                Location = new Point(contentLeft, lblPaidHint.Bottom + 12),
                Size = new Size(contentWidth, 237),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            UIHelper.StyleDataGridView(dgvPaid);
            panelContent.Controls.Add(dgvPaid);

            var lblUnpaid = new Label
            {
                Text = "Đăng ký chưa thanh toán hoặc thanh toán thiếu",
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                AutoSize = true,
                Location = new Point(contentLeft, dgvPaid.Bottom + 20)
            };
            panelContent.Controls.Add(lblUnpaid);

            var dgvUnpaid = new DataGridView
            {
                Location = new Point(contentLeft, lblUnpaid.Bottom + 12),
                Size = new Size(contentWidth, Math.Max(panelContent.ClientSize.Height - (lblUnpaid.Bottom + 32), 260)),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            UIHelper.StyleDataGridView(dgvUnpaid);
            panelContent.Controls.Add(dgvUnpaid);

            void LoadData()
            {
                var keyword = txtKeyword.Text.Trim();
                try
                {
                    invoiceItems = dao.GetAll()
                        .Select(NormalizeHoaDonForView)
                        .ToList();
                    if (!string.IsNullOrWhiteSpace(keyword))
                    {
                        invoiceItems = invoiceItems.Where(x =>
                                x.MaHD.ToString().Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                x.TenHV.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                x.TenGoi.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                x.TenNguoiLap.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                x.TenNguoiThanhToan.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                            .ToList();
                    }

                    dgvPaid.DataSource = invoiceItems;
                    ConfigureHoaDonGrid(dgvPaid);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                    return;
                }

                try
                {
                    unpaidItems = dao.GetUnpaidRegistrations(keyword, chkNoInvoice.Checked, chkUnderpaid.Checked);
                    dgvUnpaid.DataSource = unpaidItems;
                    ConfigureUnpaidGrid(dgvUnpaid);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }

            void ExportInvoice()
            {
                if (dgvPaid.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn hóa đơn cần xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var hd = (HoaDon)dgvPaid.CurrentRow.DataBoundItem;
                using var sfd = new SaveFileDialog
                {
                    Filter = "PDF (*.pdf)|*.pdf|Excel Workbook (*.xlsx)|*.xlsx",
                    FileName = $"HoaDon_{hd.MaHD}_{hd.TenHV.Replace(" ", "")}_{DateTime.Now:yyyyMMdd}.pdf"
                };
                if (sfd.ShowDialog() != DialogResult.OK) return;

                try
                {
                    var extension = Path.GetExtension(sfd.FileName).ToLowerInvariant();
                    if (extension == ".pdf")
                        ReportExportHelper.ExportSingleInvoicePdf(sfd.FileName, hd);
                    else
                        ReportExportHelper.ExportSingleInvoice(sfd.FileName, hd);

                    MessageBox.Show($"Đã xuất hóa đơn thành công ra file {extension.ToUpperInvariant()}!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            void PreviewSelectedInvoice()
            {
                if (dgvPaid.CurrentRow?.DataBoundItem is not HoaDon hd)
                {
                    MessageBox.Show("Vui lòng chọn hóa đơn cần xem trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    var previewPath = Path.Combine(
                        Path.GetTempPath(),
                        $"AppGym_HoaDon_{hd.MaHD}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");

                    ReportExportHelper.ExportSingleInvoicePdf(previewPath, hd);
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = previewPath,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể mở xem trước hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            void OpenSelectedInvoiceEditor()
            {
                if (dgvPaid.CurrentRow?.DataBoundItem is not HoaDon hd) return;
                if (new FormHoaDonDetail(hd, _currentUser).ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }

            void OpenUnpaidInvoiceCreator()
            {
                if (dgvUnpaid.CurrentRow?.DataBoundItem is not DangKyUnpaidItem item) return;
                if (new FormHoaDonDetail(null, _currentUser, item.MaDK).ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }

            void ExportReport()
            {
                try
                {
                    using var sfd = new SaveFileDialog
                    {
                        Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                        FileName = $"BaoCaoThanhToan_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                    };
                    if (sfd.ShowDialog() != DialogResult.OK) return;

                    var reportItems = invoiceItems.Select(x => new ThanhToanReportItem
                    {
                        MaHD = x.MaHD,
                        MaDK = x.MaDK,
                        HoTen = x.TenHV,
                        TenGoi = x.TenGoi,
                        NgayThanhToan = x.NgayThanhToan,
                        SoTien = x.SoTien,
                        HinhThucTT = x.HinhThucTT,
                        TenNguoiLap = x.TenNguoiLap,
                        TenNguoiThanhToan = x.TenNguoiThanhToan,
                        GhiChu = x.GhiChu
                    }).ToList();

                    ReportExportHelper.ExportThanhToanReport(
                        sfd.FileName,
                        reportItems,
                        unpaidItems,
                        true,
                        true);

                    MessageBox.Show("Đã xuất file thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            LoadData();
            txtKeyword.TextChanged += (s, e) => LoadData();
            chkNoInvoice.CheckedChanged += (s, e) => LoadData();
            chkUnderpaid.CheckedChanged += (s, e) => LoadData();
            btnRefresh.Click += (s, e) => { txtKeyword.Clear(); chkNoInvoice.Checked = true; chkUnderpaid.Checked = true; LoadData(); };
            btnAdd.Click += (s, e) => { if (new FormHoaDonDetail(null, _currentUser).ShowDialog() == DialogResult.OK) LoadData(); };
            btnPreview.Click += (s, e) => PreviewSelectedInvoice();
            btnDelete.Click += (s, e) => { if (dgvPaid.CurrentRow == null) return; var hd = (HoaDon)dgvPaid.CurrentRow.DataBoundItem; if (MessageBox.Show($"Xóa hóa đơn #{hd.MaHD}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) { try { dao.Delete(hd.MaHD); LoadData(); } catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); } } };
            btnExportInvoice.Click += (s, e) => ExportInvoice();
            btnExportReport.Click += (s, e) => ExportReport();
            dgvPaid.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) OpenSelectedInvoiceEditor(); };
            dgvUnpaid.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) OpenUnpaidInvoiceCreator(); };

            ApplyPermissions(Permissions.HoaDon, btnAdd, null, btnDelete);
        }

        private static HoaDon NormalizeHoaDonForView(HoaDon item)
        {
            item.HinhThucTT = NormalizePaymentMethodText(item.HinhThucTT);
            item.GhiChu = NormalizeDisplayNote(item.GhiChu);
            return item;
        }

        private static string NormalizePaymentMethodText(string value)
        {
            return value switch
            {
                "Tiá»\u0081n máº·t" => "Ti\u1EC1n m\u1EB7t",
                "Chuyá»ƒn khoáº£n" => "Chuy\u1EC3n kho\u1EA3n",
                "Tháº»" => "Th\u1EBB",
                _ => value
            };
        }

        private static string NormalizeDisplayNote(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            return value.StartsWith("SEED_MONTHLY_", StringComparison.OrdinalIgnoreCase)
                ? string.Empty
                : value;
        }

        private void ConfigureHoaDonGrid(DataGridView dgv)
        {
            if (dgv.Columns.Contains("MaDK")) dgv.Columns["MaDK"].Visible = false;
            if (dgv.Columns.Contains("MaNguoiLap")) dgv.Columns["MaNguoiLap"].Visible = false;
            if (dgv.Columns.Contains("MaNguoiThanhToan")) dgv.Columns["MaNguoiThanhToan"].Visible = false;
            if (dgv.Columns.Contains("MaHD")) dgv.Columns["MaHD"].HeaderText = "Mã HĐ";
            if (dgv.Columns.Contains("TenHV")) dgv.Columns["TenHV"].HeaderText = "Học viên";
            if (dgv.Columns.Contains("TenGoi")) dgv.Columns["TenGoi"].HeaderText = "Gói tập";
            if (dgv.Columns.Contains("TenNguoiLap")) dgv.Columns["TenNguoiLap"].HeaderText = "Người lên HĐ";
            if (dgv.Columns.Contains("TenNguoiThanhToan")) dgv.Columns["TenNguoiThanhToan"].HeaderText = "Người thanh toán";
            if (dgv.Columns.Contains("NgayThanhToan")) dgv.Columns["NgayThanhToan"].HeaderText = "Ngày TT";
            if (dgv.Columns.Contains("SoTien"))
            {
                dgv.Columns["SoTien"].HeaderText = "Số tiền";
                dgv.Columns["SoTien"].DefaultCellStyle.Format = "#,##0";
            }
            if (dgv.Columns.Contains("HinhThucTT")) dgv.Columns["HinhThucTT"].HeaderText = "Hình thức";
            if (dgv.Columns.Contains("GhiChu")) dgv.Columns["GhiChu"].HeaderText = "Ghi chú";
        }

        private void ConfigureUnpaidGrid(DataGridView dgv)
        {
            if (dgv.Columns.Contains("MaDK")) dgv.Columns["MaDK"].HeaderText = "Mã ĐK";
            if (dgv.Columns.Contains("MaHV")) dgv.Columns["MaHV"].HeaderText = "Mã HV";
            if (dgv.Columns.Contains("HoTen")) dgv.Columns["HoTen"].HeaderText = "Học viên";
            if (dgv.Columns.Contains("MaGoi")) dgv.Columns["MaGoi"].HeaderText = "Mã Gói";
            if (dgv.Columns.Contains("TenGoi")) dgv.Columns["TenGoi"].HeaderText = "Gói tập";
            if (dgv.Columns.Contains("NgayBatDau")) dgv.Columns["NgayBatDau"].HeaderText = "Ngày BĐ";
            if (dgv.Columns.Contains("NgayHetHan")) dgv.Columns["NgayHetHan"].HeaderText = "Ngày HH";
            if (dgv.Columns.Contains("TongDaThanhToan"))
            {
                dgv.Columns["TongDaThanhToan"].HeaderText = "Đã thanh toán";
                dgv.Columns["TongDaThanhToan"].DefaultCellStyle.Format = "#,##0";
            }
            if (dgv.Columns.Contains("GiaGoi"))
            {
                dgv.Columns["GiaGoi"].HeaderText = "Giá gói";
                dgv.Columns["GiaGoi"].DefaultCellStyle.Format = "#,##0";
            }
            if (dgv.Columns.Contains("ConThieu"))
            {
                dgv.Columns["ConThieu"].HeaderText = "Còn thiếu";
                dgv.Columns["ConThieu"].DefaultCellStyle.Format = "#,##0";
            }
        }

        // ==================== CA LAM ====================
        private void ShowCaLam()
        {
            ClearContent();
            SetPageTitle("Quản lý Ca làm");
            BuildCrudPage("Tìm theo mã ca, tên ca, giờ...", out var dgv, out var txtSearch, out var btnAdd, out var btnEdit, out var btnDelete, out var btnRefresh);
            var dao = new CaLamDAO();
            void LoadData(string kw = "")
            {
                try
                {
                    dgv.DataSource = string.IsNullOrWhiteSpace(kw) ? dao.GetAll() : dao.Search(kw);
                    dgv.Columns["MaCa"].HeaderText = "Mã Ca"; dgv.Columns["TenCa"].HeaderText = "Tên ca";
                    dgv.Columns["GioBatDau"].HeaderText = "Giờ bắt đầu"; dgv.Columns["GioKetThuc"].HeaderText = "Giờ kết thúc";
                    dgv.Columns["GioBatDau"].DefaultCellStyle.Format = @"hh\:mm";
                    dgv.Columns["GioKetThuc"].DefaultCellStyle.Format = @"hh\:mm";
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
            LoadData();
            txtSearch.TextChanged += (s, e) => LoadData(txtSearch.Text.Trim());
            btnRefresh.Click += (s, e) => { txtSearch.Clear(); LoadData(); };
            btnAdd.Click     += (s, e) => { if (new FormCaLamDetail(null).ShowDialog() == DialogResult.OK) LoadData(); };
            btnEdit.Click    += (s, e) => { if (dgv.CurrentRow == null) return; var ca = (CaLam)dgv.CurrentRow.DataBoundItem; if (new FormCaLamDetail(ca).ShowDialog() == DialogResult.OK) LoadData(); };
            btnDelete.Click  += (s, e) => { if (dgv.CurrentRow == null) return; var ca = (CaLam)dgv.CurrentRow.DataBoundItem; if (MessageBox.Show($"Xóa ca làm \"{ca.TenCa}\"?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) { try { dao.Delete(ca.MaCa); LoadData(); } catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); } } };
            ApplyPermissions(Permissions.CaLam, btnAdd, btnEdit, btnDelete);
        }

        // ==================== PHAN QUYEN ====================
        private void ShowPhanQuyen()
        {
            if (!_perm.CanAssignPermissions)
            {
                MessageBox.Show("Chỉ Admin mới có quyền phân quyền.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using var dlg = new FormPhanQuyen();
            dlg.ShowDialog(this);
        }

        private void ApplyPermissions(string module, Button? btnAdd, Button? btnEdit, Button? btnDelete)
        {
            if (btnAdd != null) btnAdd.Visible = _perm.CanAdd(module);
            if (btnEdit != null) btnEdit.Visible = _perm.CanEdit(module);
            if (btnDelete != null) btnDelete.Visible = _perm.CanDelete(module);
        }

        // ==================== BUILD CRUD PAGE ====================
        private void BuildCrudPage(string searchPlaceholder, out DataGridView dgv, out TextBox txtSearch,
            out Button btnAdd, out Button btnEdit, out Button btnDelete, out Button btnRefresh)
        {
            const int pagePadding = 18;
            const int toolbarHeight = 68;
            const int sectionGap = 14;
            int w = panelContent.ClientSize.Width;
            int h = panelContent.ClientSize.Height;
            int surfaceWidth = Math.Max(760, w - pagePadding * 2);
            int gridHeight = Math.Max(240, h - (pagePadding * 2) - toolbarHeight - sectionGap);

            var toolbarCard = UIHelper.CreateCard(surfaceWidth, toolbarHeight);
            toolbarCard.Location = new Point(pagePadding, pagePadding);
            toolbarCard.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            toolbarCard.Padding = Padding.Empty;
            UIHelper.RoundControl(toolbarCard, 22);
            toolbarCard.Resize += (s, e) => UIHelper.RoundControl(toolbarCard, 22);
            panelContent.Controls.Add(toolbarCard);

            var searchWrap = new Panel
            {
                BackColor = Color.FromArgb(247, 248, 251),
                Location = new Point(16, 15),
                Size = new Size(340, 38)
            };
            searchWrap.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(223, 229, 238));
                e.Graphics.DrawRectangle(pen, 0, 0, searchWrap.Width - 1, searchWrap.Height - 1);
            };
            UIHelper.RoundControl(searchWrap, 18);
            searchWrap.Resize += (s, e) => UIHelper.RoundControl(searchWrap, 18);
            toolbarCard.Controls.Add(searchWrap);

            txtSearch = UIHelper.CreateSearchBox(searchWrap.Width - 32);
            txtSearch.BorderStyle = BorderStyle.None;
            txtSearch.BackColor = searchWrap.BackColor;
            txtSearch.Font = new Font("Segoe UI", 10.5F);
            txtSearch.Location = new Point(16, 9);
            txtSearch.Size = new Size(searchWrap.Width - 32, 20);
            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSearch.PlaceholderText = searchPlaceholder;
            searchWrap.Controls.Add(txtSearch);

            int toolbarRight = toolbarCard.Width - 16;
            Button PlaceToolbarButton(Button button)
            {
                toolbarRight -= button.Width;
                button.Location = new Point(toolbarRight, 15);
                button.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                toolbarCard.Controls.Add(button);
                toolbarRight -= 10;
                return button;
            }

            btnRefresh = PlaceToolbarButton(UIHelper.CreateButton("Làm mới", Color.FromArgb(52, 152, 219), Color.White, 104, 36));
            btnDelete = PlaceToolbarButton(UIHelper.CreateButton("Xóa", Color.FromArgb(231, 76, 60), Color.White, 88, 36));
            btnEdit = PlaceToolbarButton(UIHelper.CreateButton("Sửa", Color.FromArgb(41, 128, 185), Color.White, 88, 36));
            btnAdd = PlaceToolbarButton(UIHelper.CreateButton("+ Thêm mới", Color.FromArgb(39, 174, 96), Color.White, 128, 36));

            var gridCard = UIHelper.CreateCard(surfaceWidth, gridHeight);
            gridCard.Location = new Point(pagePadding, toolbarCard.Bottom + sectionGap);
            gridCard.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            gridCard.Padding = new Padding(1);
            UIHelper.RoundControl(gridCard, 24);
            gridCard.Resize += (s, e) => UIHelper.RoundControl(gridCard, 24);
            panelContent.Controls.Add(gridCard);

            dgv = new DataGridView { Dock = DockStyle.Fill };
            UIHelper.StyleDataGridView(dgv);
            gridCard.Controls.Add(dgv);
        }
    }
}

