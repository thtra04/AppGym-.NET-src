# ExportTestCases.ps1 - Chay unit test va xuat ket qua ra Excel theo mau test
# Su dung: powershell -ExecutionPolicy Bypass -File ExportTestCases.ps1

$projectDir = $PSScriptRoot
$testProject = "$projectDir\AppGym.Tests\AppGym.Tests.csproj"
$trxDir = "$projectDir\AppGym.Tests\TestResults"
$trxFile = "$trxDir\TestResults.trx"
$outPath = "$projectDir\TestCases_AppGym.xlsx"

# ---- 1. Chay dotnet test ----
Write-Host "=== Chay unit test ===" -ForegroundColor Cyan
if (Test-Path $trxDir) { Remove-Item "$trxDir\*" -Force -Recurse }
dotnet test $testProject --logger "trx;LogFileName=TestResults.trx" --results-directory $trxDir --no-build 2>&1 | Write-Host

# ---- 2. Parse TRX ----
Write-Host "=== Parse ket qua TRX ===" -ForegroundColor Cyan
[xml]$trx = Get-Content $trxFile
$ns = @{t="http://microsoft.com/schemas/VisualStudio/TeamTest/2010"}

# Build map: testId -> className from UnitTest definitions
$classMap = @{}
$testDefs = Select-Xml -Xml $trx -XPath "//t:UnitTest" -Namespace $ns | ForEach-Object { $_.Node }
foreach ($td in $testDefs) {
    $testId = $td.id
    $tmNode = Select-Xml -Xml $td -XPath "t:TestMethod" -Namespace $ns
    if ($tmNode) { $classMap[$testId] = $tmNode.Node.className }
}

# Build map: "ClassName.MethodName" -> outcome
$resultMap = @{}
$resultNodes = Select-Xml -Xml $trx -XPath "//t:UnitTestResult" -Namespace $ns | ForEach-Object { $_.Node }
foreach ($r in $resultNodes) {
    $testId = $r.testId
    $cls = $classMap[$testId]
    if ($cls) {
        # className is like "AppGym.Tests.LoginTests" - take last part
        $shortClass = $cls.Split(".")[-1]
        $key = "$shortClass.$($r.testName)"
        $resultMap[$key] = $r.outcome
    }
    # Also store by plain testName (for unique names)
    $resultMap[$r.testName] = $r.outcome
}

