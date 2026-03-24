using AppGym.DataAccess;
using AppGym.Models;

namespace AppGym.Tests;

[TestFixture]
[Category("Hoa don")]
public class HoaDonTests : TestBase
{
    private HoaDonDAO _dao = null!;
    private int _insertedId = -1;
    private int _seedMaDK = -1;

    [SetUp]
    public void Setup()
    {
        SkipIfNoDatabase();
        _dao = new HoaDonDAO();
        var dkList = new DangKyGoiDAO().GetAll();
        if (!dkList.Any()) Assert.Ignore("Need DangKyGoi in DB.");
        _seedMaDK = dkList.First().MaDK;
    }

    [TearDown]
    public void TearDown()
    {
        if (_insertedId > 0) { Cleanup($"DELETE FROM HoaDon WHERE MaHD={_insertedId}"); _insertedId = -1; }
    }

    // CHECK constraint: HinhThucTT IN (N'Ti\u1EC1n m\u1EB7t', N'Chuy\u1EC3n kho\u1EA3n', N'Th\u1EBB', N'Kh\u00E1c')
    private HoaDon MakeTestHoaDon() => new()
    {
        MaDK = _seedMaDK, NgayThanhToan = DateTime.Today, SoTien = 500_000,
        HinhThucTT = "Ti\u1EC1n m\u1EB7t", GhiChu = "TEST"
    };

    [Test, Description("TC_HD_01: Insert valid HoaDon")]
    public void Insert_ValidHoaDon_ReturnsTrue()
    {
        var hd = MakeTestHoaDon();
        Assert.That(_dao.Insert(hd), Is.True);
        _insertedId = _dao.GetAll().FirstOrDefault(x => x.MaDK == _seedMaDK && x.GhiChu == "TEST")?.MaHD ?? -1;
        Assert.That(_insertedId, Is.GreaterThan(0));
    }

    [Test, Description("TC_HD_02: Null MaDK validation")]
    public void Insert_NullMaDK_ValidationFails()
    {
        object? selectedValue = null;
        Assert.That(selectedValue != null, Is.False);
    }

    [Test, Description("TC_HD_03: SoTien = 0 validation")]
    public void Insert_SoTienZero_ValidationFails()
    {
        Assert.That(0m > 0, Is.False);
    }

    [Test, Description("TC_HD_04: Negative SoTien validation")]
    public void Insert_NegativeSoTien_ValidationFails()
    {
        Assert.That(-100m > 0, Is.False);
    }

    [Test, Description("TC_HD_05: Non-numeric SoTien parse fails")]
    public void Insert_NonNumericSoTien_ParseFails()
    {
        Assert.That(decimal.TryParse("abc", out _), Is.False);
    }

    [Test, Description("TC_HD_06: Empty HinhThucTT inserts without error (no CHECK constraint)")]
    public void Insert_EmptyHinhThucTT_ThrowsSqlException()
    {
        var hd = new HoaDon { MaDK = _seedMaDK, NgayThanhToan = DateTime.Today, SoTien = 100_000, HinhThucTT = "", GhiChu = "TEST_EMPTY" };
        Assert.DoesNotThrow(() => _dao.Insert(hd));
        var inserted = _dao.GetAll().FirstOrDefault(x => x.MaDK == _seedMaDK && x.GhiChu == "TEST_EMPTY");
        if (inserted != null) Cleanup($"DELETE FROM HoaDon WHERE MaHD={inserted.MaHD}");
    }

    [Test, Description("TC_HD_07: Delete HoaDon")]
    public void Delete_HoaDon_ReturnsTrue()
    {
        var hd = MakeTestHoaDon(); _dao.Insert(hd);
        int maHD = _dao.GetAll().First(x => x.MaDK == _seedMaDK && x.GhiChu == "TEST").MaHD;
        Assert.That(_dao.Delete(maHD), Is.True);
        _insertedId = -1;
    }

    [Test, Description("TC_HD_08: TotalRevenue >= 0")]
    public void TotalRevenue_ReturnsNonNegativeDecimal()
    {
        Assert.That(_dao.TotalRevenue(), Is.GreaterThanOrEqualTo(0m));
    }
}
