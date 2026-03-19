# AppGym.Tests

Project NUnit test cho ?ng d?ng **AppGym – Qu?n lý phòng gym**.

## C?u trúc th? m?c

```
AppGym.Tests/
??? TestBase.cs              # Base class: connection string, helper SkipIfNoDatabase(), Cleanup()
??? LoginTests.cs            # TC_LOGIN_01 ? TC_LOGIN_07
??? HocVienTests.cs          # TC_HV_01    ? TC_HV_10
??? HuanLuyenVienTests.cs    # TC_HLV_01   ? TC_HLV_05
??? GoiTapTests.cs           # TC_GT_01    ? TC_GT_06
??? DangKyGoiTests.cs        # TC_DK_01    ? TC_DK_07
??? PhanCongTests.cs         # TC_PC_01    ? TC_PC_06
??? HoaDonTests.cs           # TC_HD_01    ? TC_HD_08
??? CaLamTests.cs            # TC_CL_01    ? TC_CL_05
??? DashboardTests.cs        # TC_DB_01    ? TC_DB_05
??? BugReportTests.cs        # TC_BUG_01   ? TC_BUG_10
```

## Danh sách Test Case (71 tests)

| File | Category | S? TC | Mô t? |
|------|----------|-------|-------|
| LoginTests | Dang nhap | 7 | ??ng nh?p ?úng, sai m?t kh?u, tr?ng, tài kho?n khóa |
| HocVienTests | Hoc vien | 10 | CRUD + tìm ki?m h?c viên |
| HuanLuyenVienTests | Huan luyen vien | 5 | CRUD + tìm ki?m HLV |
| GoiTapTests | Goi tap | 6 | CRUD + xác nh?n bug #8 (thi?u Search) |
| DangKyGoiTests | Dang ky goi | 8 | CRUD + xác nh?n bug #9 (thi?u Search) |
| PhanCongTests | Phan cong PT | 6 | CRUD + bug #5 (GhiChu null) |
| HoaDonTests | Hoa don | 8 | CRUD + validation SoTien + bug #4 (GhiChu null) |
| CaLamTests | Ca lam | 5 | CRUD ca làm |
| DashboardTests | Dashboard | 6 | Ki?m tra các Count, TotalRevenue, Take(10) |
| BugReportTests | Bug Report | 10 | Xác nh?n toàn b? 10 bug ?ã phát hi?n |

## Yêu c?u tr??c khi ch?y

1. **SQL Server** ?ang ch?y c?c b? (`Server=.`)
2. **Database `GymManagementDB`** ?ã ???c t?o b?ng `setup_db.sql`
3. Có ít nh?t 1 b?n ghi trong các b?ng chính (HocVien, HuanLuyenVien, GoiTap, CaLam)

> N?u không k?t n?i ???c DB, t?t c? test s? t? ??ng **Skip** (không Fail).

## Cách ch?y

### Ch?y t?t c? test
```powershell
dotnet test AppGym.Tests\AppGym.Tests.csproj
```

### Ch?y theo category (module)
```powershell
# Ch? ch?y test ??ng nh?p
dotnet test --filter "Category=Dang nhap"

# Ch? ch?y test h?c viên
dotnet test --filter "Category=Hoc vien"

# Ch? ch?y bug report
dotnet test --filter "Category=Bug Report"
```

### Ch?y theo tên test c? th?
```powershell
dotnet test --filter "TestName=Login_ValidCredentials_ReturnsUser"
```

### Ch?y v?i output chi ti?t
```powershell
dotnet test --logger "console;verbosity=detailed"
```

## ??i Connection String

M? file `TestBase.cs`, s?a h?ng s? `ConnStr`:

```csharp
protected const string ConnStr =
    @"Server=TEN_SERVER;Database=GymManagementDB;Trusted_Connection=True;TrustServerCertificate=True;";
```

## L?u ý

- Các test **Positive** s? insert d? li?u test r?i **t? d?n** trong `TearDown`.
- Các test **Bug Report** xác nh?n bug t?n t?i — khi bug ???c s?a, các test này s? Fail (?ây là ch? ý).
- Test `TC_BUG_01` s? Pass khi fix l?i encoding trong `setup_db.sql` (??i `N'Qu?n tr? viên'` ? `N'Qu?n tr? viên'`).