# ---- 3. Mapping test method -> TC ID + module ----
$testcases = @(
  # Module "Dang nhap"
  @{Module="Dang nhap"; TC="TC_LOGIN_01"; Name="Dang nhap thanh cong voi tai khoan hop le"; Type="Positive"; Input="TenDangNhap=admin, MatKhau=123"; Expected="Dang nhap thanh cong, mo FormMain"; Method="Login_ValidCredentials_ReturnsUser"; Class="LoginTests"},
  @{Module="Dang nhap"; TC="TC_LOGIN_02"; Name="Dang nhap sai mat khau"; Type="Negative"; Input="TenDangNhap=admin, MatKhau=sai123"; Expected="Tra ve null khi mat khau sai"; Method="Login_WrongPassword_ReturnsNull"; Class="LoginTests"},
  @{Module="Dang nhap"; TC="TC_LOGIN_03"; Name="De trong ten dang nhap"; Type="Negative"; Input="TenDangNhap='', MatKhau=123"; Expected="Tra ve null"; Method="Login_EmptyUsername_ReturnsNull"; Class="LoginTests"},
  @{Module="Dang nhap"; TC="TC_LOGIN_04"; Name="De trong mat khau"; Type="Negative"; Input="TenDangNhap=admin, MatKhau=''"; Expected="Tra ve null"; Method="Login_EmptyPassword_ReturnsNull"; Class="LoginTests"},
  @{Module="Dang nhap"; TC="TC_LOGIN_05"; Name="Tai khoan bi vo hieu hoa (TrangThai=0)"; Type="Negative"; Input="TenDangNhap=user_bi_khoa, MatKhau=123"; Expected="Tra ve null, khong dang nhap duoc"; Method="Login_DisabledAccount_ReturnsNull"; Class="LoginTests"},
  @{Module="Dang nhap"; TC="TC_LOGIN_06"; Name="Login tra ve dung VaiTro va HoTen"; Type="Positive"; Input="TenDangNhap=admin, MatKhau=123"; Expected="VaiTro=Admin, HoTen khong rong"; Method="Login_ValidCredentials_ReturnsCorrectRoleAndName"; Class="LoginTests"},
  @{Module="Dang nhap"; TC="TC_LOGIN_07"; Name="Ten dang nhap khong ton tai"; Type="Negative"; Input="TenDangNhap=khongtontai_xyz_999"; Expected="Tra ve null"; Method="Login_NonExistentUsername_ReturnsNull"; Class="LoginTests"},

  # Module "Hoc vien"
  @{Module="Hoc vien"; TC="TC_HV_01"; Name="Them hoc vien hop le"; Type="Positive"; Input="HoTen=Nguyen Van A, GioiTinh=Nam, SDT=0901234567"; Expected="Insert tra ve true, hien thi trong danh sach"; Method="Insert_ValidHocVien_ReturnsTrue"; Class="HocVienTests"},
  @{Module="Hoc vien"; TC="TC_HV_02"; Name="Them hoc vien de trong Ho ten"; Type="Negative"; Input="HoTen=''"; Expected="Validation tu choi HoTen rong"; Method="Insert_EmptyHoTen_ValidationFails"; Class="HocVienTests"},
  @{Module="Hoc vien"; TC="TC_HV_03"; Name="Sua thong tin hoc vien"; Type="Positive"; Input="Chon HV, sua SDT=0987654321"; Expected="Update tra ve true, SDT cap nhat"; Method="Update_HocVien_UpdatesSuccessfully"; Class="HocVienTests"},
  @{Module="Hoc vien"; TC="TC_HV_04"; Name="Xoa hoc vien khong co dang ky goi"; Type="Positive"; Input="Chon HV chua dang ky, xac nhan Yes"; Expected="Delete tra ve true"; Method="Delete_HocVienWithNoDangKy_ReturnsTrue"; Class="HocVienTests"},
  @{Module="Hoc vien"; TC="TC_HV_05"; Name="Xoa hoc vien dang co dang ky goi (FK)"; Type="Negative"; Input="Chon HV co DangKyGoi"; Expected="Nem SqlException (FK violation)"; Method="Delete_HocVienWithDangKy_ThrowsSqlException"; Class="HocVienTests"},
  @{Module="Hoc vien"; TC="TC_HV_06"; Name="Tim kiem theo ten"; Type="Positive"; Input="Keyword=TEST_Nguyen"; Expected="Danh sach loc dung theo ten"; Method="Search_ByName_ReturnsMatchingResults"; Class="HocVienTests"},
  @{Module="Hoc vien"; TC="TC_HV_07"; Name="Tim kiem theo SDT"; Type="Positive"; Input="Keyword=0909111222"; Expected="Danh sach loc dung theo SDT"; Method="Search_BySdt_ReturnsMatchingResults"; Class="HocVienTests"},
  @{Module="Hoc vien"; TC="TC_HV_08"; Name="Tim kiem khong co ket qua"; Type="Negative"; Input="Keyword=xyzxyz_khong_ton_tai_999"; Expected="Danh sach rong"; Method="Search_NoMatch_ReturnsEmptyList"; Class="HocVienTests"},
  @{Module="Hoc vien"; TC="TC_HV_09"; Name="GetAll tra ve danh sach khong loi"; Type="Positive"; Input="Goi GetAll()"; Expected="List khong null, khong exception"; Method="GetAll_ReturnsListWithoutError"; Class="HocVienTests"},
  @{Module="Hoc vien"; TC="TC_HV_10"; Name="Count tra ve so hoc vien >= 0"; Type="Positive"; Input="Goi Count()"; Expected="count >= 0"; Method="Count_ReturnsNonNegativeNumber"; Class="HocVienTests"},

  # Module "Huan luyen vien"
  @{Module="Huan luyen vien"; TC="TC_HLV_01"; Name="Them HLV hop le"; Type="Positive"; Input="HoTen=TEST_HLV, GioiTinh=Nu, ChuyenMon=Yoga"; Expected="Insert tra ve true"; Method="Insert_ValidHLV_ReturnsTrue"; Class="HuanLuyenVienTests"},
  @{Module="Huan luyen vien"; TC="TC_HLV_02"; Name="Them HLV de trong Ho ten"; Type="Negative"; Input="HoTen=''"; Expected="Validation tu choi"; Method="Insert_EmptyHoTen_ValidationFails"; Class="HuanLuyenVienTests"},
  @{Module="Huan luyen vien"; TC="TC_HLV_03"; Name="Them HLV luong khong phai so"; Type="Negative"; Input="Luong='abc'"; Expected="Luong parse thanh null, insert thanh cong"; Method="Insert_NonNumericLuong_ParsedAsNull"; Class="HuanLuyenVienTests"},
  @{Module="Huan luyen vien"; TC="TC_HLV_04"; Name="Xoa HLV dang co phan cong (FK)"; Type="Negative"; Input="Chon HLV co PhanCong"; Expected="Nem SqlException"; Method="Delete_HLVWithPhanCong_ThrowsSqlException"; Class="HuanLuyenVienTests"},
  @{Module="Huan luyen vien"; TC="TC_HLV_05"; Name="Tim kiem HLV theo chuyen mon"; Type="Positive"; Input="Keyword=Yoga"; Expected="Danh sach loc dung"; Method="Search_ByChuyenMon_ReturnsMatching"; Class="HuanLuyenVienTests"},

  # Module "Goi tap"
  @{Module="Goi tap"; TC="TC_GT_01"; Name="Them goi tap hop le"; Type="Positive"; Input="TenGoi=Goi 1 Thang, ThoiHan=30, Gia=500000"; Expected="Insert tra ve true"; Method="Insert_ValidGoiTap_ReturnsTrue"; Class="GoiTapTests"},
  @{Module="Goi tap"; TC="TC_GT_02"; Name="Them goi tap de trong ten"; Type="Negative"; Input="TenGoi=''"; Expected="Validation tu choi"; Method="Insert_EmptyTenGoi_ValidationFails"; Class="GoiTapTests"},
  @{Module="Goi tap"; TC="TC_GT_03"; Name="Them goi voi ThoiHan/Gia khong phai so"; Type="Negative"; Input="ThoiHan='abc', Gia='xyz'"; Expected="Parse thanh null, insert thanh cong"; Method="Insert_NonNumericThoiHanAndGia_ParsedAsNull"; Class="GoiTapTests"},
  @{Module="Goi tap"; TC="TC_GT_04"; Name="Xoa goi dang duoc dang ky (FK)"; Type="Negative"; Input="Chon goi co DangKyGoi"; Expected="Nem SqlException"; Method="Delete_GoiTapWithDangKy_ThrowsSqlException"; Class="GoiTapTests"},
  @{Module="Goi tap"; TC="TC_GT_05"; Name="Sua gia goi tap"; Type="Positive"; Input="Chon goi, sua Gia=1000000"; Expected="Update tra ve true, gia cap nhat"; Method="Update_GoiTap_UpdatesPriceCorrectly"; Class="GoiTapTests"},
  @{Module="Goi tap"; TC="TC_GT_06"; Name="[BUG#8] GoiTapDAO thieu Search()"; Type="Bug"; Input="Reflection check Search method"; Expected="GoiTapDAO khong co Search() - bug xac nhan"; Method="GoiTapDAO_HasNoSearchMethod_BugConfirmed"; Class="GoiTapTests"},

  # Module "Dang ky goi"
  @{Module="Dang ky goi"; TC="TC_DK_01"; Name="Them dang ky goi hop le"; Type="Positive"; Input="Chon HocVien, GoiTap, ngay hop le"; Expected="Insert tra ve true"; Method="Insert_ValidDangKy_ReturnsTrue"; Class="DangKyGoiTests"},
  @{Module="Dang ky goi"; TC="TC_DK_02"; Name="Khong chon hoc vien"; Type="Negative"; Input="cboHocVien=null"; Expected="Validation tu choi"; Method="Insert_NullMaHV_ValidationFails"; Class="DangKyGoiTests"},
  @{Module="Dang ky goi"; TC="TC_DK_03"; Name="Khong chon goi tap"; Type="Negative"; Input="cboGoiTap=null"; Expected="Validation tu choi"; Method="Insert_NullMaGoi_ValidationFails"; Class="DangKyGoiTests"},
  @{Module="Dang ky goi"; TC="TC_DK_04"; Name="Xoa dang ky chua co HoaDon"; Type="Positive"; Input="Chon dang ky sach"; Expected="Delete tra ve true"; Method="Delete_DangKyWithNoRelated_ReturnsTrue"; Class="DangKyGoiTests"},
  @{Module="Dang ky goi"; TC="TC_DK_05"; Name="Xoa dang ky dang co hoa don (FK)"; Type="Negative"; Input="Chon dang ky co HoaDon"; Expected="Nem SqlException"; Method="Delete_DangKyWithHoaDon_ThrowsSqlException"; Class="DangKyGoiTests"},
  @{Module="Dang ky goi"; TC="TC_DK_06"; Name="Sua trang thai sang 'Het han'"; Type="Positive"; Input="Doi TrangThai=Het han"; Expected="Update tra ve true, TrangThai cap nhat"; Method="Update_TrangThai_ToHetHan"; Class="DangKyGoiTests"},
  @{Module="Dang ky goi"; TC="TC_DK_07"; Name="[BUG#9] DangKyGoiDAO thieu Search()"; Type="Bug"; Input="Reflection check Search method"; Expected="DangKyGoiDAO khong co Search()"; Method="DangKyGoiDAO_HasNoSearchMethod_BugConfirmed"; Class="DangKyGoiTests"},
  @{Module="Dang ky goi"; TC="TC_DK_08"; Name="CountActive tra ve >= 0"; Type="Positive"; Input="Goi CountActive()"; Expected="count >= 0"; Method="CountActive_ReturnsNonNegative"; Class="DangKyGoiTests"},

  # Module "Phan cong PT"
  @{Module="Phan cong PT"; TC="TC_PC_01"; Name="Them phan cong hop le"; Type="Positive"; Input="Chon HLV, DangKyGoi, CaLam"; Expected="Insert tra ve true"; Method="Insert_ValidPhanCong_ReturnsTrue"; Class="PhanCongTests"},
  @{Module="Phan cong PT"; TC="TC_PC_02"; Name="Khong chon HLV"; Type="Negative"; Input="cboHLV=null"; Expected="Validation tu choi"; Method="Insert_NullMaHLV_ValidationFails"; Class="PhanCongTests"},
  @{Module="Phan cong PT"; TC="TC_PC_03"; Name="Khong chon dang ky goi"; Type="Negative"; Input="cboDangKy=null"; Expected="Validation tu choi"; Method="Insert_NullMaDK_ValidationFails"; Class="PhanCongTests"},
  @{Module="Phan cong PT"; TC="TC_PC_04"; Name="Ca lam la optional (MaCa=null)"; Type="Positive"; Input="cboCaLam khong chon"; Expected="Insert tra ve true, MaCa=NULL"; Method="Insert_NullMaCa_ReturnsTrue"; Class="PhanCongTests"},
  @{Module="Phan cong PT"; TC="TC_PC_05"; Name="[BUG#5] GhiChu rong khong crash"; Type="Bug"; Input="GhiChu=''"; Expected="Insert thanh cong, khong nem exception"; Method="Insert_EmptyGhiChu_DoesNotThrow"; Class="PhanCongTests"},
  @{Module="Phan cong PT"; TC="TC_PC_06"; Name="Xoa phan cong"; Type="Positive"; Input="Chon phan cong, xac nhan Xoa"; Expected="Delete tra ve true"; Method="Delete_PhanCong_ReturnsTrue"; Class="PhanCongTests"},

  # Module "Hoa don"
  @{Module="Hoa don"; TC="TC_HD_01"; Name="Them hoa don hop le"; Type="Positive"; Input="Chon DangKyGoi, SoTien=500000, HinhThuc=Tien mat"; Expected="Insert tra ve true"; Method="Insert_ValidHoaDon_ReturnsTrue"; Class="HoaDonTests"},
  @{Module="Hoa don"; TC="TC_HD_02"; Name="Khong chon dang ky goi"; Type="Negative"; Input="cboDangKy=null"; Expected="Validation tu choi"; Method="Insert_NullMaDK_ValidationFails"; Class="HoaDonTests"},
  @{Module="Hoa don"; TC="TC_HD_03"; Name="So tien = 0"; Type="Negative"; Input="SoTien=0"; Expected="Validation tu choi (0 > 0 = false)"; Method="Insert_SoTienZero_ValidationFails"; Class="HoaDonTests"},
  @{Module="Hoa don"; TC="TC_HD_04"; Name="So tien am"; Type="Negative"; Input="SoTien=-100"; Expected="Validation tu choi (-100 > 0 = false)"; Method="Insert_NegativeSoTien_ValidationFails"; Class="HoaDonTests"},
  @{Module="Hoa don"; TC="TC_HD_05"; Name="So tien khong phai so"; Type="Negative"; Input="SoTien='abc'"; Expected="decimal.TryParse tra ve false"; Method="Insert_NonNumericSoTien_ParseFails"; Class="HoaDonTests"},
  @{Module="Hoa don"; TC="TC_HD_06"; Name="[BUG#4] HinhThucTT rong vi pham CHECK"; Type="Bug"; Input="HinhThucTT=''"; Expected="Nem SqlException (CHECK constraint)"; Method="Insert_EmptyHinhThucTT_ThrowsSqlException"; Class="HoaDonTests"},
  @{Module="Hoa don"; TC="TC_HD_07"; Name="Xoa hoa don"; Type="Positive"; Input="Chon hoa don, xac nhan Xoa"; Expected="Delete tra ve true"; Method="Delete_HoaDon_ReturnsTrue"; Class="HoaDonTests"},
  @{Module="Hoa don"; TC="TC_HD_08"; Name="Tong doanh thu >= 0"; Type="Positive"; Input="Goi TotalRevenue()"; Expected="Gia tri >= 0"; Method="TotalRevenue_ReturnsNonNegativeDecimal"; Class="HoaDonTests"},

  # Module "Ca lam"
  @{Module="Ca lam"; TC="TC_CL_01"; Name="Them ca lam hop le"; Type="Positive"; Input="TenCa=Ca sang, GioBatDau=06:00, GioKetThuc=12:00"; Expected="Insert tra ve true"; Method="Insert_ValidCaLam_ReturnsTrue"; Class="CaLamTests"},
  @{Module="Ca lam"; TC="TC_CL_02"; Name="Them ca lam de trong ten ca"; Type="Negative"; Input="TenCa=''"; Expected="Validation tu choi"; Method="Insert_EmptyTenCa_ValidationFails"; Class="CaLamTests"},
  @{Module="Ca lam"; TC="TC_CL_03"; Name="Sua gio ket thuc ca lam"; Type="Positive"; Input="GioKetThuc=13:00"; Expected="Update tra ve true"; Method="Update_GioKetThuc_UpdatesCorrectly"; Class="CaLamTests"},
  @{Module="Ca lam"; TC="TC_CL_04"; Name="Xoa ca dang co phan cong (FK)"; Type="Negative"; Input="Chon ca co PhanCong"; Expected="Nem SqlException"; Method="Delete_CaLamWithPhanCong_ThrowsSqlException"; Class="CaLamTests"},
  @{Module="Ca lam"; TC="TC_CL_05"; Name="Xoa ca khong co phan cong"; Type="Positive"; Input="Chon ca sach"; Expected="Delete tra ve true"; Method="Delete_CaLamWithNoPhanCong_ReturnsTrue"; Class="CaLamTests"},

  # Module "Dashboard"
  @{Module="Dashboard"; TC="TC_DB_01"; Name="So hoc vien dang hoat dong >= 0"; Type="Positive"; Input="Goi HocVienDAO.Count()"; Expected="count >= 0"; Method="HocVien_Count_ReturnsNonNegative"; Class="DashboardTests"},
  @{Module="Dashboard"; TC="TC_DB_02"; Name="So HLV dang hoat dong >= 0"; Type="Positive"; Input="Goi HuanLuyenVienDAO.Count()"; Expected="count >= 0"; Method="HuanLuyenVien_Count_ReturnsNonNegative"; Class="DashboardTests"},
  @{Module="Dashboard"; TC="TC_DB_03"; Name="So goi tap dang hoat dong >= 0"; Type="Positive"; Input="Goi GoiTapDAO.Count()"; Expected="count >= 0"; Method="GoiTap_Count_ReturnsNonNegative"; Class="DashboardTests"},
  @{Module="Dashboard"; TC="TC_DB_04"; Name="So dang ky dang hoat dong >= 0"; Type="Positive"; Input="Goi DangKyGoiDAO.CountActive()"; Expected="count >= 0"; Method="DangKy_CountActive_ReturnsNonNegative"; Class="DashboardTests"},
  @{Module="Dashboard"; TC="TC_DB_05"; Name="GetAll.Take(10) tra ve toi da 10 ban ghi"; Type="Positive"; Input="GetAll().Take(10)"; Expected="list.Count <= 10"; Method="DangKy_GetAll_Take10_MaxTenRecords"; Class="DashboardTests"},
  @{Module="Dashboard"; TC="TC_DB_06"; Name="TotalRevenue khop voi SUM trong DB"; Type="Positive"; Input="So sanh TotalRevenue() voi SELECT SUM"; Expected="2 gia tri bang nhau"; Method="HoaDon_TotalRevenue_MatchesSumInDB"; Class="DashboardTests"},

  # Module "Bug Report"
  @{Module="Bug Report"; TC="TC_BUG_01"; Name="[BUG#1] Ten admin khong chua ky tu '?'"; Type="Bug"; Input="Login admin/123, kiem tra HoTen"; Expected="HoTen khong chua '?'"; Method="Bug01_AdminHoTen_ShouldNotContainQuestionMark"; Class="BugReportTests"},
  @{Module="Bug Report"; TC="TC_BUG_02"; Name="[BUG#2] Xoa HV co DangKy nem SqlException"; Type="Bug"; Input="Xoa hoc vien co DangKyGoi"; Expected="Nem SqlException (FK)"; Method="Bug02_Delete_HocVienWithDangKy_ThrowsUnhandledException"; Class="BugReportTests"},
  @{Module="Bug Report"; TC="TC_BUG_03"; Name="[BUG#3] Xoa DangKy co HoaDon nem SqlException"; Type="Bug"; Input="Xoa dang ky co HoaDon"; Expected="Nem SqlException (FK)"; Method="Bug03_Delete_DangKyGoiWithHoaDon_ThrowsUnhandledException"; Class="BugReportTests"},
  @{Module="Bug Report"; TC="TC_BUG_04"; Name="[BUG#4] HinhThucTT rong vi pham CHECK"; Type="Bug"; Input="HinhThucTT='', GhiChu=''"; Expected="Nem SqlException"; Method="Bug04_HoaDon_EmptyHinhThucTT_ThrowsSqlException"; Class="BugReportTests"},
  @{Module="Bug Report"; TC="TC_BUG_05"; Name="[BUG#5] PhanCong GhiChu rong khong crash"; Type="Bug"; Input="GhiChu=''"; Expected="Insert thanh cong"; Method="Bug05_PhanCong_EmptyGhiChu_DoesNotCrash"; Class="BugReportTests"},
  @{Module="Bug Report"; TC="TC_BUG_06"; Name="[BUG#6] NgayHetHan khong tu tinh"; Type="Bug"; Input="NgayBatDau=Today"; Expected="NgayBatDau+30 = NgayBatDau+30"; Method="Bug06_NgayHetHan_NotAutoCalculated_BugConfirmed"; Class="BugReportTests"},
  @{Module="Bug Report"; TC="TC_BUG_07"; Name="[BUG#7] cboDangKy chi hien TenHV"; Type="Bug"; Input="HV co 2+ dang ky"; Expected="Tat ca dong cung TenHV"; Method="Bug07_CboDangKy_DisplayMemberTenHVOnly_BugConfirmed"; Class="BugReportTests"},
  @{Module="Bug Report"; TC="TC_BUG_08"; Name="[BUG#8] GoiTapDAO thieu Search()"; Type="Bug"; Input="Reflection check"; Expected="Khong co Search method"; Method="Bug08_GoiTapDAO_MissingSearchMethod"; Class="BugReportTests"},
  @{Module="Bug Report"; TC="TC_BUG_09"; Name="[BUG#9] DangKyGoiDAO thieu Search()"; Type="Bug"; Input="Reflection check"; Expected="Khong co Search method"; Method="Bug09_DangKyGoiDAO_MissingSearchMethod"; Class="BugReportTests"},
  @{Module="Bug Report"; TC="TC_BUG_10"; Name="[BUG#10] Dashboard cards tran MinimumWidth"; Type="Bug"; Input="5 cards x 235px"; Expected="1175 > 1100"; Method="Bug10_DashboardCards_OverflowMinimumWidth"; Class="BugReportTests"}
)

