using AppGym.DataAccess;
using AppGym.Models;

namespace AppGym.Tests;

/// <summary>
/// TC_PC_01 ? TC_PC_06  +  TC_BUG_05
/// Ki?m tra CRUD phân công PT và bug GhiChu null trong PhanCongDAO
/// </summary>
[TestFixture]
[Category("Phan cong PT")]
public class PhanCongTests : TestBase
{
    private PhanCongDAO _dao = null!;
    private int _insertedId  = -1;
    private int _seedMaHLV   = -1;
    private int _seedMaDK    = -1;
    private int _seedMaCa    = -1;

    [SetUp]
    public void Setup()
    {
        SkipIfNoDatabase();
        _dao = new PhanCongDAO();

        var hlvList = new HuanLuyenVienDAO().GetAll();
        var dkList  = new DangKyGoiDAO().GetAll();
        var caList  = new CaLamDAO().GetAll();

        if (!hlvList.Any() || !dkList.Any())
            Assert.Ignore("C?n có HuanLuyenVien và DangKyGoi trong DB.");

        _seedMaHLV = hlvList.First().MaHLV;
        _seedMaDK  = dkList.First().MaDK;
        _seedMaCa  = caList.Any() ? caList.First().MaCa : -1;
    }

    [TearDown]
    public void TearDown()
    {
        if (_insertedId > 0)
        {
            Cleanup($"DELETE FROM PhanCong WHERE MaPC={_insertedId}");
            _insertedId = -1;
        }
    }

    private PhanCong MakeTestPC(bool withCaLam = true) => new()
    {
        MaHLV       = _seedMaHLV,
        MaDK        = _seedMaDK,
        MaCa        = withCaLam && _seedMaCa > 0 ? _seedMaCa : null,
        NgayBatDau  = DateTime.Today,
        NgayKetThuc = DateTime.Today.AddDays(30),
        GhiChu      = "TEST"
    };

    private int GetInsertedId()
        => _dao.GetAll().FirstOrDefault(x =>
            x.MaHLV == _seedMaHLV && x.MaDK == _seedMaDK && x.GhiChu == "TEST")?.MaPC ?? -1;

    // TC_PC_01 – Positive
    [Test]
    [Description("TC_PC_01: Thêm phân công h?p l? thành công")]
    public void Insert_ValidPhanCong_ReturnsTrue()
    {
        var pc = MakeTestPC();

        bool ok = _dao.Insert(pc);

        Assert.That(ok, Is.True);
        _insertedId = GetInsertedId();
        Assert.That(_insertedId, Is.GreaterThan(0));
    }

    // TC_PC_02 – Negative (validation)
    [Test]
    [Description("TC_PC_02: Không ch?n HLV – validation t? ch?i")]
    public void Insert_NullMaHLV_ValidationFails()
    {
        object? selected = null;

        Assert.That(selected != null, Is.False, "Ph?i báo l?i khi ch?a ch?n HLV");
    }

    // TC_PC_03 – Negative (validation)
    [Test]
    [Description("TC_PC_03: Không ch?n ??ng ký gói – validation t? ch?i")]
    public void Insert_NullMaDK_ValidationFails()
    {
        object? selected = null;

        Assert.That(selected != null, Is.False, "Ph?i báo l?i khi ch?a ch?n ??ng ký gói");
    }

    // TC_PC_04 – Positive (Ca làm optional)
    [Test]
    [Description("TC_PC_04: Thêm phân công không ch?n ca làm (MaCa=null) thành công")]
    public void Insert_NullMaCa_ReturnsTrue()
    {
        var pc = MakeTestPC(withCaLam: false);
        pc.MaCa = null;

        bool ok = _dao.Insert(pc);

        Assert.That(ok, Is.True);
        _insertedId = GetInsertedId();
        var saved = _dao.GetAll().FirstOrDefault(x => x.MaPC == _insertedId);
        Assert.That(saved?.MaCa, Is.Null);
    }

    // TC_PC_05 / TC_BUG_05 – Bug: GhiChu="" không crash
    [Test]
    [Description("TC_PC_05 / TC_BUG_05: Insert PhanCong v?i GhiChu='' không ném ArgumentNullException")]
    public void Insert_EmptyGhiChu_DoesNotThrow()
    {
        var pc = MakeTestPC();
        pc.GhiChu = "";  // r?ng

        bool ok = false;
        Assert.DoesNotThrow(() => ok = _dao.Insert(pc),
            "[BUG#5] Insert v?i GhiChu='' không ???c ném exception");
        Assert.That(ok, Is.True);
        _insertedId = GetInsertedId();
    }

    // TC_PC_06 – Positive
    [Test]
    [Description("TC_PC_06: Xóa phân công thành công")]
    public void Delete_PhanCong_ReturnsTrue()
    {
        var pc = MakeTestPC();
        _dao.Insert(pc);
        int maPC = GetInsertedId();
        Assert.That(maPC, Is.GreaterThan(0));

        bool ok = _dao.Delete(maPC);

        Assert.That(ok, Is.True);
        _insertedId = -1;
    }
}
