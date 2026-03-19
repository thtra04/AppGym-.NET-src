using AppGym.DataAccess;
using AppGym.Helpers;
using AppGym.Models;

namespace AppGym.Forms
{
    public partial class FormHocVienDetail : Form
    {
        private HocVien? _hv;

        public FormHocVienDetail(HocVien? hv)
        {
            _hv = hv;
            InitializeComponent();
            Text = _hv == null ? "Thêm Học viên" : "Sửa Học viên";
            if (hv != null) LoadData(hv);
        }

        private void LoadData(HocVien hv)
        {
            txtHoTen.Text = hv.HoTen;
            cboGioiTinh.SelectedItem = hv.GioiTinh;
            if (hv.NgaySinh.HasValue) dtpNgaySinh.Value = hv.NgaySinh.Value;
            txtSDT.Text = hv.SDT;
            txtEmail.Text = hv.Email;
            if (hv.NgayDangKy.HasValue) dtpNgayDangKy.Value = hv.NgayDangKy.Value;
            chkTrangThai.Checked = hv.TrangThai;
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var hv = _hv ?? new HocVien();
            hv.HoTen = txtHoTen.Text.Trim();
            hv.GioiTinh = cboGioiTinh.SelectedItem?.ToString() ?? "Nam";
            hv.NgaySinh = dtpNgaySinh.Value.Date;
            hv.SDT = txtSDT.Text.Trim();
            hv.Email = txtEmail.Text.Trim();
            hv.NgayDangKy = dtpNgayDangKy.Value.Date;
            hv.TrangThai = chkTrangThai.Checked;

            var dao = new HocVienDAO();
            try
            {
                bool ok = _hv == null ? dao.Insert(hv) : dao.Update(hv);
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
