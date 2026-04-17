using AppGym.DataAccess;
using AppGym.Helpers;
using AppGym.Models;

namespace AppGym.Forms
{
    public partial class FormGoiTapDetail : Form
    {
        private GoiTap? _goiTap;

        public FormGoiTapDetail(GoiTap? gt)
        {
            _goiTap = gt;
            InitializeComponent();
            Text = _goiTap == null ? "Thêm Gói tập" : "Sửa Gói tập";
            if (gt != null) LoadData(gt);
        }

        private void LoadData(GoiTap gt)
        {
            txtTenGoi.Text = gt.TenGoi;
            txtThoiHan.Text = gt.ThoiHan?.ToString() ?? "";
            txtGia.Text = gt.Gia?.ToString() ?? "";
            txtMoTa.Text = gt.MoTa;
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenGoi.Text))
            {
                MessageBox.Show("Vui lòng nhập tên gói!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var gt = _goiTap ?? new GoiTap();
            gt.TenGoi = txtTenGoi.Text.Trim();
            gt.ThoiHan = int.TryParse(txtThoiHan.Text, out var th) ? th : null;
            gt.Gia = decimal.TryParse(txtGia.Text, out var g) ? g : null;
            gt.MoTa = txtMoTa.Text.Trim();

            var dao = new GoiTapDAO();
            try
            {
                bool ok = _goiTap == null ? dao.Insert(gt) : dao.Update(gt);
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
