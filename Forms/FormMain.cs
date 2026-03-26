using AppGym.DataAccess;
using AppGym.Helpers;
using AppGym.Models;

namespace AppGym.Forms
{
    public partial class FormMain : Form
    {
        private TaiKhoan _currentUser;
        private Button? _activeButton;

        public FormMain(TaiKhoan user)
        {
            _currentUser = user;
            InitializeComponent();
            lblUserName.Text = $"{_currentUser.HoTen} ({_currentUser.VaiTro})";
            BuildMenuButtons();
            Load += (s, e) => ShowDashboard();
            Resize += (s, e) => lblUserName.Location = new Point(panelTopBar.Width - lblUserName.Width - 30, 20);
        }

        private void BuildMenuButtons()
        {
            var menuItems = new (string text, string icon, Action action)[]
            {
                ("Tổng quan",        "\u25A3", ShowDashboard),
                ("Học viên",         "\u263A", ShowHocVien),
                ("Huấn luyện viên",  "\u2605", ShowHuanLuyenVien),
                ("Gói tập",          "\u25C6", ShowGoiTap),
                ("Đăng ký gói",      "\u25B6", ShowDangKyGoi),
                ("Phân công PT",     "\u2611", ShowPhanCong),
                ("Hóa đơn",          "\u25B2", ShowHoaDon),
                ("Ca làm",           "\u25CB", ShowCaLam),
            };

            int yPos = 95;
            foreach (var item in menuItems)
            {
                var btn = new Button
                {
                    Text      = $"  {item.icon}   {item.text}",
                    Size      = new Size(240, 48),
                    Location  = new Point(0, yPos),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.FromArgb(30, 39, 73),
                    ForeColor = Color.FromArgb(200, 200, 220),
                    Font      = new Font("Segoe UI", 11, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Padding   = new Padding(15, 0, 0, 0),
                    Cursor    = Cursors.Hand,
                    Tag       = item.action
                };
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(52, 63, 100);
                btn.Click += MenuButton_Click;
                panelSidebar.Controls.Add(btn);
                yPos += 48;
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

        private void SetActiveButton(Button btn)
        {
            if (_activeButton != null)
            {
                _activeButton.BackColor = Color.FromArgb(30, 39, 73);
                _activeButton.ForeColor = Color.FromArgb(200, 200, 220);
            }
            _activeButton = btn;
            _activeButton.BackColor = Color.FromArgb(52, 152, 219);
            _activeButton.ForeColor = Color.White;
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
            ShowTaiKhoan();
        }

        // ==================== TAI KHOAN ====================
        private void ShowTaiKhoan()
        {
            ClearContent();
            SetPageTitle("Quản lý tài khoản");

            var panel = new Panel { Dock = DockStyle.Fill, AutoScroll = true, Padding = new Padding(30) };
            panelContent.Controls.Add(panel);

            var lblTitle = new Label
            {
                Text = "Thông tin tài khoản",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                AutoSize = true,
                Location = new Point(30, 20)
            };
            panel.Controls.Add(lblTitle);

            int y = 70;
            var fields = new (string label, string value)[]
            {
                ("Mã tài khoản:", _currentUser.MaTK.ToString()),
                ("Tên đăng nhập:", _currentUser.TenDangNhap),
                ("Họ tên:", _currentUser.HoTen),
                ("Vai trò:", _currentUser.VaiTro),
                ("Trạng thái:", _currentUser.TrangThai ? "Đang hoạt động" : "Bị khóa"),
                ("Ngày tạo:", _currentUser.TaoLuc?.ToString("dd/MM/yyyy HH:mm") ?? "N/A"),
            };

            foreach (var field in fields)
            {
                panel.Controls.Add(new Label
                {
                    Text = field.label,
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    ForeColor = Color.FromArgb(80, 80, 80),
                    AutoSize = true,
                    Location = new Point(30, y)
                });
                panel.Controls.Add(new Label
                {
                    Text = field.value,
                    Font = new Font("Segoe UI", 11),
                    ForeColor = Color.FromArgb(44, 62, 80),
                    AutoSize = true,
                    Location = new Point(200, y)
                });
                y += 40;
            }

            var btnChangePass = new Button
            {
                Text = "Đổi mật khẩu",
                Size = new Size(160, 42),
                Location = new Point(30, y + 20),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnChangePass.FlatAppearance.BorderSize = 0;
            btnChangePass.Click += BtnChangePass_Click;
            panel.Controls.Add(btnChangePass);
        }

        private void BtnChangePass_Click(object? sender, EventArgs e)
        {
            using var dlg = new Form
            {
                Text = "Đổi mật khẩu",
                Size = new Size(400, 280),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.White
            };

            var lblOld = new Label { Text = "Mật khẩu cũ:", Location = new Point(20, 25), AutoSize = true, Font = new Font("Segoe UI", 10) };
            var txtOld = new TextBox { Location = new Point(20, 50), Size = new Size(340, 30), Font = new Font("Segoe UI", 11), UseSystemPasswordChar = true };
            var lblNew = new Label { Text = "Mật khẩu mới:", Location = new Point(20, 90), AutoSize = true, Font = new Font("Segoe UI", 10) };
            var txtNew = new TextBox { Location = new Point(20, 115), Size = new Size(340, 30), Font = new Font("Segoe UI", 11), UseSystemPasswordChar = true };
            var lblConfirm = new Label { Text = "Xác nhận mật khẩu:", Location = new Point(20, 155), AutoSize = true, Font = new Font("Segoe UI", 10) };
            var txtConfirm = new TextBox { Location = new Point(20, 180), Size = new Size(340, 30), Font = new Font("Segoe UI", 11), UseSystemPasswordChar = true };

            var btnSave = new Button
            {
                Text = "Lưu", Size = new Size(100, 38), Location = new Point(260, 220),
                FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(39, 174, 96),
                ForeColor = Color.White, Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnSave.FlatAppearance.BorderSize = 0;

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

            dlg.Controls.AddRange(new Control[] { lblOld, txtOld, lblNew, txtNew, lblConfirm, txtConfirm, btnSave });
            dlg.ShowDialog();
        }

        private void SetPageTitle(string title) => lblPageTitle.Text = title;

        private void ClearContent()
        {
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
            try
            {
                hvCount  = new HocVienDAO().Count();
                hlvCount = new HuanLuyenVienDAO().Count();
                goiCount = new GoiTapDAO().Count();
                dkCount  = new DangKyGoiDAO().CountActive();
                revenue  = new HoaDonDAO().TotalRevenue();
            }
            catch { }

            string fmtRev;
            if (revenue >= 1_000_000_000) fmtRev = (revenue / 1_000_000_000m).ToString("#,##0.#") + " t\u1EF7";
            else if (revenue >= 1_000_000) fmtRev = (revenue / 1_000_000m).ToString("#,##0.#") + " tr";
            else if (revenue >= 1_000) fmtRev = (revenue / 1_000m).ToString("#,##0") + "K";
            else fmtRev = revenue.ToString("#,##0") + "\u0111";

            var cards = new (string title, string value, string icon, Color color)[]
            {
                ("H\u1ECDc vi\u00EAn",         hvCount.ToString(),    "\u263A", Color.FromArgb(52, 152, 219)),
                ("Hu\u1EA5n luy\u1EC7n vi\u00EAn",  hlvCount.ToString(), "\u2605", Color.FromArgb(46, 204, 113)),
                ("G\u00F3i t\u1EADp",          goiCount.ToString(),   "\u25C6", Color.FromArgb(155, 89, 182)),
                ("\u0110\u0103ng k\u00FD \u0111ang H\u0110", dkCount.ToString(), "\u25B6", Color.FromArgb(241, 196, 15)),
                ("Doanh thu",        fmtRev,                          "\u25B2", Color.FromArgb(230, 126, 34)),
            };

            int contentW = panelContent.ClientSize.Width - 40;
            int cardGap = 15;
            int cardWidth = (contentW - cardGap * 4) / 5;
            if (cardWidth < 150) cardWidth = 150;
            int cardHeight = 140;

            int xPos = 10, yPos = 10;
            foreach (var card in cards)
            {
                var shadow = new Panel { Size = new Size(cardWidth + 2, cardHeight + 2), Location = new Point(xPos - 1, yPos - 1), BackColor = Color.FromArgb(215, 218, 225) };
                panelContent.Controls.Add(shadow);

                var p = new Panel { Size = new Size(cardWidth, cardHeight), Location = new Point(xPos, yPos), BackColor = Color.White };
                p.Controls.Add(new Panel { Size = new Size(5, cardHeight), Location = new Point(0, 0), BackColor = card.color });
                p.Controls.Add(new Label { Text = card.icon, Font = new Font("Segoe UI", 20), ForeColor = card.color, AutoSize = true, Location = new Point(16, 12), BackColor = Color.Transparent });
                p.Controls.Add(new Label { Text = card.value, Font = new Font("Segoe UI", 22, FontStyle.Bold), ForeColor = card.color, AutoSize = true, Location = new Point(16, 50), MaximumSize = new Size(cardWidth - 25, 0), BackColor = Color.Transparent });
                p.Controls.Add(new Label { Text = card.title, Font = new Font("Segoe UI", 9.5f), ForeColor = Color.FromArgb(120, 130, 140), AutoSize = true, Location = new Point(16, 105), BackColor = Color.Transparent });

                panelContent.Controls.Add(p);
                p.BringToFront();
                xPos += cardWidth + cardGap;
            }

            yPos = cardHeight + 35;
            var lblSection = new Label { Text = "\u2611  \u0110\u0103ng k\u00FD g\u1EA7n \u0111\u00E2y", Font = new Font("Segoe UI", 13, FontStyle.Bold), ForeColor = Color.FromArgb(44, 62, 80), AutoSize = true, Location = new Point(10, yPos) };
            panelContent.Controls.Add(lblSection);
            lblSection.BringToFront();

            var dgv = new DataGridView { Location = new Point(10, yPos + 40), Size = new Size(panelContent.ClientSize.Width - 60, 320), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
            UIHelper.StyleDataGridView(dgv);
            panelContent.Controls.Add(dgv);
            dgv.BringToFront();

            try
            {
                var list = new DangKyGoiDAO().GetAll();
                dgv.DataSource = list.Take(10).ToList();
                dgv.Columns["MaHV"].Visible = false;
                dgv.Columns["MaGoi"].Visible = false;
                dgv.Columns["MaDK"].HeaderText = "M\u00E3 \u0110K";
                dgv.Columns["TenHV"].HeaderText = "H\u1ECDc vi\u00EAn";
                dgv.Columns["TenGoi"].HeaderText = "G\u00F3i t\u1EADp";
                dgv.Columns["NgayBatDau"].HeaderText = "Ng\u00E0y B\u0110";
                dgv.Columns["NgayHetHan"].HeaderText = "Ng\u00E0y HH";
                dgv.Columns["TrangThai"].HeaderText = "Tr\u1EA1ng th\u00E1i";
                dgv.Columns["GhiChu"].HeaderText = "Ghi ch\u00FA";
            }
            catch { }
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
                    dgv.Columns["NgayDangKy"].HeaderText = "Ngày ĐK"; dgv.Columns["TrangThai"].HeaderText = "Trạng thái";
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
            LoadData();
            txtSearch.TextChanged += (s, e) => LoadData(txtSearch.Text.Trim());
            btnRefresh.Click  += (s, e) => { txtSearch.Clear(); LoadData(); };
            btnAdd.Click      += (s, e) => { if (new FormHocVienDetail(null).ShowDialog() == DialogResult.OK) LoadData(); };
            btnEdit.Click     += (s, e) => { if (dgv.CurrentRow == null) return; var hv = (HocVien)dgv.CurrentRow.DataBoundItem; if (new FormHocVienDetail(hv).ShowDialog() == DialogResult.OK) LoadData(); };
            btnDelete.Click   += (s, e) => { if (dgv.CurrentRow == null) return; var hv = (HocVien)dgv.CurrentRow.DataBoundItem; if (MessageBox.Show($"Xóa \"{hv.HoTen}\"?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) { try { dao.Delete(hv.MaHV); LoadData(); } catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); } } };
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
                    dgv.Columns["TrangThai"].HeaderText = "Trạng thái";
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
            LoadData();
            txtSearch.TextChanged += (s, e) => LoadData(txtSearch.Text.Trim());
            btnRefresh.Click += (s, e) => { txtSearch.Clear(); LoadData(); };
            btnAdd.Click     += (s, e) => { if (new FormHLVDetail(null).ShowDialog() == DialogResult.OK) LoadData(); };
            btnEdit.Click    += (s, e) => { if (dgv.CurrentRow == null) return; var hlv = (HuanLuyenVien)dgv.CurrentRow.DataBoundItem; if (new FormHLVDetail(hlv).ShowDialog() == DialogResult.OK) LoadData(); };
            btnDelete.Click  += (s, e) => { if (dgv.CurrentRow == null) return; var hlv = (HuanLuyenVien)dgv.CurrentRow.DataBoundItem; if (MessageBox.Show($"Xóa \"{hlv.HoTen}\"?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) { try { dao.Delete(hlv.MaHLV); LoadData(); } catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); } } };
        }

        // ==================== GOI TAP ====================
        private void ShowGoiTap()
        {
            ClearContent();
            SetPageTitle("Quản lý Gói tập");
            BuildCrudPage("Tìm kiếm gói tập...", out var dgv, out var txtSearch, out var btnAdd, out var btnEdit, out var btnDelete, out var btnRefresh);
            var dao = new GoiTapDAO();
            void LoadData()
            {
                try
                {
                    dgv.DataSource = dao.GetAll();
                    dgv.Columns["MaGoi"].HeaderText = "Mã Gói"; dgv.Columns["TenGoi"].HeaderText = "Tên gói";
                    dgv.Columns["ThoiHan"].HeaderText = "Thời hạn (ngày)";
                    dgv.Columns["Gia"].HeaderText = "Giá"; dgv.Columns["Gia"].DefaultCellStyle.Format = "#,##0";
                    dgv.Columns["MoTa"].HeaderText = "Mô tả"; dgv.Columns["TrangThai"].HeaderText = "Trạng thái";
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
            LoadData();
            btnRefresh.Click += (s, e) => LoadData();
            btnAdd.Click     += (s, e) => { if (new FormGoiTapDetail(null).ShowDialog() == DialogResult.OK) LoadData(); };
            btnEdit.Click    += (s, e) => { if (dgv.CurrentRow == null) return; var gt = (GoiTap)dgv.CurrentRow.DataBoundItem; if (new FormGoiTapDetail(gt).ShowDialog() == DialogResult.OK) LoadData(); };
            btnDelete.Click  += (s, e) => { if (dgv.CurrentRow == null) return; var gt = (GoiTap)dgv.CurrentRow.DataBoundItem; if (MessageBox.Show($"Xóa \"{gt.TenGoi}\"?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) { try { dao.Delete(gt.MaGoi); LoadData(); } catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); } } };
        }

        // ==================== DANG KY GOI ====================
        private void ShowDangKyGoi()
        {
            ClearContent();
            SetPageTitle("Quản lý Đăng ký gói");
            BuildCrudPage("Tìm kiếm...", out var dgv, out var txtSearch, out var btnAdd, out var btnEdit, out var btnDelete, out var btnRefresh);
            var dao = new DangKyGoiDAO();
            void LoadData()
            {
                try
                {
                    dgv.DataSource = dao.GetAll();
                    dgv.Columns["MaHV"].Visible = false; dgv.Columns["MaGoi"].Visible = false;
                    dgv.Columns["MaDK"].HeaderText = "Mã ĐK"; dgv.Columns["TenHV"].HeaderText = "Học viên";
                    dgv.Columns["TenGoi"].HeaderText = "Gói tập"; dgv.Columns["NgayBatDau"].HeaderText = "Ngày BĐ";
                    dgv.Columns["NgayHetHan"].HeaderText = "Ngày HH"; dgv.Columns["TrangThai"].HeaderText = "Trạng thái";
                    dgv.Columns["GhiChu"].HeaderText = "Ghi chú";
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
            LoadData();
            btnRefresh.Click += (s, e) => LoadData();
            btnAdd.Click     += (s, e) => { if (new FormDangKyGoiDetail(null).ShowDialog() == DialogResult.OK) LoadData(); };
            btnEdit.Click    += (s, e) => { if (dgv.CurrentRow == null) return; var dk = (DangKyGoi)dgv.CurrentRow.DataBoundItem; if (new FormDangKyGoiDetail(dk).ShowDialog() == DialogResult.OK) LoadData(); };
            btnDelete.Click  += (s, e) => { if (dgv.CurrentRow == null) return; var dk = (DangKyGoi)dgv.CurrentRow.DataBoundItem; if (MessageBox.Show($"Xóa đăng ký #{dk.MaDK}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) { try { dao.Delete(dk.MaDK); LoadData(); } catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); } } };
        }

        // ==================== PHAN CONG ====================
        private void ShowPhanCong()
        {
            ClearContent();
            SetPageTitle("Quản lý Phân công PT");
            BuildCrudPage("Tìm kiếm...", out var dgv, out var txtSearch, out var btnAdd, out var btnEdit, out var btnDelete, out var btnRefresh);
            var dao = new PhanCongDAO();
            void LoadData()
            {
                try
                {
                    dgv.DataSource = dao.GetAll();
                    dgv.Columns["MaHLV"].Visible = false; dgv.Columns["MaDK"].Visible = false; dgv.Columns["MaCa"].Visible = false;
                    dgv.Columns["MaPC"].HeaderText = "Mã PC"; dgv.Columns["TenHLV"].HeaderText = "HLV";
                    dgv.Columns["TenHV"].HeaderText = "Học viên"; dgv.Columns["TenCa"].HeaderText = "Ca làm";
                    dgv.Columns["NgayBatDau"].HeaderText = "Ngày BĐ"; dgv.Columns["NgayKetThuc"].HeaderText = "Ngày KT";
                    dgv.Columns["GhiChu"].HeaderText = "Ghi chú";
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
            LoadData();
            btnRefresh.Click += (s, e) => LoadData();
            btnAdd.Click     += (s, e) => { if (new FormPhanCongDetail(null).ShowDialog() == DialogResult.OK) LoadData(); };
            btnEdit.Click    += (s, e) => { if (dgv.CurrentRow == null) return; var pc = (PhanCong)dgv.CurrentRow.DataBoundItem; if (new FormPhanCongDetail(pc).ShowDialog() == DialogResult.OK) LoadData(); };
            btnDelete.Click  += (s, e) => { if (dgv.CurrentRow == null) return; var pc = (PhanCong)dgv.CurrentRow.DataBoundItem; if (MessageBox.Show($"Xóa phân công #{pc.MaPC}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) { try { dao.Delete(pc.MaPC); LoadData(); } catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); } } };
        }

        // ==================== HOA DON ====================
        private void ShowHoaDon()
        {
            ClearContent();
            SetPageTitle("Quản lý Hóa đơn");
            BuildCrudPage("Tìm kiếm...", out var dgv, out var txtSearch, out var btnAdd, out var btnEdit, out var btnDelete, out var btnRefresh);
            var dao = new HoaDonDAO();
            void LoadData()
            {
                try
                {
                    dgv.DataSource = dao.GetAll();
                    dgv.Columns["MaDK"].Visible = false;
                    dgv.Columns["MaHD"].HeaderText = "Mã HĐ"; dgv.Columns["TenHV"].HeaderText = "Học viên";
                    dgv.Columns["TenGoi"].HeaderText = "Gói tập"; dgv.Columns["NgayThanhToan"].HeaderText = "Ngày TT";
                    dgv.Columns["SoTien"].HeaderText = "Số tiền"; dgv.Columns["SoTien"].DefaultCellStyle.Format = "#,##0";
                    dgv.Columns["HinhThucTT"].HeaderText = "Hình thức"; dgv.Columns["GhiChu"].HeaderText = "Ghi chú";
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
            LoadData();
            btnRefresh.Click += (s, e) => LoadData();
            btnAdd.Click     += (s, e) => { if (new FormHoaDonDetail(null).ShowDialog() == DialogResult.OK) LoadData(); };
            btnEdit.Click    += (s, e) => { if (dgv.CurrentRow == null) return; var hd = (HoaDon)dgv.CurrentRow.DataBoundItem; if (new FormHoaDonDetail(hd).ShowDialog() == DialogResult.OK) LoadData(); };
            btnDelete.Click  += (s, e) => { if (dgv.CurrentRow == null) return; var hd = (HoaDon)dgv.CurrentRow.DataBoundItem; if (MessageBox.Show($"Xóa hóa đơn #{hd.MaHD}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) { try { dao.Delete(hd.MaHD); LoadData(); } catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); } } };
        }

        // ==================== CA LAM ====================
        private void ShowCaLam()
        {
            ClearContent();
            SetPageTitle("Quản lý Ca làm");
            BuildCrudPage("Tìm kiếm...", out var dgv, out var txtSearch, out var btnAdd, out var btnEdit, out var btnDelete, out var btnRefresh);
            var dao = new CaLamDAO();
            void LoadData()
            {
                try
                {
                    dgv.DataSource = dao.GetAll();
                    dgv.Columns["MaCa"].HeaderText = "Mã Ca"; dgv.Columns["TenCa"].HeaderText = "Tên ca";
                    dgv.Columns["GioBatDau"].HeaderText = "Giờ bắt đầu"; dgv.Columns["GioKetThuc"].HeaderText = "Giờ kết thúc";
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
            LoadData();
            btnRefresh.Click += (s, e) => LoadData();
            btnAdd.Click     += (s, e) => { if (new FormCaLamDetail(null).ShowDialog() == DialogResult.OK) LoadData(); };
            btnEdit.Click    += (s, e) => { if (dgv.CurrentRow == null) return; var ca = (CaLam)dgv.CurrentRow.DataBoundItem; if (new FormCaLamDetail(ca).ShowDialog() == DialogResult.OK) LoadData(); };
            btnDelete.Click  += (s, e) => { if (dgv.CurrentRow == null) return; var ca = (CaLam)dgv.CurrentRow.DataBoundItem; if (MessageBox.Show($"Xóa ca làm \"{ca.TenCa}\"?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) { try { dao.Delete(ca.MaCa); LoadData(); } catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); } } };
        }

        // ==================== BUILD CRUD PAGE ====================
        private void BuildCrudPage(string searchPlaceholder, out DataGridView dgv, out TextBox txtSearch,
            out Button btnAdd, out Button btnEdit, out Button btnDelete, out Button btnRefresh)
        {
            int w = panelContent.ClientSize.Width;
            int h = panelContent.ClientSize.Height;
            int toolbarH = 55;

            dgv = new DataGridView { Location = new Point(0, toolbarH), Size = new Size(w, h - toolbarH), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom };
            UIHelper.StyleDataGridView(dgv);
            panelContent.Controls.Add(dgv);

            txtSearch = UIHelper.CreateSearchBox(280);
            txtSearch.Location = new Point(10, 12);
            txtSearch.PlaceholderText = searchPlaceholder;
            panelContent.Controls.Add(txtSearch);

            btnAdd = UIHelper.CreateButton("+ Thêm mới", Color.FromArgb(39, 174, 96), Color.White, 120, 35);
            btnAdd.Location = new Point(w - 420, 10); btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panelContent.Controls.Add(btnAdd);

            btnEdit = UIHelper.CreateButton("Sửa", Color.FromArgb(41, 128, 185), Color.White, 80, 35);
            btnEdit.Location = new Point(w - 295, 10); btnEdit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panelContent.Controls.Add(btnEdit);

            btnDelete = UIHelper.CreateButton("Xóa", Color.FromArgb(231, 76, 60), Color.White, 80, 35);
            btnDelete.Location = new Point(w - 210, 10); btnDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panelContent.Controls.Add(btnDelete);

            btnRefresh = UIHelper.CreateButton("Làm mới", Color.FromArgb(52, 152, 219), Color.White, 85, 35);
            btnRefresh.Location = new Point(w - 125, 10); btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panelContent.Controls.Add(btnRefresh);

            txtSearch.BringToFront(); btnAdd.BringToFront(); btnEdit.BringToFront(); btnDelete.BringToFront(); btnRefresh.BringToFront();
        }
    }
}
