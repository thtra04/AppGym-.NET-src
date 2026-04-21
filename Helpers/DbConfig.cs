using System.Text.Json;
using AppGym.DataAccess;
using Microsoft.Data.SqlClient;

namespace AppGym.Helpers
{
    /// <summary>
    /// Persists the SQL Server connection settings to %LOCALAPPDATA%\AppGym\appsettings.json
    /// so the app works on machines where the default (local)\SQLEXPRESS instance is missing.
    /// </summary>
    public static class DbConfig
    {
        public class Settings
        {
            public string Server { get; set; } = @"(local)\SQLEXPRESS";
            public string Database { get; set; } = "GymManagementDB";
            public bool IntegratedSecurity { get; set; } = true;
            public string? UserId { get; set; }
            public string? Password { get; set; }
        }

        private static string ConfigDir =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AppGym");
        private static string ConfigPath => Path.Combine(ConfigDir, "appsettings.json");

        public static Settings Load()
        {
            try
            {
                if (File.Exists(ConfigPath))
                {
                    var json = File.ReadAllText(ConfigPath);
                    var s = JsonSerializer.Deserialize<Settings>(json);
                    if (s != null) return s;
                }
            }
            catch { /* fallback to defaults */ }
            return new Settings();
        }

        public static void Save(Settings s)
        {
            Directory.CreateDirectory(ConfigDir);
            File.WriteAllText(ConfigPath, JsonSerializer.Serialize(s, new JsonSerializerOptions { WriteIndented = true }));
        }

        public static string BuildConnectionString(Settings s)
        {
            var b = new SqlConnectionStringBuilder
            {
                DataSource = s.Server,
                InitialCatalog = s.Database,
                TrustServerCertificate = true,
                ConnectTimeout = 8
            };
            if (s.IntegratedSecurity)
            {
                b.IntegratedSecurity = true;
            }
            else
            {
                b.UserID = s.UserId ?? string.Empty;
                b.Password = s.Password ?? string.Empty;
            }
            return b.ConnectionString;
        }

        public static (bool ok, string? error) TryConnect(Settings s)
        {
            try
            {
                using var c = new SqlConnection(BuildConnectionString(s));
                c.Open();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        /// <summary>Apply saved settings to DatabaseHelper. Returns true if connection works.</summary>
        public static bool ApplySaved()
        {
            var s = Load();
            DatabaseHelper.ConnectionString = BuildConnectionString(s);
            return TryConnect(s).ok;
        }
    }
}
