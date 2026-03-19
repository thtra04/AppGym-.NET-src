namespace AppGym.Forms
{
    partial class FormHocVienDetail
    {
        private System.ComponentModel.IContainer components = null;

        private TextBox txtHoTen;
        private ComboBox cboGioiTinh;
        private DateTimePicker dtpNgaySinh;
        private TextBox txtSDT;
        private TextBox txtEmail;
        private DateTimePicker dtpNgayDangKy;
        private CheckBox chkTrangThai;
        private Label lblHoTen;
        private Label lblGioiTinh;
        private Label lblNgaySinh;
        private Label lblSDT;
        private Label lblEmail;
        private Label lblNgayDangKy;
        private Label lblTrangThai;
        private Button btnSave;
        private Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtHoTen = new TextBox();
            this.cboGioiTinh = new ComboBox();
            this.dtpNgaySinh = new DateTimePicker();
            this.txtSDT = new TextBox();
            this.txtEmail = new TextBox();
            this.dtpNgayDangKy = new DateTimePicker();
            this.chkTrangThai = new CheckBox();
            this.lblHoTen = new Label();
            this.lblGioiTinh = new Label();
            this.lblNgaySinh = new Label();
            this.lblSDT = new Label();
            this.lblEmail = new Label();
            this.lblNgayDangKy = new Label();
            this.lblTrangThai = new Label();
            this.btnSave = new Button();
            this.btnCancel = new Button();
            this.SuspendLayout();

            // lblHoTen
            this.lblHoTen.AutoSize = true;
            this.lblHoTen.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblHoTen.Location = new Point(20, 23);
            this.lblHoTen.Text = "Họ tên:";

            // txtHoTen
            this.txtHoTen.BorderStyle = BorderStyle.FixedSingle;
            this.txtHoTen.Font = new Font("Segoe UI", 10F);
            this.txtHoTen.Location = new Point(160, 20);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new Size(290, 27);

            // lblGioiTinh
            this.lblGioiTinh.AutoSize = true;
            this.lblGioiTinh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblGioiTinh.Location = new Point(20, 75);
            this.lblGioiTinh.Text = "Giới tính:";

            // cboGioiTinh
            this.cboGioiTinh.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboGioiTinh.Font = new Font("Segoe UI", 10F);
            this.cboGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });
            this.cboGioiTinh.Location = new Point(160, 72);
            this.cboGioiTinh.Name = "cboGioiTinh";
            this.cboGioiTinh.Size = new Size(290, 27);
            this.cboGioiTinh.SelectedIndex = 0;

            // lblNgaySinh
            this.lblNgaySinh.AutoSize = true;
            this.lblNgaySinh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblNgaySinh.Location = new Point(20, 127);
            this.lblNgaySinh.Text = "Ngày sinh:";

            // dtpNgaySinh
            this.dtpNgaySinh.Font = new Font("Segoe UI", 10F);
            this.dtpNgaySinh.Format = DateTimePickerFormat.Short;
            this.dtpNgaySinh.Location = new Point(160, 124);
            this.dtpNgaySinh.Name = "dtpNgaySinh";
            this.dtpNgaySinh.Size = new Size(290, 27);

            // lblSDT
            this.lblSDT.AutoSize = true;
            this.lblSDT.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblSDT.Location = new Point(20, 179);
            this.lblSDT.Text = "SĐT:";

            // txtSDT
            this.txtSDT.BorderStyle = BorderStyle.FixedSingle;
            this.txtSDT.Font = new Font("Segoe UI", 10F);
            this.txtSDT.Location = new Point(160, 176);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new Size(290, 27);

            // lblEmail
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblEmail.Location = new Point(20, 231);
            this.lblEmail.Text = "Email:";

            // txtEmail
            this.txtEmail.BorderStyle = BorderStyle.FixedSingle;
            this.txtEmail.Font = new Font("Segoe UI", 10F);
            this.txtEmail.Location = new Point(160, 228);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new Size(290, 27);

            // lblNgayDangKy
            this.lblNgayDangKy.AutoSize = true;
            this.lblNgayDangKy.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblNgayDangKy.Location = new Point(20, 283);
            this.lblNgayDangKy.Text = "Ngày ĐK:";

            // dtpNgayDangKy
            this.dtpNgayDangKy.Font = new Font("Segoe UI", 10F);
            this.dtpNgayDangKy.Format = DateTimePickerFormat.Short;
            this.dtpNgayDangKy.Location = new Point(160, 280);
            this.dtpNgayDangKy.Name = "dtpNgayDangKy";
            this.dtpNgayDangKy.Size = new Size(290, 27);

            // lblTrangThai
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblTrangThai.Location = new Point(20, 335);
            this.lblTrangThai.Text = "Trạng thái:";

            // chkTrangThai
            this.chkTrangThai.AutoSize = true;
            this.chkTrangThai.Checked = true;
            this.chkTrangThai.CheckState = CheckState.Checked;
            this.chkTrangThai.Font = new Font("Segoe UI", 10F);
            this.chkTrangThai.Location = new Point(160, 333);
            this.chkTrangThai.Name = "chkTrangThai";
            this.chkTrangThai.Text = "Hoạt động";

            // btnSave
            this.btnSave.BackColor = Color.FromArgb(39, 174, 96);
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnSave.ForeColor = Color.White;
            this.btnSave.Location = new Point(105, 385);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(130, 40);
            this.btnSave.Text = "Lưu";
            this.btnSave.Click += new EventHandler(this.BtnSave_Click);

            // btnCancel
            this.btnCancel.BackColor = Color.FromArgb(231, 76, 60);
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnCancel.ForeColor = Color.White;
            this.btnCancel.Location = new Point(255, 385);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(130, 40);
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += new EventHandler(this.BtnCancel_Click);

            // FormHocVienDetail
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(245, 246, 250);
            this.ClientSize = new Size(490, 450);
            this.Controls.Add(this.lblHoTen);
            this.Controls.Add(this.txtHoTen);
            this.Controls.Add(this.lblGioiTinh);
            this.Controls.Add(this.cboGioiTinh);
            this.Controls.Add(this.lblNgaySinh);
            this.Controls.Add(this.dtpNgaySinh);
            this.Controls.Add(this.lblSDT);
            this.Controls.Add(this.txtSDT);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblNgayDangKy);
            this.Controls.Add(this.dtpNgayDangKy);
            this.Controls.Add(this.lblTrangThai);
            this.Controls.Add(this.chkTrangThai);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormHocVienDetail";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Học viên";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
