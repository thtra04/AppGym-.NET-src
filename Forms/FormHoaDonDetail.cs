using AppGym.DataAccess;
using AppGym.Helpers;
using AppGym.Models;

namespace AppGym.Forms
{
    public partial class FormHoaDonDetail : Form
    {
        private HoaDon? _hd;

        public FormHoaDonDetail(HoaDon? hd)
        {
            _hd = hd;
            InitializeComponent();
            Text = _hd == null ? "Thêm Hóa đơn" : "Sửa Hóa đơn";
            LoadCombos();
            if (hd != null) LoadData(hd);
        }

        private void LoadCombos()
        {
            try
            {
                var dkList = new DangKyGoiDAO().GetAll();
                cboDangKy.DisplayMember = "TenHV";
                cboDangKy.ValueMember = "MaDK";
                cboDangKy.DataSource = dkList;
            }
            catch { }
        }

        private void LoadData(HoaDon hd)
        {
            cboDangKy.SelectedValue = hd.MaDK;
            if (hd.NgayThanhToan.HasValue) dtpNgayTT.Value = hd.NgayThanhToan.Value;
            txtSoTien.Text = hd.SoTien?.ToString() ?? "";
            cboHinhThuc.SelectedItem = hd.HinhThucTT;
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

            var hd = _hd ?? new HoaDon();
            hd.MaDK = (int)cboDangKy.SelectedValue;
            hd.NgayThanhToan = dtpNgayTT.Value;
            hd.SoTien = soTien;
            hd.HinhThucTT = cboHinhThuc.SelectedItem?.ToString() ?? "Tiền mặt";
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
