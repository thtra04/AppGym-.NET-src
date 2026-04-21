namespace AppGym.Helpers
{
    public static class ThemeColors
    {
        // Primary
        public static Color Primary = Color.FromArgb(19, 28, 46);
        public static Color PrimaryDark = Color.FromArgb(11, 18, 33);
        public static Color PrimaryLight = Color.FromArgb(36, 52, 82);

        // Accent
        public static Color Accent = Color.FromArgb(232, 169, 84);
        public static Color AccentHover = Color.FromArgb(244, 186, 109);
        public static Color AccentSoft = Color.FromArgb(252, 243, 228);

        // Background
        public static Color Background = Color.FromArgb(243, 245, 249);
        public static Color CardBackground = Color.White;
        public static Color SurfaceMuted = Color.FromArgb(232, 236, 243);

        // Text
        public static Color TextPrimary = Color.FromArgb(29, 37, 51);
        public static Color TextSecondary = Color.FromArgb(104, 115, 134);
        public static Color TextOnDark = Color.White;

        // Status
        public static Color Success = Color.FromArgb(41, 148, 99);
        public static Color Warning = Color.FromArgb(231, 160, 56);
        public static Color Danger = Color.FromArgb(210, 84, 84);
        public static Color Info = Color.FromArgb(56, 120, 201);

        // Sidebar
        public static Color SidebarBg = Color.FromArgb(16, 24, 40);
        public static Color SidebarHover = Color.FromArgb(33, 47, 73);
        public static Color SidebarActive = Color.FromArgb(232, 169, 84);
    }

    public static class UIHelper
    {
        public static void StyleDataGridView(DataGridView dgv)
        {
            dgv.BorderStyle = BorderStyle.None;
            dgv.BackgroundColor = ThemeColors.CardBackground;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(233, 236, 241);
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.RowHeadersVisible = false;
            dgv.EnableHeadersVisualStyles = false;
            dgv.RowTemplate.Height = 40;

            dgv.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = ThemeColors.Primary,
                ForeColor = ThemeColors.TextOnDark,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(8, 0, 0, 0),
                SelectionBackColor = ThemeColors.Primary,
                SelectionForeColor = ThemeColors.TextOnDark
            };
            dgv.ColumnHeadersHeight = 44;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dgv.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = ThemeColors.CardBackground,
                ForeColor = ThemeColors.TextPrimary,
                Font = new Font("Segoe UI", 9.5f),
                SelectionBackColor = Color.FromArgb(242, 231, 210),
                SelectionForeColor = ThemeColors.TextPrimary,
                Padding = new Padding(8, 0, 0, 0)
            };

            dgv.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(248, 249, 250),
                ForeColor = ThemeColors.TextPrimary,
                Font = new Font("Segoe UI", 9.5f),
                SelectionBackColor = Color.FromArgb(242, 231, 210),
                SelectionForeColor = ThemeColors.TextPrimary,
                Padding = new Padding(8, 0, 0, 0)
            };
        }

        public static Button CreateButton(string text, Color bgColor, Color foreColor, int width = 120, int height = 38)
        {
            var btn = new Button
            {
                Text = text,
                Size = new Size(width, height),
                FlatStyle = FlatStyle.Flat,
                BackColor = bgColor,
                ForeColor = foreColor,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = ThemeColors.AccentHover;
            btn.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(bgColor, 0.08f);
            btn.Region = System.Drawing.Region.FromHrgn(
                CreateRoundRectRgn(0, 0, btn.Width, btn.Height, 14, 14));
            return btn;
        }

        public static TextBox CreateSearchBox(int width = 280)
        {
            return new TextBox
            {
                Size = new Size(width, 36),
                Font = new Font("Segoe UI", 11),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                ForeColor = ThemeColors.TextPrimary
            };
        }

        public static Panel CreateCard(int width, int height)
        {
            var panel = new Panel
            {
                Size = new Size(width, height),
                BackColor = ThemeColors.CardBackground,
                Padding = new Padding(16)
            };
            panel.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(232, 236, 242));
                e.Graphics.DrawRectangle(pen, 0, 0, panel.Width - 1, panel.Height - 1);
            };
            return panel;
        }

        [System.Runtime.InteropServices.DllImport("Gdi32.dll")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect,
            int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        public static void RoundControl(Control ctrl, int radius = 10)
        {
            ctrl.Region = System.Drawing.Region.FromHrgn(
                CreateRoundRectRgn(0, 0, ctrl.Width, ctrl.Height, radius, radius));
        }
    }
}
