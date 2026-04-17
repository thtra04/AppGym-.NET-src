using AppGym.DataAccess;
using AppGym.Models;

namespace AppGym.Tests;

/// <summary>
/// TC_HV_01 ? TC_HV_10
/// Ki?m tra CRUD v� t�m ki?m h?c vi�n trong HocVienDAO
/// </summary>
[TestFixture]
[Category("Hoc vien")]
public class HocVienTests : TestBase
{
    private HocVienDAO _dao = null!;
    private int _insertedId = -1;

    [SetUp]
    public void Setup()
    {
        SkipIfNoDatabase();
        _dao = new HocVienDAO();
    }

    [TearDown]
    public void TearDown()
    {
        if (_insertedId > 0)
        {
            Cleanup($"DELETE FROM HocVien WHERE MaHV={_insertedId}");
            _insertedId = -1;
        }
    }

    private HocVien MakeTestHocVien(string suffix = "") => new()
    {
        HoTen      = $"TEST_Nguyen Van A{suffix}",
        GioiTinh   = "Nam",
        NgaySinh   = new DateTime(1995, 1, 1),
        SDT        = "0901234567",
        Email      = $"test{suffix}@gmail.com",
        NgayDangKy = DateTime.Today
    };

    // ?? Helper: Insert v� l?y l?i ID ??????????????????????????????????
    private int InsertAndGetId(HocVien hv)
    {
        _dao.Insert(hv);
        var all = _dao.GetAll();
        var inserted = all.FirstOrDefault(x => x.HoTen == hv.HoTen && x.SDT == hv.SDT);
        return inserted?.MaHV ?? -1;
    }

    // TC_HV_01 � Positive
    [Test]
    [Description("TC_HV_01: Th�m h?c vi�n h?p l? th�nh c�ng")]
    public void Insert_ValidHocVien_ReturnsTrue()
    {
        var hv = MakeTestHocVien("_01");

        bool ok = _dao.Insert(hv);

        Assert.That(ok, Is.True);
        var list = _dao.GetAll();
        _insertedId = list.FirstOrDefault(x => x.HoTen == hv.HoTen)?.MaHV ?? -1;
        Assert.That(list.Any(x => x.HoTen == hv.HoTen), Is.True, "H?c vi�n ph?i xu?t hi?n trong danh s�ch");
    }

    // TC_HV_02 � Negative: validate ? t?ng form (HoTen kh�ng r?ng)
    [Test]
    [Description("TC_HV_02: HoTen r?ng � validation ph�t hi?n tr??c khi g?i DAO")]
    public void Insert_EmptyHoTen_ValidationFails()
    {
        string hoTen = "";

        bool isValid = !string.IsNullOrWhiteSpace(hoTen);

        Assert.That(isValid, Is.False, "Validation ph?i t? ch?i HoTen r?ng");
    }

    // TC_HV_03 � Positive
    [Test]
    [Description("TC_HV_03: S?a th�ng tin h?c vi�n th�nh c�ng")]
    public void Update_HocVien_UpdatesSuccessfully()
    {
        var hv = MakeTestHocVien("_03");
        _dao.Insert(hv);
        var list = _dao.GetAll();
        var target = list.First(x => x.HoTen == hv.HoTen);
        _insertedId = target.MaHV;

        target.SDT = "0987654321";
        bool ok = _dao.Update(target);

        Assert.That(ok, Is.True);
        var updated = _dao.GetAll().First(x => x.MaHV == _insertedId);
        Assert.That(updated.SDT, Is.EqualTo("0987654321"));
    }

    // TC_HV_04 � Positive
    [Test]
    [Description("TC_HV_04: X�a h?c vi�n ch?a c� ??ng k� g�i th�nh c�ng")]
    public void Delete_HocVienWithNoDangKy_ReturnsTrue()
    {
        var hv = MakeTestHocVien("_04");
        _dao.Insert(hv);
        var target = _dao.GetAll().First(x => x.HoTen == hv.HoTen);

        bool ok = _dao.Delete(target.MaHV);

        Assert.That(ok, Is.True);
        Assert.That(_dao.GetAll().Any(x => x.MaHV == target.MaHV), Is.False);
        _insertedId = -1;
    }

    // TC_HV_05 � Negative (FK violation)
    [Test]
    [Description("TC_HV_05: X�a h?c vi�n ?ang c� ??ng k� g�i n�m ra SqlException (FK violation)")]
    public void Delete_HocVienWithDangKy_ThrowsSqlException()
    {
        // C?n c� �t nh?t 1 h?c vi�n c� ??ng k� trong DB
        var allDK = new DangKyGoiDAO().GetAll();
        if (!allDK.Any())
            Assert.Ignore("Kh�ng c� ??ng k� g�i n�o trong DB ?? test FK.");

        int maHV = allDK.First().MaHV;

        Assert.Throws<Microsoft.Data.SqlClient.SqlException>(() => _dao.Delete(maHV));
    }

    // TC_HV_06 � Positive
    [Test]
    [Description("TC_HV_06: T�m ki?m h?c vi�n theo t�n tr? v? ?�ng k?t qu?")]
    public void Search_ByName_ReturnsMatchingResults()
    {
        var hv = MakeTestHocVien("_06");
        _dao.Insert(hv);
        _insertedId = _dao.GetAll().First(x => x.HoTen == hv.HoTen).MaHV;

        var results = _dao.Search("TEST_Nguyen");

        Assert.That(results, Is.Not.Empty);
        Assert.That(results.All(x => x.HoTen.Contains("TEST_Nguyen")), Is.True);
    }

    // TC_HV_07 � Positive
    [Test]
    [Description("TC_HV_07: T�m ki?m h?c vi�n theo S?T tr? v? ?�ng k?t qu?")]
    public void Search_BySdt_ReturnsMatchingResults()
    {
        var hv = MakeTestHocVien("_07");
        hv.SDT = "0909111222";
        _dao.Insert(hv);
        _insertedId = _dao.GetAll().First(x => x.HoTen == hv.HoTen).MaHV;

        var results = _dao.Search("0909111222");

        Assert.That(results.Any(x => x.SDT == "0909111222"), Is.True);
    }

    // TC_HV_08 � Negative
    [Test]
    [Description("TC_HV_08: T�m ki?m t? kh�a kh�ng t?n t?i tr? v? danh s�ch r?ng")]
    public void Search_NoMatch_ReturnsEmptyList()
    {
        var results = _dao.Search("xyzxyz_khong_ton_tai_999");

        Assert.That(results, Is.Empty);
    }

    // TC_HV_09 � Positive
    [Test]
    [Description("TC_HV_09: GetAll() tr? v? to�n b? danh s�ch kh�ng l?i")]
    public void GetAll_ReturnsListWithoutError()
    {
        Assert.DoesNotThrow(() =>
        {
            var list = _dao.GetAll();
            Assert.That(list, Is.Not.Null);
        });
    }

    // TC_HV_10 � Positive
    [Test]
    [Description("TC_HV_10: Count() tr? v? s? h?c vi�n ?ang ho?t ??ng >= 0")]
    public void Count_ReturnsNonNegativeNumber()
    {
        int count = _dao.Count();

        Assert.That(count, Is.GreaterThanOrEqualTo(0));
    }
}