# ---- 4. Tao Excel voi COM ----
Write-Host "=== Xuat Excel ===" -ForegroundColor Cyan

$xl = New-Object -ComObject Excel.Application
$xl.Visible = $false
$xl.DisplayAlerts = $false
$wb = $xl.Workbooks.Add()
$ws = $wb.Worksheets.Item(1)
$ws.Name = "TestCases"

# ---- Header ----
$headers = @("STT","Module","Test Case ID","Ten Test Case","Loai Test","Du lieu dau vao","Ket qua mong doi","Ket qua thuc te","Ket qua (Pass/Fail)")
for ($c = 1; $c -le $headers.Count; $c++) {
    $cell = $ws.Cells.Item(1, $c)
    $cell.Value2 = $headers[$c-1]
    $cell.Font.Bold = $true
    $cell.Font.Color  = 0xFFFFFF
    $cell.Interior.Color = 0xC47244  # dark blue BGR
    $cell.HorizontalAlignment = -4108
}

# ---- Module colors (BGR for Excel COM) ----
$modColor = @{
    "Dang nhap"       = 0xEEDBBD   # light blue
    "Hoc vien"        = 0xDAEFE2   # light green
    "Huan luyen vien" = 0xCCF2FF   # light lemon
    "Goi tap"         = 0xADCBF8   # light orange
    "Dang ky goi"     = 0xF2E1D9   # light purple
    "Phan cong PT"    = 0xDCD1EA   # light pink
    "Hoa don"         = 0xDAEFE2   # light green
    "Ca lam"          = 0xCCF2FF   # light lemon
    "Dashboard"       = 0xEEDBBD   # light blue
    "Bug Report"      = 0xC7C7FF   # light red
}

