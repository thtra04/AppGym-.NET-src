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

    // TC_LOGIN_01 – Positive
    [Test]
    [Description("TC_LOGIN_01: ??ng nh?p thành công v?i tài kho?n h?p l? admin/123")]
    public void Login_ValidCredentials_ReturnsUser()
    {
        var user = _dao.Login("admin", "123");

        Assert.That(user, Is.Not.Null, "User ph?i không null khi ??ng nh?p ?úng");
        Assert.That(user!.TenDangNhap, Is.EqualTo("admin"));
        Assert.That(user.TrangThai, Is.True);
    }

    // TC_LOGIN_02 – Negative
    [Test]
    [Description("TC_LOGIN_02: ??ng nh?p sai m?t kh?u tr? v? null")]
    public void Login_WrongPassword_ReturnsNull()
    {
        var user = _dao.Login("admin", "sai123");

        Assert.That(user, Is.Null, "Ph?i tr? v? null khi m?t kh?u sai");
    }

    // TC_LOGIN_03 – Negative
    [Test]
    [Description("TC_LOGIN_03: Tên ??ng nh?p r?ng tr? v? null")]
    public void Login_EmptyUsername_ReturnsNull()
    {
        var user = _dao.Login("", "123");

        Assert.That(user, Is.Null);
    }

    // TC_LOGIN_04 – Negative
    [Test]
    [Description("TC_LOGIN_04: M?t kh?u r?ng tr? v? null")]
    public void Login_EmptyPassword_ReturnsNull()
    {
        var user = _dao.Login("admin", "");

        Assert.That(user, Is.Null);
    }

    // TC_LOGIN_05 – Negative
    [Test]
    [Description("TC_LOGIN_05: Tài kho?n b? vô hi?u hóa (TrangThai=0) không ??ng nh?p ???c")]
    public void Login_DisabledAccount_ReturnsNull()
    {
        // Setup: t?o tài kho?n b? khóa
        Cleanup("DELETE FROM TaiKhoan WHERE TenDangNhap='user_bi_khoa'");
        using var conn = new Microsoft.Data.SqlClient.SqlConnection(ConnStr);
        conn.Open();
        using var cmd = new Microsoft.Data.SqlClient.SqlCommand(@"
            DECLARE @s UNIQUEIDENTIFIER = NEWID();
            INSERT INTO TaiKhoan(TenDangNhap, Salt, MatKhauHash, HoTen, VaiTro, TrangThai)
            VALUES('user_bi_khoa', CAST(@s AS VARBINARY(32)),
                   HASHBYTES('SHA2_512', CONVERT(varbinary(200), '123' + '|' + CONVERT(varchar(50), @s))),
                   N'User Bi Khoa', N'NhanVien', 0)", conn);
        cmd.ExecuteNonQuery();

        var user = _dao.Login("user_bi_khoa", "123");

        Assert.That(user, Is.Null, "Tài kho?n b? khóa không ???c ??ng nh?p");
    }

    [TearDown]
    public void Cleanup_DisabledAccount()
    {
        Cleanup("DELETE FROM TaiKhoan WHERE TenDangNhap='user_bi_khoa'");
    }

    // TC_LOGIN_06 – Positive (logic)
    [Test]
    [Description("TC_LOGIN_06: Login tr? v? ?úng thông tin VaiTro và HoTen")]
    public void Login_ValidCredentials_ReturnsCorrectRoleAndName()
    {
        var user = _dao.Login("admin", "123");

        Assert.That(user, Is.Not.Null);
        Assert.That(user!.VaiTro, Is.EqualTo("Admin").IgnoreCase);
        Assert.That(user.HoTen, Is.Not.Empty);
    }

    // TC_LOGIN_07 – Negative
    [Test]
    [Description("TC_LOGIN_07: Tên ??ng nh?p không t?n t?i trong h? th?ng tr? v? null")]
    public void Login_NonExistentUsername_ReturnsNull()
    {
        var user = _dao.Login("khongtontai_xyz_999", "123");

        Assert.That(user, Is.Null);
    }
}
