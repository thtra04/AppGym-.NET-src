using AppGym.DataAccess;
using AppGym.Helpers;
using AppGym.Models;

namespace AppGym.Forms
{
    public partial class FormPhanCongDetail : Form
    {
        private PhanCong? _pc;
        private CheckBox? _chkOnlyUnassigned;
        private Label? _lblFilterHint;
        private List<DangKyGoi> _allDangKy = new();
        private List<DangKyGoi> _unassignedDangKy = new();
        private bool _suppressDateSync;

        public FormPhanCongDetail(PhanCong? pc)
        {
            _pc = pc;
            InitializeComponent();
            Text = (_pc == null || _pc.MaPC == 0) ? "Thêm Phân công" : "Sửa Phân công";
            AddUnassignedFilter();
            LoadCombos();
            cboDangKy.SelectedIndexChanged += CboDangKy_SelectedIndexChanged;
            if (pc != null && pc.MaPC != 0)
            {
                LoadData(pc);
            }
            else
            {
                // New assignment: pre-select MaDK if caller provided one, then auto-fill dates from registration.
                if (pc != null && pc.MaDK != 0)
                {
                    try { cboDangKy.SelectedValue = pc.MaDK; } catch { }
                }
                CboDangKy_SelectedIndexChanged(cboDangKy, EventArgs.Empty);
            }
        }

        private void AddUnassignedFilter()
        {
            _chkOnlyUnassigned = new CheckBox
            {
                Text = "Chỉ hiển thị đăng ký chưa có PT",
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F),
                Location = new Point(20, 60),
                Checked = (_pc == null || _pc.MaPC == 0)
            };
            _chkOnlyUnassigned.CheckedChanged += (_, _) => RefreshDangKyCombo();
            Controls.Add(_chkOnlyUnassigned);
            _chkOnlyUnassigned.BringToFront();

            _lblFilterHint = new Label
            {
                Text = "Chỉ hiện những đăng ký chưa được gán PT nào (mỗi đăng ký tối đa 1 PT).",
                AutoSize = false,
                Size = new Size(470, 18),
                Location = new Point(20, 84),
                Font = new Font("Segoe UI", 8.5F, FontStyle.Italic),
                ForeColor = Color.FromArgb(120, 130, 148)
            };
            Controls.Add(_lblFilterHint);
            _lblFilterHint.BringToFront();
        }

        private void CboDangKy_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_suppressDateSync) return;
            if (cboDangKy.SelectedItem is not DangKyGoi dk) return;
            // Auto-fill the assignment dates from the selected registration so the PT contract
            // spans the student's actual membership period by default.
            if (dk.NgayBatDau.HasValue) dtpBatDau.Value = ClampToPickerRange(dk.NgayBatDau.Value);
            if (dk.NgayHetHan.HasValue) dtpKetThuc.Value = ClampToPickerRange(dk.NgayHetHan.Value);
        }

        private static DateTime ClampToPickerRange(DateTime d)
        {
            if (d < DateTimePicker.MinimumDateTime) return DateTimePicker.MinimumDateTime;
            if (d > DateTimePicker.MaximumDateTime) return DateTimePicker.MaximumDateTime;
            return d;
        }

        private void LoadCombos()
        {
            try
            {
                var hlvList = new HuanLuyenVienDAO().GetAll();
                cboHLV.DisplayMember = "HoTen";
                cboHLV.ValueMember = "MaHLV";
                cboHLV.DataSource = hlvList;

                _allDangKy = new DangKyGoiDAO().GetAll();
                _unassignedDangKy = new PhanCongDAO().GetDangKyChuaPhanCong();
                RefreshDangKyCombo();

                var caList = new CaLamDAO().GetAll();
                cboCaLam.DisplayMember = "TenCa";
                cboCaLam.ValueMember = "MaCa";
                cboCaLam.DataSource = caList;
            }
            catch { }
        }

        private void RefreshDangKyCombo()
        {
            bool onlyUnassigned = _chkOnlyUnassigned?.Checked == true;
            List<DangKyGoi> source;
            if (onlyUnassigned)
            {
                source = _unassignedDangKy.ToList();
                if (_pc != null && !source.Any(d => d.MaDK == _pc.MaDK))
                {
                    var current = _allDangKy.FirstOrDefault(d => d.MaDK == _pc.MaDK);
                    if (current != null) source.Insert(0, current);
                }
            }
            else
            {
                source = _allDangKy.ToList();
            }

            _suppressDateSync = true;
            try
            {
                cboDangKy.DisplayMember = "TenHV";
                cboDangKy.ValueMember = "MaDK";
                cboDangKy.DataSource = source;
                if (_pc != null) cboDangKy.SelectedValue = _pc.MaDK;
            }
            finally { _suppressDateSync = false; }
        }

        private void LoadData(PhanCong pc)
        {
            _suppressDateSync = true;
            try
            {
                cboHLV.SelectedValue = pc.MaHLV;
                cboDangKy.SelectedValue = pc.MaDK;
                if (pc.MaCa.HasValue) cboCaLam.SelectedValue = pc.MaCa.Value;
                if (pc.NgayBatDau.HasValue) dtpBatDau.Value = ClampToPickerRange(pc.NgayBatDau.Value);
                if (pc.NgayKetThuc.HasValue) dtpKetThuc.Value = ClampToPickerRange(pc.NgayKetThuc.Value);
                txtGhiChu.Text = pc.GhiChu;
            }
            finally { _suppressDateSync = false; }
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

            if (pc.NgayKetThuc < pc.NgayBatDau)
            {
                MessageBox.Show("Ngày kết thúc phải sau ngày bắt đầu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dao = new PhanCongDAO();

            // Mỗi đăng ký gói chỉ có tối đa 1 PT.
            if (dao.ExistsForDangKy(pc.MaDK, _pc?.MaPC))
            {
                MessageBox.Show(
                    "Đăng ký này đã được phân công một PT. Mỗi học viên/gói tập chỉ được phân công 1 PT.",
                    "Không thể phân công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool ok = (_pc == null || _pc.MaPC == 0) ? dao.Insert(pc) : dao.Update(pc);
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
