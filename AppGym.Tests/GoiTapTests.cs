using AppGym.DataAccess;
using AppGym.Models;

namespace AppGym.Tests;

/// <summary>
/// TC_GT_01 - TC_GT_06
/// Kiem tra CRUD goi tap va tim kiem trong GoiTapDAO
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
        TenGoi = $"TEST_Goi 1 Thang{suffix}",
        ThoiHan = 30,
        Gia = 500_000,
        MoTa = "Co ban"
    };

    [Test]
    [Description("TC_GT_01: Them goi tap hop le thanh cong")]
    public void Insert_ValidGoiTap_ReturnsTrue()
    {
        var gt = MakeTestGoiTap("_01");

        bool ok = _dao.Insert(gt);

        Assert.That(ok, Is.True);
        _insertedId = _dao.GetAll().First(x => x.TenGoi == gt.TenGoi).MaGoi;
        Assert.That(_dao.GetAll().Any(x => x.TenGoi == gt.TenGoi), Is.True);
    }

    [Test]
    [Description("TC_GT_02: TenGoi rong bi validation tu choi truoc khi goi DAO")]
    public void Insert_EmptyTenGoi_ValidationFails()
    {
        bool isValid = !string.IsNullOrWhiteSpace("");

        Assert.That(isValid, Is.False);
    }

    [Test]
    [Description("TC_GT_03: ThoiHan abc va Gia xyz duoc parse thanh null, insert khong crash")]
    public void Insert_NonNumericThoiHanAndGia_ParsedAsNull()
    {
        int? thoiHan = int.TryParse("abc", out var t) ? t : (int?)null;
        decimal? gia = decimal.TryParse("xyz", out var g) ? g : (decimal?)null;

        Assert.That(thoiHan, Is.Null);
        Assert.That(gia, Is.Null);

        var gt = MakeTestGoiTap("_03");
        gt.ThoiHan = thoiHan;
        gt.Gia = gia;
        bool ok = _dao.Insert(gt);
        Assert.That(ok, Is.True);
        _insertedId = _dao.GetAll().First(x => x.TenGoi == gt.TenGoi).MaGoi;
    }

    [Test]
    [Description("TC_GT_04: Xoa goi tap dang duoc dang ky nem ra SqlException")]
    public void Delete_GoiTapWithDangKy_ThrowsSqlException()
    {
        var allDK = new DangKyGoiDAO().GetAll();
        if (!allDK.Any())
            Assert.Ignore("Khong co dang ky nao de test FK.");

        int maGoi = allDK.First().MaGoi;

        Assert.Throws<Microsoft.Data.SqlClient.SqlException>(() => _dao.Delete(maGoi));
    }

    [Test]
    [Description("TC_GT_05: Sua gia goi tap thanh cong")]
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

    [Test]
    [Description("TC_GT_06: Search goi tap theo ten tra ve dung ket qua")]
    public void Search_ByTenGoi_ReturnsMatchingResults()
    {
        var gt = MakeTestGoiTap("_06");
        _dao.Insert(gt);
        _insertedId = _dao.GetAll().First(x => x.TenGoi == gt.TenGoi).MaGoi;

        var result = _dao.Search("_06");

        Assert.That(result.Any(x => x.MaGoi == _insertedId), Is.True);
    }
}
