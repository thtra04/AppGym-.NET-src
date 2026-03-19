namespace AppGym.Forms
{
    partial class FormHLVDetail
    {
        private System.ComponentModel.IContainer components = null;

        private TextBox txtHoTen;
        private ComboBox cboGioiTinh;
        private TextBox txtSDT;
        private TextBox txtChuyenMon;
        private TextBox txtLuong;
        private CheckBox chkTrangThai;
        private Label lblHoTen;
        private Label lblGioiTinh;
        private Label lblSDT;
        private Label lblChuyenMon;
        private Label lblLuong;
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
            txtHoTen = new TextBox();
            cboGioiTinh = new ComboBox();
            txtSDT = new TextBox();
            txtChuyenMon = new TextBox();
            txtLuong = new TextBox();
            chkTrangThai = new CheckBox();
            lblHoTen = new Label();
            lblGioiTinh = new Label();
            lblSDT = new Label();
            lblChuyenMon = new Label();
            lblLuong = new Label();
            lblTrangThai = new Label();
            btnSave = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // txtHoTen
            // 
            txtHoTen.BorderStyle = BorderStyle.FixedSingle;
            txtHoTen.Font = new Font("Segoe UI", 10F);
            txtHoTen.Location = new Point(183, 27);
            txtHoTen.Margin = new Padding(3, 4, 3, 4);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(331, 30);
            txtHoTen.TabIndex = 1;
            // 
            // cboGioiTinh
            // 
            cboGioiTinh.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGioiTinh.Font = new Font("Segoe UI", 10F);
            cboGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });
            cboGioiTinh.Location = new Point(183, 96);
            cboGioiTinh.Margin = new Padding(3, 4, 3, 4);
            cboGioiTinh.Name = "cboGioiTinh";
            cboGioiTinh.Size = new Size(331, 31);
            cboGioiTinh.TabIndex = 3;
            // 
            // txtSDT
            // 
            txtSDT.BorderStyle = BorderStyle.FixedSingle;
            txtSDT.Font = new Font("Segoe UI", 10F);
            txtSDT.Location = new Point(183, 165);
            txtSDT.Margin = new Padding(3, 4, 3, 4);
            txtSDT.Name = "txtSDT";
            txtSDT.Size = new Size(331, 30);
            txtSDT.TabIndex = 5;
            // 
            // txtChuyenMon
            // 
            txtChuyenMon.BorderStyle = BorderStyle.FixedSingle;
            txtChuyenMon.Font = new Font("Segoe UI", 10F);
            txtChuyenMon.Location = new Point(183, 235);
            txtChuyenMon.Margin = new Padding(3, 4, 3, 4);
            txtChuyenMon.Name = "txtChuyenMon";
            txtChuyenMon.Size = new Size(331, 30);
            txtChuyenMon.TabIndex = 7;
            // 
            // txtLuong
            // 
            txtLuong.BorderStyle = BorderStyle.FixedSingle;
            txtLuong.Font = new Font("Segoe UI", 10F);
            txtLuong.Location = new Point(183, 304);
            txtLuong.Margin = new Padding(3, 4, 3, 4);
            txtLuong.Name = "txtLuong";
            txtLuong.Size = new Size(331, 30);
            txtLuong.TabIndex = 9;
            // 
            // chkTrangThai
            // 
            chkTrangThai.AutoSize = true;
            chkTrangThai.Checked = true;
            chkTrangThai.CheckState = CheckState.Checked;
            chkTrangThai.Font = new Font("Segoe UI", 10F);
            chkTrangThai.Location = new Point(183, 375);
            chkTrangThai.Margin = new Padding(3, 4, 3, 4);
            chkTrangThai.Name = "chkTrangThai";
            chkTrangThai.Size = new Size(109, 27);
            chkTrangThai.TabIndex = 11;
            chkTrangThai.Text = "Hoạt động";
            // 
            // lblHoTen
            // 
            lblHoTen.AutoSize = true;
            lblHoTen.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblHoTen.Location = new Point(23, 31);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Size = new Size(66, 23);
            lblHoTen.TabIndex = 0;
            lblHoTen.Text = "Họ tên:";
            lblHoTen.Click += lblHoTen_Click;
            // 
            // lblGioiTinh
            // 
            lblGioiTinh.AutoSize = true;
            lblGioiTinh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblGioiTinh.Location = new Point(23, 100);
            lblGioiTinh.Name = "lblGioiTinh";
            lblGioiTinh.Size = new Size(81, 23);
            lblGioiTinh.TabIndex = 2;
            lblGioiTinh.Text = "Giới tính:";
            // 
            // lblSDT
            // 
            lblSDT.AutoSize = true;
            lblSDT.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSDT.Location = new Point(23, 169);
            lblSDT.Name = "lblSDT";
            lblSDT.Size = new Size(42, 23);
            lblSDT.TabIndex = 4;
            lblSDT.Text = "SĐT:";
            // 
            // lblChuyenMon
            // 
            lblChuyenMon.AutoSize = true;
            lblChuyenMon.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblChuyenMon.Location = new Point(23, 239);
            lblChuyenMon.Name = "lblChuyenMon";
            lblChuyenMon.Size = new Size(115, 23);
            lblChuyenMon.TabIndex = 6;
            lblChuyenMon.Text = "Chuyên môn:";
            // 
            // lblLuong
            // 
            lblLuong.AutoSize = true;
            lblLuong.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblLuong.Location = new Point(23, 308);
            lblLuong.Name = "lblLuong";
            lblLuong.Size = new Size(58, 23);
            lblLuong.TabIndex = 8;
            lblLuong.Text = "Lưong:";
            // 
            // lblTrangThai
            // 
            lblTrangThai.AutoSize = true;
            lblTrangThai.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTrangThai.Location = new Point(23, 377);
            lblTrangThai.Name = "lblTrangThai";
            lblTrangThai.Size = new Size(95, 23);
            lblTrangThai.TabIndex = 10;
            lblTrangThai.Text = "Trạng thái:";
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(39, 174, 96);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(120, 440);
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
            btnCancel.Location = new Point(291, 440);
            btnCancel.Margin = new Padding(3, 4, 3, 4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(149, 53);
            btnCancel.TabIndex = 13;
            btnCancel.Text = "Hủy";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += BtnCancel_Click;
            // 
            // FormHLVDetail
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 246, 250);
            ClientSize = new Size(560, 527);
            Controls.Add(lblHoTen);
            Controls.Add(txtHoTen);
            Controls.Add(lblGioiTinh);
            Controls.Add(cboGioiTinh);
            Controls.Add(lblSDT);
            Controls.Add(txtSDT);
            Controls.Add(lblChuyenMon);
            Controls.Add(txtChuyenMon);
            Controls.Add(lblLuong);
            Controls.Add(txtLuong);
            Controls.Add(lblTrangThai);
            Controls.Add(chkTrangThai);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormHLVDetail";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Huấn luyện viên";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
