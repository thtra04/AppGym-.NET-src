using AppGym.Forms;

namespace AppGym;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

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
}