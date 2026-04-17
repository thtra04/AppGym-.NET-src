using AppGym.DataAccess;
using AppGym.Helpers;
using AppGym.Models;

namespace AppGym.Forms
{
    public partial class FormPhongTapDetail : Form
    {
        private PhongTap? _phongTap;

        public FormPhongTapDetail(PhongTap? pt)
        {
            _phongTap = pt;
            InitializeComponent();
            Text = _phongTap == null ? "Thêm Phòng tập" : "Sửa Phòng tập";
            if (pt != null) LoadData(pt);
        }

        private void LoadData(PhongTap pt)
        {
            txtTenPhong.Text = pt.TenPhong;
            txtDiaChi.Text = pt.DiaChi;
            txtSucChua.Text = pt.SucChua?.ToString() ?? "";
            txtMoTa.Text = pt.MoTa;
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenPhong.Text))
            {
                MessageBox.Show("Vui lòng nhập tên phòng tập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtSucChua.Text) && (!int.TryParse(txtSucChua.Text, out var sc) || sc <= 0))
            {
                MessageBox.Show("Sức chứa phải là số nguyên dương!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var pt = _phongTap ?? new PhongTap();
            pt.TenPhong = txtTenPhong.Text.Trim();
            pt.DiaChi = txtDiaChi.Text.Trim();
            pt.SucChua = int.TryParse(txtSucChua.Text, out var sucChua) ? sucChua : null;
            pt.MoTa = txtMoTa.Text.Trim();

            var dao = new PhongTapDAO();
            try
            {
                bool ok = _phongTap == null ? dao.Insert(pt) : dao.Update(pt);
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
