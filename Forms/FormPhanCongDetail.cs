using AppGym.DataAccess;
using AppGym.Helpers;
using AppGym.Models;

namespace AppGym.Forms
{
    public partial class FormPhanCongDetail : Form
    {
        private PhanCong? _pc;

        public FormPhanCongDetail(PhanCong? pc)
        {
            _pc = pc;
            InitializeComponent();
            Text = _pc == null ? "Thêm Phân công" : "Sửa Phân công";
            LoadCombos();
            if (pc != null) LoadData(pc);
        }

        private void LoadCombos()
        {
            try
            {
                var hlvList = new HuanLuyenVienDAO().GetAll();
                cboHLV.DisplayMember = "HoTen";
                cboHLV.ValueMember = "MaHLV";
                cboHLV.DataSource = hlvList;

                var dkList = new DangKyGoiDAO().GetAll();
                cboDangKy.DisplayMember = "TenHV";
                cboDangKy.ValueMember = "MaDK";
                cboDangKy.DataSource = dkList;

                var caList = new CaLamDAO().GetAll();
                cboCaLam.DisplayMember = "TenCa";
                cboCaLam.ValueMember = "MaCa";
                cboCaLam.DataSource = caList;
            }
            catch { }
        }

        private void LoadData(PhanCong pc)
        {
            cboHLV.SelectedValue = pc.MaHLV;
            cboDangKy.SelectedValue = pc.MaDK;
            if (pc.MaCa.HasValue) cboCaLam.SelectedValue = pc.MaCa.Value;
            if (pc.NgayBatDau.HasValue) dtpBatDau.Value = pc.NgayBatDau.Value;
            if (pc.NgayKetThuc.HasValue) dtpKetThuc.Value = pc.NgayKetThuc.Value;
            txtGhiChu.Text = pc.GhiChu;
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (cboHLV.SelectedValue == null || cboDangKy.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var pc = _pc ?? new PhanCong();
            pc.MaHLV = (int)cboHLV.SelectedValue;
            pc.MaDK = (int)cboDangKy.SelectedValue;
            pc.MaCa = cboCaLam.SelectedValue as int?;
            pc.NgayBatDau = dtpBatDau.Value.Date;
            pc.NgayKetThuc = dtpKetThuc.Value.Date;
            pc.GhiChu = txtGhiChu.Text.Trim();

            var dao = new PhanCongDAO();
            try
            {
                bool ok = _pc == null ? dao.Insert(pc) : dao.Update(pc);
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
