namespace AppGym.Forms
{
    partial class FormDangKyGoiDetail
    {
        private System.ComponentModel.IContainer components = null;

        private ComboBox cboHocVien;
        private ComboBox cboGoiTap;
        private DateTimePicker dtpBatDau;
        private DateTimePicker dtpHetHan;
        private ComboBox cboTrangThai;
        private TextBox txtGhiChu;
        private Label lblHocVien;
        private Label lblGoiTap;
        private Label lblBatDau;
        private Label lblHetHan;
        private Label lblTrangThai;
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
            cboHocVien = new ComboBox();
            cboGoiTap = new ComboBox();
            dtpBatDau = new DateTimePicker();
            dtpHetHan = new DateTimePicker();
            cboTrangThai = new ComboBox();
            txtGhiChu = new TextBox();
            lblHocVien = new Label();
            lblGoiTap = new Label();
            lblBatDau = new Label();
            lblHetHan = new Label();
            lblTrangThai = new Label();
            lblGhiChu = new Label();
            btnSave = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // cboHocVien
            // 
            cboHocVien.DropDownStyle = ComboBoxStyle.DropDownList;
            cboHocVien.Font = new Font("Segoe UI", 10F);
            cboHocVien.Location = new Point(194, 27);
            cboHocVien.Margin = new Padding(3, 4, 3, 4);
            cboHocVien.Name = "cboHocVien";
            cboHocVien.Size = new Size(354, 31);
            cboHocVien.TabIndex = 1;
            // 
            // cboGoiTap
            // 
            cboGoiTap.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGoiTap.Font = new Font("Segoe UI", 10F);
            cboGoiTap.Location = new Point(194, 96);
            cboGoiTap.Margin = new Padding(3, 4, 3, 4);
            cboGoiTap.Name = "cboGoiTap";
            cboGoiTap.Size = new Size(354, 31);
            cboGoiTap.TabIndex = 3;
            // 
            // dtpBatDau
            // 
            dtpBatDau.Font = new Font("Segoe UI", 10F);
            dtpBatDau.Format = DateTimePickerFormat.Short;
            dtpBatDau.Location = new Point(194, 165);
            dtpBatDau.Margin = new Padding(3, 4, 3, 4);
            dtpBatDau.Name = "dtpBatDau";
            dtpBatDau.Size = new Size(354, 30);
            dtpBatDau.TabIndex = 5;
            // 
            // dtpHetHan
            // 
            dtpHetHan.Font = new Font("Segoe UI", 10F);
            dtpHetHan.Format = DateTimePickerFormat.Short;
            dtpHetHan.Location = new Point(194, 235);
            dtpHetHan.Margin = new Padding(3, 4, 3, 4);
            dtpHetHan.Name = "dtpHetHan";
            dtpHetHan.Size = new Size(354, 30);
            dtpHetHan.TabIndex = 7;
            // 
            // cboTrangThai
            // 
            cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTrangThai.Font = new Font("Segoe UI", 10F);
            cboTrangThai.Items.AddRange(new object[] { "Đang hoạt động", "Hết hạn", "Tạm dừng", "Hủy" });
            cboTrangThai.Location = new Point(194, 304);
            cboTrangThai.Margin = new Padding(3, 4, 3, 4);
            cboTrangThai.Name = "cboTrangThai";
            cboTrangThai.Size = new Size(354, 31);
            cboTrangThai.TabIndex = 9;
            // 
            // txtGhiChu
            // 
            txtGhiChu.BorderStyle = BorderStyle.FixedSingle;
            txtGhiChu.Font = new Font("Segoe UI", 10F);
            txtGhiChu.Location = new Point(194, 373);
            txtGhiChu.Margin = new Padding(3, 4, 3, 4);
            txtGhiChu.Name = "txtGhiChu";
            txtGhiChu.Size = new Size(354, 30);
            txtGhiChu.TabIndex = 11;
            // 
            // lblHocVien
            // 
            lblHocVien.AutoSize = true;
            lblHocVien.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblHocVien.Location = new Point(23, 31);
            lblHocVien.Name = "lblHocVien";
            lblHocVien.Size = new Size(81, 23);
            lblHocVien.TabIndex = 0;
            lblHocVien.Text = "Học viên:";
            lblHocVien.Click += lblHocVien_Click;
            // 
            // lblGoiTap
            // 
            lblGoiTap.AutoSize = true;
            lblGoiTap.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblGoiTap.Location = new Point(23, 100);
            lblGoiTap.Name = "lblGoiTap";
            lblGoiTap.Size = new Size(71, 23);
            lblGoiTap.TabIndex = 2;
            lblGoiTap.Text = "Gói tập:";
            // 
            // lblBatDau
            // 
            lblBatDau.AutoSize = true;
            lblBatDau.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBatDau.Location = new Point(23, 169);
            lblBatDau.Name = "lblBatDau";
            lblBatDau.Size = new Size(116, 23);
            lblBatDau.TabIndex = 4;
            lblBatDau.Text = "Ngày bắt đầu:";
            // 
            // lblHetHan
            // 
            lblHetHan.AutoSize = true;
            lblHetHan.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblHetHan.Location = new Point(23, 239);
            lblHetHan.Name = "lblHetHan";
            lblHetHan.Size = new Size(118, 23);
            lblHetHan.TabIndex = 6;
            lblHetHan.Text = "Ngày hết hạn:";
            // 
            // lblTrangThai
            // 
            lblTrangThai.AutoSize = true;
            lblTrangThai.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTrangThai.Location = new Point(23, 308);
            lblTrangThai.Name = "lblTrangThai";
            lblTrangThai.Size = new Size(95, 23);
            lblTrangThai.TabIndex = 8;
            lblTrangThai.Text = "Trạng thái:";
            // 
            // lblGhiChu
            // 
            lblGhiChu.AutoSize = true;
            lblGhiChu.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblGhiChu.Location = new Point(23, 377);
            lblGhiChu.Name = "lblGhiChu";
            lblGhiChu.Size = new Size(75, 23);
            lblGhiChu.TabIndex = 10;
            lblGhiChu.Text = "Ghi chú:";
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(39, 174, 96);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(137, 440);
            btnSave.Margin = new Padding(3, 4, 3, 4);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(149, 53);
            btnSave.TabIndex = 12;
            btnSave.Text = "Lưu";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += BtnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(231, 76, 60);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(309, 440);
            btnCancel.Margin = new Padding(3, 4, 3, 4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(149, 53);
            btnCancel.TabIndex = 13;
            btnCancel.Text = "Hủy";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += BtnCancel_Click;
            // 
            // FormDangKyGoiDetail
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 246, 250);
            ClientSize = new Size(583, 527);
            Controls.Add(lblHocVien);
            Controls.Add(cboHocVien);
            Controls.Add(lblGoiTap);
            Controls.Add(cboGoiTap);
            Controls.Add(lblBatDau);
            Controls.Add(dtpBatDau);
            Controls.Add(lblHetHan);
            Controls.Add(dtpHetHan);
            Controls.Add(lblTrangThai);
            Controls.Add(cboTrangThai);
            Controls.Add(lblGhiChu);
            Controls.Add(txtGhiChu);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormDangKyGoiDetail";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Đăng ký gói";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
