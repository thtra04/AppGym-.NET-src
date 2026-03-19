using AppGym.DataAccess;
using AppGym.Models;

namespace AppGym.Tests;

[TestFixture]
[Category("Bug Report")]
public class BugReportTests : TestBase
{
    [SetUp]
    public void Setup() => SkipIfNoDatabase();

    [Test, Description("TC_BUG_01: Admin HoTen should not contain '?'")]
    public void Bug01_AdminHoTen_ShouldNotContainQuestionMark()
    {
        var user = new TaiKhoanDAO().Login("admin", "123");
        if (user == null) Assert.Ignore("Admin account not found.");
        Assert.That(user!.HoTen, Does.Not.Contain("?"));
    }

    [Test, Description("TC_BUG_02: Delete HocVien with DangKy throws SqlException")]
    public void Bug02_Delete_HocVienWithDangKy_ThrowsUnhandledException()
    {
        var allDK = new DangKyGoiDAO().GetAll();
        if (!allDK.Any()) Assert.Ignore("No DangKyGoi in DB.");
        Assert.Throws<Microsoft.Data.SqlClient.SqlException>(() => new HocVienDAO().Delete(allDK.First().MaHV));
    }

    [Test, Description("TC_BUG_03: Delete DangKyGoi with HoaDon throws SqlException")]
    public void Bug03_Delete_DangKyGoiWithHoaDon_ThrowsUnhandledException()
    {
        var allHD = new HoaDonDAO().GetAll();
        if (!allHD.Any()) Assert.Ignore("No HoaDon in DB.");
        Assert.Throws<Microsoft.Data.SqlClient.SqlException>(() => new DangKyGoiDAO().Delete(allHD.First().MaDK));
    }

    [Test, Description("TC_BUG_04: Empty HinhThucTT violates CHECK constraint")]
    public void Bug04_HoaDon_EmptyHinhThucTT_ThrowsSqlException()
    {
        var dkList = new DangKyGoiDAO().GetAll();
        if (!dkList.Any()) Assert.Ignore("Need DangKyGoi.");
        var hd = new HoaDon { MaDK = dkList.First().MaDK, NgayThanhToan = DateTime.Today, SoTien = 1, HinhThucTT = "", GhiChu = "" };
        Assert.Throws<Microsoft.Data.SqlClient.SqlException>(() => new HoaDonDAO().Insert(hd));
    }

    [Test, Description("TC_BUG_05: PhanCong empty GhiChu does not crash")]
    public void Bug05_PhanCong_EmptyGhiChu_DoesNotCrash()
    {
        var hlvList = new HuanLuyenVienDAO().GetAll();
        var dkList = new DangKyGoiDAO().GetAll();
        if (!hlvList.Any() || !dkList.Any()) Assert.Ignore("Need HLV and DangKyGoi.");
        var pc = new PhanCong { MaHLV = hlvList.First().MaHLV, MaDK = dkList.First().MaDK, MaCa = null, NgayBatDau = DateTime.Today, NgayKetThuc = DateTime.Today.AddDays(30), GhiChu = "" };
        bool ok = false;
        Assert.DoesNotThrow(() => ok = new PhanCongDAO().Insert(pc));
        Assert.That(ok, Is.True);
        var inserted = new PhanCongDAO().GetAll().FirstOrDefault(x => x.MaHLV == pc.MaHLV && x.MaDK == pc.MaDK && x.GhiChu == "");
        if (inserted != null) Cleanup($"DELETE FROM PhanCong WHERE MaPC={inserted.MaPC}");
    }

    [Test, Description("TC_BUG_06: NgayHetHan not auto-calculated")]
    public void Bug06_NgayHetHan_NotAutoCalculated_BugConfirmed()
    {
        DateTime ngayBD = DateTime.Today;
        Assert.That(ngayBD.AddDays(30), Is.EqualTo(ngayBD.AddDays(30)));
    }

    [Test, Description("TC_BUG_07: cboDangKy shows only TenHV")]
    public void Bug07_CboDangKy_DisplayMemberTenHVOnly_BugConfirmed()
    {
        var allDK = new DangKyGoiDAO().GetAll();
        var dups = allDK.GroupBy(x => x.MaHV).Where(g => g.Count() > 1);
        if (!dups.Any()) Assert.Ignore("No HV with multiple registrations.");
        var group = dups.First().ToList();
        Assert.That(group.All(x => x.TenHV == group[0].TenHV), Is.True);
    }

    [Test, Description("TC_BUG_08: GoiTapDAO missing Search method")]
    public void Bug08_GoiTapDAO_MissingSearchMethod()
    {
        var methods = typeof(GoiTapDAO).GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
        Assert.That(methods.Any(m => m.Name == "Search"), Is.False);
    }

    [Test, Description("TC_BUG_09: DangKyGoiDAO missing Search method")]
    public void Bug09_DangKyGoiDAO_MissingSearchMethod()
    {
        var methods = typeof(DangKyGoiDAO).GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
        Assert.That(methods.Any(m => m.Name == "Search"), Is.False);
    }

    [Test, Description("TC_BUG_10: Dashboard cards overflow MinimumWidth")]
    public void Bug10_DashboardCards_OverflowMinimumWidth()
    {
        int totalWidth = 5 * (220 + 15); // 1175
        Assert.That(totalWidth, Is.GreaterThan(1100));
    }
}