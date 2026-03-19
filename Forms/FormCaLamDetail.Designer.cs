namespace AppGym.Forms
{
    partial class FormCaLamDetail
    {
        private System.ComponentModel.IContainer components = null;

        private TextBox txtTenCa;
        private DateTimePicker dtpBatDau;
        private DateTimePicker dtpKetThuc;
        private Label lblTenCa;
        private Label lblBatDau;
        private Label lblKetThuc;
        private Button btnSave;
        private Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtTenCa = new TextBox();
            this.dtpBatDau = new DateTimePicker();
            this.dtpKetThuc = new DateTimePicker();
            this.lblTenCa = new Label();
            this.lblBatDau = new Label();
            this.lblKetThuc = new Label();
            this.btnSave = new Button();
            this.btnCancel = new Button();
            this.SuspendLayout();

            // lblTenCa
            this.lblTenCa.AutoSize = true;
            this.lblTenCa.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblTenCa.Location = new Point(20, 28);
            this.lblTenCa.Text = "Tên ca:";

            // txtTenCa
            this.txtTenCa.BorderStyle = BorderStyle.FixedSingle;
            this.txtTenCa.Font = new Font("Segoe UI", 10F);
            this.txtTenCa.Location = new Point(160, 25);
            this.txtTenCa.Name = "txtTenCa";
            this.txtTenCa.Size = new Size(250, 27);

            // lblBatDau
            this.lblBatDau.AutoSize = true;
            this.lblBatDau.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblBatDau.Location = new Point(20, 83);
            this.lblBatDau.Text = "Giờ bắt đầu:";

            // dtpBatDau
            this.dtpBatDau.Font = new Font("Segoe UI", 10F);
            this.dtpBatDau.Format = DateTimePickerFormat.Time;
            this.dtpBatDau.Location = new Point(160, 80);
            this.dtpBatDau.Name = "dtpBatDau";
            this.dtpBatDau.ShowUpDown = true;
            this.dtpBatDau.Size = new Size(250, 27);

            // lblKetThuc
            this.lblKetThuc.AutoSize = true;
            this.lblKetThuc.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblKetThuc.Location = new Point(20, 138);
            this.lblKetThuc.Text = "Giờ kết thúc:";

            // dtpKetThuc
            this.dtpKetThuc.Font = new Font("Segoe UI", 10F);
            this.dtpKetThuc.Format = DateTimePickerFormat.Time;
            this.dtpKetThuc.Location = new Point(160, 135);
            this.dtpKetThuc.Name = "dtpKetThuc";
            this.dtpKetThuc.ShowUpDown = true;
            this.dtpKetThuc.Size = new Size(250, 27);

            // btnSave
            this.btnSave.BackColor = Color.FromArgb(39, 174, 96);
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnSave.ForeColor = Color.White;
            this.btnSave.Location = new Point(65, 190);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(130, 40);
            this.btnSave.Text = "Lưu";
            this.btnSave.Click += new EventHandler(this.BtnSave_Click);

            // btnCancel
            this.btnCancel.BackColor = Color.FromArgb(231, 76, 60);
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnCancel.ForeColor = Color.White;
            this.btnCancel.Location = new Point(215, 190);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(130, 40);
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += new EventHandler(this.BtnCancel_Click);

            // FormCaLamDetail
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(245, 246, 250);
            this.ClientSize = new Size(440, 255);
            this.Controls.Add(this.lblTenCa);
            this.Controls.Add(this.txtTenCa);
            this.Controls.Add(this.lblBatDau);
            this.Controls.Add(this.dtpBatDau);
            this.Controls.Add(this.lblKetThuc);
            this.Controls.Add(this.dtpKetThuc);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCaLamDetail";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Ca làm";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