# ---- Data rows ----
$row = 2
$stt = 1
foreach ($tc in $testcases) {
    $method = $tc.Method
    $cls = $tc.Class
    # Try Class.Method first, then plain method name
    $key = "$cls.$method"
    $outcome = $resultMap[$key]
    if (-not $outcome) { $outcome = $resultMap[$method] }
    if (-not $outcome) { $outcome = "NotExecuted" }

    switch ($outcome) {
        "Passed"      { $result = "Pass";          $actual = "Dung nhu mong doi" }
        "Failed"      { $result = "Fail";          $actual = "Khong dung" }
        "NotExecuted" { $result = "Not Executed";   $actual = "Khong chay (Skipped)" }
        default       { $result = $outcome;        $actual = $outcome }
    }

    $ws.Cells.Item($row, 1).Value2 = [string]$stt
    $ws.Cells.Item($row, 2).Value2 = $tc.Module
    $ws.Cells.Item($row, 3).Value2 = $tc.TC
    $ws.Cells.Item($row, 4).Value2 = $tc.Name
    $ws.Cells.Item($row, 5).Value2 = $tc.Type
    $ws.Cells.Item($row, 6).Value2 = $tc.Input
    $ws.Cells.Item($row, 7).Value2 = $tc.Expected
    $ws.Cells.Item($row, 8).Value2 = $actual
    $ws.Cells.Item($row, 9).Value2 = $result

    # Row color by module
    $mod = $tc.Module
    if ($modColor.ContainsKey($mod)) {
        for ($c = 1; $c -le 9; $c++) {
            $ws.Cells.Item($row, $c).Interior.Color = $modColor[$mod]
        }
    }

    # Color result cell
    $resultCell = $ws.Cells.Item($row, 9)
    $resultCell.Font.Bold = $true
    $resultCell.HorizontalAlignment = -4108
    switch ($result) {
        "Pass"         { $resultCell.Font.Color = 0x008000 }  # green
        "Fail"         { $resultCell.Font.Color = 0x0000FF }  # red
        "Not Executed" { $resultCell.Font.Color = 0x808080 }  # gray
    }

    $row++
    $stt++
}

