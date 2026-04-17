using AppGym.DataAccess;
using AppGym.Helpers;
using AppGym.Models;

namespace AppGym.Forms
{
    public partial class FormHLVDetail : Form
    {
        private HuanLuyenVien? _hlv;

        public FormHLVDetail(HuanLuyenVien? hlv)
        {
            _hlv = hlv;
            InitializeComponent();
            Text = _hlv == null ? "Thêm Huấn luyện viên" : "Sửa Huấn luyện viên";
            if (hlv != null) LoadData(hlv);
        }

        private void LoadData(HuanLuyenVien hlv)
        {
            txtHoTen.Text = hlv.HoTen;
            cboGioiTinh.SelectedItem = hlv.GioiTinh;
            txtSDT.Text = hlv.SDT;
            txtChuyenMon.Text = hlv.ChuyenMon;
            txtLuong.Text = hlv.Luong?.ToString() ?? "";
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var hlv = _hlv ?? new HuanLuyenVien();
            hlv.HoTen = txtHoTen.Text.Trim();
            hlv.GioiTinh = cboGioiTinh.SelectedItem?.ToString() ?? "Nam";
            hlv.SDT = txtSDT.Text.Trim();
            hlv.ChuyenMon = txtChuyenMon.Text.Trim();
            hlv.Luong = decimal.TryParse(txtLuong.Text, out var l) ? l : null;

            var dao = new HuanLuyenVienDAO();
            try
            {
                bool ok = _hlv == null ? dao.Insert(hlv) : dao.Update(hlv);
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

        private void lblHoTen_Click(object sender, EventArgs e)
        {

        }
    }
}
