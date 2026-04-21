using AppGym.DataAccess;
using AppGym.Helpers;
using AppGym.Models;

namespace AppGym.Forms
{
    public class FormPhanQuyen : Form
    {
        private readonly TaiKhoanDAO _tkDao = new();
        private readonly QuyenTaiKhoanDAO _qDao = new();

        private ComboBox cboTaiKhoan = null!;
        private Panel panelMatrix = null!;
        private Label lblHint = null!;
        private Button btnSave = null!;
        private Button btnReload = null!;

        // Per-account checkbox map: module -> (View, Add, Edit, Delete)
        private readonly Dictionary<string, CheckBox[]> _checkboxes = new();
        private TaiKhoan? _selected;

        public FormPhanQuyen()
        {
            Text = "Phân quyền tài khoản";
            Size = new Size(880, 640);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.Sizable;
            MinimumSize = new Size(720, 540);
            BackColor = Color.FromArgb(245, 246, 250);
            BuildUi();
            LoadAccounts();
        }

        private void BuildUi()
        {
            var lblTitle = new Label
            {
                Text = "Phân quyền chi tiết theo tài khoản",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(34, 49, 63),
                AutoSize = true,
                Location = new Point(24, 18)
            };
            Controls.Add(lblTitle);

            lblHint = new Label
            {
                Text = "Chỉ áp dụng cho tài khoản Nhân viên. Admin có toàn quyền và Quản lý có sẵn quyền CRUD trên dữ liệu + quản lý nhân viên.",
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(100, 110, 130),
                AutoSize = true,
                Location = new Point(24, 50)
            };
            Controls.Add(lblHint);

            var lblTk = new Label
            {
                Text = "Tài khoản:",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(24, 88)
            };
            Controls.Add(lblTk);

            cboTaiKhoan = new ComboBox
            {
                Location = new Point(120, 84),
                Size = new Size(420, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F)
            };
            cboTaiKhoan.SelectedIndexChanged += (_, _) => LoadPermissionsForSelected();
            Controls.Add(cboTaiKhoan);

            btnReload = new Button
            {
                Text = "Tải lại",
                Location = new Point(560, 82),
                Size = new Size(100, 34),
                BackColor = Color.FromArgb(41, 128, 185),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold)
            };
            btnReload.FlatAppearance.BorderSize = 0;
            btnReload.Click += (_, _) => { LoadAccounts(); };
            Controls.Add(btnReload);

            panelMatrix = new Panel
            {
                Location = new Point(24, 130),
                Size = new Size(ClientSize.Width - 48, ClientSize.Height - 200),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                AutoScroll = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };
            Controls.Add(panelMatrix);

