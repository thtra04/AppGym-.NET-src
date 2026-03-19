using AppGym.DataAccess;
using Microsoft.Data.SqlClient;

namespace AppGym.Tests;

/// <summary>
/// Base class cung c?p connection string và helper dùng chung cho t?t c? test.
/// Tr??c khi ch?y test, ??m b?o SQL Server ?ang ch?y và GymManagementDB t?n t?i.
/// </summary>
public abstract class TestBase
{
    // ?? ??i connection string n?u c?n ??????????????????????????????????
    protected const string ConnStr =
        @"Server=DESKTOP-O5A1RCH\THANHTRA;Database=GymManagementDB;Trusted_Connection=True;TrustServerCertificate=True;";

    [OneTimeSetUp]
    public virtual void GlobalSetup()
    {
        DatabaseHelper.ConnectionString = ConnStr;
    }

    /// <summary>Ki?m tra DB có th? k?t n?i không. B? qua test n?u không k?t n?i ???c.</summary>
    protected static void SkipIfNoDatabase()
    {
        try
        {
            using var conn = new SqlConnection(ConnStr);
            conn.Open();
        }
        catch
        {
            Assert.Ignore("Không th? k?t n?i database. B? qua test này.");
        }
    }

    /// <summary>Xóa b?n ghi test b?ng câu SQL tu? ý (dùng trong TearDown).</summary>
    protected static void Cleanup(string sql)
    {
        try
        {
            using var conn = new SqlConnection(ConnStr);
            conn.Open();
            using var cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }
        catch { /* ignore cleanup errors */ }
    }
}
