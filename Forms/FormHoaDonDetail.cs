using AppGym.DataAccess;
using AppGym.Models;

namespace AppGym.Forms
{
    public partial class FormHoaDonDetail : Form
    {
        private HoaDon? _hd;
        private List<DangKyGoi> _dkList = new();
        private List<GoiTap> _goiList = new();
        private Dictionary<int, DangKyGoi> _dkMap = new();

        public FormHoaDonDetail(HoaDon? hd)
        {
            _hd = hd;
            InitializeComponent();
            Text = _hd == null ? "Thêm Hóa đơn" : "Sửa Hóa đơn";
            LoadCombos();
            if (hd != null) LoadData(hd);

            cboDangKy.SelectedIndexChanged += CboDangKy_SelectedIndexChanged;
        }

        private void CboDangKy_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_hd != null) return; // don't auto-fill when editing
            if (cboDangKy.SelectedValue == null) return;

            if (int.TryParse(cboDangKy.SelectedValue.ToString(), out var maDK) && _dkMap.TryGetValue(maDK, out var dk))
            {
                try
                {
                    var goi = _goiList.FirstOrDefault(g => g.MaGoi == dk.MaGoi);
                    if (goi?.Gia != null)
                    {
                        txtSoTien.Text = goi.Gia.Value.ToString("0");
                    }
                }
                catch { }
            }
        }

        private void LoadCombos()
        {
            try
            {
                _dkList = new DangKyGoiDAO().GetAll();
                _goiList = new GoiTapDAO().GetAll();
                _dkMap = _dkList.ToDictionary(x => x.MaDK, x => x);

                var displayList = _dkList.Select(dk => new
                {
                    MaDK = dk.MaDK,
                    Display = $"{dk.TenHV} - {dk.TenGoi} ({(dk.NgayBatDau?.ToString("dd/MM/yyyy") ?? "N/A")} -> {(dk.NgayHetHan?.ToString("dd/MM/yyyy") ?? "N/A")})"
                }).ToList();

                cboDangKy.DisplayMember = "Display";
                cboDangKy.ValueMember = "MaDK";
                cboDangKy.DataSource = displayList;
            }
            catch { }
        }

        private void LoadData(HoaDon hd)
        {
            cboDangKy.SelectedValue = hd.MaDK;
            if (hd.NgayThanhToan.HasValue) dtpNgayTT.Value = hd.NgayThanhToan.Value;
            txtSoTien.Text = hd.SoTien?.ToString() ?? "";

            if (!string.IsNullOrWhiteSpace(hd.HinhThucTT) && cboHinhThuc.Items.Contains(hd.HinhThucTT))
            {
                cboHinhThuc.SelectedItem = hd.HinhThucTT;
            }

            txtGhiChu.Text = hd.GhiChu;
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (cboDangKy.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn đăng ký gói!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtSoTien.Text, out var soTien) || soTien <= 0)
            {
                MessageBox.Show("Số tiền phải > 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var hinhThuc = cboHinhThuc.SelectedItem?.ToString()?.Trim();
            if (string.IsNullOrWhiteSpace(hinhThuc))
            {
                MessageBox.Show("Vui lòng chọn hình thức thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var hd = _hd ?? new HoaDon();
            hd.MaDK = (int)cboDangKy.SelectedValue;
            hd.NgayThanhToan = dtpNgayTT.Value;
            hd.SoTien = soTien;
            hd.HinhThucTT = hinhThuc;
            hd.GhiChu = txtGhiChu.Text.Trim();

            var dao = new HoaDonDAO();
            try
            {
                bool ok = _hd == null ? dao.Insert(hd) : dao.Update(hd);
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
