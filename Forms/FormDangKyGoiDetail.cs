using AppGym.DataAccess;
using AppGym.Models;

namespace AppGym.Forms
{
    public partial class FormDangKyGoiDetail : Form
    {
        private const int PlaceholderMaGoi = 0;
        private readonly DangKyGoi? _dangKy;
        private readonly TaiKhoan _currentUser;
        private readonly List<GoiTap> _goiTapList = new();
        // True when the end-date has been auto-suggested and the user hasn't manually overridden it yet.
        private bool _hetHanIsAutoSuggested = true;
        private bool _suppressHetHanChanged;

        public FormDangKyGoiDetail(DangKyGoi? dangKy, TaiKhoan currentUser)
        {
            _dangKy = dangKy;
            _currentUser = currentUser;
            InitializeComponent();
            Text = _dangKy == null ? "Thêm đăng ký gói" : "Sửa đăng ký gói";
            LoadCombos();
            if (_dangKy != null)
            {
                LoadData(_dangKy);
                // Existing record: respect stored end date as user-set value.
                _hetHanIsAutoSuggested = false;
            }

            cboGoiTap.SelectedIndexChanged += (_, _) =>
            {
                SuggestNgayHetHan();
                UpdateThanhToanInfo();
            };
            // Re-suggest only when end-date hasn't been touched by the user yet.
            dtpBatDau.ValueChanged += (_, _) => SuggestNgayHetHan();
            dtpHetHan.ValueChanged += (_, _) =>
            {
                if (_suppressHetHanChanged) return;
                _hetHanIsAutoSuggested = false;
            };
            UpdateThanhToanInfo();
        }

        private void LoadCombos()
        {
            try
            {
                var hocVienList = new HocVienDAO().GetAll();
                cboHocVien.DisplayMember = "HoTen";
                cboHocVien.ValueMember = "MaHV";
                cboHocVien.DataSource = hocVienList;

                _goiTapList.Clear();
                _goiTapList.Add(new GoiTap
                {
                    MaGoi = PlaceholderMaGoi,
                    TenGoi = "Chọn gói tập"
                });
                _goiTapList.AddRange(new GoiTapDAO().GetAll());
                cboGoiTap.DisplayMember = "TenGoi";
                cboGoiTap.ValueMember = "MaGoi";
                cboGoiTap.DataSource = _goiTapList;
                if (_dangKy == null) cboGoiTap.SelectedIndex = 0;

                var accountOptions = new TaiKhoanDAO().GetAll()
                    .Select(x => new
                    {
                        x.MaTK,
                        Display = $"{x.HoTen} ({x.TenDangNhap}){(x.HoatDong ? "" : " - tạm khóa")}"
                    })
                    .ToList();
                cboNguoiLap.DisplayMember = "Display";
                cboNguoiLap.ValueMember = "MaTK";
                cboNguoiLap.DataSource = accountOptions;
                cboNguoiLap.SelectedValue = _dangKy?.MaNguoiLap ?? _currentUser.MaTK;
            }
            catch
            {
                // Let the save action surface real DB errors when needed.
            }
        }

        private void LoadData(DangKyGoi dangKy)
        {
            cboHocVien.SelectedValue = dangKy.MaHV;
            cboGoiTap.SelectedValue = dangKy.MaGoi;
            if (dangKy.MaNguoiLap.HasValue)
            {
                cboNguoiLap.SelectedValue = dangKy.MaNguoiLap.Value;
            }

            if (dangKy.NgayBatDau.HasValue) dtpBatDau.Value = dangKy.NgayBatDau.Value;
            if (dangKy.NgayHetHan.HasValue) dtpHetHan.Value = dangKy.NgayHetHan.Value;
            txtGhiChu.Text = dangKy.GhiChu;
            UpdateThanhToanInfo();
        }

        private void SuggestNgayHetHan()
        {
            // Only auto-fill end-date when the user hasn't taken control of it yet.
            if (!_hetHanIsAutoSuggested) return;
            var selectedGoi = GetSelectedGoiTap();
            if (selectedGoi?.ThoiHan.HasValue == true)
            {
                _suppressHetHanChanged = true;
                dtpHetHan.Value = dtpBatDau.Value.Date.AddDays(selectedGoi.ThoiHan.Value);
                _suppressHetHanChanged = false;
            }
        }

        private void UpdateThanhToanInfo()
        {
            var giaGoi = 0m;
            var selectedGoi = GetSelectedGoiTap();
            if (selectedGoi != null)
            {
                giaGoi = selectedGoi.Gia ?? 0;
            }

            var daThanhToan = _dangKy?.DaThanhToan ?? 0m;
            var conThieu = Math.Max(giaGoi - daThanhToan, 0);
            var trangThai = selectedGoi == null
                ? "Chưa chọn gói tập"
                : daThanhToan <= 0
                ? "Chưa thanh toán"
                : conThieu > 0
                    ? $"Đã thanh toán {FormatCurrency(daThanhToan)}"
                    : "Đã thanh toán đủ";

            lblGiaGoiValue.Text = FormatCurrency(giaGoi);
            lblDaThanhToanValue.Text = FormatCurrency(daThanhToan);
            lblTrangThaiValue.Text = selectedGoi != null && conThieu > 0 && daThanhToan > 0
                ? $"{trangThai} - còn thiếu {FormatCurrency(conThieu)}"
                : trangThai;
            lblTrangThaiValue.ForeColor = selectedGoi == null
                ? Color.FromArgb(127, 140, 141)
                : conThieu <= 0 && daThanhToan > 0
                    ? Color.FromArgb(39, 174, 96)
                : daThanhToan > 0
                    ? Color.FromArgb(243, 156, 18)
                    : Color.FromArgb(231, 76, 60);
        }

        private static string FormatCurrency(decimal value) => value.ToString("#,##0") + " VNĐ";

        private GoiTap? GetSelectedGoiTap()
        {
            return cboGoiTap.SelectedItem is GoiTap selectedGoi && selectedGoi.MaGoi != PlaceholderMaGoi
                ? selectedGoi
                : null;
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            var selectedGoi = GetSelectedGoiTap();
            if (cboHocVien.SelectedValue == null || selectedGoi == null || cboNguoiLap.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dangKy = _dangKy ?? new DangKyGoi();
            dangKy.MaHV = (int)cboHocVien.SelectedValue;
            dangKy.MaGoi = selectedGoi.MaGoi;
            dangKy.MaNguoiLap = (int)cboNguoiLap.SelectedValue;
            dangKy.NgayBatDau = dtpBatDau.Value.Date;
            dangKy.NgayHetHan = dtpHetHan.Value.Date;

            if (dangKy.NgayHetHan < dangKy.NgayBatDau)
            {
                MessageBox.Show("Ngày hết hạn phải sau ngày bắt đầu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dangKy.GhiChu = txtGhiChu.Text.Trim();

            var dao = new DangKyGoiDAO();
            try
            {
                bool ok = _dangKy == null ? dao.Insert(dangKy) : dao.Update(dangKy);
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
