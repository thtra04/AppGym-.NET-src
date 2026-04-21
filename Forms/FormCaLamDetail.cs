using AppGym.DataAccess;
using AppGym.Helpers;
using AppGym.Models;

namespace AppGym.Forms
{
    public partial class FormCaLamDetail : Form
    {
        private readonly CaLam? _ca;

        public FormCaLamDetail(CaLam? ca)
        {
            _ca = ca;
            InitializeComponent();
            ConfigureFormUi();
            Text = _ca == null ? "Thêm ca làm" : "Sửa ca làm";
            if (ca != null) LoadData(ca);
        }

        private void ConfigureFormUi()
        {
            AcceptButton = btnSave;
            CancelButton = btnCancel;
            btnSave.FlatAppearance.BorderSize = 0;
            btnCancel.FlatAppearance.BorderSize = 0;
            UIHelper.RoundControl(btnSave, 16);
            UIHelper.RoundControl(btnCancel, 16);
            dtpBatDau.Format = DateTimePickerFormat.Custom;
            dtpBatDau.CustomFormat = "HH:mm";
            dtpKetThuc.Format = DateTimePickerFormat.Custom;
            dtpKetThuc.CustomFormat = "HH:mm";
        }

        private void LoadData(CaLam ca)
        {
            txtTenCa.Text = ca.TenCa;
            if (ca.GioBatDau.HasValue)
                dtpBatDau.Value = DateTime.Today.Add(ca.GioBatDau.Value);
            if (ca.GioKetThuc.HasValue)
                dtpKetThuc.Value = DateTime.Today.Add(ca.GioKetThuc.Value);
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenCa.Text))
            {
                MessageBox.Show("Vui lòng nhập tên ca!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var ca = _ca ?? new CaLam();
            ca.TenCa = txtTenCa.Text.Trim();
            ca.GioBatDau = dtpBatDau.Value.TimeOfDay;
            ca.GioKetThuc = dtpKetThuc.Value.TimeOfDay;

            if (ca.GioKetThuc <= ca.GioBatDau)
            {
                MessageBox.Show("Giờ kết thúc phải sau giờ bắt đầu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dao = new CaLamDAO();
            try
            {
                bool ok = _ca == null ? dao.Insert(ca) : dao.Update(ca);
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
