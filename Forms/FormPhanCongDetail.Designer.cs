namespace AppGym.Forms
{
    partial class FormPhanCongDetail
    {
        private System.ComponentModel.IContainer components = null;

        private ComboBox cboHLV;
        private ComboBox cboDangKy;
        private ComboBox cboCaLam;
        private DateTimePicker dtpBatDau;
        private DateTimePicker dtpKetThuc;
        private TextBox txtGhiChu;
        private Label lblHLV;
        private Label lblDangKy;
        private Label lblCaLam;
        private Label lblBatDau;
        private Label lblKetThuc;
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
            this.cboHLV = new ComboBox();
            this.cboDangKy = new ComboBox();
            this.cboCaLam = new ComboBox();
            this.dtpBatDau = new DateTimePicker();
            this.dtpKetThuc = new DateTimePicker();
            this.txtGhiChu = new TextBox();
            this.lblHLV = new Label();
            this.lblDangKy = new Label();
            this.lblCaLam = new Label();
            this.lblBatDau = new Label();
            this.lblKetThuc = new Label();
            this.lblGhiChu = new Label();
            this.btnSave = new Button();
            this.btnCancel = new Button();
            this.SuspendLayout();

            // lblHLV
            this.lblHLV.AutoSize = true;
            this.lblHLV.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblHLV.Location = new Point(20, 23);
            this.lblHLV.Text = "Huấn luyện viên:";

            // cboHLV
            this.cboHLV.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboHLV.Font = new Font("Segoe UI", 10F);
            this.cboHLV.Location = new Point(180, 20);
            this.cboHLV.Name = "cboHLV";
            this.cboHLV.Size = new Size(300, 27);

            // lblDangKy
            this.lblDangKy.AutoSize = true;
            this.lblDangKy.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblDangKy.Location = new Point(20, 117);
            this.lblDangKy.Text = "Đăng ký gói:";

            // cboDangKy
            this.cboDangKy.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboDangKy.Font = new Font("Segoe UI", 10F);
            this.cboDangKy.Location = new Point(180, 114);
            this.cboDangKy.Name = "cboDangKy";
            this.cboDangKy.Size = new Size(300, 27);

            // lblCaLam
            this.lblCaLam.AutoSize = true;
            this.lblCaLam.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblCaLam.Location = new Point(20, 161);
            this.lblCaLam.Text = "Ca làm:";

            // cboCaLam
            this.cboCaLam.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboCaLam.Font = new Font("Segoe UI", 10F);
            this.cboCaLam.Location = new Point(180, 158);
            this.cboCaLam.Name = "cboCaLam";
            this.cboCaLam.Size = new Size(300, 27);

            // lblBatDau
            this.lblBatDau.AutoSize = true;
            this.lblBatDau.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblBatDau.Location = new Point(20, 205);
            this.lblBatDau.Text = "Ngày bắt đầu:";

            // dtpBatDau
            this.dtpBatDau.Font = new Font("Segoe UI", 10F);
            this.dtpBatDau.Format = DateTimePickerFormat.Short;
            this.dtpBatDau.Location = new Point(180, 202);
            this.dtpBatDau.Name = "dtpBatDau";
            this.dtpBatDau.Size = new Size(300, 27);

            // lblKetThuc
            this.lblKetThuc.AutoSize = true;
            this.lblKetThuc.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblKetThuc.Location = new Point(20, 249);
            this.lblKetThuc.Text = "Ngày kết thúc:";

            // dtpKetThuc
            this.dtpKetThuc.Font = new Font("Segoe UI", 10F);
            this.dtpKetThuc.Format = DateTimePickerFormat.Short;
            this.dtpKetThuc.Location = new Point(180, 246);
            this.dtpKetThuc.Name = "dtpKetThuc";
            this.dtpKetThuc.Size = new Size(300, 27);

            // lblGhiChu
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblGhiChu.Location = new Point(20, 293);
            this.lblGhiChu.Text = "Ghi chú:";

            // txtGhiChu
            this.txtGhiChu.BorderStyle = BorderStyle.FixedSingle;
            this.txtGhiChu.Font = new Font("Segoe UI", 10F);
            this.txtGhiChu.Location = new Point(180, 290);
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new Size(300, 27);

            // btnSave
            this.btnSave.BackColor = Color.FromArgb(39, 174, 96);
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnSave.ForeColor = Color.White;
            this.btnSave.Location = new Point(120, 340);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(130, 40);
            this.btnSave.Text = "Lưu";
            this.btnSave.Click += new EventHandler(this.BtnSave_Click);

            // btnCancel
            this.btnCancel.BackColor = Color.FromArgb(231, 76, 60);
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnCancel.ForeColor = Color.White;
            this.btnCancel.Location = new Point(270, 340);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(130, 40);
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += new EventHandler(this.BtnCancel_Click);

            // FormPhanCongDetail
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(245, 246, 250);
            this.ClientSize = new Size(510, 400);
            this.Controls.Add(this.lblHLV);
            this.Controls.Add(this.cboHLV);
            this.Controls.Add(this.lblDangKy);
            this.Controls.Add(this.cboDangKy);
            this.Controls.Add(this.lblCaLam);
            this.Controls.Add(this.cboCaLam);
            this.Controls.Add(this.lblBatDau);
            this.Controls.Add(this.dtpBatDau);
            this.Controls.Add(this.lblKetThuc);
            this.Controls.Add(this.dtpKetThuc);
            this.Controls.Add(this.lblGhiChu);
            this.Controls.Add(this.txtGhiChu);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPhanCongDetail";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Phân công PT";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
