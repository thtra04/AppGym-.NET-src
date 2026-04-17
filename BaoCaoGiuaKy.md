# BÁO CÁO GIỮA KỲ MÔN KIỂM THỬ PHẦN MỀM

**Ứng dụng kiểm thử:** AppGym – Quản lý phòng Gym  
**Ngày báo cáo:** 05/04/2026  

---

## MỤC LỤC

- [CHƯƠNG 1: TỔNG QUAN DỰ ÁN](#chương-1-tổng-quan-dự-án)
- [CHƯƠNG 2: PHÂN TÍCH YÊU CẦU VÀ PHẠM VI KIỂM THỬ](#chương-2-phân-tích-yêu-cầu-và-phạm-vi-kiểm-thử)
- [CHƯƠNG 3: KẾ HOẠCH KIỂM THỬ](#chương-3-kế-hoạch-kiểm-thử)
- [CHƯƠNG 4: THIẾT KẾ CA KIỂM THỬ](#chương-4-thiết-kế-ca-kiểm-thử)
- [CHƯƠNG 5: THỰC THI KIỂM THỬ](#chương-5-thực-thi-kiểm-thử)
- [PHẦN 6: KIỂM THỬ TỰ ĐỘNG (AUTOMATION TEST)](#phần-6-kiểm-thử-tự-động-automation-test)
- [PHẦN 7: BÁO CÁO LỖI (BUG REPORT)](#phần-7-báo-cáo-lỗi-bug-report)
- [PHẦN 8: KẾT QUẢ VÀ ĐÁNH GIÁ](#phần-8-kết-quả-và-đánh-giá)
- [TÀI LIỆU THAM KHẢO](#tài-liệu-tham-khảo)

---

## CHƯƠNG 1: TỔNG QUAN DỰ ÁN

### 1.1 Giới thiệu ứng dụng kiểm thử

**AppGym** là ứng dụng desktop quản lý phòng Gym được phát triển trên nền tảng .NET Windows Forms. Ứng dụng cho phép quản lý toàn diện các nghiệp vụ của phòng tập thể hình bao gồm: quản lý học viên, huấn luyện viên, gói tập, đăng ký gói, phân công huấn luyện viên, quản lý ca làm, hóa đơn thanh toán và dashboard thống kê.

Ứng dụng sử dụng kiến trúc phân lớp:
- **Models**: Các lớp thực thể dữ liệu (HocVien, HuanLuyenVien, GoiTap, CaLam, DangKyGoi, PhanCong, HoaDon, PhongTap, TaiKhoan)
- **DataAccess (DAO)**: Lớp truy xuất dữ liệu với các DAO tương ứng cho từng thực thể
- **Forms**: Giao diện người dùng WinForms cho từng chức năng
- **Helpers**: Lớp hỗ trợ (ThemeHelper)

### 1.2 Mục tiêu

- Kiểm thử toàn diện ứng dụng AppGym nhằm đảm bảo chất lượng phần mềm trước khi triển khai
- Phát hiện và báo cáo các lỗi (bug) trong ứng dụng
- Xây dựng bộ test case tự động bằng NUnit Framework để kiểm thử hồi quy
- Đánh giá mức độ ổn định và tin cậy của các chức năng chính
- Rèn luyện kỹ năng kiểm thử phần mềm thực tế trên dự án .NET

### 1.3 Công nghệ sử dụng

#### 1.3.1 .NET Windows Forms
- **Nền tảng:** .NET 8.0 (net8.0-windows)
- **Kiểu ứng dụng:** WinExe (Windows Desktop Application)
- **Framework UI:** Windows Forms
- **Ngôn ngữ:** C# với nullable enable, implicit usings

#### 1.3.2 SQL Server
- **Phiên bản:** SQL Server Express (LocalDB)
- **Database:** GymManagementDB
- **Kết nối:** Windows Authentication (Trusted_Connection=True)
- **Driver:** Microsoft.Data.SqlClient v5.2.0
- **Cấu trúc:** 11 bảng dữ liệu (PhongTap, HocVien, HuanLuyenVien, CaLam, GoiTap, DangKyGoi, PhanCong, HoaDon, Quyen, TaiKhoan, TaiKhoan_Quyen)

#### 1.3.3 NUnit Framework
- **NUnit:** v3.14.0
- **NUnit3TestAdapter:** v4.5.0
- **NUnit.Analyzers:** v3.9.0
- **Microsoft.NET.Test.Sdk:** v17.8.0
- **Code Coverage:** Coverlet.Collector v6.0.0

#### 1.3.4 Visual Studio 2022
- IDE chính để phát triển và chạy test
- Tích hợp Test Explorer để quản lý và thực thi test case
- Hỗ trợ debug và phân tích kết quả test

### 1.4 Thời gian dự kiến kiểm thử

| Giai đoạn | Nội dung | Thời gian |
|-----------|----------|-----------|
| Giai đoạn 1 | Phân tích yêu cầu, xác định phạm vi kiểm thử | Tuần 1 |
| Giai đoạn 2 | Thiết kế ca kiểm thử (test case) | Tuần 2 |
| Giai đoạn 3 | Viết test script tự động (NUnit) | Tuần 3 |
| Giai đoạn 4 | Thực thi kiểm thử, phát hiện bug | Tuần 4 |
| Giai đoạn 5 | Báo cáo kết quả, đánh giá | Tuần 5 |

---

## CHƯƠNG 2: PHÂN TÍCH YÊU CẦU VÀ PHẠM VI KIỂM THỬ

### 2.1 Danh sách các chức năng của ứng dụng

| STT | Chức năng | Mô tả | Module |
|-----|-----------|-------|--------|
| 1 | Đăng nhập | Xác thực tài khoản (username/password), kiểm tra trạng thái tài khoản, phân quyền Admin/NhanVien | FormLogin |
| 2 | Dashboard | Hiển thị thống kê tổng quan: số học viên, HLV, gói tập đang hoạt động, doanh thu | FormMain |
| 3 | Quản lý Học viên | Thêm, sửa, xóa, tìm kiếm học viên theo tên/SĐT/email | FormHocVienDetail |
| 4 | Quản lý Huấn luyện viên | Thêm, sửa, xóa, tìm kiếm HLV theo tên/chuyên môn | FormHLVDetail |
| 5 | Quản lý Gói tập | Thêm, sửa, xóa gói tập (tên, thời hạn, giá, mô tả) | FormGoiTapDetail |
| 6 | Quản lý Đăng ký gói | Thêm, sửa, xóa đăng ký gói tập cho học viên | FormDangKyGoiDetail |
| 7 | Quản lý Ca làm | Thêm, sửa, xóa ca làm (tên ca, giờ bắt đầu, giờ kết thúc) | FormCaLamDetail |
| 8 | Phân công HLV | Phân công HLV cho đăng ký gói, chọn ca làm | FormPhanCongDetail |
| 9 | Quản lý Hóa đơn | Thêm, sửa, xóa hóa đơn thanh toán | FormHoaDonDetail |
| 10 | Quản lý Phòng tập | Thêm, sửa, xóa thông tin phòng tập | FormPhongTapDetail |

### 2.2 Phạm vi kiểm thử

**Trong phạm vi kiểm thử:**
- Kiểm thử chức năng đăng nhập (TaiKhoanDAO.Login)
- Kiểm thử CRUD (Create, Read, Update, Delete) cho tất cả các module: Học viên, HLV, Gói tập, Ca làm, Đăng ký gói, Phân công, Hóa đơn
- Kiểm thử tìm kiếm (Search) trên các module có hỗ trợ
- Kiểm thử validation dữ liệu đầu vào
- Kiểm thử ràng buộc khóa ngoại (Foreign Key constraint)
- Kiểm thử dashboard và thống kê
- Kiểm thử các bug đã phát hiện

**Ngoài phạm vi:**
- Kiểm thử giao diện UI (UI automation)
- Kiểm thử bảo mật nâng cao
- Kiểm thử tải (load testing)
- Kiểm thử đa nền tảng
- Kiểm thử quản lý phòng tập (PhongTapDAO) – chưa có test

### 2.3 Đối tượng kiểm thử

| Đối tượng | Lớp kiểm thử | Mức độ |
|-----------|---------------|--------|
| TaiKhoanDAO | LoginTests | Kiểm thử đơn vị (Unit Test) |
| HocVienDAO | HocVienTests | Kiểm thử đơn vị + tích hợp DB |
| HuanLuyenVienDAO | HuanLuyenVienTests | Kiểm thử đơn vị + tích hợp DB |
| GoiTapDAO | GoiTapTests | Kiểm thử đơn vị + tích hợp DB |
| CaLamDAO | CaLamTests | Kiểm thử đơn vị + tích hợp DB |
| DangKyGoiDAO | DangKyGoiTests | Kiểm thử đơn vị + tích hợp DB |
| PhanCongDAO | PhanCongTests | Kiểm thử đơn vị + tích hợp DB |
| HoaDonDAO | HoaDonTests | Kiểm thử đơn vị + tích hợp DB |
| Dashboard (tổng hợp DAO) | DashboardTests | Kiểm thử tích hợp |
| Các bug đã phát hiện | BugReportTests | Kiểm thử hồi quy |

### 2.4 Ma trận liên kết yêu cầu - ca kiểm thử

| Yêu cầu | Test Case | Loại |
|----------|-----------|------|
| REQ-01: Đăng nhập hệ thống | TC_LOGIN_01 → TC_LOGIN_07 | Positive & Negative |
| REQ-02: Quản lý học viên (CRUD + Search) | TC_HV_01 → TC_HV_10 | Positive & Negative |
| REQ-03: Quản lý HLV (CRUD + Search) | TC_HLV_01 → TC_HLV_05 | Positive & Negative |
| REQ-04: Quản lý gói tập (CRUD) | TC_GT_01 → TC_GT_06 | Positive & Negative |
| REQ-05: Quản lý ca làm (CRUD) | TC_CL_01 → TC_CL_05 | Positive & Negative |
| REQ-06: Đăng ký gói tập (CRUD) | TC_DK_01 → TC_DK_07 + CountActive | Positive & Negative |
| REQ-07: Phân công HLV (CRUD) | TC_PC_01 → TC_PC_06 | Positive & Negative |
| REQ-08: Quản lý hóa đơn (CRUD) | TC_HD_01 → TC_HD_08 | Positive & Negative |
| REQ-09: Dashboard thống kê | TC_DB_01 → TC_DB_05 + TC_HD_08 | Positive |
| REQ-10: Xác nhận bug | TC_BUG_01 → TC_BUG_10 | Bug Confirmation |

---

## CHƯƠNG 3: KẾ HOẠCH KIỂM THỬ

### 3.1 Mục tiêu kiểm thử

- **Đảm bảo tính đúng đắn:** Tất cả chức năng CRUD hoạt động chính xác theo yêu cầu
- **Phát hiện lỗi:** Tìm ra các bug tiềm ẩn trong logic xử lý dữ liệu, ràng buộc DB
- **Kiểm tra validation:** Đảm bảo ứng dụng xử lý đúng các trường hợp dữ liệu không hợp lệ
- **Xác nhận ràng buộc toàn vẹn dữ liệu:** Foreign Key, CHECK constraint hoạt động chính xác
- **Kiểm thử hồi quy:** Đảm bảo các bug đã fix không tái phát

### 3.2 Chiến lược kiểm thử

#### 3.2.1 Kiểm thử hộp trắng (White-box Testing)
- **Kiểm tra luồng xử lý DAO:** Phân tích mã nguồn các lớp DAO (HocVienDAO, GoiTapDAO, TaiKhoanDAO...) để thiết kế test case bao phủ các nhánh xử lý
- **Kiểm tra câu truy vấn SQL:** Xác minh các câu INSERT, UPDATE, DELETE, SELECT có tham số đúng
- **Kiểm tra xử lý null/DBNull:** Đảm bảo các trường nullable được xử lý đúng (sử dụng DBNull.Value)
- **Kiểm tra kiểu dữ liệu:** Parse string sang int/decimal, xử lý khi parse thất bại
- **Kiểm tra xác thực đăng nhập:** Phân tích hàm Login() với việc hash mật khẩu SHA2_512 + Salt

#### 3.2.2 Kiểm thử hộp đen (Black-box Testing)
- **Phân lớp tương đương (Equivalence Partitioning):**
  - Dữ liệu hợp lệ: HoTen không rỗng, SDT đúng định dạng, Email hợp lệ
  - Dữ liệu không hợp lệ: HoTen rỗng, ThoiHan = "abc", SoTien < 0
- **Phân tích giá trị biên (Boundary Value Analysis):**
  - SoTien = 0, SoTien = -1, SoTien = 1 (biên dương)
  - Count() >= 0 (không âm)
- **Bảng quyết định (Decision Table):**
  - Đăng nhập: username đúng/sai × password đúng/sai × tài khoản bị khóa/mở → kết quả

### 3.3 Môi trường kiểm thử

| Thành phần | Chi tiết |
|------------|----------|
| Hệ điều hành | Windows 10/11 |
| IDE | Visual Studio 2022 |
| Runtime | .NET 8.0 |
| Database | SQL Server Express (LocalDB) |
| Database name | GymManagementDB |
| Test Framework | NUnit 3.14.0 |
| Test Adapter | NUnit3TestAdapter 4.5.0 |
| Connection | `Server=(local)\SQLEXPRESS;Database=GymManagementDB;Trusted_Connection=True;TrustServerCertificate=True` |

### 3.4 Tiêu chí bắt đầu kiểm thử

- SQL Server Express đang chạy và có thể kết nối
- Database GymManagementDB đã được tạo bằng script `CodeTaoBang.sql`
- Dữ liệu mẫu đã được import bằng `data.sql` hoặc `import_data.sql`
- Có ít nhất 1 bản ghi trong các bảng chính: HocVien, HuanLuyenVien, GoiTap, CaLam, DangKyGoi
- Tài khoản admin/123 tồn tại và hoạt động
- Project biên dịch thành công (`dotnet build`)

### 3.5 Tiêu chí kết thúc kiểm thử

- Tất cả 71 test case đã được thực thi
- Tỷ lệ Pass ≥ 90% (các test Fail phải có lý do rõ ràng)
- Tất cả bug Critical và High đã được báo cáo
- Kết quả test được lưu trong file TestResults.trx
- Báo cáo bug đầy đủ với mức độ nghiêm trọng

### 3.6 Rủi ro và biện pháp xử lý

| Rủi ro | Mức độ | Biện pháp xử lý |
|--------|--------|------------------|
| Không kết nối được SQL Server | Cao | Test tự động Skip (Assert.Ignore) khi không có DB, không Fail |
| Dữ liệu test ảnh hưởng DB | Trung bình | TearDown tự động xóa dữ liệu test sau mỗi test case |
| FK constraint ngăn xóa dữ liệu test | Trung bình | Kiểm tra FK trước khi test, sử dụng Assert.Ignore nếu thiếu dữ liệu |
| Encoding lỗi (tiếng Việt) | Thấp | Dùng NVARCHAR và N'' prefix trong SQL |
| Test case phụ thuộc lẫn nhau | Thấp | Mỗi test tự tạo và tự dọn dữ liệu riêng |

---

## CHƯƠNG 4: THIẾT KẾ CA KIỂM THỬ

### 4.1 Cơ sở thiết kế ca kiểm thử

Ca kiểm thử được thiết kế dựa trên:
- **Đặc tả chức năng:** Mỗi module (Học viên, HLV, Gói tập...) có CRUD → thiết kế test cho Insert, Update, Delete, GetAll, Search
- **Mã nguồn DAO:** Phân tích từng phương thức trong các lớp DAO để bao phủ các nhánh xử lý
- **Ràng buộc database:** CHECK constraint, FOREIGN KEY, UNIQUE → thiết kế test case negative
- **Validation UI:** Kiểm tra các trường bắt buộc, định dạng dữ liệu → test validation logic

### 4.2 Phân loại ca kiểm thử

| Loại | Số lượng | Mô tả |
|------|----------|-------|
| Positive Test | ~35 | Kiểm tra chức năng hoạt động đúng với dữ liệu hợp lệ |
| Negative Test | ~26 | Kiểm tra ứng dụng xử lý đúng dữ liệu không hợp lệ |
| Bug Confirmation | 10 | Xác nhận các bug đã phát hiện |
| **Tổng cộng** | **71** | |

### 4.3 Thiết kế test case cho các chức năng chính

#### 4.3.1 Đăng nhập (LoginTests – 7 test case)

| Mã TC | Tên test case | Mô tả | Đầu vào | Kết quả mong đợi | Loại |
|-------|---------------|-------|---------|-------------------|------|
| TC_LOGIN_01 | Login_ValidCredentials_ReturnsUser | Đăng nhập thành công với tài khoản hợp lệ | username="admin", password="123" | User != null, TenDangNhap="admin", TrangThai=true | Positive |
| TC_LOGIN_02 | Login_WrongPassword_ReturnsNull | Đăng nhập sai mật khẩu | username="admin", password="sai123" | null | Negative |
| TC_LOGIN_03 | Login_EmptyUsername_ReturnsNull | Tên đăng nhập rỗng | username="", password="123" | null | Negative |
| TC_LOGIN_04 | Login_EmptyPassword_ReturnsNull | Mật khẩu rỗng | username="admin", password="" | null | Negative |
| TC_LOGIN_05 | Login_DisabledAccount_ReturnsNull | Tài khoản bị vô hiệu hóa (TrangThai=0) | username="user_bi_khoa", password="123" | null | Negative |
| TC_LOGIN_06 | Login_ValidCredentials_ReturnsCorrectRoleAndName | Login trả về đúng thông tin VaiTro và HoTen | username="admin", password="123" | VaiTro="Admin", HoTen not empty | Positive |
| TC_LOGIN_07 | Login_NonExistentUsername_ReturnsNull | Tên đăng nhập không tồn tại trong hệ thống | username="khongtontai_xyz_999", password="123" | null | Negative |

#### 4.3.2 Quản lý Học viên (HocVienTests – 10 test case)

| Mã TC | Tên test case | Mô tả | Kết quả mong đợi | Loại |
|-------|---------------|-------|-------------------|------|
| TC_HV_01 | Insert_ValidHocVien_ReturnsTrue | Thêm học viên hợp lệ thành công | Insert trả về true, xuất hiện trong GetAll | Positive |
| TC_HV_02 | Insert_EmptyHoTen_ValidationFails | HoTen rỗng – validation phát hiện | IsNullOrWhiteSpace("") = true | Negative |
| TC_HV_03 | Update_HocVien_UpdatesSuccessfully | Sửa thông tin học viên (SDT) thành công | Update trả về true, SDT đã thay đổi | Positive |
| TC_HV_04 | Delete_HocVienWithNoDangKy_ReturnsTrue | Xóa học viên chưa có đăng ký gói thành công | Delete trả về true, không còn trong list | Positive |
| TC_HV_05 | Delete_HocVienWithDangKy_ThrowsSqlException | Xóa học viên đang có đăng ký gói → FK violation | Ném SqlException | Negative |
| TC_HV_06 | Search_ByName_ReturnsMatchingResults | Tìm kiếm học viên theo tên | Trả về list không rỗng, HoTen chứa keyword | Positive |
| TC_HV_07 | Search_BySdt_ReturnsMatchingResults | Tìm kiếm học viên theo SĐT | Trả về list có SDT khớp | Positive |
| TC_HV_08 | Search_NoMatch_ReturnsEmptyList | Tìm kiếm từ khóa không tồn tại | Trả về list rỗng | Negative |
| TC_HV_09 | GetAll_ReturnsListWithoutError | GetAll() trả về toàn bộ danh sách không lỗi | list != null, không throw exception | Positive |
| TC_HV_10 | Count_ReturnsNonNegativeNumber | Count() trả về số học viên đang hoạt động >= 0 | count >= 0 | Positive |

#### 4.3.3 Quản lý Huấn luyện viên (HuanLuyenVienTests – 5 test case)

| Mã TC | Tên test case | Mô tả | Kết quả mong đợi | Loại |
|-------|---------------|-------|-------------------|------|
| TC_HLV_01 | Insert_ValidHLV_ReturnsTrue | Thêm HLV hợp lệ | Insert trả về true | Positive |
| TC_HLV_02 | Insert_EmptyHoTen_ValidationFails | HoTen rỗng – validation | Validation fail | Negative |
| TC_HLV_03 | Insert_NonNumericLuong_ParsedAsNull | Lương = "abc" → parse null | Luong = null, insert vẫn thành công | Negative |
| TC_HLV_04 | Delete_HLVWithPhanCong_ThrowsSqlException | Xóa HLV đang có phân công → FK | Ném SqlException | Negative |
| TC_HLV_05 | Search_ByChuyenMon_ReturnsMatching | Tìm kiếm HLV theo chuyên môn | Trả về list có ChuyenMon khớp | Positive |

#### 4.3.4 Quản lý Gói tập (GoiTapTests – 6 test case)

| Mã TC | Tên test case | Mô tả | Kết quả mong đợi | Loại |
|-------|---------------|-------|-------------------|------|
| TC_GT_01 | Insert_ValidGoiTap_ReturnsTrue | Thêm gói tập hợp lệ | Insert trả về true | Positive |
| TC_GT_02 | Insert_EmptyTenGoi_ValidationFails | TenGoi rỗng – validation | Validation fail | Negative |
| TC_GT_03 | Insert_NonNumericThoiHanAndGia_ParsedAsNull | ThoiHan="abc", Gia="xyz" → null | Parse thành null, insert không crash | Negative |
| TC_GT_04 | Delete_GoiTapWithDangKy_ThrowsSqlException | Xóa gói tập đang được đăng ký → FK | Ném SqlException | Negative |
| TC_GT_05 | Update_GoiTap_UpdatesPriceCorrectly | Sửa giá gói tập | Update trả về true, giá đã thay đổi | Positive |
| TC_GT_06 | GoiTapDAO_HasNoSearchMethod_BugConfirmed | GoiTapDAO không có Search() → bug #8 | Không tìm thấy method Search | Bug |

#### 4.3.5 Quản lý Ca làm (CaLamTests – 5 test case)

| Mã TC | Tên test case | Mô tả | Kết quả mong đợi | Loại |
|-------|---------------|-------|-------------------|------|
| TC_CL_01 | Insert_ValidCaLam_ReturnsTrue | Thêm ca làm hợp lệ | Insert trả về true | Positive |
| TC_CL_02 | Insert_EmptyTenCa_ValidationFails | TenCa rỗng – validation | Validation fail | Negative |
| TC_CL_03 | Update_GioKetThuc_UpdatesCorrectly | Sửa giờ kết thúc ca làm | Update trả về true, giờ đã thay đổi | Positive |
| TC_CL_04 | Delete_CaLamWithPhanCong_ThrowsSqlException | Xóa ca làm đang có phân công → FK | Ném SqlException | Negative |
| TC_CL_05 | Delete_CaLamWithNoPhanCong_ReturnsTrue | Xóa ca làm không có phân công | Delete trả về true | Positive |

#### 4.3.6 Đăng ký gói (DangKyGoiTests – 8 test case)

| Mã TC | Tên test case | Mô tả | Kết quả mong đợi | Loại |
|-------|---------------|-------|-------------------|------|
| TC_DK_01 | Insert_ValidDangKy_ReturnsTrue | Thêm đăng ký gói hợp lệ | Insert trả về true | Positive |
| TC_DK_02 | Insert_NullMaHV_ValidationFails | MaHV null – validation | Validation fail | Negative |
| TC_DK_03 | Insert_NullMaGoi_ValidationFails | MaGoi null – validation | Validation fail | Negative |
| TC_DK_04 | Delete_DangKyWithNoRelated_ReturnsTrue | Xóa đăng ký không có hóa đơn liên quan | Delete trả về true | Positive |
| TC_DK_05 | Delete_DangKyWithHoaDon_ThrowsSqlException | Xóa đăng ký có hóa đơn → FK | Ném SqlException | Negative |
| TC_DK_06 | Update_TrangThai_ToHetHan | Cập nhật trạng thái thành "Hết hạn" | Update trả về true, trạng thái đã thay đổi | Positive |
| TC_DK_07 | DangKyGoiDAO_HasNoSearchMethod_BugConfirmed | DangKyGoiDAO không có Search() → bug #9 | Không tìm thấy method Search | Bug |
| TC_DK_08 | CountActive_ReturnsNonNegative | CountActive() trả về >= 0 | count >= 0 | Positive |

#### 4.3.7 Phân công HLV (PhanCongTests – 6 test case)

| Mã TC | Tên test case | Mô tả | Kết quả mong đợi | Loại |
|-------|---------------|-------|-------------------|------|
| TC_PC_01 | Insert_ValidPhanCong_ReturnsTrue | Thêm phân công hợp lệ | Insert trả về true | Positive |
| TC_PC_02 | Insert_NullMaHLV_ValidationFails | Không chọn HLV – validation | Validation fail | Negative |
| TC_PC_03 | Insert_NullMaDK_ValidationFails | Không chọn đăng ký gói – validation | Validation fail | Negative |
| TC_PC_04 | Insert_NullMaCa_ReturnsTrue | Thêm phân công không chọn ca làm (optional) | Insert trả về true, MaCa=null | Positive |
| TC_PC_05 | Insert_EmptyGhiChu_DoesNotThrow | Insert với GhiChu="" không crash → bug #5 | Không ném exception, insert thành công | Bug |
| TC_PC_06 | Delete_PhanCong_ReturnsTrue | Xóa phân công thành công | Delete trả về true | Positive |

#### 4.3.8 Quản lý Hóa đơn (HoaDonTests – 8 test case)

| Mã TC | Tên test case | Mô tả | Kết quả mong đợi | Loại |
|-------|---------------|-------|-------------------|------|
| TC_HD_01 | Insert_ValidHoaDon_ReturnsTrue | Thêm hóa đơn hợp lệ | Insert trả về true | Positive |
| TC_HD_02 | Insert_NullMaDK_ValidationFails | MaDK null – validation | Validation fail | Negative |
| TC_HD_03 | Insert_SoTienZero_ValidationFails | SoTien = 0 – validation | 0 > 0 = false | Negative |
| TC_HD_04 | Insert_NegativeSoTien_ValidationFails | SoTien < 0 – validation | -100 > 0 = false | Negative |
| TC_HD_05 | Insert_NonNumericSoTien_ParseFails | SoTien = "abc" – parse fail | decimal.TryParse = false | Negative |
| TC_HD_06 | Insert_EmptyHinhThucTT_ThrowsSqlException | HinhThucTT rỗng insert → bug #4 | Insert thành công (thiếu CHECK) | Bug |
| TC_HD_07 | Delete_HoaDon_ReturnsTrue | Xóa hóa đơn thành công | Delete trả về true | Positive |
| TC_HD_08 | TotalRevenue_ReturnsNonNegativeDecimal | TotalRevenue() >= 0 | revenue >= 0 | Positive |

#### 4.3.9 Dashboard (DashboardTests – 6 test case)

| Mã TC | Tên test case | Mô tả | Kết quả mong đợi | Loại |
|-------|---------------|-------|-------------------|------|
| TC_DB_01 | HocVien_Count_ReturnsNonNegative | Số học viên đang hoạt động >= 0 | count >= 0 | Positive |
| TC_DB_02 | HuanLuyenVien_Count_ReturnsNonNegative | Số HLV đang hoạt động >= 0 | count >= 0 | Positive |
| TC_DB_03 | GoiTap_Count_ReturnsNonNegative | Số gói tập đang hoạt động >= 0 | count >= 0 | Positive |
| TC_DB_04 | DangKy_CountActive_ReturnsNonNegative | Số đăng ký "Đang hoạt động" >= 0 | count >= 0 | Positive |
| TC_DB_05 | DangKy_GetAll_Take10_MaxTenRecords | GetAll().Take(10) không quá 10 bản ghi | list.Count <= 10 | Positive |
| TC_DB_06 | HoaDon_TotalRevenue_MatchesSumInDB | TotalRevenue() khớp SUM(SoTien) trong DB | fromDAO == fromDB | Positive |

### 4.4 Thiết kế dữ liệu kiểm thử

#### Dữ liệu kiểm thử cho Học viên:
```csharp
HocVien testHV = new()
{
    HoTen      = "TEST_Nguyen Van A",
    GioiTinh   = "Nam",
    NgaySinh   = new DateTime(1995, 1, 1),
    SDT        = "0901234567",
    Email      = "test@gmail.com",
    NgayDangKy = DateTime.Today,
    TrangThai  = true
};
```

#### Dữ liệu kiểm thử cho HLV:
```csharp
HuanLuyenVien testHLV = new()
{
    HoTen = "TEST_HLV B", GioiTinh = "Nữ", SDT = "0912345678",
    ChuyenMon = "Yoga", Luong = 5_000_000, TrangThai = true
};
```

#### Dữ liệu kiểm thử cho Gói tập:
```csharp
GoiTap testGT = new()
{
    TenGoi = "TEST_Goi 1 Thang", ThoiHan = 30, Gia = 500_000,
    MoTa = "Co ban", TrangThai = true
};
```

#### Dữ liệu kiểm thử cho Ca làm:
```csharp
CaLam testCL = new()
{
    TenCa = "TEST_Ca sang", GioBatDau = new TimeSpan(6, 0, 0),
    GioKetThuc = new TimeSpan(12, 0, 0)
};
```

#### Dữ liệu kiểm thử cho Đăng nhập:
| Username | Password | Mô tả |
|----------|----------|-------|
| admin | 123 | Tài khoản hợp lệ |
| admin | sai123 | Sai mật khẩu |
| "" | 123 | Username rỗng |
| admin | "" | Password rỗng |
| user_bi_khoa | 123 | Tài khoản bị khóa (TrangThai=0) |
| khongtontai_xyz_999 | 123 | Username không tồn tại |

### 4.5 Thiết kế kịch bản kiểm thử tự động

Mỗi test class tuân theo mô hình:

```
[TestFixture] → Class chứa các test case cho 1 module
├── TestBase (base class) → Cung cấp ConnectionString, SkipIfNoDatabase(), Cleanup()
├── [OneTimeSetUp] → GlobalSetup: thiết lập connection string
├── [SetUp] → Kiểm tra DB, khởi tạo DAO
├── [Test] → Từng test case độc lập
│   ├── Arrange: Tạo dữ liệu test (MakeTestXxx)
│   ├── Act: Gọi phương thức DAO
│   └── Assert: Kiểm tra kết quả (NUnit Assert.That)
└── [TearDown] → Cleanup: Xóa dữ liệu test đã tạo
```

**Đặc điểm kịch bản tự động:**
- Test tự động Skip khi không kết nối được DB (không Fail)
- Mỗi test tự tạo dữ liệu → thực thi → tự dọn dẹp (Self-contained)
- Sử dụng suffix (`_01`, `_02`...) để tránh trùng dữ liệu giữa các test
- Test Bug Report xác nhận bug tồn tại → khi fix bug, test sẽ Fail (thiết kế có chủ đích)

### 4.6 Bảng tổng hợp test case

| File Test | Category | Số TC | Positive | Negative | Bug |
|-----------|----------|-------|----------|----------|-----|
| LoginTests.cs | Đăng nhập | 7 | 2 | 5 | 0 |
| HocVienTests.cs | Học viên | 10 | 6 | 4 | 0 |
| HuanLuyenVienTests.cs | Huấn luyện viên | 5 | 2 | 3 | 0 |
| GoiTapTests.cs | Gói tập | 6 | 2 | 3 | 1 |
| CaLamTests.cs | Ca làm | 5 | 3 | 2 | 0 |
| DangKyGoiTests.cs | Đăng ký gói | 8 | 4 | 3 | 1 |
| PhanCongTests.cs | Phân công PT | 6 | 3 | 2 | 1 |
| HoaDonTests.cs | Hóa đơn | 8 | 3 | 4 | 1 |
| DashboardTests.cs | Dashboard | 6 | 6 | 0 | 0 |
| BugReportTests.cs | Bug Report | 10 | 0 | 0 | 10 |
| **Tổng cộng** | | **71** | **31** | **26** | **14** |

---

## CHƯƠNG 5: THỰC THI KIỂM THỬ

### 5.1 Kiểm thử chức năng

#### 5.1.1 Kiểm thử chức năng Đăng nhập
- **Phương thức kiểm thử:** TaiKhoanDAO.Login(username, password)
- **Kết quả:** 7/7 test case Pass
- **Phân tích:**
  - Đăng nhập đúng tài khoản admin/123 trả về đúng thông tin user
  - Đăng nhập sai mật khẩu, username rỗng, password rỗng, tài khoản bị khóa, username không tồn tại → trả về null
  - Hàm Login sử dụng HASHBYTES('SHA2_512') với Salt để mã hóa mật khẩu

#### 5.1.2 Kiểm thử CRUD Học viên
- **Phương thức:** HocVienDAO.Insert(), Update(), Delete(), GetAll(), Search(), Count()
- **Kết quả:** 10/10 test case Pass
- **Phân tích:**
  - Insert thành công với dữ liệu hợp lệ
  - Update SDT thành công
  - Delete thành công khi không có đăng ký gói liên quan
  - Delete ném SqlException khi có FK violation (đăng ký gói tồn tại)
  - Search tìm kiếm theo HoTen và SDT chính xác

#### 5.1.3 Kiểm thử CRUD Gói tập
- **Phương thức:** GoiTapDAO.Insert(), Update(), Delete(), GetAll(), Count()
- **Kết quả:** 6/6 test case Pass
- **Phát hiện bug:** GoiTapDAO thiếu phương thức Search() → chức năng tìm kiếm trên UI không hoạt động (BUG#8)

#### 5.1.4 Kiểm thử CRUD Hóa đơn
- **Phương thức:** HoaDonDAO.Insert(), Delete(), GetAll(), TotalRevenue()
- **Kết quả:** 8/8 test case Pass
- **Phát hiện bug:** HinhThucTT="" insert thành công mà không bị CHECK constraint bắt (BUG#4)

### 5.2 Kiểm thử tích hợp

#### 5.2.1 Kiểm thử tích hợp DAO - Database
- Tất cả các DAO đều được kiểm thử tích hợp với SQL Server thực
- Kiểm tra ràng buộc Foreign Key giữa các bảng:
  - HocVien → DangKyGoi (TC_HV_05)
  - GoiTap → DangKyGoi (TC_GT_04)
  - DangKyGoi → HoaDon (TC_DK_05)
  - DangKyGoi → PhanCong (TC_CL_04)
  - HuanLuyenVien → PhanCong (TC_HLV_04)
  - CaLam → PhanCong (TC_CL_04)

#### 5.2.2 Kiểm thử Dashboard
- Dashboard tổng hợp dữ liệu từ nhiều DAO: HocVienDAO.Count(), HuanLuyenVienDAO.Count(), GoiTapDAO.Count(), DangKyGoiDAO.CountActive(), HoaDonDAO.TotalRevenue()
- TotalRevenue() khớp với SUM(SoTien) trực tiếp từ DB (TC_DB_06)
- GetAll().Take(10) đảm bảo không vượt quá 10 bản ghi

### 5.3 Kiểm thử hệ thống

- Hệ thống đăng nhập → phân quyền (Admin/NhanVien) hoạt động đúng
- Luồng nghiệp vụ: Tạo Học viên → Tạo Gói tập → Đăng ký gói → Phân công HLV → Tạo Hóa đơn đã được kiểm tra qua các test tích hợp
- Ràng buộc toàn vẹn dữ liệu giữa các bảng hoạt động chính xác (FK constraint)

### 5.4 Kiểm thử hiệu năng

- Count() và TotalRevenue() sử dụng truy vấn đơn giản (SELECT COUNT, SELECT SUM) → hiệu năng tốt
- GetAll() trả về toàn bộ dữ liệu, chưa có phân trang → có thể ảnh hưởng hiệu năng khi dữ liệu lớn
- Dashboard sử dụng Take(10) để giới hạn dữ liệu hiển thị

### 5.5 Kiểm thử hồi quy

- **BugReportTests.cs** chứa 10 test case xác nhận các bug đã phát hiện
- Khi bug được fix, test tương ứng sẽ Fail → đảm bảo nhận biết khi bug tái phát
- Các test hồi quy bao phủ: encoding lỗi, FK violation không xử lý, thiếu Search(), validation thiếu

---

## PHẦN 6: KIỂM THỬ TỰ ĐỘNG (AUTOMATION TEST)

### 6.1 Công cụ sử dụng và lý do lựa chọn

| Công cụ | Phiên bản | Lý do lựa chọn |
|---------|-----------|-----------------|
| **NUnit Framework** | 3.14.0 | Framework kiểm thử phổ biến nhất cho .NET, hỗ trợ đầy đủ Annotation (TestFixture, Test, SetUp, TearDown), Assert API phong phú |
| **NUnit3TestAdapter** | 4.5.0 | Tích hợp Test Explorer trong Visual Studio, hỗ trợ chạy và debug test trực tiếp |
| **Microsoft.NET.Test.Sdk** | 17.8.0 | SDK chuẩn của Microsoft cho dự án test .NET |
| **Coverlet** | 6.0.0 | Thu thập code coverage để đánh giá mức độ bao phủ của test |
| **Microsoft.Data.SqlClient** | 5.2.0 | Kết nối SQL Server, dùng cùng version với ứng dụng chính |

**Lý do chọn NUnit thay vì xUnit/MSTest:**
- NUnit có Assert API dạng constraint model (`Assert.That(...)`) trực quan, dễ đọc
- Hỗ trợ `[Category]` để phân nhóm test theo module
- Hỗ trợ `[Description]` để mô tả test case rõ ràng
- `Assert.Ignore()` cho phép skip test khi không đủ điều kiện (ví dụ: không có DB)

### 6.2 Các test script đã viết

| File | Số test | Chức năng kiểm thử |
|------|---------|---------------------|
| TestBase.cs | 0 | Base class: connection string, SkipIfNoDatabase(), Cleanup() |
| LoginTests.cs | 7 | Đăng nhập: đúng/sai credentials, tài khoản bị khóa, rỗng |
| HocVienTests.cs | 10 | CRUD học viên, tìm kiếm theo tên/SĐT, Count |
| HuanLuyenVienTests.cs | 5 | CRUD HLV, tìm kiếm theo chuyên môn |
| GoiTapTests.cs | 6 | CRUD gói tập, xác nhận bug thiếu Search |
| CaLamTests.cs | 5 | CRUD ca làm, FK constraint |
| DangKyGoiTests.cs | 8 | CRUD đăng ký gói, CountActive, bug thiếu Search |
| PhanCongTests.cs | 6 | CRUD phân công, GhiChu rỗng bug |
| HoaDonTests.cs | 8 | CRUD hóa đơn, validation SoTien, TotalRevenue |
| DashboardTests.cs | 6 | Thống kê Count, TotalRevenue, Take(10) |
| BugReportTests.cs | 10 | Xác nhận 10 bug đã phát hiện |
| **Tổng** | **71** | |

### 6.3 Hướng dẫn cài đặt và chạy script

#### 6.3.1 Cài đặt môi trường

**Yêu cầu hệ thống:**
- Windows 10/11
- .NET 8.0 SDK
- SQL Server Express (hoặc SQL Server LocalDB)
- Visual Studio 2022 (khuyến nghị)

**Các bước cài đặt:**

1. **Cài đặt SQL Server Express:**
   ```
   Tải từ: https://www.microsoft.com/en-us/sql-server/sql-server-downloads
   ```

2. **Tạo database:**
   ```powershell
   sqlcmd -S "(local)\SQLEXPRESS" -i CodeTaoBang.sql
   sqlcmd -S "(local)\SQLEXPRESS" -i data.sql
   ```

3. **Restore NuGet packages:**
   ```powershell
   cd AppGym
   dotnet restore AppGym.Tests\AppGym.Tests.csproj
   ```

4. **Build project:**
   ```powershell
   dotnet build AppGym.Tests\AppGym.Tests.csproj
   ```

5. **Chạy test:**
   ```powershell
   # Chạy tất cả test
   dotnet test AppGym.Tests\AppGym.Tests.csproj

   # Chạy theo category
   dotnet test --filter "Category=Dang nhap"
   dotnet test --filter "Category=Hoc vien"
   dotnet test --filter "Category=Bug Report"

   # Chạy với output chi tiết
   dotnet test --logger "console;verbosity=detailed"

   # Xuất kết quả TRX
   dotnet test --logger "trx;LogFileName=TestResults.trx"
   ```

#### 6.3.2 Cấu trúc code chính

```
AppGym.Tests/
├── TestBase.cs                 # Base class
│   ├── ConnStr                 # Connection string đến GymManagementDB
│   ├── GlobalSetup()           # Thiết lập DatabaseHelper.ConnectionString
│   ├── SkipIfNoDatabase()      # Skip test nếu không kết nối được DB
│   └── Cleanup(sql)            # Chạy SQL cleanup sau test
│
├── LoginTests.cs               # 7 test: đăng nhập
├── HocVienTests.cs             # 10 test: CRUD + Search học viên
│   ├── MakeTestHocVien()       # Helper tạo dữ liệu test
│   ├── InsertAndGetId()        # Insert và lấy ID
│   └── [TearDown] → Cleanup   # Tự xóa dữ liệu sau test
│
├── HuanLuyenVienTests.cs       # 5 test: CRUD + Search HLV
├── GoiTapTests.cs              # 6 test: CRUD gói tập + bug
├── CaLamTests.cs               # 5 test: CRUD ca làm
├── DangKyGoiTests.cs           # 8 test: CRUD đăng ký gói
├── PhanCongTests.cs            # 6 test: CRUD phân công
├── HoaDonTests.cs              # 8 test: CRUD hóa đơn
├── DashboardTests.cs           # 6 test: thống kê
└── BugReportTests.cs           # 10 test: xác nhận bug
```

**Mẫu test tiêu biểu:**
```csharp
[Test]
[Description("TC_HV_01: Thêm học viên hợp lệ thành công")]
public void Insert_ValidHocVien_ReturnsTrue()
{
    // Arrange
    var hv = MakeTestHocVien("_01");

    // Act
    bool ok = _dao.Insert(hv);

    // Assert
    Assert.That(ok, Is.True);
    var list = _dao.GetAll();
    _insertedId = list.FirstOrDefault(x => x.HoTen == hv.HoTen)?.MaHV ?? -1;
    Assert.That(list.Any(x => x.HoTen == hv.HoTen), Is.True, 
        "Học viên phải xuất hiện trong danh sách");
}
```

### 6.4 Kết quả chạy tự động

| Category | Tổng TC | Pass | Fail | Skip | Tỷ lệ Pass |
|----------|---------|------|------|------|-------------|
| Đăng nhập | 7 | 7 | 0 | 0 | 100% |
| Học viên | 10 | 10 | 0 | 0 | 100% |
| Huấn luyện viên | 5 | 5 | 0 | 0 | 100% |
| Gói tập | 6 | 6 | 0 | 0 | 100% |
| Ca làm | 5 | 5 | 0 | 0 | 100% |
| Đăng ký gói | 8 | 8 | 0 | 0 | 100% |
| Phân công PT | 6 | 6 | 0 | 0 | 100% |
| Hóa đơn | 8 | 8 | 0 | 0 | 100% |
| Dashboard | 6 | 6 | 0 | 0 | 100% |
| Bug Report | 10 | 10 | 0 | 0 | 100% |
| **Tổng** | **71** | **71** | **0** | **0** | **100%** |

> **Lưu ý:** Tất cả 71 test case đều Pass. Các test Bug Report được thiết kế để Pass khi bug còn tồn tại (xác nhận bug). Khi bug được fix, các test này sẽ Fail → cần cập nhật test.

### 6.5 Nhận xét về automation

**Ưu điểm:**
- Bộ test tự động bao phủ 71 test case trên 10 module chính
- Test tự động cleanup dữ liệu → không ảnh hưởng database
- Có cơ chế Skip khi không có DB → không Fail không đúng lý do
- Phân nhóm theo Category giúp chạy từng module riêng biệt
- Tuân theo mô hình AAA (Arrange – Act – Assert) chuẩn

**Hạn chế:**
- Chưa có kiểm thử UI tự động (chỉ test ở tầng DAO)
- Phụ thuộc vào SQL Server thực → không thể chạy offline hoàn toàn
- Chưa có mock/stub cho database → tốc độ test phụ thuộc DB
- Chưa kiểm thử PhongTapDAO
- Một số test negative chỉ kiểm tra validation logic chứ không gọi DAO thực (test validation ở tầng UI)

---

## PHẦN 7: BÁO CÁO LỖI (BUG REPORT)

### 7.1 Danh sách toàn bộ bug tìm được

| Bug ID | Tên bug | Mô tả chi tiết | Test Case xác nhận | Mức độ |
|--------|---------|-----------------|---------------------|--------|
| BUG#01 | Admin HoTen chứa ký tự "?" | Tên hiển thị của admin chứa ký tự "?" do lỗi encoding khi insert dữ liệu trong setup_db.sql. Nguyên nhân: dùng chuỗi không có prefix N'' cho ký tự Unicode tiếng Việt | TC_BUG_01 | Medium |
| BUG#02 | Xóa học viên có đăng ký gói → crash | Khi xóa HocVien đang có bản ghi DangKyGoi liên quan, DAO ném SqlException (FK violation) mà không được xử lý ở tầng UI → ứng dụng crash hoặc hiển thị lỗi không thân thiện | TC_BUG_02, TC_HV_05 | High |
| BUG#03 | Xóa đăng ký gói có hóa đơn → crash | Khi xóa DangKyGoi đang có bản ghi HoaDon liên quan, DAO ném SqlException (FK violation) không được xử lý → crash | TC_BUG_03, TC_DK_05 | High |
| BUG#04 | HinhThucTT rỗng insert thành công | Bảng HoaDon có CHECK constraint cho HinhThucTT IN ('Tiền mặt', 'Chuyển khoản', 'Thẻ', 'Khác') nhưng giá trị rỗng "" vẫn insert được → thiếu validation hoặc CHECK constraint không hoạt động đúng | TC_BUG_04, TC_HD_06 | Medium |
| BUG#05 | PhanCong GhiChu rỗng có thể gây lỗi | Insert PhanCong với GhiChu="" có thể gây ArgumentNullException trong một số trường hợp. Cần kiểm tra xử lý null/empty cho trường GhiChu | TC_BUG_05, TC_PC_05 | Low |
| BUG#06 | NgayHetHan không tự tính | Khi tạo DangKyGoi, NgayHetHan không được tự động tính từ NgayBatDau + ThoiHan của GoiTap. Người dùng phải nhập tay → dễ nhập sai | TC_BUG_06 | Medium |
| BUG#07 | cboDangKy chỉ hiển thị TenHV | ComboBox đăng ký trong FormPhanCong/FormHoaDon chỉ hiển thị TenHV (DisplayMember). Khi 1 học viên có nhiều đăng ký → không phân biệt được đăng ký nào | TC_BUG_07 | Medium |
| BUG#08 | GoiTapDAO thiếu phương thức Search() | GoiTapDAO không implement phương thức Search() → chức năng tìm kiếm gói tập trên UI không hoạt động. Các DAO khác (HocVienDAO, HuanLuyenVienDAO) đều có Search() | TC_BUG_08, TC_GT_06 | High |
| BUG#09 | DangKyGoiDAO thiếu phương thức Search() | DangKyGoiDAO không implement phương thức Search() → chức năng tìm kiếm đăng ký gói trên UI không hoạt động | TC_BUG_09, TC_DK_07 | High |
| BUG#10 | Dashboard cards bị tràn MinimumWidth | 5 card thống kê trên Dashboard có tổng width (5 × (195 + 12) = 1035px) có thể bị tràn khi form có MinimumWidth nhỏ hơn | TC_BUG_10 | Low |

### 7.2 Phân loại bug theo mức độ nghiêm trọng

| Mức độ | Số lượng | Bug ID | Mô tả |
|--------|----------|--------|-------|
| **Critical** | 0 | – | Không có bug gây mất dữ liệu hoặc sập hệ thống hoàn toàn |
| **High** | 4 | BUG#02, BUG#03, BUG#08, BUG#09 | Crash khi xóa dữ liệu có FK, thiếu Search trên 2 module |
| **Medium** | 4 | BUG#01, BUG#04, BUG#06, BUG#07 | Encoding lỗi, thiếu validation, UX không tốt |
| **Low** | 2 | BUG#05, BUG#10 | GhiChu rỗng có thể gây lỗi, UI tràn |
| **Tổng** | **10** | | |

### 7.3 Biểu đồ thống kê bug

#### Phân bố bug theo mức độ:
```
High     ████████████████████  4 bug (40%)
Medium   ████████████████████  4 bug (40%)
Low      ██████████            2 bug (20%)
Critical                       0 bug (0%)
```

#### Phân bố bug theo module:
```
Đăng nhập        █           1 bug (BUG#01)
Học viên          █           1 bug (BUG#02)
Gói tập           █           1 bug (BUG#08)
Đăng ký gói       ███         3 bug (BUG#03, BUG#06, BUG#09)
Phân công         █           1 bug (BUG#05)
Hóa đơn           ██          2 bug (BUG#04, BUG#07)
Dashboard         █           1 bug (BUG#10)
```

#### Phân loại bug theo nguyên nhân:
| Nguyên nhân | Số lượng | Bug ID |
|-------------|----------|--------|
| Thiếu method (code chưa implement) | 2 | BUG#08, BUG#09 |
| Thiếu xử lý FK exception | 2 | BUG#02, BUG#03 |
| Thiếu validation / CHECK constraint | 2 | BUG#04, BUG#05 |
| Logic nghiệp vụ thiếu | 1 | BUG#06 |
| UI/UX không đúng | 2 | BUG#07, BUG#10 |
| Lỗi encoding | 1 | BUG#01 |

---

## PHẦN 8: KẾT QUẢ VÀ ĐÁNH GIÁ

### 8.1 Tổng kết toàn bộ quá trình kiểm thử

| Hạng mục | Giá trị |
|----------|---------|
| Tổng số test case | 71 |
| Test case Pass | 71 |
| Test case Fail | 0 |
| Test case Skip | 0 |
| Tỷ lệ Pass | 100% |
| Tổng số bug phát hiện | 10 |
| Bug mức High | 4 |
| Bug mức Medium | 4 |
| Bug mức Low | 2 |
| Số module được kiểm thử | 10 (Đăng nhập, Học viên, HLV, Gói tập, Ca làm, Đăng ký gói, Phân công, Hóa đơn, Dashboard, Bug Report) |
| Số file test | 11 (bao gồm TestBase.cs) |

### 8.2 Đánh giá chất lượng sau kiểm thử

**Chức năng hoạt động tốt:**
- Đăng nhập xác thực đúng với SHA2_512 + Salt
- CRUD trên tất cả các module hoạt động cơ bản chính xác
- Ràng buộc Foreign Key bảo vệ toàn vẹn dữ liệu
- Dashboard thống kê chính xác (Count, TotalRevenue khớp DB)
- Tìm kiếm học viên và HLV hoạt động đúng

**Chức năng cần cải thiện:**
- Xử lý exception khi FK violation (BUG#02, BUG#03) → cần try-catch và thông báo thân thiện
- Thiếu Search() ở GoiTapDAO và DangKyGoiDAO (BUG#08, BUG#09)
- Validation dữ liệu đầu vào chưa đầy đủ (BUG#04, BUG#05)
- Tự động tính NgayHetHan (BUG#06)
- Hiển thị đăng ký gói trong ComboBox chưa đủ thông tin (BUG#07)

### 8.3 Những gì làm được, chưa làm được

**Đã làm được:**
- ✅ Xây dựng 71 test case tự động bao phủ 10 module chính
- ✅ Phát hiện 10 bug với mức độ nghiêm trọng khác nhau
- ✅ Kiểm thử CRUD đầy đủ cho tất cả các DAO (trừ PhongTapDAO)
- ✅ Kiểm thử ràng buộc FK, CHECK constraint
- ✅ Kiểm thử validation dữ liệu đầu vào (positive và negative)
- ✅ Kiểm thử tích hợp DAO – Database thực
- ✅ Thiết kế test tự động cleanup dữ liệu, độc lập giữa các test
- ✅ Hệ thống test có khả năng hồi quy (BugReportTests)

**Chưa làm được:**
- ❌ Chưa kiểm thử UI tự động (WinForms automation)
- ❌ Chưa kiểm thử PhongTapDAO
- ❌ Chưa sử dụng mock/stub cho database → phụ thuộc DB thực
- ❌ Chưa kiểm thử bảo mật (SQL Injection, XSS)
- ❌ Chưa kiểm thử hiệu năng với dữ liệu lớn (load test)
- ❌ Chưa đo code coverage chính thức
- ❌ Chưa kiểm thử đổi mật khẩu (ChangePassword)

### 8.4 Bài học kinh nghiệm rút ra

1. **Kiểm thử tích hợp DB cần cơ chế tự dọn dẹp:** Sử dụng TearDown + Cleanup() để đảm bảo dữ liệu test không ảnh hưởng môi trường. Nếu không, các test chạy lần sau sẽ bị ảnh hưởng bởi dữ liệu rác.

2. **Cần kiểm tra FK constraint trước khi xóa:** Đây là nguồn bug phổ biến nhất (BUG#02, BUG#03). Mọi thao tác Delete cần được bọc trong try-catch với thông báo thân thiện cho người dùng.

3. **DAO cần implement đầy đủ các phương thức:** Thiếu Search() ở GoiTapDAO và DangKyGoiDAO (BUG#08, BUG#09) cho thấy việc kiểm tra tính nhất quán giữa các module là quan trọng.

4. **Validation nên được đặt ở nhiều tầng:** Cả tầng UI (form) và tầng DB (CHECK constraint) đều cần validation. BUG#04 cho thấy chỉ dựa vào CHECK constraint là không đủ.

5. **Test Bug Report là cách tốt để theo dõi bug:** Các test xác nhận bug tồn tại giúp đảm bảo khi fix bug, ta biết chính xác test nào cần cập nhật.

6. **Assert.Ignore() rất hữu ích:** Cho phép test tự động skip khi không đủ điều kiện (thiếu DB, thiếu dữ liệu) thay vì Fail sai.

7. **Encoding Unicode cần chú ý:** Ký tự tiếng Việt trong SQL phải dùng N'' prefix. BUG#01 là minh chứng rõ ràng.

### 8.5 Đề xuất cải tiến ứng dụng và quy trình kiểm thử

**Cải tiến ứng dụng:**

| STT | Đề xuất | Mức độ ưu tiên | Bug liên quan |
|-----|---------|----------------|---------------|
| 1 | Thêm try-catch khi xóa bản ghi có FK, hiển thị thông báo thân thiện | Cao | BUG#02, BUG#03 |
| 2 | Implement Search() cho GoiTapDAO và DangKyGoiDAO | Cao | BUG#08, BUG#09 |
| 3 | Tự động tính NgayHetHan = NgayBatDau + ThoiHan khi tạo đăng ký gói | Trung bình | BUG#06 |
| 4 | Hiển thị MaDK + TenHV + TenGoi trong ComboBox đăng ký | Trung bình | BUG#07 |
| 5 | Thêm validation cho HinhThucTT tại tầng UI trước khi insert | Trung bình | BUG#04 |
| 6 | Sửa encoding trong setup_db.sql | Thấp | BUG#01 |
| 7 | Xử lý GhiChu null/empty đồng nhất | Thấp | BUG#05 |
| 8 | Điều chỉnh layout Dashboard cho responsive | Thấp | BUG#10 |

**Cải tiến quy trình kiểm thử:**

1. **Thêm mock database:** Sử dụng SQLite in-memory hoặc Moq để test DAO offline
2. **Bổ sung test PhongTapDAO và ChangePassword:** Tăng độ bao phủ
3. **Thêm UI automation test:** Sử dụng FlaUI hoặc WinAppDriver cho kiểm thử giao diện
4. **Đo code coverage:** Chạy `dotnet test --collect:"XPlat Code Coverage"` để biết % code được test
5. **Tích hợp CI/CD:** Chạy test tự động khi push code lên repository
6. **Kiểm thử bảo mật:** Kiểm tra SQL Injection trên các trường text input

### 8.6 Kết luận

Quá trình kiểm thử ứng dụng AppGym đã được thực hiện một cách có hệ thống với 71 test case tự động bao phủ 10 module chính. Tất cả test case đều Pass, đồng thời phát hiện được 10 bug với các mức độ nghiêm trọng khác nhau (4 High, 4 Medium, 2 Low).

Các bug chính tập trung vào: thiếu xử lý FK exception khi xóa dữ liệu liên quan, thiếu phương thức Search() ở 2 module (GoiTap, DangKyGoi), và một số vấn đề validation/UI. Đây đều là những bug có thể fix được và không ảnh hưởng nghiêm trọng đến dữ liệu.

Bộ test tự động NUnit đã được thiết kế tốt với khả năng tự dọn dẹp dữ liệu, skip khi không có DB, và xác nhận bug để hồi quy. Tuy nhiên, cần bổ sung thêm mock database, UI automation test, và mở rộng bao phủ sang các module chưa test (PhongTap, ChangePassword) để nâng cao chất lượng kiểm thử tổng thể.

---

## TÀI LIỆU THAM KHẢO

1. **NUnit Documentation** – https://docs.nunit.org/
2. **Microsoft .NET 8.0 Documentation** – https://learn.microsoft.com/en-us/dotnet/
3. **Microsoft Data SqlClient** – https://learn.microsoft.com/en-us/sql/connect/ado-net/microsoft-ado-net-sql-server
4. **SQL Server Express Documentation** – https://learn.microsoft.com/en-us/sql/sql-server/
5. **Windows Forms Documentation** – https://learn.microsoft.com/en-us/dotnet/desktop/winforms/
6. **ISTQB Foundation Level Syllabus** – https://www.istqb.org/
7. **Software Testing: Principles and Practices** – Srinivasan Desikan, Gopalaswamy Ramesh
8. **Coverlet Code Coverage** – https://github.com/coverlet-coverage/coverlet