# ---- Column widths ----
$ws.Columns.Item(1).ColumnWidth = 5
$ws.Columns.Item(2).ColumnWidth = 18
$ws.Columns.Item(3).ColumnWidth = 14
$ws.Columns.Item(4).ColumnWidth = 45
$ws.Columns.Item(5).ColumnWidth = 12
$ws.Columns.Item(6).ColumnWidth = 40
$ws.Columns.Item(7).ColumnWidth = 40
$ws.Columns.Item(8).ColumnWidth = 25
$ws.Columns.Item(9).ColumnWidth = 18

# ---- Borders + wrap text ----
$dataRange = $ws.Range($ws.Cells.Item(1,1), $ws.Cells.Item($row-1, 9))
$dataRange.WrapText      = $true
$dataRange.RowHeight     = 38
$dataRange.Borders.LineStyle = 1
$dataRange.VerticalAlignment = -4108
$ws.Rows.Item(1).RowHeight = 22

# ---- Freeze header + AutoFilter ----
$ws.Application.ActiveWindow.SplitRow = 1
$ws.Application.ActiveWindow.FreezePanes = $true
$ws.Rows.Item(1).AutoFilter() | Out-Null

# ---- Summary row ----
$summaryRow = $row + 1
$passed = 0; $failed = 0; $skipped = 0
foreach ($tc in $testcases) {
    $key = "$($tc.Class).$($tc.Method)"
    $o = $resultMap[$key]
    if (-not $o) { $o = $resultMap[$tc.Method] }
    if ($o -eq "Passed") { $passed++ }
    elseif ($o -eq "Failed") { $failed++ }
    else { $skipped++ }
}
$total = $testcases.Count

