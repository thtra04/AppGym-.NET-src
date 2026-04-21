using AppGym.DataAccess;
using AppGym.Models;

namespace AppGym.Tests;

[TestFixture]
[Category("Tai khoan")]
public class TaiKhoanTests : TestBase
{
    private TaiKhoanDAO _dao = null!;
    private int _insertedId = -1;

    [SetUp]
    public void Setup()
    {
        SkipIfNoDatabase();
        _dao = new TaiKhoanDAO();
    }

    [TearDown]
    public void TearDown()
    {
        if (_insertedId > 0)
        {
            Cleanup($"DELETE FROM TaiKhoan WHERE MaTK={_insertedId}");
            _insertedId = -1;
        }
    }

    private TaiKhoan MakeTestAccount(string suffix = "") => new()
    {
        TenDangNhap = $"test_tk_{Guid.NewGuid():N}".Substring(0, 14) + suffix,
        HoTen = $"Tai Khoan Test {suffix}",
        VaiTro = "NhanVien"
    };

    [Test]
    public void GetAll_ReturnsAccountList()
    {
        var list = _dao.GetAll();

        Assert.That(list, Is.Not.Null);
        Assert.That(list.Count, Is.GreaterThanOrEqualTo(1));
    }

    [Test]
    public void Insert_ValidAccount_ReturnsTrue()
    {
        var account = MakeTestAccount("01");

        bool ok = _dao.Insert(account, "123456");

        Assert.That(ok, Is.True);
        var inserted = _dao.Login(account.TenDangNhap, "123456");
        Assert.That(inserted, Is.Not.Null);
        _insertedId = inserted!.MaTK;
    }

    [Test]
    public void Search_ByUsername_ReturnsMatchingAccount()
    {
        var account = MakeTestAccount("02");
        _dao.Insert(account, "123456");
        var inserted = _dao.Login(account.TenDangNhap, "123456");
        _insertedId = inserted!.MaTK;

        var result = _dao.Search(account.TenDangNhap);

        Assert.That(result.Any(x => x.MaTK == _insertedId), Is.True);
    }

    [Test]
    public void Update_HoTenAndRole_ReturnsTrue()
    {
        var account = MakeTestAccount("03");
        _dao.Insert(account, "123456");
        var inserted = _dao.Login(account.TenDangNhap, "123456");
        _insertedId = inserted!.MaTK;

        inserted.HoTen = "Da Cap Nhat";
        inserted.VaiTro = "NhanVien";
        bool ok = _dao.Update(inserted);

        Assert.That(ok, Is.True);
        var updated = _dao.GetById(_insertedId);
        Assert.That(updated!.HoTen, Is.EqualTo("Da Cap Nhat"));
    }

    [Test]
    public void ChangePassword_AllowsLoginWithNewPassword()
    {
        var account = MakeTestAccount("04");
        _dao.Insert(account, "123456");
        var inserted = _dao.Login(account.TenDangNhap, "123456");
        _insertedId = inserted!.MaTK;

        _dao.ChangePassword(_insertedId, "654321");
        var relogin = _dao.Login(account.TenDangNhap, "654321");

        Assert.That(relogin, Is.Not.Null);
        Assert.That(relogin!.MaTK, Is.EqualTo(_insertedId));
    }

    [Test]
    public void Update_CanToggleAccountStatus()
    {
        var account = MakeTestAccount("05");
        _dao.Insert(account, "123456");
        var inserted = _dao.Login(account.TenDangNhap, "123456");
        _insertedId = inserted!.MaTK;

        inserted.HoatDong = false;
        bool ok = _dao.Update(inserted);

        Assert.That(ok, Is.True);
        var updated = _dao.GetById(_insertedId);
        Assert.That(updated, Is.Not.Null);
        Assert.That(updated!.HoatDong, Is.False);
    }

    [Test]
    public void Login_InactiveAccount_ReturnsNull()
    {
        var account = MakeTestAccount("06");
        _dao.Insert(account, "123456");
        var inserted = _dao.Login(account.TenDangNhap, "123456");
        _insertedId = inserted!.MaTK;

        inserted.HoatDong = false;
        _dao.Update(inserted);

        var relogin = _dao.Login(account.TenDangNhap, "123456");

        Assert.That(relogin, Is.Null);
    }
}
