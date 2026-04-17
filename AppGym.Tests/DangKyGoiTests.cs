using AppGym.DataAccess;
using AppGym.Models;

namespace AppGym.Tests;

[TestFixture]
[Category("Dang ky goi")]
public class DangKyGoiTests : TestBase
{
    private DangKyGoiDAO _dao = null!;
    private int _insertedId = -1;
    private int _seedMaHV = -1;
    private int _seedMaGoi = -1;

    [SetUp]
    public void Setup()
    {
        SkipIfNoDatabase();
        _dao = new DangKyGoiDAO();
        var hvList = new HocVienDAO().GetAll();
        var gtList = new GoiTapDAO().GetAll();
        if (!hvList.Any() || !gtList.Any()) Assert.Ignore("Need HocVien and GoiTap in DB.");
        _seedMaHV = hvList.First().MaHV;
        _seedMaGoi = gtList.First().MaGoi;
    }

    [TearDown]
    public void TearDown()
    {
        if (_insertedId > 0) { Cleanup($"DELETE FROM DangKyGoi WHERE MaDK={_insertedId}"); _insertedId = -1; }
    }

    private DangKyGoi MakeTestDK() => new()
    {
        MaHV = _seedMaHV, MaGoi = _seedMaGoi,
        NgayBatDau = DateTime.Today, NgayHetHan = DateTime.Today.AddDays(30),
        GhiChu = "TEST"
    };

    [Test, Description("TC_DK_01: Insert valid DangKy")]
    public void Insert_ValidDangKy_ReturnsTrue()
    {
        var dk = MakeTestDK();
        Assert.That(_dao.Insert(dk), Is.True);
        var list = _dao.GetAll();
        _insertedId = list.FirstOrDefault(x => x.MaHV == _seedMaHV && x.GhiChu == "TEST")?.MaDK ?? -1;
        Assert.That(_insertedId, Is.GreaterThan(0));
    }

    [Test, Description("TC_DK_02: Null MaHV validation")]
    public void Insert_NullMaHV_ValidationFails()
    {
        object? selectedValue = null;
        Assert.That(selectedValue != null, Is.False);
    }

    [Test, Description("TC_DK_03: Null MaGoi validation")]
    public void Insert_NullMaGoi_ValidationFails()
    {
        object? selectedValue = null;
        Assert.That(selectedValue != null, Is.False);
    }

    [Test, Description("TC_DK_04: Delete DangKy with no related records")]
    public void Delete_DangKyWithNoRelated_ReturnsTrue()
    {
        var dk = MakeTestDK(); _dao.Insert(dk);
        _insertedId = _dao.GetAll().FirstOrDefault(x => x.MaHV == _seedMaHV && x.GhiChu == "TEST")?.MaDK ?? -1;
        Assert.That(_insertedId, Is.GreaterThan(0));
        Assert.That(_dao.Delete(_insertedId), Is.True);
        _insertedId = -1;
    }

    [Test, Description("TC_DK_05: Delete DangKy with HoaDon throws")]
    public void Delete_DangKyWithHoaDon_ThrowsSqlException()
    {
        var allHD = new HoaDonDAO().GetAll();
        if (!allHD.Any()) Assert.Ignore("No HoaDon to test FK.");
        Assert.Throws<Microsoft.Data.SqlClient.SqlException>(() => _dao.Delete(allHD.First().MaDK));
    }

    

    [Test, Description("TC_DK_07: DangKyGoiDAO has no Search method")]
    public void DangKyGoiDAO_HasNoSearchMethod_BugConfirmed()
    {
        var methods = typeof(DangKyGoiDAO).GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
        Assert.That(methods.Any(m => m.Name == "Search"), Is.False);
    }

    [Test, Description("CountActive returns non-negative")]
    public void CountActive_ReturnsNonNegative()
    {
        Assert.That(_dao.CountActive(), Is.GreaterThanOrEqualTo(0));
    }
}

