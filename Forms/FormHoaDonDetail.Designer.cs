namespace AppGym.Forms
{
    partial class FormHoaDonDetail
    {
        private System.ComponentModel.IContainer components = null;

        private ComboBox cboDangKy;
        private DateTimePicker dtpNgayTT;
        private TextBox txtSoTien;
        private ComboBox cboHinhThuc;
        private TextBox txtGhiChu;
        private Label lblDangKy;
        private Label lblNgayTT;
        private Label lblSoTien;
        private Label lblHinhThuc;
        private Label lblGhiChu;
        private Button btnSave;
        private Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cboDangKy = new ComboBox();
            this.dtpNgayTT = new DateTimePicker();
            this.txtSoTien = new TextBox();
            this.cboHinhThuc = new ComboBox();
            this.txtGhiChu = new TextBox();
            this.lblDangKy = new Label();
            this.lblNgayTT = new Label();
            this.lblSoTien = new Label();
            this.lblHinhThuc = new Label();
            this.lblGhiChu = new Label();
            this.btnSave = new Button();
            this.btnCancel = new Button();
            this.SuspendLayout();

            // lblDangKy
            this.lblDangKy.AutoSize = true;
            this.lblDangKy.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblDangKy.Location = new Point(20, 23);
            this.lblDangKy.Text = "Đăng ký gói:";

            // cboDangKy
            this.cboDangKy.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboDangKy.Font = new Font("Segoe UI", 10F);
            this.cboDangKy.Location = new Point(170, 20);
            this.cboDangKy.Name = "cboDangKy";
            this.cboDangKy.Size = new Size(300, 27);

            // lblNgayTT
            this.lblNgayTT.AutoSize = true;
            this.lblNgayTT.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblNgayTT.Location = new Point(20, 75);
            this.lblNgayTT.Text = "Ngày thanh toán:";

            // dtpNgayTT
            this.dtpNgayTT.Font = new Font("Segoe UI", 10F);
            this.dtpNgayTT.Format = DateTimePickerFormat.Short;
            this.dtpNgayTT.Location = new Point(170, 72);
            this.dtpNgayTT.Name = "dtpNgayTT";
            this.dtpNgayTT.Size = new Size(300, 27);

            // lblSoTien
            this.lblSoTien.AutoSize = true;
            this.lblSoTien.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblSoTien.Location = new Point(20, 127);
            this.lblSoTien.Text = "Số tiền:";

            // txtSoTien
            this.txtSoTien.BorderStyle = BorderStyle.FixedSingle;
            this.txtSoTien.Font = new Font("Segoe UI", 10F);
            this.txtSoTien.Location = new Point(170, 124);
            this.txtSoTien.Name = "txtSoTien";
            this.txtSoTien.Size = new Size(300, 27);

            // lblHinhThuc
            this.lblHinhThuc.AutoSize = true;
            this.lblHinhThuc.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblHinhThuc.Location = new Point(20, 179);
            this.lblHinhThuc.Text = "Hình thức TT:";

            // cboHinhThuc
            this.cboHinhThuc.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboHinhThuc.Font = new Font("Segoe UI", 10F);
            this.cboHinhThuc.Items.AddRange(new object[] { "Tiền mặt", "Chuyển khoản", "Thẻ", "Khác" });
            this.cboHinhThuc.Location = new Point(170, 176);
            this.cboHinhThuc.Name = "cboHinhThuc";
            this.cboHinhThuc.Size = new Size(300, 27);
            this.cboHinhThuc.SelectedIndex = 0;

            // lblGhiChu
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblGhiChu.Location = new Point(20, 231);
            this.lblGhiChu.Text = "Ghi chú:";

            // txtGhiChu
            this.txtGhiChu.BorderStyle = BorderStyle.FixedSingle;
            this.txtGhiChu.Font = new Font("Segoe UI", 10F);
            this.txtGhiChu.Location = new Point(170, 228);
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new Size(300, 27);

            // btnSave
            this.btnSave.BackColor = Color.FromArgb(39, 174, 96);
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnSave.ForeColor = Color.White;
            this.btnSave.Location = new Point(120, 280);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(130, 40);
            this.btnSave.Text = "Lưu";
            this.btnSave.Click += new EventHandler(this.BtnSave_Click);

            // btnCancel
            this.btnCancel.BackColor = Color.FromArgb(231, 76, 60);
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnCancel.ForeColor = Color.White;
            this.btnCancel.Location = new Point(270, 280);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(130, 40);
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += new EventHandler(this.BtnCancel_Click);

            // FormHoaDonDetail
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(245, 246, 250);
            this.ClientSize = new Size(510, 345);
            this.Controls.Add(this.lblDangKy);
            this.Controls.Add(this.cboDangKy);
            this.Controls.Add(this.lblNgayTT);
            this.Controls.Add(this.dtpNgayTT);
            this.Controls.Add(this.lblSoTien);
            this.Controls.Add(this.txtSoTien);
            this.Controls.Add(this.lblHinhThuc);
            this.Controls.Add(this.cboHinhThuc);
            this.Controls.Add(this.lblGhiChu);
            this.Controls.Add(this.txtGhiChu);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormHoaDonDetail";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Hóa đơn";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
