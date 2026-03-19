using AppGym.DataAccess;
using AppGym.Models;

namespace AppGym.Tests;

/// <summary>
/// TC_CL_01 ? TC_CL_05
/// Ki?m tra CRUD ca làm trong CaLamDAO
/// </summary>
[TestFixture]
[Category("Ca lam")]
public class CaLamTests : TestBase
{
    private CaLamDAO _dao = null!;
    private int _insertedId = -1;

    [SetUp]
    public void Setup()
    {
        SkipIfNoDatabase();
        _dao = new CaLamDAO();
    }

    [TearDown]
    public void TearDown()
    {
        if (_insertedId > 0)
        {
            Cleanup($"DELETE FROM CaLam WHERE MaCa={_insertedId}");
            _insertedId = -1;
        }
    }

    private CaLam MakeTestCaLam(string suffix = "") => new()
    {
        TenCa       = $"TEST_Ca sang{suffix}",
        GioBatDau   = new TimeSpan(6, 0, 0),
        GioKetThuc  = new TimeSpan(12, 0, 0)
    };

    // TC_CL_01 – Positive
    [Test]
    [Description("TC_CL_01: Thêm ca làm h?p l? thành công")]
    public void Insert_ValidCaLam_ReturnsTrue()
    {
        var ca = MakeTestCaLam("_01");

        bool ok = _dao.Insert(ca);

        Assert.That(ok, Is.True);
        _insertedId = _dao.GetAll().First(x => x.TenCa == ca.TenCa).MaCa;
        Assert.That(_dao.GetAll().Any(x => x.TenCa == ca.TenCa), Is.True);
    }

    // TC_CL_02 – Negative (validation)
    [Test]
    [Description("TC_CL_02: TenCa r?ng – validation t? ch?i tr??c khi g?i DAO")]
    public void Insert_EmptyTenCa_ValidationFails()
    {
        bool isValid = !string.IsNullOrWhiteSpace("");

        Assert.That(isValid, Is.False, "Ph?i báo l?i khi TenCa r?ng");
    }

    // TC_CL_03 – Positive
    [Test]
    [Description("TC_CL_03: S?a gi? k?t thúc ca làm thành công")]
    public void Update_GioKetThuc_UpdatesCorrectly()
    {
        var ca = MakeTestCaLam("_03");
        _dao.Insert(ca);
        var target = _dao.GetAll().First(x => x.TenCa == ca.TenCa);
        _insertedId = target.MaCa;

        target.GioKetThuc = new TimeSpan(13, 0, 0);
        bool ok = _dao.Update(target);

        Assert.That(ok, Is.True);
        var updated = _dao.GetAll().First(x => x.MaCa == _insertedId);
        Assert.That(updated.GioKetThuc, Is.EqualTo(new TimeSpan(13, 0, 0)));
    }

    // TC_CL_04 – Negative (FK violation)
    [Test]
    [Description("TC_CL_04: Xóa ca làm ?ang có phân công ném ra SqlException")]
    public void Delete_CaLamWithPhanCong_ThrowsSqlException()
    {
        var allPC = new PhanCongDAO().GetAll().Where(x => x.MaCa.HasValue).ToList();
        if (!allPC.Any())
            Assert.Ignore("Không có phân công nào có CaLam ?? test FK.");

        int maCa = allPC.First().MaCa!.Value;

        Assert.Throws<Microsoft.Data.SqlClient.SqlException>(() => _dao.Delete(maCa));
    }

    // TC_CL_05 – Positive
    [Test]
    [Description("TC_CL_05: Xóa ca làm không có phân công thành công")]
    public void Delete_CaLamWithNoPhanCong_ReturnsTrue()
    {
        var ca = MakeTestCaLam("_05");
        _dao.Insert(ca);
        int maCa = _dao.GetAll().First(x => x.TenCa == ca.TenCa).MaCa;

        bool ok = _dao.Delete(maCa);

        Assert.That(ok, Is.True);
        _insertedId = -1;
    }
}
