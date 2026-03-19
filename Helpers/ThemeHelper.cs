namespace AppGym.Helpers
{
    public static class ThemeColors
    {
        // Primary
        public static Color Primary = Color.FromArgb(30, 30, 60);
        public static Color PrimaryDark = Color.FromArgb(20, 20, 45);
        public static Color PrimaryLight = Color.FromArgb(45, 45, 80);

        // Accent
        public static Color Accent = Color.FromArgb(255, 107, 53);
        public static Color AccentHover = Color.FromArgb(255, 130, 80);

        // Background
        public static Color Background = Color.FromArgb(240, 242, 245);
        public static Color CardBackground = Color.White;

        // Text
        public static Color TextPrimary = Color.FromArgb(33, 37, 41);
        public static Color TextSecondary = Color.FromArgb(108, 117, 125);
        public static Color TextOnDark = Color.White;

        // Status
        public static Color Success = Color.FromArgb(40, 167, 69);
        public static Color Warning = Color.FromArgb(255, 193, 7);
        public static Color Danger = Color.FromArgb(220, 53, 69);
        public static Color Info = Color.FromArgb(23, 162, 184);

        // Sidebar
        public static Color SidebarBg = Color.FromArgb(30, 30, 60);
        public static Color SidebarHover = Color.FromArgb(45, 45, 80);
        public static Color SidebarActive = Color.FromArgb(255, 107, 53);
    }

    public static class UIHelper
    {
        public static void StyleDataGridView(DataGridView dgv)
        {
            dgv.BorderStyle = BorderStyle.None;
            dgv.BackgroundColor = ThemeColors.CardBackground;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(230, 230, 230);
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
                SelectionBackColor = Color.FromArgb(232, 240, 254),
                SelectionForeColor = ThemeColors.TextPrimary,
                Padding = new Padding(8, 0, 0, 0)
            };

            dgv.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(248, 249, 250),
                ForeColor = ThemeColors.TextPrimary,
                Font = new Font("Segoe UI", 9.5f),
                SelectionBackColor = Color.FromArgb(232, 240, 254),
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
            btn.Region = System.Drawing.Region.FromHrgn(
                CreateRoundRectRgn(0, 0, btn.Width, btn.Height, 8, 8));
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
