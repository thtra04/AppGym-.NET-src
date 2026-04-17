using AppGym.DataAccess;

namespace AppGym.Tests;

/// <summary>
/// TC_LOGIN_01 ? TC_LOGIN_07
/// Ki?m tra logic ??ng nh?p trong TaiKhoanDAO.Login()
/// </summary>
[TestFixture]
[Category("Dang nhap")]
public class LoginTests : TestBase
{
    private TaiKhoanDAO _dao = null!;

    [SetUp]
    public void Setup()
    {
        SkipIfNoDatabase();
        _dao = new TaiKhoanDAO();
    }

    // TC_LOGIN_01 � Positive
    [Test]
    [Description("TC_LOGIN_01: ??ng nh?p th�nh c�ng v?i t�i kho?n h?p l? admin/123")]
    public void Login_ValidCredentials_ReturnsUser()
    {
        var user = _dao.Login("admin", "123");

        Assert.That(user, Is.Not.Null, "User ph?i kh�ng null khi ??ng nh?p ?�ng");
        Assert.That(user!.TenDangNhap, Is.EqualTo("admin"));
    }

    // TC_LOGIN_02 � Negative
    [Test]
    [Description("TC_LOGIN_02: ??ng nh?p sai m?t kh?u tr? v? null")]
    public void Login_WrongPassword_ReturnsNull()
    {
        var user = _dao.Login("admin", "sai123");

        Assert.That(user, Is.Null, "Ph?i tr? v? null khi m?t kh?u sai");
    }

    // TC_LOGIN_03 � Negative
    [Test]
    [Description("TC_LOGIN_03: T�n ??ng nh?p r?ng tr? v? null")]
    public void Login_EmptyUsername_ReturnsNull()
    {
        var user = _dao.Login("", "123");

        Assert.That(user, Is.Null);
    }

    // TC_LOGIN_04 � Negative
    [Test]
    [Description("TC_LOGIN_04: M?t kh?u r?ng tr? v? null")]
    public void Login_EmptyPassword_ReturnsNull()
    {
        var user = _dao.Login("admin", "");

        Assert.That(user, Is.Null);
    }



    // TC_LOGIN_06 � Positive (logic)
    [Test]
    [Description("TC_LOGIN_06: Login tr? v? ?�ng th�ng tin VaiTro v� HoTen")]
    public void Login_ValidCredentials_ReturnsCorrectRoleAndName()
    {
        var user = _dao.Login("admin", "123");

        Assert.That(user, Is.Not.Null);
        Assert.That(user!.VaiTro, Is.EqualTo("Admin").IgnoreCase);
        Assert.That(user.HoTen, Is.Not.Empty);
    }

    // TC_LOGIN_07 � Negative
    [Test]
    [Description("TC_LOGIN_07: T�n ??ng nh?p kh�ng t?n t?i trong h? th?ng tr? v? null")]
    public void Login_NonExistentUsername_ReturnsNull()
    {
        var user = _dao.Login("khongtontai_xyz_999", "123");

        Assert.That(user, Is.Null);
    }
}

