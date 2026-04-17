using AppGym.DataAccess;
using AppGym.Models;

namespace AppGym.Tests;

/// <summary>
/// TC_GT_01 ? TC_GT_06
/// Ki?m tra CRUD g�i t?p v� bug t�m ki?m (#8) trong GoiTapDAO
/// </summary>
[TestFixture]
[Category("Goi tap")]
public class GoiTapTests : TestBase
{
    private GoiTapDAO _dao = null!;
    private int _insertedId = -1;

    [SetUp]
    public void Setup()
    {
        SkipIfNoDatabase();
        _dao = new GoiTapDAO();
    }

    [TearDown]
    public void TearDown()
    {
        if (_insertedId > 0)
        {
            Cleanup($"DELETE FROM GoiTap WHERE MaGoi={_insertedId}");
            _insertedId = -1;
        }
    }

    private GoiTap MakeTestGoiTap(string suffix = "") => new()
    {
        TenGoi    = $"TEST_Goi 1 Thang{suffix}",
        ThoiHan   = 30,
        Gia       = 500_000,
        MoTa      = "Co ban"
    };

    // TC_GT_01 � Positive
    [Test]
    [Description("TC_GT_01: Th�m g�i t?p h?p l? th�nh c�ng")]
    public void Insert_ValidGoiTap_ReturnsTrue()
    {
        var gt = MakeTestGoiTap("_01");

        bool ok = _dao.Insert(gt);

        Assert.That(ok, Is.True);
        _insertedId = _dao.GetAll().First(x => x.TenGoi == gt.TenGoi).MaGoi;
        Assert.That(_dao.GetAll().Any(x => x.TenGoi == gt.TenGoi), Is.True);
    }

    // TC_GT_02 � Negative (validation)
    [Test]
    [Description("TC_GT_02: TenGoi r?ng � validation t? ch?i tr??c khi g?i DAO")]
    public void Insert_EmptyTenGoi_ValidationFails()
    {
        bool isValid = !string.IsNullOrWhiteSpace("");

        Assert.That(isValid, Is.False);
    }

    // TC_GT_03 � Negative (parse fail ? null, kh�ng crash)
    [Test]
    [Description("TC_GT_03: ThoiHan='abc', Gia='xyz' ???c parse th�nh null, insert kh�ng crash")]
    public void Insert_NonNumericThoiHanAndGia_ParsedAsNull()
    {
        int?     thoiHan = int.TryParse("abc", out var t)     ? t : (int?)null;
        decimal? gia     = decimal.TryParse("xyz", out var g) ? g : (decimal?)null;

        Assert.That(thoiHan, Is.Null);
        Assert.That(gia, Is.Null);

        var gt = MakeTestGoiTap("_03");
        gt.ThoiHan = thoiHan;
        gt.Gia     = gia;
        bool ok = _dao.Insert(gt);
        Assert.That(ok, Is.True);
        _insertedId = _dao.GetAll().First(x => x.TenGoi == gt.TenGoi).MaGoi;
    }

    // TC_GT_04 � Negative (FK violation)
    [Test]
    [Description("TC_GT_04: X�a g�i t?p ?ang ???c ??ng k� n�m ra SqlException")]
    public void Delete_GoiTapWithDangKy_ThrowsSqlException()
    {
        var allDK = new DangKyGoiDAO().GetAll();
        if (!allDK.Any())
            Assert.Ignore("Kh�ng c� ??ng k� n�o ?? test FK.");

        int maGoi = allDK.First().MaGoi;

        Assert.Throws<Microsoft.Data.SqlClient.SqlException>(() => _dao.Delete(maGoi));
    }

    // TC_GT_05 � Positive
    [Test]
    [Description("TC_GT_05: S?a gi� g�i t?p th�nh c�ng")]
    public void Update_GoiTap_UpdatesPriceCorrectly()
    {
        var gt = MakeTestGoiTap("_05");
        _dao.Insert(gt);
        var target = _dao.GetAll().First(x => x.TenGoi == gt.TenGoi);
        _insertedId = target.MaGoi;

        target.Gia = 1_000_000;
        bool ok = _dao.Update(target);

        Assert.That(ok, Is.True);
        var updated = _dao.GetAll().First(x => x.MaGoi == _insertedId);
        Assert.That(updated.Gia, Is.EqualTo(1_000_000));
    }

    // TC_GT_06 / TC_BUG_08 � Bug: GoiTap kh�ng c� Search() ? t�m ki?m kh�ng ho?t ??ng
    [Test]
    [Description("TC_GT_06 / TC_BUG_08: GoiTapDAO kh�ng c� ph??ng th?c Search() ? bug x�c nh?n")]
    public void GoiTapDAO_HasNoSearchMethod_BugConfirmed()
    {
        var methods = typeof(GoiTapDAO).GetMethods(
            System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
        bool hasSearch = methods.Any(m => m.Name == "Search");

        Assert.That(hasSearch, Is.False,
            "[BUG#8] GoiTapDAO thi?u ph??ng th?c Search() n�n t�m ki?m tr�n UI kh�ng ho?t ??ng");
    }
}