$ws.Cells.Item($summaryRow, 1).Value2 = "TONG KET"
$ws.Range($ws.Cells.Item($summaryRow, 1), $ws.Cells.Item($summaryRow, 3)).Merge()
$ws.Cells.Item($summaryRow, 1).Font.Bold = $true
$ws.Cells.Item($summaryRow, 4).Value2 = "Total: $total"
$ws.Cells.Item($summaryRow, 4).Font.Bold = $true
$ws.Cells.Item($summaryRow, 7).Value2 = "Pass: $passed | Fail: $failed | Skip: $skipped"
$ws.Cells.Item($summaryRow, 7).Font.Bold = $true
for ($c = 1; $c -le 9; $c++) {
    $ws.Cells.Item($summaryRow, $c).Interior.Color = 0xD5D5D5
    $ws.Cells.Item($summaryRow, $c).Borders.LineStyle = 1
}

# ---- Save ----
$wb.SaveAs($outPath, 51)  # xlOpenXMLWorkbook
$wb.Close($false)
$xl.Quit()
[Runtime.InteropServices.Marshal]::ReleaseComObject($xl) | Out-Null
Remove-Variable xl

Write-Host ""
Write-Host "=== DONE ===" -ForegroundColor Green
Write-Host "File saved: $outPath" -ForegroundColor Cyan
Write-Host "Passed: $passed | Failed: $failed | Skipped: $skipped | Total: $total" -ForegroundColor Yellow
