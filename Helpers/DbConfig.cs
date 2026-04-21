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

        /// <summary>Try to connect to the server only (master DB) to verify the instance is reachable.</summary>
        public static (bool ok, string? error) TryConnectServer(Settings s)
        {
            try
            {
                var b = new SqlConnectionStringBuilder(BuildConnectionString(s)) { InitialCatalog = "master" };
                using var c = new SqlConnection(b.ConnectionString);
                c.Open();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public static bool DatabaseExists(Settings s)
        {
            try
            {
                var b = new SqlConnectionStringBuilder(BuildConnectionString(s)) { InitialCatalog = "master" };
                using var c = new SqlConnection(b.ConnectionString);
                c.Open();
                using var cmd = new SqlCommand("SELECT DB_ID(@n)", c);
                cmd.Parameters.AddWithValue("@n", s.Database);
                var v = cmd.ExecuteScalar();
                return v != null && v != DBNull.Value;
            }
            catch { return false; }
        }

        /// <summary>
        /// Run setup_db.sql against the server (must succeed against master) to create the database
        /// and tables. Returns (ok, error). The script is shipped under {app}\sql\setup_db.sql.
        /// </summary>
        public static (bool ok, string? error) RunSetupScript(Settings s)
        {
            try
            {
                var sqlPath = FindSetupScript();
                if (sqlPath == null)
                    return (false, "Không tìm thấy file setup_db.sql trong thư mục cài đặt (sql\\setup_db.sql).");

                var script = File.ReadAllText(sqlPath);
                var batches = SplitGoBatches(script);

                var b = new SqlConnectionStringBuilder(BuildConnectionString(s)) { InitialCatalog = "master" };
                using var conn = new SqlConnection(b.ConnectionString);
                conn.Open();

                foreach (var batch in batches)
                {
                    if (string.IsNullOrWhiteSpace(batch)) continue;
                    using var cmd = new SqlCommand(batch, conn) { CommandTimeout = 60 };
                    cmd.ExecuteNonQuery();
                }
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        private static string? FindSetupScript()
        {
            string baseDir = AppContext.BaseDirectory;
            string[] candidates =
            {
                Path.Combine(baseDir, "sql", "setup_db.sql"),
                Path.Combine(baseDir, "setup_db.sql"),
                Path.Combine(baseDir, "..", "sql", "setup_db.sql"),
                Path.Combine(baseDir, "..", "..", "..", "setup_db.sql"),
            };
            foreach (var p in candidates)
            {
                var full = Path.GetFullPath(p);
                if (File.Exists(full)) return full;
            }
            return null;
        }

        private static List<string> SplitGoBatches(string script)
        {
            var lines = script.Replace("\r\n", "\n").Split('\n');
            var batches = new List<string>();
            var current = new System.Text.StringBuilder();
            foreach (var line in lines)
            {
                if (line.Trim().Equals("GO", StringComparison.OrdinalIgnoreCase))
                {
                    if (current.Length > 0) batches.Add(current.ToString());
                    current.Clear();
                }
                else
                {
                    current.AppendLine(line);
                }
            }
            if (current.Length > 0) batches.Add(current.ToString());
            return batches;
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
