using AppGym.Helpers;

namespace AppGym.Forms
{
    partial class FormGoiTapDetail
    {
        private System.ComponentModel.IContainer components = null;

        private TextBox txtTenGoi;
        private TextBox txtThoiHan;
        private TextBox txtGia;
        private TextBox txtMoTa;
        private CheckBox chkTrangThai;
        private Label lblTenGoi;
        private Label lblThoiHan;
        private Label lblGia;
        private Label lblMoTa;
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
            this.txtTenGoi = new TextBox();
            this.txtThoiHan = new TextBox();
            this.txtGia = new TextBox();
            this.txtMoTa = new TextBox();
            this.chkTrangThai = new CheckBox();
            this.lblTenGoi = new Label();
            this.lblThoiHan = new Label();
            this.lblGia = new Label();
            this.lblMoTa = new Label();
            this.lblTrangThai = new Label();
            this.btnSave = new Button();
            this.btnCancel = new Button();
            this.SuspendLayout();

            // lblTenGoi
            this.lblTenGoi.AutoSize = true;
            this.lblTenGoi.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblTenGoi.Location = new Point(20, 23);
            this.lblTenGoi.Name = "lblTenGoi";
            this.lblTenGoi.Text = "Tên gói:";

            // txtTenGoi
            this.txtTenGoi.BorderStyle = BorderStyle.FixedSingle;
            this.txtTenGoi.Font = new Font("Segoe UI", 10F);
            this.txtTenGoi.Location = new Point(170, 20);
            this.txtTenGoi.Name = "txtTenGoi";
            this.txtTenGoi.Size = new Size(290, 27);

            // lblThoiHan
            this.lblThoiHan.AutoSize = true;
            this.lblThoiHan.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblThoiHan.Location = new Point(20, 75);
            this.lblThoiHan.Name = "lblThoiHan";
            this.lblThoiHan.Text = "Thời hạn (ngày):";

            // txtThoiHan
            this.txtThoiHan.BorderStyle = BorderStyle.FixedSingle;
            this.txtThoiHan.Font = new Font("Segoe UI", 10F);
            this.txtThoiHan.Location = new Point(170, 72);
            this.txtThoiHan.Name = "txtThoiHan";
            this.txtThoiHan.Size = new Size(290, 27);

            // lblGia
            this.lblGia.AutoSize = true;
            this.lblGia.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblGia.Location = new Point(20, 127);
            this.lblGia.Name = "lblGia";
            this.lblGia.Text = "Giá:";

            // txtGia
            this.txtGia.BorderStyle = BorderStyle.FixedSingle;
            this.txtGia.Font = new Font("Segoe UI", 10F);
            this.txtGia.Location = new Point(170, 124);
            this.txtGia.Name = "txtGia";
            this.txtGia.Size = new Size(290, 27);

            // lblMoTa
            this.lblMoTa.AutoSize = true;
            this.lblMoTa.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblMoTa.Location = new Point(20, 179);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Text = "Mô tả:";

            // txtMoTa
            this.txtMoTa.BorderStyle = BorderStyle.FixedSingle;
            this.txtMoTa.Font = new Font("Segoe UI", 10F);
            this.txtMoTa.Location = new Point(170, 176);
            this.txtMoTa.Multiline = true;
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.Size = new Size(290, 60);

            // lblTrangThai
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblTrangThai.Location = new Point(20, 258);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Text = "Trạng thái:";

            // chkTrangThai
            this.chkTrangThai.AutoSize = true;
            this.chkTrangThai.Checked = true;
            this.chkTrangThai.CheckState = CheckState.Checked;
            this.chkTrangThai.Font = new Font("Segoe UI", 10F);
            this.chkTrangThai.Location = new Point(170, 256);
            this.chkTrangThai.Name = "chkTrangThai";
            this.chkTrangThai.Text = "Hoạt động";

            // btnSave
            this.btnSave.BackColor = Color.FromArgb(39, 174, 96);
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnSave.ForeColor = Color.White;
            this.btnSave.Location = new Point(100, 310);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(130, 40);
            this.btnSave.Text = "Lưu";
            this.btnSave.Click += new EventHandler(this.BtnSave_Click);

            // btnCancel
            this.btnCancel.BackColor = Color.FromArgb(231, 76, 60);
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnCancel.ForeColor = Color.White;
            this.btnCancel.Location = new Point(250, 310);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(130, 40);
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += new EventHandler(this.BtnCancel_Click);

            // FormGoiTapDetail
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(245, 246, 250);
            this.ClientSize = new Size(490, 375);
            this.Controls.Add(this.lblTenGoi);
            this.Controls.Add(this.txtTenGoi);
            this.Controls.Add(this.lblThoiHan);
            this.Controls.Add(this.txtThoiHan);
            this.Controls.Add(this.lblGia);
            this.Controls.Add(this.txtGia);
            this.Controls.Add(this.lblMoTa);
            this.Controls.Add(this.txtMoTa);
            this.Controls.Add(this.lblTrangThai);
            this.Controls.Add(this.chkTrangThai);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormGoiTapDetail";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Gói tập";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
