using AppGym.Helpers;

namespace AppGym.Forms
{
    partial class FormPhongTapDetail
    {
        private System.ComponentModel.IContainer components = null;

        private TextBox txtTenPhong;
        private TextBox txtDiaChi;
        private TextBox txtSucChua;
        private TextBox txtMoTa;
        private Label lblTenPhong;
        private Label lblDiaChi;
        private Label lblSucChua;
        private Label lblMoTa;
        private Button btnSave;
        private Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtTenPhong = new TextBox();
            this.txtDiaChi = new TextBox();
            this.txtSucChua = new TextBox();
            this.txtMoTa = new TextBox();
            this.lblTenPhong = new Label();
            this.lblDiaChi = new Label();
            this.lblSucChua = new Label();
            this.lblMoTa = new Label();
            this.btnSave = new Button();
            this.btnCancel = new Button();
            this.SuspendLayout();

            // lblTenPhong
            this.lblTenPhong.AutoSize = true;
            this.lblTenPhong.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblTenPhong.Location = new Point(20, 23);
            this.lblTenPhong.Name = "lblTenPhong";
            this.lblTenPhong.Text = "Tên phòng:";

            // txtTenPhong
            this.txtTenPhong.BorderStyle = BorderStyle.FixedSingle;
            this.txtTenPhong.Font = new Font("Segoe UI", 10F);
            this.txtTenPhong.Location = new Point(170, 20);
            this.txtTenPhong.Name = "txtTenPhong";
            this.txtTenPhong.Size = new Size(290, 27);

            // lblDiaChi
            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblDiaChi.Location = new Point(20, 75);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Text = "Địa chỉ:";

            // txtDiaChi
            this.txtDiaChi.BorderStyle = BorderStyle.FixedSingle;
            this.txtDiaChi.Font = new Font("Segoe UI", 10F);
            this.txtDiaChi.Location = new Point(170, 72);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new Size(290, 27);

            // lblSucChua
            this.lblSucChua.AutoSize = true;
            this.lblSucChua.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblSucChua.Location = new Point(20, 127);
            this.lblSucChua.Name = "lblSucChua";
            this.lblSucChua.Text = "Sức chứa:";

            // txtSucChua
            this.txtSucChua.BorderStyle = BorderStyle.FixedSingle;
            this.txtSucChua.Font = new Font("Segoe UI", 10F);
            this.txtSucChua.Location = new Point(170, 124);
            this.txtSucChua.Name = "txtSucChua";
            this.txtSucChua.Size = new Size(290, 27);

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

            // btnSave
            this.btnSave.BackColor = Color.FromArgb(39, 174, 96);
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnSave.ForeColor = Color.White;
            this.btnSave.Location = new Point(100, 260);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(130, 40);
            this.btnSave.Text = "Lưu";
            this.btnSave.Click += new EventHandler(this.BtnSave_Click);

            // btnCancel
            this.btnCancel.BackColor = Color.FromArgb(231, 76, 60);
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnCancel.ForeColor = Color.White;
            this.btnCancel.Location = new Point(250, 260);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(130, 40);
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += new EventHandler(this.BtnCancel_Click);

            // FormPhongTapDetail
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(245, 246, 250);
            this.ClientSize = new Size(490, 325);
            this.Controls.Add(this.lblTenPhong);
            this.Controls.Add(this.txtTenPhong);
            this.Controls.Add(this.lblDiaChi);
            this.Controls.Add(this.txtDiaChi);
            this.Controls.Add(this.lblSucChua);
            this.Controls.Add(this.txtSucChua);
            this.Controls.Add(this.lblMoTa);
            this.Controls.Add(this.txtMoTa);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPhongTapDetail";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Phòng tập";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