            btnSave = new Button
            {
                Text = "Lưu phân quyền",
                Size = new Size(160, 40),
                BackColor = Color.FromArgb(39, 174, 96),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Location = new Point(ClientSize.Width - 184, ClientSize.Height - 56);
            btnSave.Click += BtnSave_Click;
            Controls.Add(btnSave);

            var btnClose = new Button
            {
                Text = "Đóng",
                Size = new Size(100, 40),
                BackColor = Color.FromArgb(149, 165, 166),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Location = new Point(ClientSize.Width - 296, ClientSize.Height - 56);
            btnClose.Click += (_, _) => Close();
            Controls.Add(btnClose);

            BuildMatrix();
        }

        private void BuildMatrix()
        {
            panelMatrix.Controls.Clear();
            _checkboxes.Clear();

            int colXLabel = 16;
            int colXView = 320;
            int colXAdd = 410;
            int colXEdit = 500;
            int colXDelete = 590;
            int rowH = 38;
            int top = 14;

            void AddHeader(string text, int x)
            {
                panelMatrix.Controls.Add(new Label
                {
                    Text = text,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(52, 73, 94),
                    AutoSize = true,
                    Location = new Point(x, top)
                });
            }

            AddHeader("Bảng dữ liệu", colXLabel);
            AddHeader("Xem", colXView);
            AddHeader("Thêm", colXAdd);
            AddHeader("Sửa", colXEdit);
            AddHeader("Xóa", colXDelete);
            top += 32;

            foreach (var (code, display) in Permissions.CrudModules)
            {
                var lbl = new Label
                {
                    Text = display,
                    Font = new Font("Segoe UI", 10F),
                    AutoSize = true,
                    Location = new Point(colXLabel, top + 4)
                };
                panelMatrix.Controls.Add(lbl);

                var cbs = new CheckBox[4];
                int[] xs = { colXView, colXAdd, colXEdit, colXDelete };
                bool viewOnly = Permissions.ViewOnlyModules.Contains(code);
                for (int i = 0; i < 4; i++)
                {
                    cbs[i] = new CheckBox
                    {
                        Location = new Point(xs[i] + 8, top + 2),
                        AutoSize = true,
                        Text = "",
                        Enabled = !(viewOnly && i > 0)
                    };
                    int idx = i;
                    // When ticking add/edit/delete, ensure view is also ticked.
                    cbs[i].CheckedChanged += (_, _) =>
                    {
                        if (idx > 0 && cbs[idx].Checked) cbs[0].Checked = true;
                    };
                    panelMatrix.Controls.Add(cbs[i]);
                }
                _checkboxes[code] = cbs;
                top += rowH;
            }

            // Section: TaiKhoan permission row (only meaningful for QuanLy/NhanVien display; QuanLy is implicit)
            top += 8;
            panelMatrix.Controls.Add(new Label
            {
                Text = "Quyền đặc biệt",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                AutoSize = true,
                Location = new Point(colXLabel, top)
            });
            top += 28;
            var lblTk = new Label
            {
                Text = "Quản lý tài khoản",
                Font = new Font("Segoe UI", 10F),
                AutoSize = true,
                Location = new Point(colXLabel, top + 4)
            };
            panelMatrix.Controls.Add(lblTk);

            var tkCbs = new CheckBox[4];
            int[] tkXs = { colXView, colXAdd, colXEdit, colXDelete };
            for (int i = 0; i < 4; i++)
            {
                tkCbs[i] = new CheckBox
                {
                    Location = new Point(tkXs[i] + 8, top + 2),
                    AutoSize = true,
                    Text = ""
                };
                int idx = i;
                tkCbs[i].CheckedChanged += (_, _) =>
                {
                    if (idx > 0 && tkCbs[idx].Checked) tkCbs[0].Checked = true;
                };
                panelMatrix.Controls.Add(tkCbs[i]);
            }
            _checkboxes[Permissions.TaiKhoan] = tkCbs;
        }

        private void LoadAccounts()
        {
            try
            {
                var all = _tkDao.GetAll()
                    .Where(t => string.Equals(t.VaiTro, "NhanVien", StringComparison.OrdinalIgnoreCase))
                    .Select(t => new
                    {
                        t.MaTK,
                        Display = $"{t.HoTen} ({t.TenDangNhap}){(t.HoatDong ? "" : " - tạm khóa")}",
                        Account = t
                    })
                    .ToList();

                cboTaiKhoan.DisplayMember = "Display";
                cboTaiKhoan.ValueMember = "MaTK";
                cboTaiKhoan.DataSource = all;

                if (all.Count == 0)
                {
                    lblHint.Text = "Chưa có tài khoản Nhân viên nào để phân quyền. Admin/Quản lý có quyền ngầm định.";
                    btnSave.Enabled = false;
                }
                else
                {
                    btnSave.Enabled = true;
                    LoadPermissionsForSelected();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách tài khoản: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPermissionsForSelected()
        {
            ResetCheckboxes();
            if (cboTaiKhoan.SelectedItem == null) return;

            var dynItem = cboTaiKhoan.SelectedItem;
            var tkProp = dynItem.GetType().GetProperty("Account");
            _selected = tkProp?.GetValue(dynItem) as TaiKhoan;
            if (_selected == null) return;

            var grants = _qDao.GetByTaiKhoan(_selected.MaTK)
                .ToDictionary(g => g.Module, StringComparer.OrdinalIgnoreCase);

            foreach (var (module, cbs) in _checkboxes)
            {
                if (grants.TryGetValue(module, out var g))
                {
                    cbs[0].Checked = g.CanView;
                    cbs[1].Checked = g.CanAdd;
                    cbs[2].Checked = g.CanEdit;
                    cbs[3].Checked = g.CanDelete;
                }
            }

            // QuanLy implicitly has TaiKhoan permissions — make those checks read-only true.
            bool isQuanLy = string.Equals(_selected.VaiTro, "QuanLy", StringComparison.OrdinalIgnoreCase);
            if (_checkboxes.TryGetValue(Permissions.TaiKhoan, out var tkCbs))
            {
                foreach (var c in tkCbs)
                {
                    c.Enabled = !isQuanLy;
                    if (isQuanLy) c.Checked = true;
                }
            }
        }

        private void ResetCheckboxes()
        {
            foreach (var cbs in _checkboxes.Values)
            {
                foreach (var c in cbs)
                {
                    c.Enabled = true;
                    c.Checked = false;
                }
            }
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (_selected == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản.", "Thông báo");
                return;
            }

            var list = new List<QuyenTaiKhoan>();
            foreach (var (module, cbs) in _checkboxes)
            {
                // Skip persisting TaiKhoan row if account is QuanLy (implicit)
                if (string.Equals(module, Permissions.TaiKhoan, StringComparison.OrdinalIgnoreCase)
                    && string.Equals(_selected.VaiTro, "QuanLy", StringComparison.OrdinalIgnoreCase))
                    continue;

                list.Add(new QuyenTaiKhoan
                {
                    MaTK = _selected.MaTK,
                    Module = module,
                    CanView = cbs[0].Checked,
                    CanAdd = cbs[1].Checked,
                    CanEdit = cbs[2].Checked,
                    CanDelete = cbs[3].Checked
                });
            }

            try
            {
                _qDao.SaveAll(_selected.MaTK, list);
                MessageBox.Show("Đã lưu phân quyền cho tài khoản " + _selected.TenDangNhap,
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
