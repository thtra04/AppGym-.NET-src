using AppGym.DataAccess;

namespace AppGym.Tests;

/// <summary>
/// TC_DB_01 ? TC_DB_05
/// Ki?m tra các s? li?u hi?n th? trên Dashboard (Count, CountActive, TotalRevenue, GetAll.Take(10))
/// </summary>
[TestFixture]
[Category("Dashboard")]
public class DashboardTests : TestBase
{
    [SetUp]
    public void Setup() => SkipIfNoDatabase();

    // TC_DB_01 – Positive
    [Test]
    [Description("TC_DB_01: HocVienDAO.Count() tr? v? s? h?c viên ?ang ho?t ??ng >= 0")]
    public void HocVien_Count_ReturnsNonNegative()
    {
        int count = new HocVienDAO().Count();

        Assert.That(count, Is.GreaterThanOrEqualTo(0));
    }

    // TC_DB_02 – Positive
    [Test]
    [Description("TC_DB_02: HuanLuyenVienDAO.Count() tr? v? s? HLV ?ang ho?t ??ng >= 0")]
    public void HuanLuyenVien_Count_ReturnsNonNegative()
    {
        int count = new HuanLuyenVienDAO().Count();

        Assert.That(count, Is.GreaterThanOrEqualTo(0));
    }

    // TC_DB_03 – Positive
    [Test]
    [Description("TC_DB_03: GoiTapDAO.Count() tr? v? s? gói t?p ?ang ho?t ??ng >= 0")]
    public void GoiTap_Count_ReturnsNonNegative()
    {
        int count = new GoiTapDAO().Count();

        Assert.That(count, Is.GreaterThanOrEqualTo(0));
    }

    // TC_DB_04 – Positive
    [Test]
    [Description("TC_DB_04: DangKyGoiDAO.CountActive() tr? v? s? ??ng ký '?ang ho?t ??ng' >= 0")]
    public void DangKy_CountActive_ReturnsNonNegative()
    {
        int count = new DangKyGoiDAO().CountActive();

        Assert.That(count, Is.GreaterThanOrEqualTo(0));
    }

    // TC_DB_05 – Positive
    [Test]
    [Description("TC_DB_05: GetAll().Take(10) không bao gi? tr? v? quá 10 b?n ghi")]
    public void DangKy_GetAll_Take10_MaxTenRecords()
    {
        var list = new DangKyGoiDAO().GetAll().Take(10).ToList();

        Assert.That(list.Count, Is.LessThanOrEqualTo(10));
    }

    // TC_DB_04 (doanh thu) – Positive
    [Test]
    [Description("TC_HD_08: HoaDonDAO.TotalRevenue() kh?p v?i t?ng SUM(SoTien) trong DB")]
    public void HoaDon_TotalRevenue_MatchesSumInDB()
    {
        decimal fromDAO = new HoaDonDAO().TotalRevenue();

        decimal fromDB;
        using var conn = new Microsoft.Data.SqlClient.SqlConnection(ConnStr);
        conn.Open();
        using var cmd = new Microsoft.Data.SqlClient.SqlCommand(
            "SELECT ISNULL(SUM(SoTien),0) FROM HoaDon", conn);
        fromDB = (decimal)cmd.ExecuteScalar();

        Assert.That(fromDAO, Is.EqualTo(fromDB), "TotalRevenue() ph?i kh?p v?i SUM trong DB");
    }
}
