using AppGym.DataAccess;
using AppGym.Models;

namespace AppGym.Forms
{
    public partial class FormHoaDonDetail : Form
    {
        private readonly HoaDon? _hoaDon;
        private readonly TaiKhoan _currentUser;
        private readonly int? _preselectedMaDK;
        private List<DangKyGoi> _dangKyList = new();
        private List<GoiTap> _goiList = new();
        private Dictionary<int, DangKyGoi> _dangKyMap = new();

        public FormHoaDonDetail(HoaDon? hoaDon, TaiKhoan currentUser, int? preselectedMaDK = null)
        {
            _hoaDon = hoaDon;
            _currentUser = currentUser;
            _preselectedMaDK = preselectedMaDK;
            InitializeComponent();
            Text = _hoaDon == null ? "Thêm hóa đơn" : "Sửa hóa đơn";
            LoadCombos();
            if (_hoaDon != null)
            {
                LoadData(_hoaDon);
            }

            cboDangKy.SelectedIndexChanged += CboDangKy_SelectedIndexChanged;
        }

        private void CboDangKy_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_hoaDon != null) return;
            if (cboDangKy.SelectedValue == null) return;

            if (int.TryParse(cboDangKy.SelectedValue.ToString(), out var maDK) && _dangKyMap.TryGetValue(maDK, out var dangKy))
            {
                var goi = _goiList.FirstOrDefault(x => x.MaGoi == dangKy.MaGoi);
                if (goi?.Gia != null)
                {
                    txtSoTien.Text = goi.Gia.Value.ToString("0");
                }
            }
        }

        private void LoadCombos()
        {
            try
            {
                _dangKyList = new DangKyGoiDAO().GetAll();
                _goiList = new GoiTapDAO().GetAll();
                _dangKyMap = _dangKyList.ToDictionary(x => x.MaDK, x => x);

                var dangKyDisplayList = _dangKyList.Select(dangKy => new
                {
                    dangKy.MaDK,
                    Display = $"{dangKy.TenHV} - {dangKy.TenGoi} ({(dangKy.NgayBatDau?.ToString("dd/MM/yyyy") ?? "N/A")} -> {(dangKy.NgayHetHan?.ToString("dd/MM/yyyy") ?? "N/A")})"
                }).ToList();

                cboDangKy.DisplayMember = "Display";
                cboDangKy.ValueMember = "MaDK";
                cboDangKy.DataSource = dangKyDisplayList;

                var accountOptions = new TaiKhoanDAO().GetAll()
                    .Select(x => new
                    {
                        x.MaTK,
                        Display = $"{x.HoTen} ({x.TenDangNhap}){(x.HoatDong ? "" : " - tạm khóa")}"
                    })
                    .ToList();

                cboNguoiLap.DisplayMember = "Display";
                cboNguoiLap.ValueMember = "MaTK";
                cboNguoiLap.DataSource = accountOptions.ToList();
                cboNguoiThanhToan.DisplayMember = "Display";
                cboNguoiThanhToan.ValueMember = "MaTK";
                cboNguoiThanhToan.DataSource = accountOptions.ToList();

                if (_hoaDon == null)
                {
                    cboNguoiLap.SelectedValue = _currentUser.MaTK;
                    cboNguoiThanhToan.SelectedValue = _currentUser.MaTK;
                    if (_preselectedMaDK.HasValue)
                    {
                        cboDangKy.SelectedValue = _preselectedMaDK.Value;
                    }
                }
            }
            catch
            {
                // Save action will show the concrete error if the DB is unavailable.
            }
        }

        private void LoadData(HoaDon hoaDon)
        {
            cboDangKy.SelectedValue = hoaDon.MaDK;
            if (hoaDon.NgayThanhToan.HasValue) dtpNgayTT.Value = hoaDon.NgayThanhToan.Value;
            txtSoTien.Text = hoaDon.SoTien?.ToString() ?? "";

            if (!string.IsNullOrWhiteSpace(hoaDon.HinhThucTT) && cboHinhThuc.Items.Contains(hoaDon.HinhThucTT))
            {
                cboHinhThuc.SelectedItem = hoaDon.HinhThucTT;
            }

            if (hoaDon.MaNguoiLap.HasValue)
            {
                cboNguoiLap.SelectedValue = hoaDon.MaNguoiLap.Value;
            }

            if (hoaDon.MaNguoiThanhToan.HasValue)
            {
                cboNguoiThanhToan.SelectedValue = hoaDon.MaNguoiThanhToan.Value;
            }

            txtGhiChu.Text = hoaDon.GhiChu;
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (cboDangKy.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn đăng ký gói!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboNguoiLap.SelectedValue == null || cboNguoiThanhToan.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn đủ người thao tác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtSoTien.Text, out var soTien) || soTien <= 0)
            {
                MessageBox.Show("Số tiền phải lớn hơn 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var hinhThuc = cboHinhThuc.SelectedItem?.ToString()?.Trim();
            if (string.IsNullOrWhiteSpace(hinhThuc))
            {
                MessageBox.Show("Vui lòng chọn hình thức thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var hoaDon = _hoaDon ?? new HoaDon();
            hoaDon.MaDK = (int)cboDangKy.SelectedValue;
            hoaDon.NgayThanhToan = dtpNgayTT.Value;
            hoaDon.SoTien = soTien;
            hoaDon.HinhThucTT = hinhThuc;
            hoaDon.MaNguoiLap = (int)cboNguoiLap.SelectedValue;
            hoaDon.MaNguoiThanhToan = (int)cboNguoiThanhToan.SelectedValue;
            hoaDon.GhiChu = txtGhiChu.Text.Trim();

            var dao = new HoaDonDAO();
            try
            {
                bool ok = _hoaDon == null ? dao.Insert(hoaDon) : dao.Update(hoaDon);
                if (ok)
                {
                    MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
