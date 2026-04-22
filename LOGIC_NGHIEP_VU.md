# Logic Nghiệp Vụ AppGym

## 1. Mục tiêu hệ thống

AppGym là ứng dụng quản lý phòng gym theo mô hình vận hành nội bộ, tập trung vào:

- Quản lý học viên.
- Quản lý huấn luyện viên.
- Quản lý gói tập.
- Đăng ký gói cho học viên.
- Lập hóa đơn và theo dõi thanh toán.
- Phân công PT cho từng đăng ký.
- Quản lý tài khoản và phân quyền sử dụng hệ thống.

## 2. Cấu hình và khởi động

- Khi mở ứng dụng, hệ thống đọc cấu hình kết nối SQL Server đã lưu.
- Nếu kết nối thất bại, ứng dụng mở màn hình cấu hình database để người dùng chọn server, kiểm tra kết nối và có thể tự tạo database mới.
- Khi tạo database mới, hệ thống nạp script khởi tạo và sinh tài khoản mặc định:
  - `admin`
  - `admin`

## 3. Vai trò người dùng

Hệ thống hiện có 3 vai trò chính:

- `Admin`
  - Toàn quyền trên tất cả chức năng.
  - Có thể quản lý tài khoản.
  - Có thể phân quyền cho tài khoản khác.

- `QuanLy`
  - Có toàn quyền ngầm định trên các module vận hành.
  - Có thể quản lý tài khoản nhân viên.
  - Không có quyền phân quyền như Admin.

- `NhanVien`
  - Chỉ được dùng các module đã được cấp quyền trong bảng phân quyền.

## 4. Các thực thể nghiệp vụ chính

- `TaiKhoan`
  - Dùng để đăng nhập hệ thống.
  - Có vai trò, trạng thái hoạt động và thông tin người dùng.

- `HocVien`
  - Là khách hàng đăng ký tập.
  - Có thể phát sinh nhiều đăng ký gói theo thời gian.

- `HuanLuyenVien`
  - Là PT được phân công cho học viên.

- `GoiTap`
  - Chứa tên gói, thời hạn và giá.

- `DangKyGoi`
  - Liên kết giữa học viên và gói tập.
  - Là bản ghi trung tâm để theo dõi thời gian tập, thanh toán và phân công PT.

- `HoaDon`
  - Ghi nhận tiền đã thanh toán cho một đăng ký gói.

- `PhanCong`
  - Ghi nhận việc gán PT cho một đăng ký gói.

- `CaLam`
  - Quản lý ca làm việc phục vụ cho phân công PT.

## 5. Quan hệ dữ liệu

- Một `HocVien` có thể có nhiều `DangKyGoi`.
- Một `GoiTap` có thể được nhiều học viên đăng ký.
- Một `DangKyGoi` có thể có nhiều `HoaDon`.
- Một `DangKyGoi` chỉ được phép có tối đa 1 `PhanCong`.
  - Ràng buộc này được giữ cả ở logic ứng dụng lẫn unique index `UX_PhanCong_MaDK`.
- Một `PhanCong` gắn với:
  - 1 huấn luyện viên
  - 1 đăng ký gói
  - 0 hoặc 1 ca làm

## 6. Luồng nghiệp vụ chính

### 6.1. Đăng nhập

- Người dùng đăng nhập bằng `Tên đăng nhập` và `Mật khẩu`.
- Hệ thống chỉ cho đăng nhập các tài khoản đang hoạt động.
- Mật khẩu được kiểm tra bằng hash `SHA2_512` kết hợp `Salt`.

### 6.2. Quản lý học viên

- Có thể thêm, sửa và tìm kiếm học viên.
- Không cho phép xóa học viên khi vẫn còn dữ liệu liên quan như đăng ký gói.
- Trong tình huống bị khóa ngoại, hệ thống hiện thông báo ngắn gọn thay vì bung lỗi SQL thô.

### 6.3. Quản lý gói tập

- Gói tập có các thuộc tính chính:
  - Tên gói
  - Thời hạn
  - Giá
  - Mô tả
- Gói tập được dùng để tạo đăng ký cho học viên.

### 6.4. Đăng ký gói

- Khi tạo đăng ký mới, người dùng phải chọn:
  - Học viên
  - Gói tập
  - Người lập
- Ngày hết hạn được gợi ý từ:
  - `Ngày bắt đầu`
  - `Thời hạn` của gói tập
- Nếu chưa chọn gói tập, hệ thống không cho lưu đăng ký.
- Màn hình đăng ký hiển thị thêm trạng thái thanh toán của từng đăng ký:
  - Chưa thanh toán
  - Thanh toán một phần
  - Đã thanh toán đủ

### 6.5. Hóa đơn và thanh toán

- Mỗi hóa đơn gắn với 1 đăng ký gói.
- Một đăng ký gói có thể thanh toán nhiều lần.
- Tổng số tiền đã thanh toán được cộng dồn từ các hóa đơn.
- Số tiền còn thiếu được tính theo:
  - `Giá gói - Tổng đã thanh toán`
- Hệ thống có thể truy xuất danh sách chưa thanh toán hoặc thanh toán chưa đủ.

### 6.6. Phân công PT

- Chỉ các đăng ký chưa có PT mới được đưa vào danh sách chờ phân công.
- Mỗi đăng ký gói chỉ được phân công tối đa 1 PT.
- Khi sửa phân công, hệ thống vẫn kiểm tra để không tạo trùng PT trên cùng một đăng ký.

### 6.7. Quản lý tài khoản

- Tài khoản có thể được thêm, sửa, khóa hoặc đổi mật khẩu.
- Không cho phép xóa tùy tiện tài khoản đã phát sinh nghiệp vụ.
- Nếu tài khoản đã tham gia lập đăng ký hoặc hóa đơn, hướng xử lý nghiệp vụ là khóa tài khoản thay vì xóa.

## 7. Quy tắc nghiệp vụ quan trọng

- Không cho phép lưu đăng ký gói nếu thiếu học viên, gói tập hoặc người lập.
- Ngày hết hạn không được nhỏ hơn ngày bắt đầu.
- Một đăng ký gói chỉ có tối đa 1 PT.
- Không thể xóa dữ liệu cha khi vẫn còn dữ liệu con tham chiếu bằng khóa ngoại.
- Trạng thái thanh toán của đăng ký gói được suy ra từ tổng hóa đơn, không nhập tay.
- `QuanLy` có quyền vận hành rộng, nhưng quyền phân quyền chỉ thuộc `Admin`.

## 8. Dashboard và theo dõi vận hành

Phần tổng quan phục vụ theo dõi vận hành, gồm các nhóm thông tin như:

- Số lượng học viên.
- Số lượng huấn luyện viên.
- Số gói tập đang kinh doanh.
- Số đăng ký hiện có.
- Tình hình doanh thu và thanh toán.
- Trạng thái đăng ký hoặc phân bố theo gói tập.

## 9. Tóm tắt logic trung tâm

Trục nghiệp vụ chính của hệ thống là:

`HocVien -> DangKyGoi -> HoaDon / PhanCong`

Nghĩa là:

- Học viên đăng ký một gói tập.
- Từ đăng ký đó phát sinh thanh toán qua hóa đơn.
- Cũng từ đăng ký đó có thể phát sinh phân công PT.
- Các ràng buộc khóa ngoại giúp giữ tính nhất quán dữ liệu trong toàn bộ hệ thống.
