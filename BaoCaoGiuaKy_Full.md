# BÁO CÁO GIỮA KỲ MÔN KIỂM THỬ PHẦN MỀM

**Đề tài:** Kiểm thử ứng dụng AppGym – Quản lý phòng Gym  
**Ngày báo cáo:** 05/04/2026  

---

# BẢNG PHÂN CÔNG CÔNG VIỆC

| STT | Thành viên | Công việc | Tỷ lệ đóng góp |
|-----|-----------|-----------|-----------------|
| 1 | (Họ tên SV 1) | Phân tích yêu cầu, thiết kế test case Đăng nhập, Học viên, Dashboard | ...% |
| 2 | (Họ tên SV 2) | Thiết kế test case HLV, Gói tập, Ca làm, viết test script | ...% |
| 3 | (Họ tên SV 3) | Thiết kế test case Đăng ký gói, Phân công, Hóa đơn, bug report | ...% |

> *(Sinh viên tự điền thông tin)*

---

# NHẬN XÉT CỦA GIẢNG VIÊN

.......................................................................................................................................................

.......................................................................................................................................................

.......................................................................................................................................................

.......................................................................................................................................................

**Điểm:** ............./10

**Chữ ký giảng viên:** .....................

---

\newpage

# MỤC LỤC

- [CHƯƠNG 1: TỔNG QUAN DỰ ÁN](#chương-1-tổng-quan-dự-án)
  - [1.1 Giới thiệu ứng dụng kiểm thử](#11-giới-thiệu-ứng-dụng-kiểm-thử)
  - [1.2 Mục tiêu](#12-mục-tiêu)
  - [1.3 Công nghệ sử dụng](#13-công-nghệ-sử-dụng)
  - [1.4 Thời gian dự kiến kiểm thử](#14-thời-gian-dự-kiến-kiểm-thử)
- [CHƯƠNG 2: PHÂN TÍCH YÊU CẦU VÀ PHẠM VI KIỂM THỬ](#chương-2-phân-tích-yêu-cầu-và-phạm-vi-kiểm-thử)
  - [2.1 Danh sách các chức năng của ứng dụng](#21-danh-sách-các-chức-năng-của-ứng-dụng)
  - [2.2 Phạm vi kiểm thử](#22-phạm-vi-kiểm-thử)
  - [2.3 Đối tượng kiểm thử](#23-đối-tượng-kiểm-thử)
  - [2.4 Ma trận liên kết yêu cầu - ca kiểm thử](#24-ma-trận-liên-kết-yêu-cầu---ca-kiểm-thử)
- [CHƯƠNG 3: KẾ HOẠCH KIỂM THỬ](#chương-3-kế-hoạch-kiểm-thử)
  - [3.1 Mục tiêu kiểm thử](#31-mục-tiêu-kiểm-thử)
  - [3.2 Chiến lược kiểm thử](#32-chiến-lược-kiểm-thử)
  - [3.3 Môi trường kiểm thử](#33-môi-trường-kiểm-thử)
  - [3.4 Tiêu chí bắt đầu kiểm thử](#34-tiêu-chí-bắt-đầu-kiểm-thử)
  - [3.5 Tiêu chí kết thúc kiểm thử](#35-tiêu-chí-kết-thúc-kiểm-thử)
  - [3.6 Rủi ro và biện pháp xử lý](#36-rủi-ro-và-biện-pháp-xử-lý)
- [CHƯƠNG 4: THIẾT KẾ CA KIỂM THỬ](#chương-4-thiết-kế-ca-kiểm-thử)
  - [4.1 Cơ sở thiết kế ca kiểm thử](#41-cơ-sở-thiết-kế-ca-kiểm-thử)
  - [4.2 Phân loại ca kiểm thử](#42-phân-loại-ca-kiểm-thử)
  - [4.3 Thiết kế test case cho các chức năng chính](#43-thiết-kế-test-case-cho-các-chức-năng-chính)
  - [4.4 Thiết kế dữ liệu kiểm thử](#44-thiết-kế-dữ-liệu-kiểm-thử)
  - [4.5 Thiết kế kịch bản kiểm thử tự động](#45-thiết-kế-kịch-bản-kiểm-thử-tự-động)
  - [4.6 Bảng tổng hợp test case](#46-bảng-tổng-hợp-test-case)
- [CHƯƠNG 5: THỰC THI KIỂM THỬ](#chương-5-thực-thi-kiểm-thử)
  - [5.1 Kiểm thử chức năng](#51-kiểm-thử-chức-năng)
  - [5.2 Kiểm thử tích hợp](#52-kiểm-thử-tích-hợp)
  - [5.3 Kiểm thử hệ thống](#53-kiểm-thử-hệ-thống)
  - [5.4 Kiểm thử hiệu năng](#54-kiểm-thử-hiệu-năng)
  - [5.5 Kiểm thử hồi quy](#55-kiểm-thử-hồi-quy)
- [PHẦN 6: KIỂM THỬ TỰ ĐỘNG (AUTOMATION TEST)](#phần-6-kiểm-thử-tự-động-automation-test)
  - [6.1 Công cụ sử dụng và lý do lựa chọn](#61-công-cụ-sử-dụng-và-lý-do-lựa-chọn)
  - [6.2 Các test script đã viết](#62-các-test-script-đã-viết)
  - [6.3 Hướng dẫn cài đặt và chạy script](#63-hướng-dẫn-cài-đặt-và-chạy-script)
  - [6.4 Kết quả chạy tự động](#64-kết-quả-chạy-tự-động)
  - [6.5 Nhận xét về automation](#65-nhận-xét-về-automation)
- [PHẦN 7: BÁO CÁO LỖI (BUG REPORT)](#phần-7-báo-cáo-lỗi-bug-report)
  - [7.1 Danh sách toàn bộ bug tìm được](#71-danh-sách-toàn-bộ-bug-tìm-được)
  - [7.2 Phân loại bug theo mức độ nghiêm trọng](#72-phân-loại-bug-theo-mức-độ-nghiêm-trọng)
  - [7.3 Biểu đồ thống kê bug](#73-biểu-đồ-thống-kê-bug)
- [PHẦN 8: KẾT QUẢ VÀ ĐÁNH GIÁ](#phần-8-kết-quả-và-đánh-giá)
  - [8.1 Tổng kết toàn bộ quá trình kiểm thử](#81-tổng-kết-toàn-bộ-quá-trình-kiểm-thử)
  - [8.2 Đánh giá chất lượng sau kiểm thử](#82-đánh-giá-chất-lượng-sau-kiểm-thử)
  - [8.3 Những gì làm được, chưa làm được](#83-những-gì-làm-được-chưa-làm-được)
  - [8.4 Bài học kinh nghiệm rút ra](#84-bài-học-kinh-nghiệm-rút-ra)
  - [8.5 Đề xuất cải tiến ứng dụng và quy trình kiểm thử](#85-đề-xuất-cải-tiến-ứng-dụng-và-quy-trình-kiểm-thử)
  - [8.6 Kết luận](#86-kết-luận)
- [TÀI LIỆU THAM KHẢO](#tài-liệu-tham-khảo)

---

\newpage

# DANH MỤC HÌNH ẢNH

| Hình | Mô tả | Trang |
|------|-------|-------|
| Hình 1.1 | Kiến trúc phân lớp ứng dụng AppGym | ... |
| Hình 1.2 | Sơ đồ ERD (Entity-Relationship Diagram) cơ sở dữ liệu | ... |
| Hình 1.3 | Giao diện đăng nhập FormLogin | ... |
| Hình 1.4 | Giao diện chính FormMain với Dashboard | ... |
| Hình 1.5 | Cấu trúc thư mục dự án trong Visual Studio | ... |
| Hình 2.1 | Use Case Diagram tổng quan hệ thống | ... |
| Hình 2.2 | Use Case quản lý Học viên | ... |
| Hình 2.3 | Use Case quản lý Gói tập và Đăng ký | ... |
| Hình 3.1 | Quy trình kiểm thử phần mềm áp dụng | ... |
| Hình 3.2 | Ma trận kiểm thử hộp đen – Đăng nhập | ... |
| Hình 4.1 | Luồng xử lý đăng nhập (Flowchart) | ... |
| Hình 4.2 | Luồng xử lý CRUD Học viên | ... |
| Hình 5.1 | Kết quả chạy test trên Visual Studio Test Explorer | ... |
| Hình 5.2 | Chi tiết kết quả test LoginTests | ... |
| Hình 5.3 | Chi tiết kết quả test HocVienTests | ... |
| Hình 5.4 | Chi tiết kết quả test GoiTapTests | ... |
| Hình 5.5 | Chi tiết kết quả test DangKyGoiTests | ... |
| Hình 5.6 | Chi tiết kết quả test HoaDonTests | ... |
| Hình 5.7 | Chi tiết kết quả test DashboardTests | ... |
| Hình 5.8 | Chi tiết kết quả test BugReportTests | ... |
| Hình 6.1 | Cấu trúc project AppGym.Tests trong Solution Explorer | ... |
| Hình 6.2 | Terminal chạy dotnet test toàn bộ | ... |
| Hình 6.3 | Kết quả Test Explorer hiển thị 71/71 Pass | ... |
| Hình 6.4 | Kết quả chạy test theo Category "Dang nhap" | ... |
| Hình 6.5 | Kết quả chạy test theo Category "Bug Report" | ... |
| Hình 7.1 | Giao diện lỗi khi xóa học viên có đăng ký (BUG#02) | ... |
| Hình 7.2 | GoiTapDAO thiếu Search – so sánh với HocVienDAO (BUG#08) | ... |
| Hình 7.3 | HinhThucTT rỗng insert được vào DB (BUG#04) | ... |
| Hình 7.4 | cboDangKy chỉ hiển thị TenHV (BUG#07) | ... |
| Hình 7.5 | Biểu đồ phân bố bug theo mức độ | ... |
| Hình 7.6 | Biểu đồ phân bố bug theo module | ... |
| Hình 8.1 | Biểu đồ tổng quan kết quả kiểm thử | ... |
| Hình 8.2 | Biểu đồ so sánh test Pass/Fail theo module | ... |

---

# DANH MỤC BẢNG

| Bảng | Mô tả | Trang |
|------|-------|-------|
| Bảng 1.1 | Công nghệ sử dụng trong dự án | ... |
| Bảng 1.2 | Thời gian dự kiến kiểm thử | ... |
| Bảng 2.1 | Danh sách chức năng ứng dụng | ... |
| Bảng 2.2 | Ma trận liên kết yêu cầu – ca kiểm thử | ... |
| Bảng 3.1 | Môi trường kiểm thử | ... |
| Bảng 3.2 | Ma trận rủi ro và biện pháp xử lý | ... |
| Bảng 4.1 – 4.9 | Chi tiết test case cho từng module | ... |
| Bảng 4.10 | Bảng tổng hợp test case | ... |
| Bảng 5.1 – 5.9 | Kết quả thực thi kiểm thử từng module | ... |
| Bảng 6.1 | Danh sách test script | ... |
| Bảng 6.2 | Kết quả chạy tự động theo category | ... |
| Bảng 7.1 | Danh sách bug report | ... |
| Bảng 7.2 | Chi tiết từng bug | ... |
| Bảng 8.1 | Tổng kết kết quả kiểm thử | ... |

---

# DANH MỤC VIẾT TẮT

| Viết tắt | Ý nghĩa |
|----------|---------|
| TC | Test Case (Ca kiểm thử) |
| DAO | Data Access Object (Đối tượng truy cập dữ liệu) |
| CRUD | Create, Read, Update, Delete |
| FK | Foreign Key (Khóa ngoại) |
| DB | Database (Cơ sở dữ liệu) |
| UI | User Interface (Giao diện người dùng) |
| HV | Học viên |
| HLV | Huấn luyện viên |
| GT | Gói tập |
| CL | Ca làm |
| DK | Đăng ký (gói) |
| PC | Phân công |
| HD | Hóa đơn |
| PT | Personal Trainer |
| SĐT | Số điện thoại |
| SDK | Software Development Kit |
| IDE | Integrated Development Environment |
| ERD | Entity-Relationship Diagram |
| SQL | Structured Query Language |
| TRX | Test Results XML |

---

\newpage

# CHƯƠNG 1: TỔNG QUAN DỰ ÁN

## 1.1 Giới thiệu ứng dụng kiểm thử

### 1.1.1 Tổng quan

**AppGym** là ứng dụng desktop quản lý phòng Gym được phát triển trên nền tảng .NET 8.0 Windows Forms. Ứng dụng được xây dựng nhằm hỗ trợ các phòng tập gym trong việc quản lý toàn diện các hoạt động nghiệp vụ hàng ngày, bao gồm:

- Quản lý thông tin học viên đăng ký tập luyện
- Quản lý đội ngũ huấn luyện viên (HLV/PT)
- Quản lý các gói tập với thời hạn và giá khác nhau
- Đăng ký gói tập cho học viên
- Phân công huấn luyện viên kèm học viên
- Quản lý ca làm việc
- Quản lý hóa đơn thanh toán
- Dashboard thống kê tổng quan hoạt động phòng gym

Ứng dụng hướng tới đối tượng sử dụng là nhân viên lễ tân và quản lý phòng gym, hỗ trợ phân quyền Admin/NhanVien để kiểm soát truy cập.

### 1.1.2 Kiến trúc ứng dụng

Ứng dụng sử dụng **kiến trúc phân lớp** (Layered Architecture) gồm 4 tầng chính:

```
┌───────────────────────────────────────────┐
│           Presentation Layer              │
│     (Windows Forms – Forms/*.cs)          │
│  FormLogin, FormMain, FormHocVienDetail,  │
│  FormHLVDetail, FormGoiTapDetail, ...     │
├───────────────────────────────────────────┤
│           Business Logic Layer            │
│   (Validation logic trong Forms & DAO)    │
├───────────────────────────────────────────┤
│           Data Access Layer               │
│     (DataAccess/*DAO.cs)                  │
│  TaiKhoanDAO, HocVienDAO, GoiTapDAO,     │
│  HuanLuyenVienDAO, CaLamDAO, ...         │
├───────────────────────────────────────────┤
│             Database Layer                │
│     (SQL Server – GymManagementDB)        │
│  11 bảng: HocVien, HuanLuyenVien, ...    │
└───────────────────────────────────────────┘
```

> **Hình 1.1: Kiến trúc phân lớp ứng dụng AppGym**
>
> *(Chèn ảnh minh họa kiến trúc phân lớp)*

### 1.1.3 Cấu trúc thư mục dự án

```
AppGym/
├── AppGym.csproj               # File project chính (.NET 8.0 WinForms)
├── AppGym.sln                  # Solution file
├── Program.cs                  # Entry point
├── Models/                     # Lớp thực thể dữ liệu
│   ├── TaiKhoan.cs
│   ├── HocVien.cs
│   ├── HuanLuyenVien.cs
│   ├── GoiTap.cs
│   ├── CaLam.cs
│   ├── DangKyGoi.cs
│   ├── PhanCong.cs
│   ├── HoaDon.cs
│   └── PhongTap.cs
├── DataAccess/                 # Lớp truy xuất dữ liệu (DAO)
│   ├── DatabaseHelper.cs       # Quản lý connection string
│   ├── TaiKhoanDAO.cs
│   ├── HocVienDAO.cs
│   ├── HuanLuyenVienDAO.cs
│   ├── GoiTapDAO.cs
│   ├── CaLamDAO.cs
│   ├── DangKyGoiDAO.cs
│   ├── PhanCongDAO.cs
│   ├── HoaDonDAO.cs
│   └── PhongTapDAO.cs
├── Forms/                      # Giao diện người dùng
│   ├── FormLogin.cs            # Đăng nhập
│   ├── FormMain.cs             # Giao diện chính + Dashboard
│   ├── FormHocVienDetail.cs    # Thêm/sửa học viên
│   ├── FormHLVDetail.cs        # Thêm/sửa HLV
│   ├── FormGoiTapDetail.cs     # Thêm/sửa gói tập
│   ├── FormCaLamDetail.cs      # Thêm/sửa ca làm
│   ├── FormDangKyGoiDetail.cs  # Thêm/sửa đăng ký gói
│   ├── FormPhanCongDetail.cs   # Thêm/sửa phân công
│   ├── FormHoaDonDetail.cs     # Thêm/sửa hóa đơn
│   └── FormPhongTapDetail.cs   # Thêm/sửa phòng tập
├── Helpers/
│   └── ThemeHelper.cs          # Hỗ trợ giao diện
├── AppGym.Tests/               # Project kiểm thử NUnit
│   ├── AppGym.Tests.csproj
│   ├── TestBase.cs
│   ├── LoginTests.cs
│   ├── HocVienTests.cs
│   ├── HuanLuyenVienTests.cs
│   ├── GoiTapTests.cs
│   ├── CaLamTests.cs
│   ├── DangKyGoiTests.cs
│   ├── PhanCongTests.cs
│   ├── HoaDonTests.cs
│   ├── DashboardTests.cs
│   └── BugReportTests.cs
├── CodeTaoBang.sql             # Script tạo database
├── data.sql                    # Dữ liệu mẫu
├── setup_db.sql                # Setup database
└── reset_db.sql                # Reset database
```

> **Hình 1.5: Cấu trúc thư mục dự án trong Visual Studio**
>
> *(Chèn screenshot Solution Explorer từ Visual Studio)*

### 1.1.4 Sơ đồ cơ sở dữ liệu (ERD)

Cơ sở dữ liệu **GymManagementDB** gồm 11 bảng dữ liệu với các mối quan hệ:

```
┌──────────────┐       ┌──────────────┐       ┌──────────────┐
│   PhongTap   │       │   HocVien    │       │HuanLuyenVien │
│──────────────│       │──────────────│       │──────────────│
│ MaPhong (PK) │       │ MaHV (PK)    │       │ MaHLV (PK)   │
│ TenPhong     │       │ HoTen        │       │ HoTen        │
│ DiaChi       │       │ GioiTinh     │       │ GioiTinh     │
│ SucChua      │       │ NgaySinh     │       │ SDT          │
│ MoTa         │       │ SDT (UQ)     │       │ ChuyenMon    │
│ TrangThai    │       │ Email (UQ)   │       │ Luong        │
│              │       │ NgayDangKy   │       │ TrangThai    │
│              │       │ TrangThai    │       │              │
└──────────────┘       └──────┬───────┘       └──────┬───────┘
                              │ 1:N                   │ 1:N
                              ▼                       │
┌──────────────┐       ┌──────────────┐               │
│   GoiTap     │       │  DangKyGoi   │               │
│──────────────│       │──────────────│               │
│ MaGoi (PK)   │──1:N─▶│ MaDK (PK)    │               │
│ TenGoi (UQ)  │       │ MaHV (FK)    │               │
│ ThoiHan      │       │ MaGoi (FK)   │               │
│ Gia          │       │ NgayBatDau   │               │
│ MoTa         │       │ NgayHetHan   │               │
│ TrangThai    │       │ TrangThai    │               │
│              │       │ GhiChu       │               │
└──────────────┘       └──────┬───────┘               │
                              │ 1:N                   │
                    ┌─────────┴──────────┐            │
                    ▼                    ▼            ▼
             ┌──────────────┐     ┌──────────────┐
             │    HoaDon    │     │   PhanCong   │
             │──────────────│     │──────────────│
             │ MaHD (PK)    │     │ MaPC (PK)    │
             │ MaDK (FK)    │     │ MaHLV (FK)   │◀── HuanLuyenVien
             │ NgayThanhToan│     │ MaDK (FK)    │◀── DangKyGoi
             │ SoTien       │     │ MaCa (FK)    │◀── CaLam
             │ HinhThucTT   │     │ NgayBatDau   │
             │ GhiChu       │     │ NgayKetThuc  │
             └──────────────┘     │ GhiChu       │
                                  └──────────────┘

┌──────────────┐       ┌──────────────┐       ┌──────────────┐
│   CaLam      │       │   TaiKhoan   │       │    Quyen     │
│──────────────│       │──────────────│       │──────────────│
│ MaCa (PK)    │       │ MaTK (PK)    │       │ MaQuyen (PK) │
│ TenCa (UQ)   │       │ TenDangNhap  │       │ TenBang      │
│ GioBatDau    │       │ MatKhauHash  │       │ HanhDong     │
│ GioKetThuc   │       │ Salt         │       │ MoTa         │
└──────────────┘       │ HoTen        │       └──────┬───────┘
                       │ VaiTro       │              │
                       │ TrangThai    │    TaiKhoan_Quyen
                       │ TaoLuc       │     (MaTK, MaQuyen)
                       └──────────────┘
```

> **Hình 1.2: Sơ đồ ERD (Entity-Relationship Diagram) cơ sở dữ liệu GymManagementDB**
>
> *(Chèn ảnh ERD từ SQL Server Management Studio hoặc vẽ bằng draw.io)*

### 1.1.5 Giao diện chính của ứng dụng

**Màn hình đăng nhập (FormLogin):**

> **Hình 1.3: Giao diện đăng nhập FormLogin**
>
> *(Chèn screenshot màn hình đăng nhập với các trường: Tên đăng nhập, Mật khẩu, nút Đăng nhập, nút Đóng)*

Màn hình đăng nhập cho phép người dùng nhập tên đăng nhập và mật khẩu. Ứng dụng sử dụng mã hóa SHA2_512 kết hợp Salt (UNIQUEIDENTIFIER) để bảo mật mật khẩu. Khi đăng nhập thành công, hệ thống chuyển sang FormMain với thông tin người dùng.

**Màn hình chính (FormMain) với Dashboard:**

> **Hình 1.4: Giao diện chính FormMain với Dashboard thống kê**
>
> *(Chèn screenshot màn hình chính với sidebar menu bên trái và dashboard bên phải hiển thị các card thống kê: Học viên, HLV, Gói tập, Đăng ký đang HĐ, Doanh thu, Phòng tập)*

Màn hình chính gồm:
- **Sidebar bên trái**: Menu điều hướng với các mục: Tổng quan, Học viên, Huấn luyện viên, Gói tập, Đăng ký gói, Phân công PT, Hóa đơn, Ca làm, Phòng tập
- **Thanh trên (Top Bar)**: Hiển thị tên người dùng đang đăng nhập, menu dropdown (Quản lý tài khoản, Đăng xuất)
- **Vùng nội dung chính**: Hiển thị Dashboard hoặc các form quản lý tùy theo menu được chọn

## 1.2 Mục tiêu

### 1.2.1 Mục tiêu tổng quát

Kiểm thử toàn diện ứng dụng AppGym nhằm:
- Đảm bảo tất cả các chức năng CRUD hoạt động chính xác với cơ sở dữ liệu SQL Server
- Phát hiện và ghi nhận các lỗi (bug) trong ứng dụng
- Xây dựng bộ kiểm thử tự động (automation test) có khả năng chạy lại (repeatable) và tự dọn dẹp dữ liệu
- Đánh giá chất lượng tổng thể của phần mềm

### 1.2.2 Mục tiêu cụ thể

| STT | Mục tiêu | Chỉ tiêu đo lường |
|-----|----------|-------------------|
| 1 | Bao phủ kiểm thử ≥ 90% chức năng | Số module được test / Tổng module |
| 2 | Viết ≥ 60 test case tự động | Số test case trong NUnit project |
| 3 | Tỷ lệ test Pass ≥ 90% | Số Pass / Tổng test case |
| 4 | Phát hiện ≥ 5 bug | Số bug trong Bug Report |
| 5 | Phân loại bug theo mức độ | Bảng phân loại Critical/High/Medium/Low |
| 6 | Kiểm thử cả positive và negative | Tỷ lệ Positive/Negative test case |

### 1.2.3 Mục tiêu học tập

- Rèn luyện kỹ năng phân tích yêu cầu và thiết kế test case
- Thực hành viết test script tự động bằng NUnit Framework trên .NET
- Hiểu quy trình kiểm thử phần mềm từ lập kế hoạch → thiết kế → thực thi → báo cáo
- Phân biệt và áp dụng kiểm thử hộp trắng (white-box) và hộp đen (black-box)
- Viết báo cáo bug chuyên nghiệp theo chuẩn

## 1.3 Công nghệ sử dụng

### 1.3.1 .NET Windows Forms

| Thông số | Chi tiết |
|----------|----------|
| **Nền tảng** | .NET 8.0 (net8.0-windows) |
| **Kiểu ứng dụng** | WinExe (Windows Desktop Application) |
| **Framework UI** | Windows Forms |
| **Ngôn ngữ** | C# 12.0 |
| **Nullable** | enable (Nullable Reference Types) |
| **ImplicitUsings** | enable |

**File cấu hình project (AppGym.csproj):**
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net8.0-windows</TargetFramework>
    <Nullable>enable</Nullable>
    <UseWindowsForms>true</UseWindowsForms>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Microsoft.Data.SqlClient" Version="5.2.0" />
  </ItemGroup>
</Project>
```

Windows Forms là framework GUI trưởng thành của Microsoft, phù hợp cho ứng dụng desktop nội bộ doanh nghiệp. AppGym sử dụng các control chuẩn: TextBox, ComboBox, DataGridView, DateTimePicker, Button, Label, Panel, CheckBox.

### 1.3.2 SQL Server

| Thông số | Chi tiết |
|----------|----------|
| **Phiên bản** | SQL Server Express 2019+ |
| **Instance** | (local)\SQLEXPRESS |
| **Database** | GymManagementDB |
| **Xác thực** | Windows Authentication (Trusted_Connection=True) |
| **Driver** | Microsoft.Data.SqlClient v5.2.0 |
| **SSL** | TrustServerCertificate=True |
| **Số bảng** | 11 bảng |
| **Mã hóa mật khẩu** | SHA2_512 + Salt (UNIQUEIDENTIFIER) |

**Chi tiết 11 bảng dữ liệu:**

| STT | Tên bảng | Mô tả | Số cột | Primary Key |
|-----|----------|-------|--------|-------------|
| 1 | PhongTap | Phòng tập | 6 | MaPhong (IDENTITY) |
| 2 | HocVien | Học viên | 8 | MaHV (IDENTITY) |
| 3 | HuanLuyenVien | Huấn luyện viên | 7 | MaHLV (IDENTITY) |
| 4 | CaLam | Ca làm | 4 | MaCa (IDENTITY) |
| 5 | GoiTap | Gói tập | 6 | MaGoi (IDENTITY) |
| 6 | DangKyGoi | Đăng ký gói | 7 | MaDK (IDENTITY) |
| 7 | PhanCong | Phân công | 7 | MaPC (IDENTITY) |
| 8 | HoaDon | Hóa đơn | 6 | MaHD (IDENTITY) |
| 9 | Quyen | Quyền | 4 | MaQuyen (IDENTITY) |
| 10 | TaiKhoan | Tài khoản | 7 | MaTK (IDENTITY) |
| 11 | TaiKhoan_Quyen | Liên kết TK-Quyền | 2 | (MaTK, MaQuyen) |

**Các ràng buộc quan trọng trong database:**

| Bảng | Ràng buộc | Loại | Mô tả |
|------|-----------|------|-------|
| HocVien | SDT UNIQUE | Unique | Số điện thoại không trùng |
| HocVien | Email UNIQUE | Unique | Email không trùng |
| HocVien | GioiTinh IN ('Nam', 'Nữ', 'Khác') | Check | Giới tính hợp lệ |
| GoiTap | TenGoi UNIQUE | Unique | Tên gói không trùng |
| GoiTap | ThoiHan > 0 | Check | Thời hạn dương |
| GoiTap | Gia >= 0 | Check | Giá không âm |
| CaLam | TenCa UNIQUE | Unique | Tên ca không trùng |
| CaLam | GioKetThuc > GioBatDau | Check | Giờ kết thúc > giờ bắt đầu |
| DangKyGoi | TrangThai IN ('Đang hoạt động', 'Hết hạn', 'Tạm dừng', 'Hủy') | Check | Trạng thái hợp lệ |
| DangKyGoi | NgayHetHan >= NgayBatDau | Check | Ngày hết hạn >= ngày bắt đầu |
| HoaDon | SoTien > 0 | Check | Số tiền dương |
| HoaDon | HinhThucTT IN ('Tiền mặt', 'Chuyển khoản', 'Thẻ', 'Khác') | Check | Hình thức thanh toán |
| TaiKhoan | TenDangNhap UNIQUE | Unique | Tên đăng nhập không trùng |
| TaiKhoan | VaiTro IN ('Admin', 'NhanVien') | Check | Vai trò hợp lệ |
| HuanLuyenVien | SDT UNIQUE | Unique | SĐT không trùng |
| HuanLuyenVien | Luong >= 0 | Check | Lương không âm |
| PhongTap | SucChua > 0 | Check | Sức chứa dương |

**Các Foreign Key:**

| FK | Bảng gốc | Cột | Tham chiếu | Cột tham chiếu |
|----|----------|-----|-----------|-----------------|
| FK_DangKyGoi_HocVien | DangKyGoi | MaHV | HocVien | MaHV |
| FK_DangKyGoi_GoiTap | DangKyGoi | MaGoi | GoiTap | MaGoi |
| FK_PhanCong_HLV | PhanCong | MaHLV | HuanLuyenVien | MaHLV |
| FK_PhanCong_DK | PhanCong | MaDK | DangKyGoi | MaDK |
| FK_PhanCong_Ca | PhanCong | MaCa | CaLam | MaCa |
| FK_HoaDon_DangKyGoi | HoaDon | MaDK | DangKyGoi | MaDK |
| FK_TKQ_TaiKhoan | TaiKhoan_Quyen | MaTK | TaiKhoan | MaTK |
| FK_TKQ_Quyen | TaiKhoan_Quyen | MaQuyen | Quyen | MaQuyen |

### 1.3.3 NUnit Framework

| Package | Phiên bản | Mục đích |
|---------|-----------|----------|
| NUnit | 3.14.0 | Framework kiểm thử chính |
| NUnit3TestAdapter | 4.5.0 | Adapter tích hợp VS Test Explorer |
| NUnit.Analyzers | 3.9.0 | Phân tích code test, gợi ý best practice |
| Microsoft.NET.Test.Sdk | 17.8.0 | SDK kiểm thử chuẩn của Microsoft |
| Coverlet.Collector | 6.0.0 | Thu thập code coverage |
| Microsoft.Data.SqlClient | 5.2.0 | Kết nối SQL Server trong test |

**File cấu hình project test (AppGym.Tests.csproj):**
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0-windows</TargetFramework>
    <UseWindowsForms>true</UseWindowsForms>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <IsPackable>false</IsPackable>
    <IsTestProject>true</IsTestProject>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="coverlet.collector" Version="6.0.0" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.8.0" />
    <PackageReference Include="Microsoft.Data.SqlClient" Version="5.2.0" />
    <PackageReference Include="NUnit" Version="3.14.0" />
    <PackageReference Include="NUnit.Analyzers" Version="3.9.0" />
    <PackageReference Include="NUnit3TestAdapter" Version="4.5.0" />
  </ItemGroup>
  <ItemGroup>
    <Using Include="NUnit.Framework" />
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include="..\AppGym.csproj" />
  </ItemGroup>
</Project>
```

**Các Annotation NUnit sử dụng:**

| Annotation | Mô tả | Ví dụ |
|------------|-------|-------|
| [TestFixture] | Đánh dấu class chứa test | `[TestFixture] public class LoginTests` |
| [Test] | Đánh dấu phương thức là test case | `[Test] public void Login_Valid()` |
| [SetUp] | Chạy trước mỗi test | Khởi tạo DAO, kiểm tra DB |
| [TearDown] | Chạy sau mỗi test | Cleanup dữ liệu test |
| [OneTimeSetUp] | Chạy 1 lần trước tất cả test trong class | Thiết lập connection string |
| [Category] | Phân nhóm test theo module | `[Category("Dang nhap")]` |
| [Description] | Mô tả test case | `[Description("TC_LOGIN_01: ...")]` |

**Các Assert API sử dụng:**

| Assert | Mô tả | Ví dụ |
|--------|-------|-------|
| Assert.That(x, Is.True) | Kiểm tra true | `Assert.That(ok, Is.True)` |
| Assert.That(x, Is.False) | Kiểm tra false | `Assert.That(isValid, Is.False)` |
| Assert.That(x, Is.Null) | Kiểm tra null | `Assert.That(user, Is.Null)` |
| Assert.That(x, Is.Not.Null) | Kiểm tra not null | `Assert.That(user, Is.Not.Null)` |
| Assert.That(x, Is.EqualTo(y)) | So sánh bằng | `Assert.That(sdt, Is.EqualTo("0987"))` |
| Assert.That(x, Is.Empty) | Kiểm tra rỗng | `Assert.That(results, Is.Empty)` |
| Assert.That(x, Is.Not.Empty) | Kiểm tra không rỗng | `Assert.That(list, Is.Not.Empty)` |
| Assert.That(x, Is.GreaterThanOrEqualTo(0)) | >= 0 | `Assert.That(count, Is.GreaterThanOrEqualTo(0))` |
| Assert.Throws<T>(() => ...) | Kiểm tra ném exception | `Assert.Throws<SqlException>(() => ...)` |
| Assert.DoesNotThrow(() => ...) | Không ném exception | `Assert.DoesNotThrow(() => ...)` |
| Assert.Ignore(msg) | Skip test | `Assert.Ignore("No DB")` |

### 1.3.4 Visual Studio 2022

- **IDE:** Visual Studio 2022 Community Edition
- **Test Explorer:** Tích hợp sẵn, hỗ trợ NUnit qua NUnit3TestAdapter
- **Chức năng sử dụng:**
  - Solution Explorer: duyệt cấu trúc project
  - Test Explorer: chạy, debug, lọc test case
  - Output Window: xem log chạy test
  - Terminal: chạy `dotnet test` command line
  - SQL Server Object Explorer: xem database

## 1.4 Thời gian dự kiến kiểm thử

| Giai đoạn | Nội dung chi tiết | Thời gian | Kết quả đầu ra |
|-----------|-------------------|-----------|-----------------|
| GĐ 1 | Phân tích yêu cầu ứng dụng, xác định phạm vi kiểm thử, phân tích mã nguồn, database | Tuần 1 | Tài liệu phân tích yêu cầu, danh sách chức năng |
| GĐ 2 | Thiết kế ca kiểm thử: positive test, negative test, boundary value, bảng quyết định | Tuần 2 | Bảng test case chi tiết cho từng module |
| GĐ 3 | Viết test script tự động bằng NUnit: tạo project, viết TestBase, viết test từng module | Tuần 3 | 71 test case tự động trong 11 file .cs |
| GĐ 4 | Thực thi kiểm thử: chạy test, debug lỗi, phát hiện bug, ghi nhận kết quả | Tuần 4 | Kết quả test (TRX), danh sách bug |
| GĐ 5 | Viết báo cáo: tổng hợp kết quả, phân loại bug, đánh giá chất lượng, đề xuất cải tiến | Tuần 5 | Báo cáo giữa kỳ hoàn chỉnh |

---

\newpage

# CHƯƠNG 2: PHÂN TÍCH YÊU CẦU VÀ PHẠM VI KIỂM THỬ

## 2.1 Danh sách các chức năng của ứng dụng

### 2.1.1 Chức năng Đăng nhập (FormLogin)

**Mô tả:** Cho phép người dùng đăng nhập vào hệ thống bằng tên đăng nhập và mật khẩu.

**Luồng xử lý chính:**
1. Người dùng nhập Tên đăng nhập và Mật khẩu
2. Hệ thống kiểm tra rỗng → hiện thông báo nếu thiếu
3. Gọi TaiKhoanDAO.Login(username, password)
4. DAO hash mật khẩu bằng SHA2_512 + Salt, so khớp với DB
5. Nếu tìm thấy user có TrangThai=1 → đăng nhập thành công
6. Nếu không → hiện thông báo lỗi

**Mã nguồn xử lý đăng nhập (FormLogin.cs):**
```csharp
private void BtnLogin_Click(object? sender, EventArgs e)
{
    lblError.Text = "";
    var username = txtUsername.Text.Trim();
    var password = txtPassword.Text;

    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
    {
        lblError.Text = "⚠ Vui lòng nhập đầy đủ thông tin!";
        return;
    }

    try
    {
        var dao = new TaiKhoanDAO();
        var user = dao.Login(username, password);
        if (user != null)
        {
            LoggedInUser = user;
            DialogResult = DialogResult.OK;
            Close();
        }
        else
        {
            lblError.Text = "Tên đăng nhập hoặc mật khẩu không đúng!";
            txtPassword.Clear();
            txtPassword.Focus();
        }
    }
    catch (Exception ex)
    {
        lblError.Text = "Lỗi kết nối: " + ex.Message;
    }
}
```

**Mã nguồn DAO đăng nhập (TaiKhoanDAO.cs):**
```csharp
public TaiKhoan? Login(string username, string password)
{
    using var conn = DatabaseHelper.GetConnection();
    conn.Open();
    using var cmd = new SqlCommand(
        @"SELECT MaTK, TenDangNhap, HoTen, VaiTro, TrangThai, TaoLuc
          FROM TaiKhoan
          WHERE TenDangNhap = @user
            AND MatKhauHash = HASHBYTES('SHA2_512',
                CONVERT(varbinary(200),
                CAST(@pwd AS varchar(100)) + '|' +
                CONVERT(varchar(50), CAST(Salt AS UNIQUEIDENTIFIER))))
            AND TrangThai = 1", conn);
    cmd.Parameters.AddWithValue("@user", username);
    cmd.Parameters.AddWithValue("@pwd", password);
    using var reader = cmd.ExecuteReader();
    if (reader.Read())
    {
        return new TaiKhoan
        {
            MaTK = reader.GetInt32(0),
            TenDangNhap = reader.GetString(1),
            HoTen = reader.IsDBNull(2) ? "" : reader.GetString(2),
            VaiTro = reader.IsDBNull(3) ? "" : reader.GetString(3),
            TrangThai = !reader.IsDBNull(4) && reader.GetBoolean(4),
            TaoLuc = reader.IsDBNull(5) ? null : reader.GetDateTime(5)
        };
    }
    return null;
}
```

> **Hình 4.1: Luồng xử lý đăng nhập (Flowchart)**
>
> *(Chèn flowchart: Bắt đầu → Nhập username/password → Kiểm tra rỗng? → Gọi DAO.Login() → User != null? → Thành công / Thất bại)*

### 2.1.2 Chức năng Dashboard (FormMain)

**Mô tả:** Hiển thị tổng quan hoạt động phòng gym qua các chỉ số thống kê.

**Các chỉ số hiển thị:**
| Chỉ số | DAO | Phương thức | SQL |
|--------|-----|-------------|-----|
| Số học viên đang HĐ | HocVienDAO | Count() | SELECT COUNT(*) FROM HocVien WHERE TrangThai=1 |
| Số HLV đang HĐ | HuanLuyenVienDAO | Count() | SELECT COUNT(*) FROM HuanLuyenVien WHERE TrangThai=1 |
| Số gói tập đang HĐ | GoiTapDAO | Count() | SELECT COUNT(*) FROM GoiTap WHERE TrangThai=1 |
| Số đăng ký đang HĐ | DangKyGoiDAO | CountActive() | SELECT COUNT(*) FROM DangKyGoi WHERE TrangThai=N'Đang hoạt động' |
| Tổng doanh thu | HoaDonDAO | TotalRevenue() | SELECT ISNULL(SUM(SoTien),0) FROM HoaDon |
| Số phòng tập | PhongTapDAO | Count() | SELECT COUNT(*) FROM PhongTap WHERE TrangThai=1 |

### 2.1.3 Chức năng Quản lý Học viên

**Mô tả:** CRUD + Search học viên

**Model HocVien:**
```csharp
public class HocVien
{
    public int MaHV { get; set; }
    public string HoTen { get; set; } = "";
    public string GioiTinh { get; set; } = "";
    public DateTime? NgaySinh { get; set; }
    public string SDT { get; set; } = "";
    public string Email { get; set; } = "";
    public DateTime? NgayDangKy { get; set; }
    public bool TrangThai { get; set; } = true;
}
```

**Các phương thức DAO:**
| Phương thức | Chức năng | SQL chính |
|-------------|-----------|-----------|
| GetAll() | Lấy toàn bộ | SELECT * FROM HocVien ORDER BY MaHV DESC |
| Search(keyword) | Tìm kiếm | WHERE HoTen LIKE @kw OR SDT LIKE @kw OR Email LIKE @kw |
| Insert(hv) | Thêm mới | INSERT INTO HocVien (...) VALUES (...) |
| Update(hv) | Cập nhật | UPDATE HocVien SET ... WHERE MaHV=@MaHV |
| Delete(maHV) | Xóa | DELETE FROM HocVien WHERE MaHV=@MaHV |
| Count() | Đếm HĐ | SELECT COUNT(*) FROM HocVien WHERE TrangThai=1 |

**Validation ở FormHocVienDetail:**
| Trường | Validation | Thông báo lỗi |
|--------|------------|---------------|
| HoTen | Không rỗng | "Vui lòng nhập họ tên!" |
| SDT | 10-11 chữ số (regex) | "Số điện thoại không hợp lệ (10-11 chữ số)!" |
| Email | Chứa ký tự '@' | "Email không hợp lệ!" |

### 2.1.4 Chức năng Quản lý Huấn luyện viên

**Model HuanLuyenVien:**
```csharp
public class HuanLuyenVien
{
    public int MaHLV { get; set; }
    public string HoTen { get; set; } = "";
    public string GioiTinh { get; set; } = "";
    public string SDT { get; set; } = "";
    public string ChuyenMon { get; set; } = "";
    public decimal? Luong { get; set; }
    public bool TrangThai { get; set; } = true;
}
```

**Các phương thức DAO:**
| Phương thức | Chức năng |
|-------------|-----------|
| GetAll() | Lấy toàn bộ HLV |
| Search(keyword) | Tìm theo HoTen, ChuyenMon, SDT |
| Insert(hlv) | Thêm HLV |
| Update(hlv) | Sửa HLV |
| Delete(maHLV) | Xóa HLV |
| Count() | Đếm HLV đang hoạt động |

### 2.1.5 Chức năng Quản lý Gói tập

**Model GoiTap:**
```csharp
public class GoiTap
{
    public int MaGoi { get; set; }
    public string TenGoi { get; set; } = "";
    public int? ThoiHan { get; set; }
    public decimal? Gia { get; set; }
    public string MoTa { get; set; } = "";
    public bool TrangThai { get; set; } = true;
}
```

**Các phương thức DAO:**
| Phương thức | Chức năng | Ghi chú |
|-------------|-----------|---------|
| GetAll() | Lấy toàn bộ | OK |
| Insert(gt) | Thêm gói tập | OK |
| Update(gt) | Sửa gói tập | OK |
| Delete(maGoi) | Xóa gói tập | OK |
| Count() | Đếm đang HĐ | OK |
| ~~Search()~~ | ~~Tìm kiếm~~ | **THIẾU – BUG#08** |

> **Lưu ý:** GoiTapDAO **không có** phương thức Search(), trong khi HocVienDAO và HuanLuyenVienDAO đều có. Đây là BUG#08.

### 2.1.6 Chức năng Quản lý Ca làm

**Model CaLam:**
```csharp
public class CaLam
{
    public int MaCa { get; set; }
    public string TenCa { get; set; } = "";
    public TimeSpan? GioBatDau { get; set; }
    public TimeSpan? GioKetThuc { get; set; }
}
```

### 2.1.7 Chức năng Đăng ký gói

**Model DangKyGoi:**
```csharp
public class DangKyGoi
{
    public int MaDK { get; set; }
    public int MaHV { get; set; }
    public int MaGoi { get; set; }
    public DateTime? NgayBatDau { get; set; }
    public DateTime? NgayHetHan { get; set; }
    public string TrangThai { get; set; } = "Đang hoạt động";
    public string GhiChu { get; set; } = "";
    // Display fields
    public string TenHV { get; set; } = "";
    public string TenGoi { get; set; } = "";
}
```

### 2.1.8 Chức năng Phân công HLV

**Model PhanCong:**
```csharp
public class PhanCong
{
    public int MaPC { get; set; }
    public int MaHLV { get; set; }
    public int MaDK { get; set; }
    public int? MaCa { get; set; }
    public DateTime? NgayBatDau { get; set; }
    public DateTime? NgayKetThuc { get; set; }
    public string GhiChu { get; set; } = "";
    // Display fields
    public string TenHLV { get; set; } = "";
    public string TenHV { get; set; } = "";
    public string TenCa { get; set; } = "";
}
```

### 2.1.9 Chức năng Quản lý Hóa đơn

**Model HoaDon:**
```csharp
public class HoaDon
{
    public int MaHD { get; set; }
    public int MaDK { get; set; }
    public DateTime? NgayThanhToan { get; set; }
    public decimal? SoTien { get; set; }
    public string HinhThucTT { get; set; } = "";
    public string GhiChu { get; set; } = "";
    // Display fields
    public string TenHV { get; set; } = "";
    public string TenGoi { get; set; } = "";
}
```

### 2.1.10 Tổng hợp danh sách chức năng

| STT | Chức năng | Form | DAO | Model | Các phương thức DAO |
|-----|-----------|------|-----|-------|---------------------|
| 1 | Đăng nhập | FormLogin | TaiKhoanDAO | TaiKhoan | Login(), ChangePassword() |
| 2 | Dashboard | FormMain | Nhiều DAO | – | Count(), CountActive(), TotalRevenue() |
| 3 | Quản lý Học viên | FormHocVienDetail | HocVienDAO | HocVien | GetAll(), Search(), Insert(), Update(), Delete(), Count() |
| 4 | Quản lý HLV | FormHLVDetail | HuanLuyenVienDAO | HuanLuyenVien | GetAll(), Search(), Insert(), Update(), Delete(), Count() |
| 5 | Quản lý Gói tập | FormGoiTapDetail | GoiTapDAO | GoiTap | GetAll(), Insert(), Update(), Delete(), Count() |
| 6 | Quản lý Đăng ký gói | FormDangKyGoiDetail | DangKyGoiDAO | DangKyGoi | GetAll(), Insert(), Update(), Delete(), CountActive() |
| 7 | Quản lý Ca làm | FormCaLamDetail | CaLamDAO | CaLam | GetAll(), Insert(), Update(), Delete() |
| 8 | Phân công HLV | FormPhanCongDetail | PhanCongDAO | PhanCong | GetAll(), Insert(), Update(), Delete() |
| 9 | Quản lý Hóa đơn | FormHoaDonDetail | HoaDonDAO | HoaDon | GetAll(), Insert(), Update(), Delete(), TotalRevenue() |
| 10 | Quản lý Phòng tập | FormPhongTapDetail | PhongTapDAO | PhongTap | GetAll(), Insert(), Update(), Delete(), Count() |

## 2.2 Phạm vi kiểm thử

### 2.2.1 Trong phạm vi kiểm thử

| STT | Nội dung kiểm thử | Module liên quan | Mức kiểm thử |
|-----|-------------------|------------------|---------------|
| 1 | Kiểm thử chức năng đăng nhập | TaiKhoanDAO.Login() | Unit Test + Integration |
| 2 | Kiểm thử CRUD Học viên | HocVienDAO | Unit Test + Integration |
| 3 | Kiểm thử CRUD Huấn luyện viên | HuanLuyenVienDAO | Unit Test + Integration |
| 4 | Kiểm thử CRUD Gói tập | GoiTapDAO | Unit Test + Integration |
| 5 | Kiểm thử CRUD Ca làm | CaLamDAO | Unit Test + Integration |
| 6 | Kiểm thử CRUD Đăng ký gói | DangKyGoiDAO | Unit Test + Integration |
| 7 | Kiểm thử CRUD Phân công | PhanCongDAO | Unit Test + Integration |
| 8 | Kiểm thử CRUD Hóa đơn | HoaDonDAO | Unit Test + Integration |
| 9 | Kiểm thử Dashboard thống kê | Count(), CountActive(), TotalRevenue() | Integration |
| 10 | Kiểm thử tìm kiếm (Search) | HocVienDAO.Search(), HuanLuyenVienDAO.Search() | Unit Test |
| 11 | Kiểm thử validation dữ liệu | Các Form, logic validation | Unit Test |
| 12 | Kiểm thử ràng buộc FK | Delete có FK violation | Integration |
| 13 | Kiểm thử bug đã phát hiện | BugReportTests | Regression Test |

### 2.2.2 Ngoài phạm vi

| STT | Nội dung | Lý do |
|-----|----------|-------|
| 1 | Kiểm thử giao diện UI tự động | Cần tool riêng (FlaUI/WinAppDriver), phức tạp |
| 2 | Kiểm thử bảo mật (SQL Injection, XSS) | Ngoài phạm vi môn học |
| 3 | Kiểm thử tải (Load Testing) | Ứng dụng desktop, không cần |
| 4 | Kiểm thử đa nền tảng | Chỉ chạy trên Windows |
| 5 | Kiểm thử PhongTapDAO | Chưa có thời gian |
| 6 | Kiểm thử ChangePassword | Chưa có thời gian |

## 2.3 Đối tượng kiểm thử

### 2.3.1 Đối tượng kiểm thử chính

| Đối tượng | Lớp kiểm thử | Mức độ | Số test case |
|-----------|---------------|--------|--------------|
| TaiKhoanDAO | LoginTests | Unit Test + Integration | 7 |
| HocVienDAO | HocVienTests | Unit Test + Integration | 10 |
| HuanLuyenVienDAO | HuanLuyenVienTests | Unit Test + Integration | 5 |
| GoiTapDAO | GoiTapTests | Unit Test + Integration | 6 |
| CaLamDAO | CaLamTests | Unit Test + Integration | 5 |
| DangKyGoiDAO | DangKyGoiTests | Unit Test + Integration | 8 |
| PhanCongDAO | PhanCongTests | Unit Test + Integration | 6 |
| HoaDonDAO | HoaDonTests | Unit Test + Integration | 8 |
| Dashboard (tổng hợp DAO) | DashboardTests | Integration | 6 |
| Các bug đã phát hiện | BugReportTests | Regression | 10 |
| **Tổng** | **10 test class** | | **71** |

### 2.3.2 Mô tả TestBase (lớp kiểm thử cơ sở)

```csharp
public abstract class TestBase
{
    protected const string ConnStr =
        @"Server=(local)\SQLEXPRESS;Database=GymManagementDB;
          Trusted_Connection=True;TrustServerCertificate=True;";

    [OneTimeSetUp]
    public virtual void GlobalSetup()
    {
        DatabaseHelper.ConnectionString = ConnStr;
    }

    protected static void SkipIfNoDatabase()
    {
        try
        {
            using var conn = new SqlConnection(ConnStr);
            conn.Open();
        }
        catch
        {
            Assert.Ignore("Không thể kết nối database. Bỏ qua test này.");
        }
    }

    protected static void Cleanup(string sql)
    {
        try
        {
            using var conn = new SqlConnection(ConnStr);
            conn.Open();
            using var cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }
        catch { /* ignore cleanup errors */ }
    }
}
```

**Giải thích:**
- `ConnStr`: Connection string đến GymManagementDB trên SQL Server Express local
- `GlobalSetup()`: Thiết lập connection string cho DatabaseHelper trước khi chạy test
- `SkipIfNoDatabase()`: Kiểm tra kết nối DB, nếu không kết nối được → Assert.Ignore (Skip test thay vì Fail)
- `Cleanup(sql)`: Chạy câu SQL xóa dữ liệu test, bọc trong try-catch để không ảnh hưởng test khác

## 2.4 Ma trận liên kết yêu cầu - ca kiểm thử

| Mã YC | Yêu cầu | Test Case | Số TC | Positive | Negative |
|-------|---------|-----------|-------|----------|----------|
| REQ-01 | Đăng nhập hệ thống | TC_LOGIN_01 → TC_LOGIN_07 | 7 | 2 | 5 |
| REQ-02 | CRUD + Search Học viên | TC_HV_01 → TC_HV_10 | 10 | 6 | 4 |
| REQ-03 | CRUD + Search HLV | TC_HLV_01 → TC_HLV_05 | 5 | 2 | 3 |
| REQ-04 | CRUD Gói tập | TC_GT_01 → TC_GT_06 | 6 | 2 | 3+1bug |
| REQ-05 | CRUD Ca làm | TC_CL_01 → TC_CL_05 | 5 | 3 | 2 |
| REQ-06 | CRUD Đăng ký gói | TC_DK_01 → TC_DK_07 + CountActive | 8 | 4 | 3+1bug |
| REQ-07 | CRUD Phân công HLV | TC_PC_01 → TC_PC_06 | 6 | 3 | 2+1bug |
| REQ-08 | CRUD Hóa đơn | TC_HD_01 → TC_HD_08 | 8 | 3 | 4+1bug |
| REQ-09 | Dashboard thống kê | TC_DB_01 → TC_DB_05 + Revenue | 6 | 6 | 0 |
| REQ-10 | Xác nhận các bug | TC_BUG_01 → TC_BUG_10 | 10 | 0 | 10 |
| | **Tổng** | | **71** | **31** | **40** |

---

\newpage

# CHƯƠNG 3: KẾ HOẠCH KIỂM THỬ

## 3.1 Mục tiêu kiểm thử

### 3.1.1 Mục tiêu kiểm thử chức năng

- **Đảm bảo tính đúng đắn (Correctness):** Tất cả chức năng CRUD hoạt động chính xác – Insert thêm đúng dữ liệu, Update cập nhật đúng, Delete xóa đúng, GetAll trả về đầy đủ
- **Kiểm tra tính nhất quán (Consistency):** Dữ liệu sau khi Insert/Update phải nhất quán khi đọc lại bằng GetAll
- **Kiểm tra ràng buộc (Constraints):** Foreign Key, CHECK constraint, UNIQUE hoạt động đúng khi thao tác dữ liệu
- **Kiểm tra validation:** Dữ liệu không hợp lệ được phát hiện và từ chối đúng cách

### 3.1.2 Mục tiêu kiểm thử phi chức năng

- **Tính ổn định (Reliability):** Ứng dụng không crash khi gặp dữ liệu không hợp lệ
- **Khả năng phục hồi (Recoverability):** Xử lý exception đúng cách khi lỗi DB
- **Tính nhất quán dữ liệu:** Dashboard hiển thị đúng số liệu

## 3.2 Chiến lược kiểm thử

### 3.2.1 Kiểm thử hộp trắng (White-box Testing)

Kiểm thử hộp trắng dựa trên phân tích mã nguồn để thiết kế test case bao phủ các nhánh xử lý.

**a) Phân tích luồng xử lý TaiKhoanDAO.Login():**

```
Login(username, password)
│
├── Mở connection
├── Tạo SqlCommand với câu truy vấn:
│   ├── WHERE TenDangNhap = @user
│   ├── AND MatKhauHash = HASHBYTES('SHA2_512', ...)
│   └── AND TrangThai = 1
├── ExecuteReader()
│   ├── reader.Read() == true → return TaiKhoan object  ← Nhánh 1 (Success)
│   └── reader.Read() == false → return null              ← Nhánh 2 (Fail)
└── Implicit: nếu exception → throw
```

**Test case bao phủ:**
| Nhánh | Điều kiện | Test Case |
|-------|-----------|-----------|
| Nhánh 1 | Username đúng, password đúng, TrangThai=1 | TC_LOGIN_01, TC_LOGIN_06 |
| Nhánh 2a | Username đúng, password sai | TC_LOGIN_02 |
| Nhánh 2b | Username rỗng | TC_LOGIN_03 |
| Nhánh 2c | Password rỗng | TC_LOGIN_04 |
| Nhánh 2d | TrangThai=0 (bị khóa) | TC_LOGIN_05 |
| Nhánh 2e | Username không tồn tại | TC_LOGIN_07 |

**b) Phân tích HocVienDAO.Insert():**

```
Insert(hv)
│
├── Mở connection
├── Tạo INSERT INTO HocVien (...) VALUES (...)
├── AddWithValue cho từng trường:
│   ├── @HoTen → hv.HoTen
│   ├── @GioiTinh → hv.GioiTinh
│   ├── @NgaySinh → (object?)hv.NgaySinh ?? DBNull.Value    ← Nullable
│   ├── @SDT → hv.SDT
│   ├── @Email → hv.Email
│   ├── @NgayDangKy → (object?)hv.NgayDangKy ?? DBNull.Value ← Nullable
│   └── @TrangThai → hv.TrangThai
├── ExecuteNonQuery()
│   ├── > 0 → return true   ← Insert thành công
│   └── = 0 → return false  ← Không insert được
└── Exception: SDT/Email trùng (UNIQUE), GioiTinh không hợp lệ (CHECK)
```

**c) Phân tích HocVienDAO.Delete():**

```
Delete(maHV)
│
├── Mở connection  
├── DELETE FROM HocVien WHERE MaHV=@MaHV
├── ExecuteNonQuery()
│   ├── > 0 → return true   ← Xóa thành công (nhánh 1)
│   └── = 0 → return false  ← MaHV không tồn tại (nhánh 2)
└── Exception: FK violation (DangKyGoi tham chiếu MaHV) ← Nhánh 3
```

### 3.2.2 Kiểm thử hộp đen (Black-box Testing)

**a) Phân lớp tương đương (Equivalence Partitioning):**

**Đăng nhập:**
| Lớp tương đương | Đại diện | Kết quả mong đợi |
|-----------------|----------|-------------------|
| Username hợp lệ + Password đúng | admin / 123 | Đăng nhập thành công |
| Username hợp lệ + Password sai | admin / sai123 | null |
| Username rỗng | "" / 123 | null |
| Password rỗng | admin / "" | null |
| Username không tồn tại | xyz999 / 123 | null |
| Tài khoản bị khóa | user_bi_khoa / 123 | null |

**Thêm Học viên:**
| Lớp tương đương | Đại diện | Kết quả mong đợi |
|-----------------|----------|-------------------|
| Dữ liệu hợp lệ | HoTen="Nguyễn A", SDT="0901234567" | Insert thành công |
| HoTen rỗng | HoTen="" | Validation fail |
| SDT không đúng định dạng | SDT="abc" | Validation fail |
| Email không hợp lệ | Email="abc" | Validation fail |

**b) Phân tích giá trị biên (Boundary Value Analysis):**

**SoTien (Hóa đơn):**
| Giá trị | Loại | Kết quả mong đợi |
|---------|------|-------------------|
| -1 | Dưới biên | Validation fail |
| 0 | Biên | Validation fail (CHECK: SoTien > 0) |
| 1 | Trên biên | Insert thành công |
| 100000 | Trung bình | Insert thành công |

**Count() và TotalRevenue():**
| Giá trị | Mong đợi |
|---------|----------|
| Count() | >= 0 (không bao giờ âm) |
| TotalRevenue() | >= 0 (ISNULL xử lý trường hợp không có hóa đơn) |

**c) Bảng quyết định (Decision Table) – Đăng nhập:**

| Điều kiện / Hành động | R1 | R2 | R3 | R4 | R5 | R6 |
|------------------------|----|----|----|----|----|----|
| Username hợp lệ | ✓ | ✓ | ✗ | ✓ | ✗ | ✓ |
| Password đúng | ✓ | ✗ | - | ✓ | - | ✓ |
| TrangThai = 1 | ✓ | - | - | ✗ | - | ✓ |
| Username tồn tại | ✓ | ✓ | - | ✓ | ✗ | ✓ |
| **Kết quả** | **Login OK** | **null** | **null** | **null** | **null** | **Login OK** |
| **Test Case** | TC_LOGIN_01 | TC_LOGIN_02 | TC_LOGIN_03 | TC_LOGIN_05 | TC_LOGIN_07 | TC_LOGIN_06 |

> **Hình 3.2: Ma trận kiểm thử hộp đen – Đăng nhập**
>
> *(Chèn bảng quyết định dạng ma trận)*

## 3.3 Môi trường kiểm thử

| Thành phần | Chi tiết |
|------------|----------|
| **Hệ điều hành** | Windows 10/11 64-bit |
| **CPU** | Intel Core i5 trở lên |
| **RAM** | 8 GB trở lên |
| **IDE** | Visual Studio 2022 Community |
| **Runtime** | .NET 8.0 SDK |
| **Database Server** | SQL Server Express 2019+ |
| **Database Instance** | (local)\SQLEXPRESS |
| **Database Name** | GymManagementDB |
| **Test Framework** | NUnit 3.14.0 |
| **Test Adapter** | NUnit3TestAdapter 4.5.0 |
| **Test SDK** | Microsoft.NET.Test.Sdk 17.8.0 |
| **Coverage Tool** | Coverlet.Collector 6.0.0 |
| **Connection String** | `Server=(local)\SQLEXPRESS;Database=GymManagementDB;Trusted_Connection=True;TrustServerCertificate=True` |
| **Dữ liệu test** | data.sql (dữ liệu mẫu), tự tạo trong test |

## 3.4 Tiêu chí bắt đầu kiểm thử

| STT | Tiêu chí | Cách xác minh |
|-----|----------|---------------|
| 1 | SQL Server Express đang chạy | Kiểm tra Windows Services |
| 2 | Database GymManagementDB đã tạo | Chạy CodeTaoBang.sql |
| 3 | Dữ liệu mẫu đã import | Chạy data.sql |
| 4 | Có ≥ 1 bản ghi HocVien, HLV, GoiTap, CaLam, DangKyGoi | Kiểm tra trong SSMS |
| 5 | Tài khoản admin/123 tồn tại và hoạt động | Login thành công |
| 6 | Project build thành công | `dotnet build` không lỗi |
| 7 | NuGet packages đã restore | `dotnet restore` |

## 3.5 Tiêu chí kết thúc kiểm thử

| STT | Tiêu chí | Giá trị mục tiêu |
|-----|----------|-------------------|
| 1 | Tất cả test case đã thực thi | 71/71 |
| 2 | Tỷ lệ Pass | ≥ 90% |
| 3 | Bug Critical/High đã báo cáo | 100% |
| 4 | Kết quả test lưu file .trx | TestResults.trx |
| 5 | Báo cáo bug đầy đủ | Mỗi bug có ID, mô tả, mức độ, test xác nhận |

## 3.6 Rủi ro và biện pháp xử lý

| STT | Rủi ro | Xác suất | Tác động | Biện pháp xử lý |
|-----|--------|----------|----------|------------------|
| 1 | Không kết nối được SQL Server | Cao | Test không chạy được | Assert.Ignore() → Skip test, không Fail |
| 2 | Dữ liệu test ảnh hưởng DB production | Trung bình | Dữ liệu bẩn | TearDown + Cleanup tự xóa sau mỗi test |
| 3 | FK constraint ngăn xóa dữ liệu test | Trung bình | Test Fail | Kiểm tra FK trước, Assert.Ignore nếu thiếu |
| 4 | Encoding lỗi tiếng Việt | Thấp | Dữ liệu sai | Dùng NVARCHAR và N'' prefix |
| 5 | Test phụ thuộc lẫn nhau | Thấp | Kết quả sai | Mỗi test tự tạo/dọn dữ liệu riêng |
| 6 | UNIQUE constraint khi chạy song song | Thấp | Test Fail | Dùng suffix unique cho dữ liệu test |

---

\newpage

# CHƯƠNG 4: THIẾT KẾ CA KIỂM THỬ

## 4.1 Cơ sở thiết kế ca kiểm thử

Ca kiểm thử được thiết kế dựa trên 4 nguồn chính:

1. **Đặc tả chức năng:** Mỗi module có CRUD → thiết kế test cho Insert, Update, Delete, GetAll, Search
2. **Mã nguồn DAO:** Phân tích từng phương thức trong các lớp DAO, bao phủ các nhánh xử lý (success, fail, exception)
3. **Ràng buộc database:** CHECK constraint, FOREIGN KEY, UNIQUE → thiết kế test case negative
4. **Validation UI:** Kiểm tra các trường bắt buộc, định dạng dữ liệu → test validation logic

## 4.2 Phân loại ca kiểm thử

| Loại | Số lượng | Mô tả | Ví dụ |
|------|----------|-------|-------|
| **Positive Test** | 31 | Kiểm tra chức năng hoạt động đúng với dữ liệu hợp lệ | Insert hợp lệ, Search tìm thấy, Count >= 0 |
| **Negative Test** | 26 | Kiểm tra ứng dụng xử lý đúng dữ liệu không hợp lệ | Insert rỗng, Delete FK violation, Parse fail |
| **Bug Confirmation** | 14 | Xác nhận các bug đã phát hiện | Thiếu Search, FK unhandled, encoding lỗi |
| **Tổng cộng** | **71** | | |

## 4.3 Thiết kế test case cho các chức năng chính

### 4.3.1 Test Case: Đăng nhập (LoginTests.cs – 7 TC)

**TC_LOGIN_01: Đăng nhập thành công với tài khoản hợp lệ**

| Thuộc tính | Giá trị |
|------------|---------|
| **Mã TC** | TC_LOGIN_01 |
| **Tên** | Login_ValidCredentials_ReturnsUser |
| **Mô tả** | Đăng nhập thành công với tài khoản admin/123 (hợp lệ, đang hoạt động) |
| **Tiền điều kiện** | Tài khoản admin/123 tồn tại trong DB, TrangThai=1 |
| **Dữ liệu đầu vào** | username = "admin", password = "123" |
| **Bước thực hiện** | 1. Gọi TaiKhoanDAO.Login("admin", "123") |
| **Kết quả mong đợi** | User != null, TenDangNhap = "admin", TrangThai = true |
| **Loại** | Positive |
| **Mức ưu tiên** | Cao |

**Mã nguồn test:**
```csharp
[Test]
[Description("TC_LOGIN_01: Đăng nhập thành công với tài khoản hợp lệ admin/123")]
public void Login_ValidCredentials_ReturnsUser()
{
    var user = _dao.Login("admin", "123");

    Assert.That(user, Is.Not.Null, "User phải không null khi đăng nhập đúng");
    Assert.That(user!.TenDangNhap, Is.EqualTo("admin"));
    Assert.That(user.TrangThai, Is.True);
}
```

> **Hình 5.2: Kết quả test TC_LOGIN_01 trên Test Explorer (Pass)**
>
> *(Chèn screenshot Test Explorer hiển thị TC_LOGIN_01 Pass)*

---

**TC_LOGIN_02: Đăng nhập sai mật khẩu**

| Thuộc tính | Giá trị |
|------------|---------|
| **Mã TC** | TC_LOGIN_02 |
| **Tên** | Login_WrongPassword_ReturnsNull |
| **Mô tả** | Đăng nhập sai mật khẩu trả về null |
| **Dữ liệu đầu vào** | username = "admin", password = "sai123" |
| **Kết quả mong đợi** | null |
| **Loại** | Negative |

```csharp
[Test]
[Description("TC_LOGIN_02: Đăng nhập sai mật khẩu trả về null")]
public void Login_WrongPassword_ReturnsNull()
{
    var user = _dao.Login("admin", "sai123");
    Assert.That(user, Is.Null, "Phải trả về null khi mật khẩu sai");
}
```

---

**TC_LOGIN_03: Tên đăng nhập rỗng**

| Thuộc tính | Giá trị |
|------------|---------|
| **Mã TC** | TC_LOGIN_03 |
| **Tên** | Login_EmptyUsername_ReturnsNull |
| **Dữ liệu đầu vào** | username = "", password = "123" |
| **Kết quả mong đợi** | null |
| **Loại** | Negative |

---

**TC_LOGIN_04: Mật khẩu rỗng**

| Thuộc tính | Giá trị |
|------------|---------|
| **Mã TC** | TC_LOGIN_04 |
| **Tên** | Login_EmptyPassword_ReturnsNull |
| **Dữ liệu đầu vào** | username = "admin", password = "" |
| **Kết quả mong đợi** | null |
| **Loại** | Negative |

---

**TC_LOGIN_05: Tài khoản bị vô hiệu hóa (TrangThai=0)**

| Thuộc tính | Giá trị |
|------------|---------|
| **Mã TC** | TC_LOGIN_05 |
| **Tên** | Login_DisabledAccount_ReturnsNull |
| **Mô tả** | Tài khoản bị khóa (TrangThai=0) không đăng nhập được |
| **Tiền điều kiện** | Tạo tài khoản user_bi_khoa với TrangThai=0 |
| **Dữ liệu đầu vào** | username = "user_bi_khoa", password = "123" |
| **Kết quả mong đợi** | null |
| **Hậu điều kiện** | Xóa tài khoản user_bi_khoa |
| **Loại** | Negative |

```csharp
[Test]
[Description("TC_LOGIN_05: Tài khoản bị vô hiệu hóa (TrangThai=0) không đăng nhập được")]
public void Login_DisabledAccount_ReturnsNull()
{
    // Setup: tạo tài khoản bị khóa
    Cleanup("DELETE FROM TaiKhoan WHERE TenDangNhap='user_bi_khoa'");
    using var conn = new SqlConnection(ConnStr);
    conn.Open();
    using var cmd = new SqlCommand(@"
        DECLARE @s UNIQUEIDENTIFIER = NEWID();
        INSERT INTO TaiKhoan(TenDangNhap, Salt, MatKhauHash, HoTen, VaiTro, TrangThai)
        VALUES('user_bi_khoa', CAST(@s AS VARBINARY(32)),
               HASHBYTES('SHA2_512', CONVERT(varbinary(200),
               '123' + '|' + CONVERT(varchar(50), @s))),
               N'User Bi Khoa', N'NhanVien', 0)", conn);
    cmd.ExecuteNonQuery();

    var user = _dao.Login("user_bi_khoa", "123");

    Assert.That(user, Is.Null, "Tài khoản bị khóa không được đăng nhập");
}
```

---

**TC_LOGIN_06: Login trả về đúng thông tin VaiTro và HoTen**

| Thuộc tính | Giá trị |
|------------|---------|
| **Mã TC** | TC_LOGIN_06 |
| **Tên** | Login_ValidCredentials_ReturnsCorrectRoleAndName |
| **Kết quả mong đợi** | VaiTro = "Admin" (ignore case), HoTen not empty |
| **Loại** | Positive |

---

**TC_LOGIN_07: Tên đăng nhập không tồn tại**

| Thuộc tính | Giá trị |
|------------|---------|
| **Mã TC** | TC_LOGIN_07 |
| **Tên** | Login_NonExistentUsername_ReturnsNull |
| **Dữ liệu đầu vào** | username = "khongtontai_xyz_999", password = "123" |
| **Kết quả mong đợi** | null |
| **Loại** | Negative |

---

### 4.3.2 Test Case: Quản lý Học viên (HocVienTests.cs – 10 TC)

**TC_HV_01: Thêm học viên hợp lệ thành công**

| Thuộc tính | Giá trị |
|------------|---------|
| **Mã TC** | TC_HV_01 |
| **Tên** | Insert_ValidHocVien_ReturnsTrue |
| **Dữ liệu đầu vào** | HoTen="TEST_Nguyen Van A_01", GioiTinh="Nam", NgaySinh=01/01/1995, SDT="0901234567", Email="test_01@gmail.com", TrangThai=true |
| **Bước thực hiện** | 1. Tạo đối tượng HocVien<br>2. Gọi _dao.Insert(hv)<br>3. Gọi _dao.GetAll() kiểm tra |
| **Kết quả mong đợi** | Insert trả về true, học viên xuất hiện trong GetAll |
| **Hậu điều kiện** | TearDown xóa dữ liệu test |
| **Loại** | Positive |

```csharp
[Test]
[Description("TC_HV_01: Thêm học viên hợp lệ thành công")]
public void Insert_ValidHocVien_ReturnsTrue()
{
    var hv = MakeTestHocVien("_01");
    bool ok = _dao.Insert(hv);

    Assert.That(ok, Is.True);
    var list = _dao.GetAll();
    _insertedId = list.FirstOrDefault(x => x.HoTen == hv.HoTen)?.MaHV ?? -1;
    Assert.That(list.Any(x => x.HoTen == hv.HoTen), Is.True,
        "Học viên phải xuất hiện trong danh sách");
}
```

> **Hình 5.3: Kết quả test HocVienTests (10/10 Pass)**
>
> *(Chèn screenshot Test Explorer hiển thị HocVienTests)*

---

**TC_HV_02: HoTen rỗng – validation phát hiện**

| Thuộc tính | Giá trị |
|------------|---------|
| **Mã TC** | TC_HV_02 |
| **Tên** | Insert_EmptyHoTen_ValidationFails |
| **Mô tả** | HoTen rỗng – validation phát hiện trước khi gọi DAO |
| **Dữ liệu đầu vào** | HoTen = "" |
| **Kết quả mong đợi** | IsNullOrWhiteSpace("") == true → validation fail |
| **Loại** | Negative |

---

**TC_HV_03: Sửa thông tin học viên (SDT) thành công**

| Thuộc tính | Giá trị |
|------------|---------|
| **Mã TC** | TC_HV_03 |
| **Tên** | Update_HocVien_UpdatesSuccessfully |
| **Bước thực hiện** | 1. Insert học viên test<br>2. Đổi SDT = "0987654321"<br>3. Gọi Update<br>4. GetAll kiểm tra |
| **Kết quả mong đợi** | Update trả về true, SDT đã thay đổi thành "0987654321" |
| **Loại** | Positive |

---

**TC_HV_04: Xóa học viên chưa có đăng ký gói**

| Thuộc tính | Giá trị |
|------------|---------|
| **Mã TC** | TC_HV_04 |
| **Tên** | Delete_HocVienWithNoDangKy_ReturnsTrue |
| **Tiền điều kiện** | Học viên test không có đăng ký gói liên quan |
| **Kết quả mong đợi** | Delete trả về true, không còn trong danh sách |
| **Loại** | Positive |

---

**TC_HV_05: Xóa học viên đang có đăng ký gói → FK violation**

| Thuộc tính | Giá trị |
|------------|---------|
| **Mã TC** | TC_HV_05 |
| **Tên** | Delete_HocVienWithDangKy_ThrowsSqlException |
| **Tiền điều kiện** | Có ít nhất 1 đăng ký gói trong DB |
| **Bước thực hiện** | 1. Lấy MaHV từ đăng ký gói<br>2. Gọi _dao.Delete(maHV) |
| **Kết quả mong đợi** | Ném SqlException (FK violation) |
| **Loại** | Negative |

```csharp
[Test]
[Description("TC_HV_05: Xóa học viên đang có đăng ký gói ném ra SqlException")]
public void Delete_HocVienWithDangKy_ThrowsSqlException()
{
    var allDK = new DangKyGoiDAO().GetAll();
    if (!allDK.Any())
        Assert.Ignore("Không có đăng ký gói nào trong DB để test FK.");

    int maHV = allDK.First().MaHV;
    Assert.Throws<SqlException>(() => _dao.Delete(maHV));
}
```

---

**TC_HV_06 → TC_HV_10:** *(Tìm kiếm theo tên, SĐT, không tìm thấy, GetAll, Count – chi tiết tương tự)*

| Mã TC | Tên | Mô tả | Loại |
|-------|-----|-------|------|
| TC_HV_06 | Search_ByName_ReturnsMatchingResults | Tìm kiếm theo tên trả về kết quả đúng | Positive |
| TC_HV_07 | Search_BySdt_ReturnsMatchingResults | Tìm kiếm theo SĐT trả về kết quả đúng | Positive |
| TC_HV_08 | Search_NoMatch_ReturnsEmptyList | Tìm từ khóa không tồn tại trả về rỗng | Negative |
| TC_HV_09 | GetAll_ReturnsListWithoutError | GetAll() không lỗi, list != null | Positive |
| TC_HV_10 | Count_ReturnsNonNegativeNumber | Count() >= 0 | Positive |

### 4.3.3 Test Case: Quản lý HLV (HuanLuyenVienTests.cs – 5 TC)

| Mã TC | Tên test | Mô tả | Đầu vào | Kết quả mong đợi | Loại |
|-------|----------|-------|---------|-------------------|------|
| TC_HLV_01 | Insert_ValidHLV_ReturnsTrue | Thêm HLV hợp lệ | HoTen="TEST_HLV B_01", GioiTinh="Nữ", SDT="0912345678", ChuyenMon="Yoga", Luong=5000000 | Insert true | Positive |
| TC_HLV_02 | Insert_EmptyHoTen_ValidationFails | HoTen rỗng | HoTen="" | Validation fail | Negative |
| TC_HLV_03 | Insert_NonNumericLuong_ParsedAsNull | Lương="abc" → null | Luong=null (parse fail) | Insert thành công, Luong=null | Negative |
| TC_HLV_04 | Delete_HLVWithPhanCong_ThrowsSqlException | Xóa HLV có phân công | MaHLV từ PhanCong | Ném SqlException | Negative |
| TC_HLV_05 | Search_ByChuyenMon_ReturnsMatching | Tìm theo chuyên môn | keyword="Yoga" | Có kết quả ChuyenMon="Yoga" | Positive |

### 4.3.4 Test Case: Quản lý Gói tập (GoiTapTests.cs – 6 TC)

| Mã TC | Tên test | Mô tả | Kết quả mong đợi | Loại |
|-------|----------|-------|-------------------|------|
| TC_GT_01 | Insert_ValidGoiTap_ReturnsTrue | Thêm gói tập hợp lệ | Insert true | Positive |
| TC_GT_02 | Insert_EmptyTenGoi_ValidationFails | TenGoi rỗng | Validation fail | Negative |
| TC_GT_03 | Insert_NonNumericThoiHanAndGia_ParsedAsNull | ThoiHan="abc", Gia="xyz" | Parse null, insert OK | Negative |
| TC_GT_04 | Delete_GoiTapWithDangKy_ThrowsSqlException | Xóa gói có đăng ký | SqlException | Negative |
| TC_GT_05 | Update_GoiTap_UpdatesPriceCorrectly | Sửa giá gói tập | Giá cập nhật đúng | Positive |
| TC_GT_06 | GoiTapDAO_HasNoSearchMethod_BugConfirmed | Xác nhận GoiTapDAO thiếu Search() | Không tìm thấy method Search | Bug |

> **Hình 5.4: Kết quả test GoiTapTests (6/6 Pass) – xác nhận BUG#08**
>
> *(Chèn screenshot Test Explorer)*

### 4.3.5 Test Case: Quản lý Ca làm (CaLamTests.cs – 5 TC)

| Mã TC | Tên test | Mô tả | Kết quả mong đợi | Loại |
|-------|----------|-------|-------------------|------|
| TC_CL_01 | Insert_ValidCaLam_ReturnsTrue | Thêm ca làm hợp lệ | Insert true | Positive |
| TC_CL_02 | Insert_EmptyTenCa_ValidationFails | TenCa rỗng | Validation fail | Negative |
| TC_CL_03 | Update_GioKetThuc_UpdatesCorrectly | Sửa giờ kết thúc | Cập nhật đúng | Positive |
| TC_CL_04 | Delete_CaLamWithPhanCong_ThrowsSqlException | Xóa ca làm có phân công | SqlException | Negative |
| TC_CL_05 | Delete_CaLamWithNoPhanCong_ReturnsTrue | Xóa ca làm không có phân công | Delete true | Positive |

### 4.3.6 Test Case: Đăng ký gói (DangKyGoiTests.cs – 8 TC)

| Mã TC | Tên test | Mô tả | Kết quả mong đợi | Loại |
|-------|----------|-------|-------------------|------|
| TC_DK_01 | Insert_ValidDangKy_ReturnsTrue | Thêm đăng ký hợp lệ | Insert true, MaDK > 0 | Positive |
| TC_DK_02 | Insert_NullMaHV_ValidationFails | MaHV null | Validation fail | Negative |
| TC_DK_03 | Insert_NullMaGoi_ValidationFails | MaGoi null | Validation fail | Negative |
| TC_DK_04 | Delete_DangKyWithNoRelated_ReturnsTrue | Xóa đăng ký không có HD | Delete true | Positive |
| TC_DK_05 | Delete_DangKyWithHoaDon_ThrowsSqlException | Xóa đăng ký có hóa đơn | SqlException | Negative |
| TC_DK_06 | Update_TrangThai_ToHetHan | Đổi trạng thái "Hết hạn" | Update true, TrangThai changed | Positive |
| TC_DK_07 | DangKyGoiDAO_HasNoSearchMethod_BugConfirmed | Xác nhận thiếu Search() | Không có method Search | Bug |
| TC_DK_08 | CountActive_ReturnsNonNegative | CountActive() >= 0 | count >= 0 | Positive |

> **Hình 5.5: Kết quả test DangKyGoiTests (8/8 Pass)**
>
> *(Chèn screenshot)*

### 4.3.7 Test Case: Phân công HLV (PhanCongTests.cs – 6 TC)

| Mã TC | Tên test | Mô tả | Kết quả mong đợi | Loại |
|-------|----------|-------|-------------------|------|
| TC_PC_01 | Insert_ValidPhanCong_ReturnsTrue | Thêm phân công hợp lệ | Insert true, MaPC > 0 | Positive |
| TC_PC_02 | Insert_NullMaHLV_ValidationFails | Không chọn HLV | Validation fail | Negative |
| TC_PC_03 | Insert_NullMaDK_ValidationFails | Không chọn đăng ký gói | Validation fail | Negative |
| TC_PC_04 | Insert_NullMaCa_ReturnsTrue | Ca làm optional, MaCa=null | Insert true, MaCa=null | Positive |
| TC_PC_05 | Insert_EmptyGhiChu_DoesNotThrow | GhiChu="" không crash (BUG#05) | Không exception, insert OK | Bug |
| TC_PC_06 | Delete_PhanCong_ReturnsTrue | Xóa phân công thành công | Delete true | Positive |

### 4.3.8 Test Case: Quản lý Hóa đơn (HoaDonTests.cs – 8 TC)

| Mã TC | Tên test | Mô tả | Kết quả mong đợi | Loại |
|-------|----------|-------|-------------------|------|
| TC_HD_01 | Insert_ValidHoaDon_ReturnsTrue | Thêm hóa đơn hợp lệ | Insert true, MaHD > 0 | Positive |
| TC_HD_02 | Insert_NullMaDK_ValidationFails | MaDK null | Validation fail | Negative |
| TC_HD_03 | Insert_SoTienZero_ValidationFails | SoTien = 0 | 0 > 0 = false | Negative |
| TC_HD_04 | Insert_NegativeSoTien_ValidationFails | SoTien = -100 | -100 > 0 = false | Negative |
| TC_HD_05 | Insert_NonNumericSoTien_ParseFails | SoTien = "abc" | TryParse = false | Negative |
| TC_HD_06 | Insert_EmptyHinhThucTT_ThrowsSqlException | HinhThucTT="" (BUG#04) | Insert thành công (bug) | Bug |
| TC_HD_07 | Delete_HoaDon_ReturnsTrue | Xóa hóa đơn | Delete true | Positive |
| TC_HD_08 | TotalRevenue_ReturnsNonNegativeDecimal | TotalRevenue() >= 0 | revenue >= 0 | Positive |

> **Hình 5.6: Kết quả test HoaDonTests (8/8 Pass)**
>
> *(Chèn screenshot)*

### 4.3.9 Test Case: Dashboard (DashboardTests.cs – 6 TC)

| Mã TC | Tên test | Mô tả | Kết quả mong đợi | Loại |
|-------|----------|-------|-------------------|------|
| TC_DB_01 | HocVien_Count_ReturnsNonNegative | Count() HV >= 0 | count >= 0 | Positive |
| TC_DB_02 | HuanLuyenVien_Count_ReturnsNonNegative | Count() HLV >= 0 | count >= 0 | Positive |
| TC_DB_03 | GoiTap_Count_ReturnsNonNegative | Count() GoiTap >= 0 | count >= 0 | Positive |
| TC_DB_04 | DangKy_CountActive_ReturnsNonNegative | CountActive() >= 0 | count >= 0 | Positive |
| TC_DB_05 | DangKy_GetAll_Take10_MaxTenRecords | Take(10) ≤ 10 | list.Count <= 10 | Positive |
| TC_DB_06 | HoaDon_TotalRevenue_MatchesSumInDB | TotalRevenue == SUM(SoTien) | Khớp DB | Positive |

```csharp
[Test]
[Description("TC_DB_06: HoaDonDAO.TotalRevenue() khớp với SUM(SoTien) trong DB")]
public void HoaDon_TotalRevenue_MatchesSumInDB()
{
    decimal fromDAO = new HoaDonDAO().TotalRevenue();

    decimal fromDB;
    using var conn = new SqlConnection(ConnStr);
    conn.Open();
    using var cmd = new SqlCommand(
        "SELECT ISNULL(SUM(SoTien),0) FROM HoaDon", conn);
    fromDB = (decimal)cmd.ExecuteScalar();

    Assert.That(fromDAO, Is.EqualTo(fromDB),
        "TotalRevenue() phải khớp với SUM trong DB");
}
```

> **Hình 5.7: Kết quả test DashboardTests (6/6 Pass)**
>
> *(Chèn screenshot)*

## 4.4 Thiết kế dữ liệu kiểm thử

### 4.4.1 Dữ liệu test cho Học viên

```csharp
private HocVien MakeTestHocVien(string suffix = "") => new()
{
    HoTen      = $"TEST_Nguyen Van A{suffix}",
    GioiTinh   = "Nam",
    NgaySinh   = new DateTime(1995, 1, 1),
    SDT        = "0901234567",
    Email      = $"test{suffix}@gmail.com",
    NgayDangKy = DateTime.Today,
    TrangThai  = true
};
```

| Trường | Giá trị test | Lý do |
|--------|-------------|-------|
| HoTen | "TEST_Nguyen Van A_01" | Prefix "TEST_" để dễ nhận diện, suffix unique |
| GioiTinh | "Nam" | Giá trị hợp lệ theo CHECK constraint |
| NgaySinh | 01/01/1995 | Ngày hợp lệ |
| SDT | "0901234567" | 10 chữ số, đúng regex |
| Email | "test_01@gmail.com" | Có '@', đúng định dạng |
| NgayDangKy | DateTime.Today | Ngày hiện tại |
| TrangThai | true | Đang hoạt động |

### 4.4.2 Dữ liệu test cho HLV

```csharp
private HuanLuyenVien MakeTestHLV(string suffix = "") => new()
{
    HoTen = $"TEST_HLV B{suffix}", GioiTinh = "Nữ",
    SDT = "0912345678", ChuyenMon = "Yoga",
    Luong = 5_000_000, TrangThai = true
};
```

### 4.4.3 Dữ liệu test cho Gói tập

```csharp
private GoiTap MakeTestGoiTap(string suffix = "") => new()
{
    TenGoi    = $"TEST_Goi 1 Thang{suffix}",
    ThoiHan   = 30,
    Gia       = 500_000,
    MoTa      = "Co ban",
    TrangThai = true
};
```

### 4.4.4 Dữ liệu test cho Ca làm

```csharp
private CaLam MakeTestCaLam(string suffix = "") => new()
{
    TenCa       = $"TEST_Ca sang{suffix}",
    GioBatDau   = new TimeSpan(6, 0, 0),
    GioKetThuc  = new TimeSpan(12, 0, 0)
};
```

### 4.4.5 Dữ liệu test cho Đăng nhập

| Username | Password | Mô tả | TC sử dụng |
|----------|----------|-------|------------|
| admin | 123 | Tài khoản hợp lệ | TC_LOGIN_01, TC_LOGIN_06 |
| admin | sai123 | Sai mật khẩu | TC_LOGIN_02 |
| "" | 123 | Username rỗng | TC_LOGIN_03 |
| admin | "" | Password rỗng | TC_LOGIN_04 |
| user_bi_khoa | 123 | TrangThai=0 | TC_LOGIN_05 |
| khongtontai_xyz_999 | 123 | Không tồn tại | TC_LOGIN_07 |

### 4.4.6 Dữ liệu test cho Hóa đơn

```csharp
private HoaDon MakeTestHoaDon() => new()
{
    MaDK = _seedMaDK, NgayThanhToan = DateTime.Today,
    SoTien = 500_000, HinhThucTT = "Tiền mặt", GhiChu = "TEST"
};
```

**Ma trận giá trị biên SoTien:**
| Tên | Giá trị | Mong đợi | TC |
|-----|---------|----------|----|
| Âm | -100 | Validation fail | TC_HD_04 |
| Zero | 0 | Validation fail | TC_HD_03 |
| Dương nhỏ | 1 | OK | (implicit) |
| Dương lớn | 500_000 | OK | TC_HD_01 |
| Chuỗi | "abc" | Parse fail | TC_HD_05 |

## 4.5 Thiết kế kịch bản kiểm thử tự động

### 4.5.1 Mô hình test

```
[TestFixture] → Class chứa test case cho 1 module
│
├── TestBase (base class)
│   ├── ConnStr: connection string
│   ├── GlobalSetup(): thiết lập DB connection
│   ├── SkipIfNoDatabase(): skip nếu không có DB
│   └── Cleanup(sql): xóa dữ liệu test
│
├── Fields
│   ├── _dao: DAO instance
│   ├── _insertedId: ID bản ghi test đã tạo
│   └── _seedXxx: ID dữ liệu seed từ DB
│
├── [SetUp] → Kiểm tra DB, khởi tạo DAO
│
├── Helper Methods
│   ├── MakeTestXxx(): tạo đối tượng test
│   └── InsertAndGetId(): insert và lấy ID
│
├── [Test] Methods (AAA Pattern)
│   ├── Arrange: Tạo dữ liệu test
│   ├── Act: Gọi phương thức DAO
│   └── Assert: Kiểm tra kết quả
│
└── [TearDown] → Cleanup: xóa dữ liệu test
```

### 4.5.2 Mô hình AAA (Arrange – Act – Assert)

Mỗi test case tuân theo mô hình AAA chuẩn:

```csharp
[Test]
[Description("TC_HV_03: Sửa thông tin học viên thành công")]
public void Update_HocVien_UpdatesSuccessfully()
{
    // ========== ARRANGE ==========
    var hv = MakeTestHocVien("_03");      // Tạo dữ liệu test
    _dao.Insert(hv);                       // Insert vào DB
    var list = _dao.GetAll();
    var target = list.First(x => x.HoTen == hv.HoTen);
    _insertedId = target.MaHV;             // Lưu ID để cleanup

    // ========== ACT ==========
    target.SDT = "0987654321";             // Thay đổi SDT
    bool ok = _dao.Update(target);         // Gọi Update

    // ========== ASSERT ==========
    Assert.That(ok, Is.True);              // Update thành công
    var updated = _dao.GetAll().First(x => x.MaHV == _insertedId);
    Assert.That(updated.SDT, Is.EqualTo("0987654321")); // SDT đã đổi
}
```

### 4.5.3 Cơ chế tự dọn dẹp dữ liệu

```csharp
// Mỗi test class lưu _insertedId để TearDown xóa
private int _insertedId = -1;

[TearDown]
public void TearDown()
{
    if (_insertedId > 0)
    {
        Cleanup($"DELETE FROM HocVien WHERE MaHV={_insertedId}");
        _insertedId = -1;
    }
}
```

**Ưu điểm:**
- Mỗi test tự tạo và tự xóa dữ liệu → không ảnh hưởng DB
- Sử dụng suffix unique (`_01`, `_02`...) → tránh conflict
- TearDown chạy sau mỗi test → cleanup kể cả khi test fail

## 4.6 Bảng tổng hợp test case

| STT | File Test | Category | Số TC | Positive | Negative | Bug | Tổng |
|-----|-----------|----------|-------|----------|----------|-----|------|
| 1 | LoginTests.cs | Đăng nhập | 7 | 2 | 5 | 0 | 7 |
| 2 | HocVienTests.cs | Học viên | 10 | 6 | 4 | 0 | 10 |
| 3 | HuanLuyenVienTests.cs | HLV | 5 | 2 | 3 | 0 | 5 |
| 4 | GoiTapTests.cs | Gói tập | 6 | 2 | 3 | 1 | 6 |
| 5 | CaLamTests.cs | Ca làm | 5 | 3 | 2 | 0 | 5 |
| 6 | DangKyGoiTests.cs | Đăng ký gói | 8 | 4 | 3 | 1 | 8 |
| 7 | PhanCongTests.cs | Phân công | 6 | 3 | 2 | 1 | 6 |
| 8 | HoaDonTests.cs | Hóa đơn | 8 | 3 | 4 | 1 | 8 |
| 9 | DashboardTests.cs | Dashboard | 6 | 6 | 0 | 0 | 6 |
| 10 | BugReportTests.cs | Bug Report | 10 | 0 | 0 | 10 | 10 |
| | **Tổng cộng** | | **71** | **31** | **26** | **14** | **71** |

---

\newpage

# CHƯƠNG 5: THỰC THI KIỂM THỬ

## 5.1 Kiểm thử chức năng

### 5.1.1 Kiểm thử chức năng Đăng nhập

**Module:** TaiKhoanDAO.Login()  
**Test class:** LoginTests.cs  
**Số test case:** 7  

| Mã TC | Tên test | Kết quả | Thời gian | Ghi chú |
|-------|----------|---------|-----------|---------|
| TC_LOGIN_01 | Login_ValidCredentials_ReturnsUser | ✅ Pass | < 1s | Đăng nhập đúng |
| TC_LOGIN_02 | Login_WrongPassword_ReturnsNull | ✅ Pass | < 1s | Sai password → null |
| TC_LOGIN_03 | Login_EmptyUsername_ReturnsNull | ✅ Pass | < 1s | Username rỗng → null |
| TC_LOGIN_04 | Login_EmptyPassword_ReturnsNull | ✅ Pass | < 1s | Password rỗng → null |
| TC_LOGIN_05 | Login_DisabledAccount_ReturnsNull | ✅ Pass | < 1s | TrangThai=0 → null |
| TC_LOGIN_06 | Login_ValidCredentials_ReturnsCorrectRoleAndName | ✅ Pass | < 1s | VaiTro, HoTen đúng |
| TC_LOGIN_07 | Login_NonExistentUsername_ReturnsNull | ✅ Pass | < 1s | Username không tồn tại |

**Phân tích kết quả:**
- 7/7 test case Pass (100%)
- Hàm Login() xử lý đúng tất cả các trường hợp: đúng credentials, sai password, rỗng, tài khoản bị khóa, không tồn tại
- Mã hóa SHA2_512 + Salt hoạt động chính xác
- Điều kiện TrangThai=1 đảm bảo tài khoản bị khóa không đăng nhập được

> **Hình 5.2: Chi tiết kết quả test LoginTests (7/7 Pass)**
>
> *(Chèn screenshot Visual Studio Test Explorer → LoginTests → All 7 tests passing)*

### 5.1.2 Kiểm thử CRUD Học viên

**Module:** HocVienDAO  
**Test class:** HocVienTests.cs  
**Số test case:** 10  

| Mã TC | Tên test | Kết quả | Ghi chú |
|-------|----------|---------|---------|
| TC_HV_01 | Insert_ValidHocVien_ReturnsTrue | ✅ Pass | Insert OK, xuất hiện trong GetAll |
| TC_HV_02 | Insert_EmptyHoTen_ValidationFails | ✅ Pass | Validation phát hiện HoTen rỗng |
| TC_HV_03 | Update_HocVien_UpdatesSuccessfully | ✅ Pass | SDT cập nhật đúng |
| TC_HV_04 | Delete_HocVienWithNoDangKy_ReturnsTrue | ✅ Pass | Xóa thành công |
| TC_HV_05 | Delete_HocVienWithDangKy_ThrowsSqlException | ✅ Pass | FK violation đúng |
| TC_HV_06 | Search_ByName_ReturnsMatchingResults | ✅ Pass | Tìm theo tên chính xác |
| TC_HV_07 | Search_BySdt_ReturnsMatchingResults | ✅ Pass | Tìm theo SĐT chính xác |
| TC_HV_08 | Search_NoMatch_ReturnsEmptyList | ✅ Pass | Không tìm thấy → rỗng |
| TC_HV_09 | GetAll_ReturnsListWithoutError | ✅ Pass | GetAll không lỗi |
| TC_HV_10 | Count_ReturnsNonNegativeNumber | ✅ Pass | Count >= 0 |

**Phân tích kết quả:**
- 10/10 test case Pass (100%)
- CRUD hoạt động chính xác: Insert thêm đúng, Update thay đổi đúng trường, Delete xóa đúng
- Search tìm kiếm theo HoTen và SDT sử dụng LIKE '%keyword%' chính xác
- FK constraint bảo vệ toàn vẹn dữ liệu: không xóa được học viên đang có đăng ký gói

> **Hình 5.3: Chi tiết kết quả test HocVienTests (10/10 Pass)**
>
> *(Chèn screenshot)*

### 5.1.3 Kiểm thử CRUD Huấn luyện viên

**Module:** HuanLuyenVienDAO  
**Test class:** HuanLuyenVienTests.cs  
**Số test case:** 5  

| Mã TC | Tên test | Kết quả | Ghi chú |
|-------|----------|---------|---------|
| TC_HLV_01 | Insert_ValidHLV_ReturnsTrue | ✅ Pass | |
| TC_HLV_02 | Insert_EmptyHoTen_ValidationFails | ✅ Pass | |
| TC_HLV_03 | Insert_NonNumericLuong_ParsedAsNull | ✅ Pass | Luong=null OK |
| TC_HLV_04 | Delete_HLVWithPhanCong_ThrowsSqlException | ✅ Pass | FK violation |
| TC_HLV_05 | Search_ByChuyenMon_ReturnsMatching | ✅ Pass | Search "Yoga" đúng |

### 5.1.4 Kiểm thử CRUD Gói tập

**Module:** GoiTapDAO  
**Test class:** GoiTapTests.cs  
**Số test case:** 6  

| Mã TC | Tên test | Kết quả | Ghi chú |
|-------|----------|---------|---------|
| TC_GT_01 | Insert_ValidGoiTap_ReturnsTrue | ✅ Pass | |
| TC_GT_02 | Insert_EmptyTenGoi_ValidationFails | ✅ Pass | |
| TC_GT_03 | Insert_NonNumericThoiHanAndGia_ParsedAsNull | ✅ Pass | |
| TC_GT_04 | Delete_GoiTapWithDangKy_ThrowsSqlException | ✅ Pass | FK violation |
| TC_GT_05 | Update_GoiTap_UpdatesPriceCorrectly | ✅ Pass | |
| TC_GT_06 | GoiTapDAO_HasNoSearchMethod_BugConfirmed | ✅ Pass | **BUG#08 xác nhận** |

**Phát hiện BUG#08:** GoiTapDAO thiếu phương thức Search() → chức năng tìm kiếm gói tập trên UI không hoạt động.

### 5.1.5 Kiểm thử CRUD Ca làm

**Kết quả:** 5/5 Pass (100%)

### 5.1.6 Kiểm thử CRUD Đăng ký gói

**Kết quả:** 8/8 Pass (100%)  
**Phát hiện BUG#09:** DangKyGoiDAO thiếu Search()

### 5.1.7 Kiểm thử CRUD Phân công

**Kết quả:** 6/6 Pass (100%)  
**Phát hiện BUG#05:** GhiChu="" có thể gây lỗi

### 5.1.8 Kiểm thử CRUD Hóa đơn

**Kết quả:** 8/8 Pass (100%)  
**Phát hiện BUG#04:** HinhThucTT="" insert được (thiếu CHECK)

> **Hình 5.6: Kết quả test HoaDonTests (8/8 Pass)**
>
> *(Chèn screenshot)*

## 5.2 Kiểm thử tích hợp

### 5.2.1 Kiểm thử tích hợp DAO - Database

Tất cả test case đều là kiểm thử tích hợp thực tế với SQL Server:

```
Test Code ──→ DAO Class ──→ SqlConnection ──→ SQL Server
                                                  │
                                                  ▼
                                          GymManagementDB
```

**Các kịch bản tích hợp đã kiểm thử:**

| STT | Kịch bản | Test Case | Kết quả |
|-----|----------|-----------|---------|
| 1 | Insert → GetAll → Verify | TC_HV_01, TC_GT_01, TC_CL_01, TC_DK_01, TC_HD_01 | Pass |
| 2 | Insert → Update → GetAll → Verify | TC_HV_03, TC_GT_05, TC_CL_03, TC_DK_06 | Pass |
| 3 | Insert → Delete → GetAll → Verify deleted | TC_HV_04, TC_CL_05, TC_DK_04, TC_HD_07, TC_PC_06 | Pass |
| 4 | Delete with FK → SqlException | TC_HV_05, TC_GT_04, TC_DK_05, TC_CL_04, TC_HLV_04 | Pass |
| 5 | Count() matches actual data | TC_DB_01-05 | Pass |
| 6 | TotalRevenue() == SUM(SoTien) | TC_DB_06 | Pass |

### 5.2.2 Kiểm thử ràng buộc Foreign Key

| Bảng cha | Bảng con | FK | Test Case | Kết quả |
|----------|----------|-----|-----------|---------|
| HocVien → DangKyGoi | Delete HocVien | FK_DangKyGoi_HocVien | TC_HV_05, TC_BUG_02 | SqlException |
| GoiTap → DangKyGoi | Delete GoiTap | FK_DangKyGoi_GoiTap | TC_GT_04 | SqlException |
| DangKyGoi → HoaDon | Delete DangKyGoi | FK_HoaDon_DangKyGoi | TC_DK_05, TC_BUG_03 | SqlException |
| DangKyGoi → PhanCong | Delete CaLam | FK_PhanCong_Ca | TC_CL_04 | SqlException |
| HuanLuyenVien → PhanCong | Delete HLV | FK_PhanCong_HLV | TC_HLV_04 | SqlException |

### 5.2.3 Kiểm thử Dashboard

Dashboard tổng hợp dữ liệu từ 5 DAO khác nhau:

```
Dashboard
├── HocVienDAO.Count()          → TC_DB_01
├── HuanLuyenVienDAO.Count()    → TC_DB_02
├── GoiTapDAO.Count()           → TC_DB_03
├── DangKyGoiDAO.CountActive()  → TC_DB_04
├── HoaDonDAO.TotalRevenue()    → TC_DB_06
└── DangKyGoiDAO.GetAll().Take(10) → TC_DB_05
```

**Kết quả:** 6/6 Pass (100%)

> **Hình 5.7: Kết quả test DashboardTests**
>
> *(Chèn screenshot)*

## 5.3 Kiểm thử hệ thống

### 5.3.1 Luồng nghiệp vụ end-to-end

Luồng chính của phòng gym được kiểm tra gián tiếp qua các test tích hợp:

```
1. Đăng nhập (TC_LOGIN_01)
   ↓
2. Tạo Học viên (TC_HV_01)
   ↓
3. Tạo Gói tập (TC_GT_01)
   ↓
4. Đăng ký gói cho Học viên (TC_DK_01)
   ↓
5. Phân công HLV (TC_PC_01)
   ↓
6. Tạo Hóa đơn thanh toán (TC_HD_01)
   ↓
7. Dashboard hiển thị thống kê (TC_DB_01-06)
```

### 5.3.2 Phân quyền Admin/NhanVien

- Admin: truy cập tất cả menu (Tổng quan, Học viên, HLV, Gói tập, Đăng ký, Phân công, Hóa đơn, Ca làm, Phòng tập)
- NhanVien: menu bị ẩn HLV, Phân công, Ca làm (adminOnly=true)
- Nút Xóa bị ẩn cho NhanVien: `if (!IsAdmin) btnDelete.Visible = false;`

## 5.4 Kiểm thử hiệu năng

| Phương thức | SQL | Phân tích | Đánh giá |
|-------------|-----|-----------|----------|
| Count() | SELECT COUNT(*) WHERE TrangThai=1 | Truy vấn đơn giản, có index trên PK | Tốt |
| TotalRevenue() | SELECT ISNULL(SUM(SoTien),0) | Aggregate đơn giản | Tốt |
| GetAll() | SELECT * ORDER BY ... DESC | Trả về toàn bộ, không phân trang | **Cần cải thiện** |
| Search() | WHERE ... LIKE '%kw%' | Full table scan khi LIKE '%...' | Chấp nhận được cho DB nhỏ |

**Đề xuất cải thiện:**
- GetAll(): Thêm phân trang (OFFSET/FETCH)
- Search(): Dùng Full-Text Search nếu dữ liệu lớn

## 5.5 Kiểm thử hồi quy

### 5.5.1 BugReportTests.cs – 10 test case hồi quy

| Mã TC | Bug ID | Mô tả | Kết quả hiện tại | Khi bug được fix |
|-------|--------|-------|-------------------|------------------|
| TC_BUG_01 | BUG#01 | Admin HoTen chứa "?" | Pass (bug tồn tại) | Sẽ Fail → cập nhật test |
| TC_BUG_02 | BUG#02 | Delete HocVien FK crash | Pass (SqlException) | Sẽ Pass (try-catch added) |
| TC_BUG_03 | BUG#03 | Delete DangKyGoi FK crash | Pass (SqlException) | Sẽ Pass (try-catch added) |
| TC_BUG_04 | BUG#04 | Empty HinhThucTT insert OK | Pass (no CHECK) | Sẽ Fail (CHECK added) |
| TC_BUG_05 | BUG#05 | Empty GhiChu no crash | Pass | Pass |
| TC_BUG_06 | BUG#06 | NgayHetHan không tự tính | Pass (bug tồn tại) | Sẽ Fail → cập nhật |
| TC_BUG_07 | BUG#07 | cboDangKy chỉ hiện TenHV | Pass (bug tồn tại) | Sẽ Fail → cập nhật |
| TC_BUG_08 | BUG#08 | GoiTapDAO thiếu Search() | Pass (no Search) | Sẽ Fail (Search added) |
| TC_BUG_09 | BUG#09 | DangKyGoiDAO thiếu Search() | Pass (no Search) | Sẽ Fail (Search added) |
| TC_BUG_10 | BUG#10 | Dashboard cards overflow | Pass | Pass |

> **Hình 5.8: Chi tiết kết quả BugReportTests (10/10 Pass)**
>
> *(Chèn screenshot)*

---

\newpage

# PHẦN 6: KIỂM THỬ TỰ ĐỘNG (AUTOMATION TEST)

## 6.1 Công cụ sử dụng và lý do lựa chọn

### 6.1.1 So sánh framework kiểm thử .NET

| Tiêu chí | NUnit 3 | xUnit | MSTest |
|----------|---------|-------|--------|
| Phổ biến | ★★★★★ | ★★★★★ | ★★★★ |
| Assert API | Constraint Model (Assert.That) | Assert.Equal, Assert.True | Assert.AreEqual |
| Category/Group | [Category] ✓ | [Trait] | [TestCategory] |
| Description | [Description] ✓ | Không có | [Description] |
| Skip test | Assert.Ignore() ✓ | Skip.If() | Assert.Inconclusive |
| Setup/Teardown | [SetUp]/[TearDown] | Constructor/Dispose | [TestInitialize] |
| Parallel | ✓ | ✓ mặc định | ✓ |

**Lý do chọn NUnit:**
1. **Constraint Model** (`Assert.That(x, Is.EqualTo(y))`) trực quan, dễ đọc hơn `Assert.Equal(y, x)`
2. `[Category]` cho phép phân nhóm test theo module, dễ chạy riêng: `--filter "Category=Dang nhap"`
3. `[Description]` giúp mô tả test case ngay trong code
4. `Assert.Ignore()` cho phép skip test khi không đủ điều kiện → không gây Fail sai
5. NUnit là framework lâu đời nhất, tài liệu phong phú, cộng đồng lớn

### 6.1.2 Lý do chọn kiểm thử tầng DAO (không mock)

| Phương pháp | Mô tả | Ưu điểm | Nhược điểm |
|-------------|-------|---------|------------|
| **Mock/Stub** | Giả lập DB bằng Moq/NSubstitute | Nhanh, không cần DB | Không test thực tế SQL |
| **In-Memory DB** | Dùng SQLite in-memory | Nhanh, tự chứa | Khác SQL Server syntax |
| **Integration Test (đã chọn)** | Test trực tiếp với SQL Server | Test thực tế 100%, bao gồm constraint/FK | Cần SQL Server chạy |

**Lý do chọn Integration Test:**
- Kiểm tra thực tế câu SQL, ràng buộc CHECK, FK constraint
- Phát hiện bug liên quan đến DB (BUG#04: CHECK constraint không hoạt động)
- Sát với môi trường production nhất
- Cơ chế Assert.Ignore() giải quyết nhược điểm cần DB

## 6.2 Các test script đã viết

### 6.2.1 Danh sách file test

| STT | File | Số dòng code | Số test | Mô tả |
|-----|------|-------------|---------|-------|
| 1 | TestBase.cs | 48 | 0 | Base class: ConnStr, SkipIfNoDatabase(), Cleanup() |
| 2 | LoginTests.cs | 95 | 7 | Đăng nhập: đúng/sai/rỗng/khóa |
| 3 | HocVienTests.cs | 154 | 10 | CRUD + Search học viên |
| 4 | HuanLuyenVienTests.cs | 60 | 5 | CRUD + Search HLV |
| 5 | GoiTapTests.cs | 110 | 6 | CRUD gói tập + bug Search |
| 6 | CaLamTests.cs | 100 | 5 | CRUD ca làm |
| 7 | DangKyGoiTests.cs | 95 | 8 | CRUD đăng ký gói + CountActive |
| 8 | PhanCongTests.cs | 125 | 6 | CRUD phân công |
| 9 | HoaDonTests.cs | 90 | 8 | CRUD hóa đơn + TotalRevenue |
| 10 | DashboardTests.cs | 70 | 6 | Count, TotalRevenue, Take(10) |
| 11 | BugReportTests.cs | 100 | 10 | Xác nhận 10 bug |
| | **Tổng** | **~1047** | **71** | |

### 6.2.2 Chi tiết mã nguồn test đại diện

**LoginTests.cs (đầy đủ):**

```csharp
using AppGym.DataAccess;

namespace AppGym.Tests;

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

    [Test]
    [Description("TC_LOGIN_01: Đăng nhập thành công với tài khoản hợp lệ admin/123")]
    public void Login_ValidCredentials_ReturnsUser()
    {
        var user = _dao.Login("admin", "123");
        Assert.That(user, Is.Not.Null, "User phải không null khi đăng nhập đúng");
        Assert.That(user!.TenDangNhap, Is.EqualTo("admin"));
        Assert.That(user.TrangThai, Is.True);
    }

    [Test]
    [Description("TC_LOGIN_02: Đăng nhập sai mật khẩu trả về null")]
    public void Login_WrongPassword_ReturnsNull()
    {
        var user = _dao.Login("admin", "sai123");
        Assert.That(user, Is.Null, "Phải trả về null khi mật khẩu sai");
    }

    [Test]
    [Description("TC_LOGIN_03: Tên đăng nhập rỗng trả về null")]
    public void Login_EmptyUsername_ReturnsNull()
    {
        var user = _dao.Login("", "123");
        Assert.That(user, Is.Null);
    }

    [Test]
    [Description("TC_LOGIN_04: Mật khẩu rỗng trả về null")]
    public void Login_EmptyPassword_ReturnsNull()
    {
        var user = _dao.Login("admin", "");
        Assert.That(user, Is.Null);
    }

    [Test]
    [Description("TC_LOGIN_05: Tài khoản bị vô hiệu hóa")]
    public void Login_DisabledAccount_ReturnsNull()
    {
        Cleanup("DELETE FROM TaiKhoan WHERE TenDangNhap='user_bi_khoa'");
        using var conn = new Microsoft.Data.SqlClient.SqlConnection(ConnStr);
        conn.Open();
        using var cmd = new Microsoft.Data.SqlClient.SqlCommand(@"
            DECLARE @s UNIQUEIDENTIFIER = NEWID();
            INSERT INTO TaiKhoan(TenDangNhap, Salt, MatKhauHash, HoTen, VaiTro, TrangThai)
            VALUES('user_bi_khoa', CAST(@s AS VARBINARY(32)),
                   HASHBYTES('SHA2_512', CONVERT(varbinary(200),
                   '123' + '|' + CONVERT(varchar(50), @s))),
                   N'User Bi Khoa', N'NhanVien', 0)", conn);
        cmd.ExecuteNonQuery();

        var user = _dao.Login("user_bi_khoa", "123");
        Assert.That(user, Is.Null, "Tài khoản bị khóa không được đăng nhập");
    }

    [TearDown]
    public void Cleanup_DisabledAccount()
    {
        Cleanup("DELETE FROM TaiKhoan WHERE TenDangNhap='user_bi_khoa'");
    }

    [Test]
    [Description("TC_LOGIN_06: Login trả về đúng VaiTro và HoTen")]
    public void Login_ValidCredentials_ReturnsCorrectRoleAndName()
    {
        var user = _dao.Login("admin", "123");
        Assert.That(user, Is.Not.Null);
        Assert.That(user!.VaiTro, Is.EqualTo("Admin").IgnoreCase);
        Assert.That(user.HoTen, Is.Not.Empty);
    }

    [Test]
    [Description("TC_LOGIN_07: Tên đăng nhập không tồn tại")]
    public void Login_NonExistentUsername_ReturnsNull()
    {
        var user = _dao.Login("khongtontai_xyz_999", "123");
        Assert.That(user, Is.Null);
    }
}
```

## 6.3 Hướng dẫn cài đặt và chạy script

### 6.3.1 Cài đặt môi trường

**Yêu cầu hệ thống:**
- Windows 10/11 64-bit
- .NET 8.0 SDK (download: https://dotnet.microsoft.com/download)
- SQL Server Express 2019+ (download: https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- Visual Studio 2022 Community (khuyến nghị)

**Bước 1: Cài đặt .NET 8.0 SDK**

> *(Chèn screenshot trang download .NET 8.0)*

```powershell
# Kiểm tra .NET đã cài
dotnet --version
# Kết quả: 8.0.xxx
```

**Bước 2: Cài đặt SQL Server Express**

> *(Chèn screenshot SQL Server Express installation)*

```powershell
# Kiểm tra SQL Server đang chạy
Get-Service -Name 'MSSQL$SQLEXPRESS'
# Status: Running
```

**Bước 3: Tạo database**

```powershell
# Chạy script tạo bảng
sqlcmd -S "(local)\SQLEXPRESS" -i CodeTaoBang.sql

# Import dữ liệu mẫu
sqlcmd -S "(local)\SQLEXPRESS" -i data.sql
```

> *(Chèn screenshot SSMS hiển thị database GymManagementDB với 11 bảng)*

**Bước 4: Restore packages và build**

```powershell
cd AppGym
dotnet restore AppGym.Tests\AppGym.Tests.csproj
dotnet build AppGym.Tests\AppGym.Tests.csproj
```

> *(Chèn screenshot terminal build thành công)*

**Bước 5: Chạy test**

```powershell
# Chạy tất cả 71 test
dotnet test AppGym.Tests\AppGym.Tests.csproj

# Chạy theo category
dotnet test --filter "Category=Dang nhap"
dotnet test --filter "Category=Hoc vien"
dotnet test --filter "Category=Bug Report"

# Chạy test cụ thể
dotnet test --filter "TestName=Login_ValidCredentials_ReturnsUser"

# Output chi tiết
dotnet test --logger "console;verbosity=detailed"

# Xuất kết quả TRX
dotnet test --logger "trx;LogFileName=TestResults.trx"
```

> **Hình 6.2: Terminal chạy dotnet test toàn bộ**
>
> *(Chèn screenshot terminal hiển thị "Passed! - Failed: 0, Passed: 71, Skipped: 0, Total: 71")*

### 6.3.2 Cấu trúc code chính

```
AppGym.Tests/
├── TestBase.cs                 # Base class chung
│   ├── ConnStr                 # Connection string
│   ├── GlobalSetup()           # Thiết lập DatabaseHelper
│   ├── SkipIfNoDatabase()      # Skip nếu không có DB
│   └── Cleanup(sql)            # SQL cleanup
│
├── LoginTests.cs               # 7 test đăng nhập
│   ├── [SetUp] SkipIfNoDatabase + new TaiKhoanDAO()
│   ├── Login_ValidCredentials_ReturnsUser
│   ├── Login_WrongPassword_ReturnsNull
│   ├── Login_EmptyUsername_ReturnsNull
│   ├── Login_EmptyPassword_ReturnsNull
│   ├── Login_DisabledAccount_ReturnsNull
│   ├── Login_ValidCredentials_ReturnsCorrectRoleAndName
│   └── Login_NonExistentUsername_ReturnsNull
│
├── HocVienTests.cs             # 10 test học viên
│   ├── MakeTestHocVien(suffix)
│   ├── InsertAndGetId(hv)
│   ├── Insert_ValidHocVien_ReturnsTrue
│   ├── Insert_EmptyHoTen_ValidationFails
│   ├── Update_HocVien_UpdatesSuccessfully
│   ├── Delete_HocVienWithNoDangKy_ReturnsTrue
│   ├── Delete_HocVienWithDangKy_ThrowsSqlException
│   ├── Search_ByName_ReturnsMatchingResults
│   ├── Search_BySdt_ReturnsMatchingResults
│   ├── Search_NoMatch_ReturnsEmptyList
│   ├── GetAll_ReturnsListWithoutError
│   └── Count_ReturnsNonNegativeNumber
│
├── (... các file test khác tương tự ...)
│
└── BugReportTests.cs           # 10 test xác nhận bug
    ├── Bug01_AdminHoTen_ShouldNotContainQuestionMark
    ├── Bug02_Delete_HocVienWithDangKy_ThrowsUnhandled
    ├── ...
    └── Bug10_DashboardCards_OverflowMinimumWidth
```

> **Hình 6.1: Cấu trúc project AppGym.Tests trong Solution Explorer**
>
> *(Chèn screenshot Solution Explorer hiển thị tất cả file test)*

## 6.4 Kết quả chạy tự động

### 6.4.1 Tổng quan kết quả

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

> **Hình 6.3: Kết quả Test Explorer hiển thị 71/71 Pass**
>
> *(Chèn screenshot Test Explorer với tất cả 71 test hiện xanh (Pass))*

### 6.4.2 Kết quả chi tiết theo category

> **Hình 6.4: Kết quả chạy test theo Category "Dang nhap"**
>
> *(Chèn screenshot: dotnet test --filter "Category=Dang nhap" → 7 Passed)*

> **Hình 6.5: Kết quả chạy test theo Category "Bug Report"**
>
> *(Chèn screenshot: dotnet test --filter "Category=Bug Report" → 10 Passed)*

### 6.4.3 Thời gian chạy test

| Category | Thời gian trung bình |
|----------|---------------------|
| Đăng nhập | ~2s |
| Học viên | ~3s |
| HLV | ~1.5s |
| Gói tập | ~2s |
| Ca làm | ~1.5s |
| Đăng ký gói | ~2.5s |
| Phân công | ~2s |
| Hóa đơn | ~2s |
| Dashboard | ~1s |
| Bug Report | ~2s |
| **Tổng (toàn bộ 71 test)** | **~15-20s** |

## 6.5 Nhận xét về automation

### 6.5.1 Ưu điểm

1. **Bao phủ rộng:** 71 test case trên 10 module chính, bao gồm CRUD, Search, Count, validation, FK constraint
2. **Tự dọn dẹp (Self-cleaning):** TearDown + Cleanup() xóa dữ liệu test → không làm bẩn DB
3. **Cơ chế Skip thông minh:** Assert.Ignore() khi không có DB → test không Fail sai
4. **Phân nhóm rõ ràng:** [Category] cho phép chạy riêng từng module
5. **Mô hình AAA chuẩn:** Arrange – Act – Assert dễ đọc, dễ bảo trì
6. **Test hồi quy:** BugReportTests xác nhận bug tồn tại, theo dõi khi fix
7. **Chạy nhanh:** Toàn bộ 71 test hoàn thành trong ~15-20s

### 6.5.2 Hạn chế

1. **Chưa có kiểm thử UI:** Chỉ test ở tầng DAO, chưa test giao diện WinForms
2. **Phụ thuộc SQL Server:** Cần DB thực, không chạy offline được
3. **Chưa mock/stub:** Không tách biệt được DAO và DB
4. **Chưa test PhongTapDAO:** 1 module chưa có test
5. **Validation test ở tầng logic:** Một số test negative chỉ test logic validation, không gọi DAO
6. **Chưa đo code coverage:** Chưa chạy Coverlet để biết % code được test

### 6.5.3 Đề xuất cải thiện

| STT | Đề xuất | Ưu tiên |
|-----|---------|---------|
| 1 | Thêm mock DB bằng SQLite in-memory | Cao |
| 2 | Bổ sung test PhongTapDAO | Trung bình |
| 3 | Thêm UI automation (FlaUI) | Thấp |
| 4 | Đo code coverage bằng Coverlet | Trung bình |
| 5 | Tích hợp CI/CD (GitHub Actions) | Thấp |

---

\newpage

# PHẦN 7: BÁO CÁO LỖI (BUG REPORT)

## 7.1 Danh sách toàn bộ bug tìm được

### BUG#01: Admin HoTen chứa ký tự "?"

| Thuộc tính | Giá trị |
|------------|---------|
| **Bug ID** | BUG#01 |
| **Tiêu đề** | Admin HoTen chứa ký tự "?" do lỗi encoding |
| **Mức độ** | Medium |
| **Module** | Đăng nhập / TaiKhoan |
| **Mô tả** | Tên hiển thị của tài khoản admin chứa ký tự "?" thay vì ký tự tiếng Việt. Nguyên nhân: script setup_db.sql sử dụng chuỗi không có prefix N'' cho ký tự Unicode tiếng Việt khi INSERT tài khoản admin |
| **Bước tái tạo** | 1. Đăng nhập bằng admin/123<br>2. Kiểm tra user.HoTen<br>3. HoTen chứa "?" |
| **Kết quả thực tế** | HoTen chứa ký tự "?" |
| **Kết quả mong đợi** | HoTen = "Quản trị viên" (tiếng Việt đúng) |
| **Nguyên nhân gốc** | Thiếu prefix N'' trong INSERT: dùng `'Quản trị viên'` thay vì `N'Quản trị viên'` |
| **Cách sửa** | Sửa setup_db.sql: thêm N'' prefix cho tất cả chuỗi tiếng Việt |
| **Test xác nhận** | TC_BUG_01 |

> **Hình 7.3: Minh họa BUG#01 – HoTen chứa "?"**
>
> *(Chèn screenshot màn hình hiển thị tên admin với ký tự "?" thay vì tiếng Việt)*

---

### BUG#02: Xóa học viên có đăng ký gói → crash

| Thuộc tính | Giá trị |
|------------|---------|
| **Bug ID** | BUG#02 |
| **Tiêu đề** | Xóa HocVien đang có DangKyGoi → SqlException không được xử lý → crash |
| **Mức độ** | **High** |
| **Module** | Quản lý Học viên |
| **Mô tả** | Khi xóa một học viên đang có bản ghi trong bảng DangKyGoi (FK_DangKyGoi_HocVien), DAO ném SqlException do FK violation. Exception này không được bắt (try-catch) ở tầng UI → ứng dụng hiển thị lỗi hoặc crash |
| **Bước tái tạo** | 1. Mở quản lý Học viên<br>2. Chọn học viên đang có đăng ký gói<br>3. Nhấn nút Xóa → Xác nhận<br>4. Ứng dụng ném SqlException |
| **Kết quả thực tế** | SqlException: "The DELETE statement conflicted with the REFERENCE constraint FK_DangKyGoi_HocVien" |
| **Kết quả mong đợi** | Hiển thị thông báo thân thiện: "Không thể xóa học viên đang có đăng ký gói. Vui lòng hủy đăng ký trước." |
| **Nguyên nhân gốc** | HocVienDAO.Delete() không bọc try-catch, FormMain.ShowHocVien() có try-catch nhưng thông báo lỗi kỹ thuật |
| **Cách sửa** | Thêm try-catch trong Delete hoặc kiểm tra FK trước khi xóa |
| **Test xác nhận** | TC_BUG_02, TC_HV_05 |

> **Hình 7.1: Giao diện lỗi khi xóa học viên có đăng ký (BUG#02)**
>
> *(Chèn screenshot MessageBox hiển thị SqlException)*

---

### BUG#03: Xóa đăng ký gói có hóa đơn → crash

| Thuộc tính | Giá trị |
|------------|---------|
| **Bug ID** | BUG#03 |
| **Tiêu đề** | Xóa DangKyGoi đang có HoaDon → SqlException không xử lý |
| **Mức độ** | **High** |
| **Module** | Quản lý Đăng ký gói |
| **Mô tả** | Tương tự BUG#02 nhưng xảy ra khi xóa đăng ký gói có hóa đơn liên quan (FK_HoaDon_DangKyGoi) |
| **Nguyên nhân gốc** | DangKyGoiDAO.Delete() không bọc try-catch cho FK violation |
| **Cách sửa** | Thêm try-catch hoặc kiểm tra hóa đơn liên quan trước khi xóa |
| **Test xác nhận** | TC_BUG_03, TC_DK_05 |

---

### BUG#04: HinhThucTT rỗng insert thành công

| Thuộc tính | Giá trị |
|------------|---------|
| **Bug ID** | BUG#04 |
| **Tiêu đề** | HoaDon.HinhThucTT="" insert được vào DB mà không bị CHECK constraint bắt |
| **Mức độ** | Medium |
| **Module** | Quản lý Hóa đơn |
| **Mô tả** | Bảng HoaDon có CHECK constraint: HinhThucTT IN ('Tiền mặt', 'Chuyển khoản', 'Thẻ', 'Khác'). Tuy nhiên, giá trị rỗng "" vẫn insert được mà không bị từ chối. Nguyên nhân: CHECK constraint cho phép NULL, và chuỗi rỗng "" khác NULL |
| **Bước tái tạo** | 1. Tạo hóa đơn với HinhThucTT=""<br>2. Gọi HoaDonDAO.Insert()<br>3. Insert thành công (không ném exception) |
| **Kết quả thực tế** | Insert thành công, hóa đơn có HinhThucTT="" trong DB |
| **Kết quả mong đợi** | Ném SqlException hoặc validation từ chối |
| **Cách sửa** | 1. Thêm validation ở UI: kiểm tra HinhThucTT không rỗng trước khi insert<br>2. Cập nhật CHECK: `HinhThucTT IS NOT NULL AND HinhThucTT IN (...)` |
| **Test xác nhận** | TC_BUG_04, TC_HD_06 |

> **Hình 7.3: HinhThucTT rỗng insert được vào DB (BUG#04)**
>
> *(Chèn screenshot SSMS hiển thị bản ghi HoaDon có HinhThucTT = '')*

---

### BUG#05: PhanCong GhiChu rỗng có thể gây lỗi

| Thuộc tính | Giá trị |
|------------|---------|
| **Bug ID** | BUG#05 |
| **Tiêu đề** | Insert PhanCong với GhiChu="" có thể gây ArgumentNullException |
| **Mức độ** | Low |
| **Module** | Phân công HLV |
| **Mô tả** | Khi GhiChu là chuỗi rỗng, trong một số trường hợp có thể gây lỗi nếu không xử lý đúng null/empty |
| **Cách sửa** | Kiểm tra và thay GhiChu rỗng bằng null hoặc chuỗi mặc định |
| **Test xác nhận** | TC_BUG_05, TC_PC_05 |

---

### BUG#06: NgayHetHan không tự động tính

| Thuộc tính | Giá trị |
|------------|---------|
| **Bug ID** | BUG#06 |
| **Tiêu đề** | NgayHetHan không tự động tính từ NgayBatDau + ThoiHan |
| **Mức độ** | Medium |
| **Module** | Đăng ký gói |
| **Mô tả** | Khi tạo DangKyGoi, NgayHetHan phải được tự động tính: NgayHetHan = NgayBatDau + ThoiHan (ngày) dựa trên thời hạn của GoiTap đã chọn. Hiện tại, người dùng phải nhập tay NgayHetHan → dễ nhập sai |
| **Kết quả thực tế** | Người dùng phải tự nhập NgayHetHan |
| **Kết quả mong đợi** | NgayHetHan = NgayBatDau.AddDays(GoiTap.ThoiHan) tự động |
| **Cách sửa** | Trong FormDangKyGoiDetail, khi chọn GoiTap → tự tính NgayHetHan |
| **Test xác nhận** | TC_BUG_06 |

---

### BUG#07: cboDangKy chỉ hiển thị TenHV

| Thuộc tính | Giá trị |
|------------|---------|
| **Bug ID** | BUG#07 |
| **Tiêu đề** | ComboBox đăng ký chỉ hiển thị TenHV, không phân biệt được nhiều đăng ký |
| **Mức độ** | Medium |
| **Module** | Phân công HLV / Hóa đơn |
| **Mô tả** | Trong FormPhanCongDetail và FormHoaDonDetail, cboDangKy (ComboBox chọn đăng ký gói) chỉ hiển thị TenHV (tên học viên). Khi 1 học viên có nhiều đăng ký gói → không phân biệt được đăng ký nào |
| **Bước tái tạo** | 1. Tạo 2 đăng ký gói cho cùng 1 học viên<br>2. Mở FormPhanCongDetail<br>3. cboDangKy hiển thị 2 dòng giống hệt nhau (cùng TenHV) |
| **Kết quả mong đợi** | Hiển thị: "MaDK - TenHV - TenGoi" (ví dụ: "DK001 - Nguyễn Văn A - Gói 1 tháng") |
| **Cách sửa** | Đổi DisplayMember hoặc override ToString() của DangKyGoi |
| **Test xác nhận** | TC_BUG_07 |

> **Hình 7.4: cboDangKy chỉ hiển thị TenHV (BUG#07)**
>
> *(Chèn screenshot ComboBox hiển thị 2 dòng tên giống nhau)*

---

### BUG#08: GoiTapDAO thiếu phương thức Search()

| Thuộc tính | Giá trị |
|------------|---------|
| **Bug ID** | BUG#08 |
| **Tiêu đề** | GoiTapDAO không có phương thức Search() → tìm kiếm gói tập không hoạt động |
| **Mức độ** | **High** |
| **Module** | Quản lý Gói tập |
| **Mô tả** | GoiTapDAO chỉ có GetAll(), Insert(), Update(), Delete(), Count() mà **không có Search()**. Trong khi HocVienDAO và HuanLuyenVienDAO đều có Search() → tính nhất quán bị vi phạm. Chức năng tìm kiếm gói tập trên UI không hoạt động |
| **Kết quả thực tế** | typeof(GoiTapDAO).GetMethods() không chứa method "Search" |
| **Kết quả mong đợi** | GoiTapDAO.Search(keyword) tìm theo TenGoi, MoTa |
| **Cách sửa** | Thêm phương thức Search() vào GoiTapDAO tương tự HocVienDAO.Search() |
| **Test xác nhận** | TC_BUG_08, TC_GT_06 |

> **Hình 7.2: GoiTapDAO thiếu Search – so sánh với HocVienDAO (BUG#08)**
>
> *(Chèn screenshot so sánh code GoiTapDAO (không có Search) với HocVienDAO (có Search))*

---

### BUG#09: DangKyGoiDAO thiếu phương thức Search()

| Thuộc tính | Giá trị |
|------------|---------|
| **Bug ID** | BUG#09 |
| **Tiêu đề** | DangKyGoiDAO không có Search() |
| **Mức độ** | **High** |
| **Module** | Đăng ký gói |
| **Mô tả** | Tương tự BUG#08, DangKyGoiDAO thiếu Search() → tìm kiếm đăng ký gói không hoạt động |
| **Cách sửa** | Thêm Search() tìm theo TenHV, TenGoi |
| **Test xác nhận** | TC_BUG_09, TC_DK_07 |

---

### BUG#10: Dashboard cards bị tràn MinimumWidth

| Thuộc tính | Giá trị |
|------------|---------|
| **Bug ID** | BUG#10 |
| **Tiêu đề** | Dashboard cards bị tràn khi form có MinimumWidth nhỏ |
| **Mức độ** | Low |
| **Module** | Dashboard |
| **Mô tả** | 6 card thống kê có tổng width: 6 × (cardWidth + gap). Khi form bị thu nhỏ, cards có thể tràn ra ngoài vùng hiển thị |
| **Cách sửa** | Dùng FlowLayoutPanel hoặc responsive layout |
| **Test xác nhận** | TC_BUG_10 |

## 7.2 Phân loại bug theo mức độ nghiêm trọng

### 7.2.1 Tiêu chí phân loại

| Mức độ | Mô tả | Tiêu chí |
|--------|-------|----------|
| **Critical** | Hệ thống sập, mất dữ liệu | Không khởi động được, mất dữ liệu không phục hồi |
| **High** | Chức năng chính không hoạt động | Crash khi thao tác, chức năng thiếu hoàn toàn |
| **Medium** | Chức năng phụ lỗi, UX kém | Logic sai nhưng không crash, hiển thị sai |
| **Low** | Lỗi nhỏ, cosmetic | UI không đẹp, lỗi hiếm gặp |

### 7.2.2 Phân loại kết quả

| Mức độ | Số lượng | Bug ID | Mô tả tóm tắt |
|--------|----------|--------|---------------|
| **Critical** | 0 | – | Không có bug nghiêm trọng gây mất dữ liệu |
| **High** | 4 | BUG#02, BUG#03, BUG#08, BUG#09 | Crash FK unhandled (2), thiếu Search (2) |
| **Medium** | 4 | BUG#01, BUG#04, BUG#06, BUG#07 | Encoding, thiếu CHECK, thiếu auto-calc, UX |
| **Low** | 2 | BUG#05, BUG#10 | GhiChu rỗng, UI tràn |
| **Tổng** | **10** | | |

## 7.3 Biểu đồ thống kê bug

### 7.3.1 Phân bố bug theo mức độ

```
┌──────────────────────────────────────────────────┐
│           Phân bố Bug theo Mức độ                │
├──────────────────────────────────────────────────┤
│                                                  │
│  Critical │                            0 (0%)    │
│           │                                      │
│  High     │████████████████████████    4 (40%)   │
│           │                                      │
│  Medium   │████████████████████████    4 (40%)   │
│           │                                      │
│  Low      │████████████                2 (20%)   │
│           │                                      │
│           0    1    2    3    4    5              │
└──────────────────────────────────────────────────┘
```

> **Hình 7.5: Biểu đồ phân bố bug theo mức độ**
>
> *(Chèn biểu đồ tròn hoặc biểu đồ cột: Critical=0, High=4, Medium=4, Low=2)*

### 7.3.2 Phân bố bug theo module

```
┌──────────────────────────────────────────────────┐
│           Phân bố Bug theo Module                │
├──────────────────────────────────────────────────┤
│                                                  │
│  Đăng nhập   │██                     1 bug       │
│  Học viên     │██                     1 bug       │
│  Gói tập      │██                     1 bug       │
│  Đăng ký gói  │██████████             3 bug       │
│  Phân công    │██                     1 bug       │
│  Hóa đơn      │████                   2 bug       │
│  Dashboard    │██                     1 bug       │
│                                                  │
│              0    1    2    3                     │
└──────────────────────────────────────────────────┘
```

> **Hình 7.6: Biểu đồ phân bố bug theo module**
>
> *(Chèn biểu đồ cột ngang)*

### 7.3.3 Phân loại bug theo nguyên nhân

| Nguyên nhân gốc | Số lượng | Bug ID | Tỷ lệ |
|-----------------|----------|--------|--------|
| Thiếu method (code chưa implement) | 2 | BUG#08, BUG#09 | 20% |
| Thiếu xử lý FK exception | 2 | BUG#02, BUG#03 | 20% |
| Thiếu validation / CHECK constraint | 2 | BUG#04, BUG#05 | 20% |
| Logic nghiệp vụ thiếu | 1 | BUG#06 | 10% |
| UI/UX không đúng | 2 | BUG#07, BUG#10 | 20% |
| Lỗi encoding | 1 | BUG#01 | 10% |
| **Tổng** | **10** | | **100%** |

### 7.3.4 Ma trận Bug vs Module

| Module | Critical | High | Medium | Low | Tổng |
|--------|----------|------|--------|-----|------|
| Đăng nhập | 0 | 0 | 1 | 0 | 1 |
| Học viên | 0 | 1 | 0 | 0 | 1 |
| Gói tập | 0 | 1 | 0 | 0 | 1 |
| Đăng ký gói | 0 | 2 | 1 | 0 | 3 |
| Phân công | 0 | 0 | 0 | 1 | 1 |
| Hóa đơn | 0 | 0 | 2 | 0 | 2 |
| Dashboard | 0 | 0 | 0 | 1 | 1 |
| **Tổng** | **0** | **4** | **4** | **2** | **10** |

---

\newpage

# PHẦN 8: KẾT QUẢ VÀ ĐÁNH GIÁ

## 8.1 Tổng kết toàn bộ quá trình kiểm thử

### 8.1.1 Tổng quan kết quả

| Hạng mục | Giá trị | Ghi chú |
|----------|---------|---------|
| Tổng số test case | 71 | 11 file test (bao gồm TestBase.cs) |
| Test case Pass | 71 | 100% |
| Test case Fail | 0 | |
| Test case Skip | 0 | (Khi có DB kết nối) |
| Tổng số bug phát hiện | 10 | 4 High, 4 Medium, 2 Low |
| Bug Critical | 0 | Không có lỗi nghiêm trọng |
| Bug High | 4 | BUG#02, 03, 08, 09 |
| Bug Medium | 4 | BUG#01, 04, 06, 07 |
| Bug Low | 2 | BUG#05, 10 |
| Số module kiểm thử | 10 | /11 module tổng (thiếu PhongTap) |
| Tỷ lệ bao phủ module | 91% | 10/11 |
| Tổng dòng code test | ~1047 | 11 file .cs |

> **Hình 8.1: Biểu đồ tổng quan kết quả kiểm thử**
>
> *(Chèn biểu đồ: 71 Pass, 0 Fail, 0 Skip → 100% Pass Rate)*

### 8.1.2 Kết quả theo module

| Module | Số TC | Pass | Fail | Tỷ lệ | Bug phát hiện |
|--------|-------|------|------|--------|---------------|
| Đăng nhập | 7 | 7 | 0 | 100% | 0 (BUG#01 qua BugReport) |
| Học viên | 10 | 10 | 0 | 100% | 0 (BUG#02 qua BugReport) |
| HLV | 5 | 5 | 0 | 100% | 0 |
| Gói tập | 6 | 6 | 0 | 100% | 1 (BUG#08) |
| Ca làm | 5 | 5 | 0 | 100% | 0 |
| Đăng ký gói | 8 | 8 | 0 | 100% | 1 (BUG#09) |
| Phân công | 6 | 6 | 0 | 100% | 1 (BUG#05) |
| Hóa đơn | 8 | 8 | 0 | 100% | 1 (BUG#04) |
| Dashboard | 6 | 6 | 0 | 100% | 0 (BUG#10 qua BugReport) |
| Bug Report | 10 | 10 | 0 | 100% | – |

> **Hình 8.2: Biểu đồ so sánh test Pass/Fail theo module**
>
> *(Chèn biểu đồ cột xếp chồng: mỗi module là 1 cột, xanh = Pass, đỏ = Fail)*

## 8.2 Đánh giá chất lượng sau kiểm thử

### 8.2.1 Chất lượng tốt

| Tiêu chí | Đánh giá | Chi tiết |
|----------|----------|----------|
| **CRUD hoạt động chính xác** | ✅ Tốt | Tất cả Insert/Update/Delete/GetAll hoạt động đúng trên 8 module |
| **Đăng nhập bảo mật** | ✅ Tốt | SHA2_512 + Salt, kiểm tra TrangThai, xử lý mọi case |
| **FK constraint** | ✅ Tốt | Bảo vệ toàn vẹn dữ liệu, ngăn xóa dữ liệu có reference |
| **Dashboard chính xác** | ✅ Tốt | Count, TotalRevenue khớp 100% với DB |
| **Search (nơi có)** | ✅ Tốt | HocVien, HLV search bằng LIKE hoạt động chính xác |
| **Xử lý nullable** | ✅ Tốt | DBNull.Value cho các trường nullable |

### 8.2.2 Chất lượng cần cải thiện

| Tiêu chí | Đánh giá | Chi tiết | Bug liên quan |
|----------|----------|----------|---------------|
| **Xử lý FK exception** | ❌ Kém | Crash khi xóa dữ liệu có FK | BUG#02, BUG#03 |
| **Tính nhất quán DAO** | ❌ Kém | 2 DAO thiếu Search() | BUG#08, BUG#09 |
| **Validation đầu vào** | ⚠ Trung bình | Thiếu CHECK cho HinhThucTT rỗng | BUG#04 |
| **Auto-calculate** | ⚠ Trung bình | NgayHetHan không tự tính | BUG#06 |
| **UI/UX** | ⚠ Trung bình | cboDangKy không phân biệt, card tràn | BUG#07, BUG#10 |
| **Encoding** | ⚠ Trung bình | Ký tự tiếng Việt lỗi | BUG#01 |

### 8.2.3 Chỉ số chất lượng

| Chỉ số | Giá trị | Đánh giá |
|--------|---------|----------|
| Bug Density | 10 bug / 10 module = 1.0 bug/module | Trung bình |
| Test Effectiveness | 10 bug / 71 TC = 14% | Phát hiện bug hiệu quả |
| High Bug Ratio | 4/10 = 40% | Cao – cần ưu tiên fix |
| Module Coverage | 10/11 = 91% | Tốt |
| Test Pass Rate | 71/71 = 100% | Tốt |

## 8.3 Những gì làm được, chưa làm được

### 8.3.1 Đã làm được

| STT | Nội dung | Chi tiết |
|-----|----------|----------|
| 1 | ✅ Xây dựng 71 test case tự động | 10 test class bao phủ 10 module |
| 2 | ✅ Phát hiện 10 bug | 4 High, 4 Medium, 2 Low |
| 3 | ✅ Kiểm thử CRUD đầy đủ | Tất cả DAO (trừ PhongTap) |
| 4 | ✅ Kiểm thử FK constraint | 5 cặp FK đã kiểm tra |
| 5 | ✅ Kiểm thử validation | Positive + Negative cho mỗi module |
| 6 | ✅ Kiểm thử tích hợp DAO-DB | Test trực tiếp với SQL Server |
| 7 | ✅ Test tự cleanup dữ liệu | TearDown + Cleanup() |
| 8 | ✅ Test hồi quy (BugReport) | 10 test xác nhận bug |
| 9 | ✅ Cơ chế Skip khi không có DB | Assert.Ignore() |
| 10 | ✅ Phân nhóm test theo Category | Chạy riêng từng module |

### 8.3.2 Chưa làm được

| STT | Nội dung | Lý do |
|-----|----------|-------|
| 1 | ❌ Kiểm thử UI tự động | Cần FlaUI/WinAppDriver, phức tạp |
| 2 | ❌ Kiểm thử PhongTapDAO | Thiếu thời gian |
| 3 | ❌ Mock/Stub database | Chưa triển khai |
| 4 | ❌ Kiểm thử bảo mật | Ngoài phạm vi |
| 5 | ❌ Kiểm thử hiệu năng dữ liệu lớn | Ứng dụng desktop nhỏ |
| 6 | ❌ Đo code coverage | Chưa chạy Coverlet |
| 7 | ❌ Kiểm thử ChangePassword | Thiếu thời gian |
| 8 | ❌ Tích hợp CI/CD | Chưa setup GitHub Actions |

## 8.4 Bài học kinh nghiệm rút ra

### 8.4.1 Bài học về thiết kế test

1. **Test tích hợp DB cần cơ chế tự dọn dẹp:** Sử dụng TearDown + Cleanup() để đảm bảo dữ liệu test không ảnh hưởng môi trường. Nếu không, các test chạy lần sau sẽ bị ảnh hưởng bởi dữ liệu rác.

2. **Suffix unique cho dữ liệu test:** Dùng suffix `_01`, `_02`... cho mỗi test case để tránh conflict khi chạy song song hoặc khi dữ liệu cũ chưa được cleanup.

3. **Assert.Ignore() rất hữu ích:** Cho phép test tự động skip khi không đủ điều kiện (thiếu DB, thiếu dữ liệu) thay vì Fail sai. Giúp test suite có thể chạy trên mọi máy.

### 8.4.2 Bài học về bug thường gặp

4. **FK constraint là nguồn bug phổ biến:** BUG#02, BUG#03 cho thấy mọi thao tác Delete cần được bọc trong try-catch với thông báo thân thiện. Đây là lỗi rất phổ biến trong ứng dụng CRUD.

5. **Tính nhất quán DAO rất quan trọng:** BUG#08, BUG#09 – khi một số DAO có Search() mà số khác không có → tính nhất quán bị vi phạm. Cần có interface chuẩn (ISearchableDAO) để đảm bảo.

6. **Validation phải ở nhiều tầng:** BUG#04 cho thấy chỉ dựa vào CHECK constraint là không đủ. Cần validation ở cả tầng UI (form) và tầng DB.

### 8.4.3 Bài học về encoding

7. **Unicode tiếng Việt cần chú ý:** BUG#01 – ký tự tiếng Việt trong SQL phải dùng N'' prefix. Đây là lỗi nhỏ nhưng ảnh hưởng trải nghiệm người dùng.

### 8.4.4 Bài học về quy trình

8. **Bug Report test là cách tốt để theo dõi bug:** Các test xác nhận bug tồn tại giúp đảm bảo khi fix bug, ta biết chính xác test nào cần cập nhật.

9. **Kiểm thử sớm phát hiện lỗi sớm:** Nhiều bug (BUG#08, BUG#09) có thể được phát hiện sớm nếu có unit test ngay từ đầu.

10. **Review code cũng là kiểm thử:** Một số bug (BUG#06, BUG#07) được phát hiện qua review code/UI chứ không chỉ qua test tự động.

## 8.5 Đề xuất cải tiến ứng dụng và quy trình kiểm thử

### 8.5.1 Cải tiến ứng dụng

| STT | Đề xuất | Ưu tiên | Bug liên quan | Chi tiết |
|-----|---------|---------|---------------|----------|
| 1 | Thêm try-catch khi Delete | **Cao** | BUG#02, BUG#03 | Bọc delete trong try-catch, hiển thị thông báo thân thiện khi FK violation |
| 2 | Implement Search() cho GoiTapDAO | **Cao** | BUG#08 | Thêm `Search(keyword)` tìm theo TenGoi, MoTa |
| 3 | Implement Search() cho DangKyGoiDAO | **Cao** | BUG#09 | Thêm `Search(keyword)` tìm theo TenHV, TenGoi |
| 4 | Auto-calculate NgayHetHan | Trung bình | BUG#06 | Khi chọn GoiTap → NgayHetHan = NgayBatDau + ThoiHan |
| 5 | Cải thiện cboDangKy DisplayMember | Trung bình | BUG#07 | Hiển thị "MaDK - TenHV - TenGoi" |
| 6 | Thêm validation HinhThucTT | Trung bình | BUG#04 | Validate ở UI trước khi insert |
| 7 | Fix encoding setup_db.sql | Thấp | BUG#01 | Thêm N'' prefix cho chuỗi tiếng Việt |
| 8 | Xử lý GhiChu null/empty | Thấp | BUG#05 | Thay "" bằng null hoặc giá trị mặc định |
| 9 | Responsive Dashboard layout | Thấp | BUG#10 | Dùng FlowLayoutPanel |

### 8.5.2 Cải tiến quy trình kiểm thử

| STT | Đề xuất | Lý do |
|-----|---------|-------|
| 1 | **Thêm mock database** | Dùng SQLite in-memory hoặc Moq → test nhanh hơn, không phụ thuộc SQL Server |
| 2 | **Bổ sung test PhongTapDAO** | Tăng module coverage lên 100% |
| 3 | **Đo code coverage** | Chạy Coverlet: `dotnet test --collect:"XPlat Code Coverage"` → biết % code được test |
| 4 | **Thêm UI automation test** | Dùng FlaUI hoặc WinAppDriver → test giao diện tự động |
| 5 | **Tích hợp CI/CD** | GitHub Actions tự chạy test khi push → phát hiện lỗi sớm |
| 6 | **Kiểm thử bảo mật** | Kiểm tra SQL Injection trên các trường text input |
| 7 | **Tạo interface IBaseDAO** | Đảm bảo tất cả DAO có cùng methods (GetAll, Search, Insert, Update, Delete, Count) |
| 8 | **TestContainers** | Dùng Docker container SQL Server cho test → tự tạo/xóa DB mỗi lần chạy |

### 8.5.3 Đề xuất fix bug ưu tiên cao

**Fix BUG#02 & BUG#03 (FK exception):**
```csharp
// Trước (FormMain.cs):
btnDelete.Click += (s, e) => {
    try {
        dao.Delete(hv.MaHV);
        LoadData();
    } catch (Exception ex) {
        MessageBox.Show("Lỗi: " + ex.Message); // Thông báo kỹ thuật
    }
};

// Sau (đề xuất):
btnDelete.Click += (s, e) => {
    try {
        dao.Delete(hv.MaHV);
        LoadData();
    } catch (SqlException ex) when (ex.Number == 547) { // FK violation
        MessageBox.Show(
            "Không thể xóa: dữ liệu đang được sử dụng ở nơi khác.\n" +
            "Vui lòng xóa dữ liệu liên quan trước.",
            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    } catch (Exception ex) {
        MessageBox.Show("Lỗi: " + ex.Message);
    }
};
```

**Fix BUG#08 (thêm Search cho GoiTapDAO):**
```csharp
// Thêm vào GoiTapDAO.cs:
public List<GoiTap> Search(string keyword)
{
    var list = new List<GoiTap>();
    using var conn = DatabaseHelper.GetConnection();
    conn.Open();
    using var cmd = new SqlCommand(
        "SELECT * FROM GoiTap WHERE TenGoi LIKE @kw OR MoTa LIKE @kw ORDER BY MaGoi DESC",
        conn);
    cmd.Parameters.AddWithValue("@kw", $"%{keyword}%");
    using var reader = cmd.ExecuteReader();
    while (reader.Read())
    {
        list.Add(new GoiTap { /* ... map fields ... */ });
    }
    return list;
}
```

## 8.6 Kết luận

### 8.6.1 Tóm tắt kết quả

Quá trình kiểm thử ứng dụng **AppGym – Quản lý phòng Gym** đã được thực hiện một cách có hệ thống và toàn diện:

- **71 test case tự động** được viết bằng NUnit Framework, bao phủ **10 module chính** (91% tổng module)
- **Tỷ lệ Pass: 100%** (71/71 test case)
- **10 bug được phát hiện** với phân loại: 4 High, 4 Medium, 2 Low
- Không có bug Critical (không gây mất dữ liệu hoặc sập hệ thống hoàn toàn)

### 8.6.2 Đánh giá tổng thể

Ứng dụng AppGym có chất lượng **trung bình khá**:
- Các chức năng CRUD hoạt động chính xác
- Đăng nhập bảo mật tốt (SHA2_512 + Salt)
- Ràng buộc toàn vẹn dữ liệu (FK, CHECK) hoạt động
- Tuy nhiên còn thiếu xử lý exception, thiếu chức năng Search ở 2 module, và một số vấn đề UX

### 8.6.3 Giá trị thu được

Qua dự án kiểm thử này, nhóm đã:
1. Nắm vững quy trình kiểm thử: Phân tích → Thiết kế → Thực thi → Báo cáo
2. Thực hành viết test script NUnit chuyên nghiệp
3. Áp dụng kiểm thử hộp trắng (phân tích code) và hộp đen (phân lớp tương đương, giá trị biên)
4. Phát triển kỹ năng viết Bug Report chuẩn
5. Hiểu tầm quan trọng của test tự động trong đảm bảo chất lượng phần mềm

### 8.6.4 Hướng phát triển

Trong giai đoạn tiếp theo (báo cáo cuối kỳ), nhóm dự kiến:
- Fix tất cả 4 bug High priority
- Bổ sung test cho PhongTapDAO và ChangePassword
- Đo code coverage bằng Coverlet
- Triển khai UI automation test với FlaUI
- Tích hợp CI/CD qua GitHub Actions

---

\newpage

# TÀI LIỆU THAM KHẢO

1. **NUnit Documentation** – NUnit Contributors. (2024). *NUnit 3.x Documentation*. https://docs.nunit.org/

2. **Microsoft .NET 8.0 Documentation** – Microsoft. (2024). *.NET 8 Documentation*. https://learn.microsoft.com/en-us/dotnet/

3. **Microsoft Data SqlClient** – Microsoft. (2024). *Microsoft.Data.SqlClient Documentation*. https://learn.microsoft.com/en-us/sql/connect/ado-net/microsoft-ado-net-sql-server

4. **SQL Server Express Documentation** – Microsoft. (2024). *SQL Server Technical Documentation*. https://learn.microsoft.com/en-us/sql/sql-server/

5. **Windows Forms Documentation** – Microsoft. (2024). *Windows Forms for .NET*. https://learn.microsoft.com/en-us/dotnet/desktop/winforms/

6. **ISTQB Foundation Level Syllabus** – ISTQB. (2023). *Certified Tester Foundation Level Syllabus v4.0*. https://www.istqb.org/

7. **Software Testing: Principles and Practices** – Desikan, S., & Ramesh, G. (2006). *Software Testing: Principles and Practices*. Pearson Education.

8. **Coverlet Code Coverage** – Coverlet Contributors. (2024). *Coverlet - Cross Platform Code Coverage for .NET*. https://github.com/coverlet-coverage/coverlet

9. **The Art of Software Testing** – Myers, G. J., Sandler, C., & Badgett, T. (2011). *The Art of Software Testing* (3rd Edition). Wiley.

10. **Lessons Learned in Software Testing** – Kaner, C., Bach, J., & Pettichord, B. (2002). *Lessons Learned in Software Testing*. Wiley.

---

# PHỤ LỤC

## Phụ lục A: Script tạo Database (CodeTaoBang.sql)

```sql
IF DB_ID('GymManagementDB') IS NOT NULL
BEGIN
    ALTER DATABASE GymManagementDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE GymManagementDB;
END
GO

CREATE DATABASE GymManagementDB;
GO

USE GymManagementDB;
GO

-- 1) PHONGTAP
CREATE TABLE dbo.PhongTap (
    MaPhong     INT IDENTITY(1,1) PRIMARY KEY,
    TenPhong    NVARCHAR(100) NOT NULL,
    DiaChi      NVARCHAR(200) NULL,
    SucChua     INT NULL CHECK (SucChua IS NULL OR SucChua > 0),
    MoTa        NVARCHAR(300) NULL,
    TrangThai   BIT NULL DEFAULT (1)
);

-- 2) HOCVIEN
CREATE TABLE dbo.HocVien (
    MaHV        INT IDENTITY(1,1) PRIMARY KEY,
    HoTen       NVARCHAR(100) NOT NULL,
    GioiTinh    NVARCHAR(10) NULL CHECK (GioiTinh IN (N'Nam', N'Nữ', N'Khác')),
    NgaySinh    DATE NULL,
    SDT         VARCHAR(20) NULL UNIQUE,
    Email       VARCHAR(100) NULL UNIQUE,
    NgayDangKy  DATE NULL DEFAULT (GETDATE()),
    TrangThai   BIT NULL DEFAULT (1)
);

-- 3) HUANLUYENVIEN
CREATE TABLE dbo.HuanLuyenVien (
    MaHLV       INT IDENTITY(1,1) PRIMARY KEY,
    HoTen       NVARCHAR(100) NOT NULL,
    GioiTinh    NVARCHAR(10) NULL CHECK (GioiTinh IN (N'Nam', N'Nữ', N'Khác')),
    SDT         VARCHAR(20) NULL UNIQUE,
    ChuyenMon   NVARCHAR(100) NULL,
    Luong       DECIMAL(18,2) NULL CHECK (Luong IS NULL OR Luong >= 0),
    TrangThai   BIT NULL DEFAULT (1)
);

-- 4) CALAM
CREATE TABLE dbo.CaLam (
    MaCa        INT IDENTITY(1,1) PRIMARY KEY,
    TenCa       NVARCHAR(50) NOT NULL UNIQUE,
    GioBatDau   TIME NULL,
    GioKetThuc  TIME NULL,
    CONSTRAINT CK_CaLam_Time CHECK (
        GioBatDau IS NULL OR GioKetThuc IS NULL OR GioKetThuc > GioBatDau
    )
);

-- 5) GOITAP
CREATE TABLE dbo.GoiTap (
    MaGoi       INT IDENTITY(1,1) PRIMARY KEY,
    TenGoi      NVARCHAR(100) NOT NULL UNIQUE,
    ThoiHan     INT NULL CHECK (ThoiHan IS NULL OR ThoiHan > 0),
    Gia         DECIMAL(18,2) NULL CHECK (Gia IS NULL OR Gia >= 0),
    MoTa        NVARCHAR(300) NULL,
    TrangThai   BIT NULL DEFAULT (1)
);

-- 6) DANGKYGOI
CREATE TABLE dbo.DangKyGoi (
    MaDK        INT IDENTITY(1,1) PRIMARY KEY,
    MaHV        INT NOT NULL,
    MaGoi       INT NOT NULL,
    NgayBatDau  DATE NULL,
    NgayHetHan  DATE NULL,
    TrangThai   NVARCHAR(20) NULL DEFAULT(N'Đang hoạt động')
        CHECK (TrangThai IN (N'Đang hoạt động', N'Hết hạn', N'Tạm dừng', N'Hủy')),
    GhiChu      NVARCHAR(300) NULL,
    CONSTRAINT FK_DangKyGoi_HocVien FOREIGN KEY (MaHV) REFERENCES dbo.HocVien(MaHV),
    CONSTRAINT FK_DangKyGoi_GoiTap  FOREIGN KEY (MaGoi) REFERENCES dbo.GoiTap(MaGoi)
);

-- 7) PHANCONG
CREATE TABLE dbo.PhanCong (
    MaPC        INT IDENTITY(1,1) PRIMARY KEY,
    MaHLV       INT NOT NULL,
    MaDK        INT NOT NULL,
    MaCa        INT NULL,
    NgayBatDau  DATE NULL,
    NgayKetThuc DATE NULL,
    GhiChu      NVARCHAR(300) NULL,
    CONSTRAINT FK_PhanCong_HLV FOREIGN KEY (MaHLV) REFERENCES dbo.HuanLuyenVien(MaHLV),
    CONSTRAINT FK_PhanCong_DK  FOREIGN KEY (MaDK)  REFERENCES dbo.DangKyGoi(MaDK),
    CONSTRAINT FK_PhanCong_Ca  FOREIGN KEY (MaCa)  REFERENCES dbo.CaLam(MaCa)
);

-- 8) HOADON
CREATE TABLE dbo.HoaDon (
    MaHD            INT IDENTITY(1,1) PRIMARY KEY,
    MaDK            INT NOT NULL,
    NgayThanhToan   DATETIME NULL DEFAULT (GETDATE()),
    SoTien          DECIMAL(18,2) NULL CHECK (SoTien IS NULL OR SoTien > 0),
    HinhThucTT      NVARCHAR(30) NULL
        CHECK (HinhThucTT IN (N'Tiền mặt', N'Chuyển khoản', N'Thẻ', N'Khác')),
    GhiChu          NVARCHAR(300) NULL,
    CONSTRAINT FK_HoaDon_DangKyGoi FOREIGN KEY (MaDK) REFERENCES dbo.DangKyGoi(MaDK)
);

-- 9) QUYEN
CREATE TABLE dbo.Quyen (
    MaQuyen     INT IDENTITY(1,1) PRIMARY KEY,
    TenBang     NVARCHAR(80) NULL,
    HanhDong    NVARCHAR(30) NULL,
    MoTa        NVARCHAR(200) NULL,
    CONSTRAINT UQ_Quyen UNIQUE (TenBang, HanhDong)
);

-- 10) TAIKHOAN
CREATE TABLE dbo.TaiKhoan (
    MaTK        INT IDENTITY(1,1) PRIMARY KEY,
    TenDangNhap VARCHAR(50) NOT NULL UNIQUE,
    MatKhauHash VARBINARY(MAX) NOT NULL,
    Salt        UNIQUEIDENTIFIER DEFAULT NEWID(),
    HoTen       NVARCHAR(100) NULL,
    VaiTro      NVARCHAR(20) NULL DEFAULT(N'NhanVien')
        CHECK (VaiTro IN (N'Admin', N'NhanVien')),
    TrangThai   BIT NULL DEFAULT (1),
    TaoLuc      DATETIME NULL DEFAULT (GETDATE())
);

-- 11) TAIKHOAN_QUYEN
CREATE TABLE dbo.TaiKhoan_Quyen (
    MaTK        INT NOT NULL,
    MaQuyen     INT NOT NULL,
    CONSTRAINT PK_TaiKhoan_Quyen PRIMARY KEY (MaTK, MaQuyen),
    CONSTRAINT FK_TKQ_TaiKhoan FOREIGN KEY (MaTK) REFERENCES dbo.TaiKhoan(MaTK),
    CONSTRAINT FK_TKQ_Quyen    FOREIGN KEY (MaQuyen) REFERENCES dbo.Quyen(MaQuyen)
);
```

## Phụ lục B: Mã nguồn TestBase.cs

```csharp
using AppGym.DataAccess;
using Microsoft.Data.SqlClient;

namespace AppGym.Tests;

public abstract class TestBase
{
    protected const string ConnStr =
        @"Server=(local)\SQLEXPRESS;Database=GymManagementDB;
          Trusted_Connection=True;TrustServerCertificate=True;";

    [OneTimeSetUp]
    public virtual void GlobalSetup()
    {
        DatabaseHelper.ConnectionString = ConnStr;
    }

    protected static void SkipIfNoDatabase()
    {
        try
        {
            using var conn = new SqlConnection(ConnStr);
            conn.Open();
        }
        catch
        {
            Assert.Ignore("Không thể kết nối database. Bỏ qua test này.");
        }
    }

    protected static void Cleanup(string sql)
    {
        try
        {
            using var conn = new SqlConnection(ConnStr);
            conn.Open();
            using var cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }
        catch { /* ignore cleanup errors */ }
    }
}
```

## Phụ lục C: Mã nguồn DatabaseHelper.cs

```csharp
using Microsoft.Data.SqlClient;

namespace AppGym.DataAccess
{
    public static class DatabaseHelper
    {
        private static string _connectionString =
            @"Server=(local)\SQLEXPRESS;Database=GymManagementDB;
              Integrated Security=True;TrustServerCertificate=True;";

        public static string ConnectionString
        {
            get => _connectionString;
            set => _connectionString = value;
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
```

## Phụ lục D: Kết quả TestResults.trx

*(Đính kèm file TestResults.trx xuất từ lệnh: `dotnet test --logger "trx;LogFileName=TestResults.trx"`)*

---

**--- HẾT BÁO CÁO ---**
