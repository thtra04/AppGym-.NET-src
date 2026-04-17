using AppGym.DataAccess;
using AppGym.Helpers;
using AppGym.Models;

namespace AppGym.Forms
{
    public partial class FormDangKyGoiDetail : Form
    {
        private DangKyGoi? _dk;
        private List<GoiTap> _goiTapList = new();

        public FormDangKyGoiDetail(DangKyGoi? dk)
        {
            _dk = dk;
            InitializeComponent();
            Text = _dk == null ? "Thêm Đăng ký gói" : "Sửa Đăng ký gói";
            LoadCombos();
            if (dk != null)
            {
                LoadData(dk);
            }

            cboGoiTap.SelectedIndexChanged += (s, e) => UpdateNgayHetHan();
            dtpBatDau.ValueChanged += (s, e) => UpdateNgayHetHan();
        }

        private void LoadCombos()
        {
            try
            {
                var hvList = new HocVienDAO().GetAll();
                cboHocVien.DisplayMember = "HoTen";
                cboHocVien.ValueMember = "MaHV";
                cboHocVien.DataSource = hvList;

                _goiTapList = new GoiTapDAO().GetAll();
                cboGoiTap.DisplayMember = "TenGoi";
                cboGoiTap.ValueMember = "MaGoi";
                cboGoiTap.DataSource = _goiTapList;
            }
            catch { }
        }

        private void LoadData(DangKyGoi dk)
        {
            cboHocVien.SelectedValue = dk.MaHV;
            cboGoiTap.SelectedValue = dk.MaGoi;
            if (dk.NgayBatDau.HasValue) dtpBatDau.Value = dk.NgayBatDau.Value;
            if (dk.NgayHetHan.HasValue) dtpHetHan.Value = dk.NgayHetHan.Value;
            txtGhiChu.Text = dk.GhiChu;
        }

        private void UpdateNgayHetHan()
        {
            if (cboGoiTap.SelectedItem is GoiTap selectedGoi && selectedGoi.ThoiHan.HasValue)
            {
                dtpHetHan.Value = dtpBatDau.Value.Date.AddDays(selectedGoi.ThoiHan.Value);
            }
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (cboHocVien.SelectedValue == null || cboGoiTap.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dk = _dk ?? new DangKyGoi();
            dk.MaHV = (int)cboHocVien.SelectedValue;
            dk.MaGoi = (int)cboGoiTap.SelectedValue;
            dk.NgayBatDau = dtpBatDau.Value.Date;
            dk.NgayHetHan = dtpHetHan.Value.Date;

            if (dk.NgayHetHan < dk.NgayBatDau)
            {
                MessageBox.Show("Ngày hết hạn phải sau ngày bắt đầu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dk.GhiChu = txtGhiChu.Text.Trim();

            var dao = new DangKyGoiDAO();
            try
            {
                bool ok = _dk == null ? dao.Insert(dk) : dao.Update(dk);
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
