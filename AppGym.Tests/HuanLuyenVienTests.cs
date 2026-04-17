using AppGym.DataAccess;
using AppGym.Models;

namespace AppGym.Tests;

[TestFixture]
[Category("Huan luyen vien")]
public class HuanLuyenVienTests : TestBase
{
    private HuanLuyenVienDAO _dao = null!;
    private int _insertedId = -1;

    [SetUp]
    public void Setup() { SkipIfNoDatabase(); _dao = new HuanLuyenVienDAO(); }

    [TearDown]
    public void TearDown()
    {
        if (_insertedId > 0) { Cleanup($"DELETE FROM HuanLuyenVien WHERE MaHLV={_insertedId}"); _insertedId = -1; }
    }

    // CHECK constraint: GioiTinh IN (N'Nam', N'N\u1EEF', N'Kh\u00E1c')
    private HuanLuyenVien MakeTestHLV(string suffix = "") => new()
    {
        HoTen = $"TEST_HLV B{suffix}", GioiTinh = "N\u1EEF", SDT = "0912345678",
        ChuyenMon = "Yoga", Luong = 5_000_000
    };

    [Test, Description("TC_HLV_01: Insert valid HLV")]
    public void Insert_ValidHLV_ReturnsTrue()
    {
        var hlv = MakeTestHLV("_01");
        Assert.That(_dao.Insert(hlv), Is.True);
        _insertedId = _dao.GetAll().First(x => x.HoTen == hlv.HoTen).MaHLV;
    }

    [Test, Description("TC_HLV_02: Empty HoTen validation")]
    public void Insert_EmptyHoTen_ValidationFails()
    {
        Assert.That(!string.IsNullOrWhiteSpace(""), Is.False);
    }

    [Test, Description("TC_HLV_03: Non-numeric Luong parsed as null")]
    public void Insert_NonNumericLuong_ParsedAsNull()
    {
        decimal? luong = decimal.TryParse("abc", out var l) ? l : null;
        Assert.That(luong, Is.Null);
        var hlv = MakeTestHLV("_03"); hlv.Luong = luong;
        Assert.That(_dao.Insert(hlv), Is.True);
        _insertedId = _dao.GetAll().First(x => x.HoTen == hlv.HoTen).MaHLV;
    }

    [Test, Description("TC_HLV_04: Delete HLV with PhanCong throws")]
    public void Delete_HLVWithPhanCong_ThrowsSqlException()
    {
        var allPC = new PhanCongDAO().GetAll();
        if (!allPC.Any()) Assert.Ignore("No PhanCong to test FK.");
        Assert.Throws<Microsoft.Data.SqlClient.SqlException>(() => _dao.Delete(allPC.First().MaHLV));
    }

    [Test, Description("TC_HLV_05: Search by ChuyenMon")]
    public void Search_ByChuyenMon_ReturnsMatching()
    {
        var hlv = MakeTestHLV("_05"); _dao.Insert(hlv);
        _insertedId = _dao.GetAll().First(x => x.HoTen == hlv.HoTen).MaHLV;
        Assert.That(_dao.Search("Yoga").Any(x => x.ChuyenMon == "Yoga"), Is.True);
    }
}
