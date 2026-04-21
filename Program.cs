using AppGym.DataAccess;
using AppGym.Forms;
using AppGym.Helpers;
using QuestPDF.Infrastructure;

namespace AppGym;

static class Program
{
    [STAThread]
    static void Main()
    {
        QuestPDF.Settings.License = LicenseType.Community;
        ApplicationConfiguration.Initialize();

        // Apply saved DB settings (if any). If the connection fails, prompt the user to configure.
        if (!DbConfig.ApplySaved())
        {
            if (!PromptForDbConfig("Không kết nối được SQL Server với cấu hình hiện tại. Vui lòng cấu hình lại."))
                return;
        }

        while (true)
        {
            try
            {
                DatabaseSchemaSynchronizer.EnsureSchemaUpToDate();
                break;
            }
            catch (Exception ex)
            {
                var choice = MessageBox.Show(
                    "Không thể đồng bộ cấu trúc dữ liệu trước khi mở ứng dụng:\n" + ex.Message +
                    "\n\nBấm OK để cấu hình lại kết nối SQL Server, hoặc Cancel để thoát.",
                    "Lỗi dữ liệu",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Error);
                if (choice != DialogResult.OK) return;
                if (!PromptForDbConfig("Cấu hình lại kết nối SQL Server:")) return;
            }
        }

        while (true)
        {
            var loginForm = new FormLogin();
            if (loginForm.ShowDialog() == DialogResult.OK && loginForm.LoggedInUser != null)
            {
                var mainForm = new FormMain(loginForm.LoggedInUser);
                var result = mainForm.ShowDialog();
                if (result != DialogResult.Retry)
                    break;
            }
            else
            {
                break;
            }
        }
    }

    private static bool PromptForDbConfig(string message)
    {
        MessageBox.Show(
            message + "\n\nỞ hộp thoại tiếp theo:\n" +
            "  1. Chọn Server (vd: (local)\\SQLEXPRESS, (localdb)\\MSSQLLocalDB).\n" +
            "  2. Bấm \"Kiểm tra\" để thử kết nối.\n" +
            "  3. Nếu database chưa có, bấm \"Tạo DB mới\" để tự cài.\n" +
            "  4. Bấm \"Lưu & Tiếp tục\".",
            "Cấu hình kết nối SQL Server",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
        using var dlg = new FormDbConnection();
        return dlg.ShowDialog() == DialogResult.OK;
    }
}
