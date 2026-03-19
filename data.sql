USE GymManagementDB;
USE GymManagementDB;
GO

/* =========================
   1) HOCVIEN (8 dòng)
   ========================= */
INSERT INTO dbo.HocVien (HoTen, GioiTinh, NgaySinh, SDT, Email, NgayDangKy, TrangThai)
VALUES
(N'Nguyễn Minh Anh', N'Nữ',  '2000-03-12', '0901000001', 'minhanh01@gmail.com', '2024-11-10', 1),
(N'Trần Quốc Bảo',   N'Nam', '1998-07-25', '0901000002', 'quocbao98@gmail.com', '2024-12-01', 1),
(N'Lê Hoàng Long',   N'Nam', '2001-01-05', '0901000003', 'hoanglong01@gmail.com','2025-01-15', 1),
(N'Phạm Thùy Dung',  N'Nữ',  '1999-10-30', '0901000004', 'thuydung99@gmail.com', '2025-02-20', 1),
(N'Võ Gia Hân',      N'Nữ',  '2002-05-18', '0901000005', 'giahan02@gmail.com',   '2025-03-02', 1),
(N'Bùi Đức Huy',     N'Nam', '1997-12-09', '0901000006', 'duchuy97@gmail.com',   '2025-03-28', 1),
(N'Đặng Khánh Linh', N'Nữ',  '2003-09-21', '0901000007', 'khanhlinh03@gmail.com','2025-04-10', 1),
(N'Ngô Nhật Nam',    N'Khác','1996-02-14', '0901000008', 'nhatnam96@gmail.com',  '2025-05-05', 0);
GO

/* =========================
   2) HUANLUYENVIEN (6 dòng)
   ========================= */
INSERT INTO dbo.HuanLuyenVien (HoTen, GioiTinh, SDT, ChuyenMon, Luong, TrangThai)
VALUES
(N'Phan Tuấn Kiệt',  N'Nam', '0912000001', N'Tăng cơ - Giảm mỡ', 18000000, 1),
(N'Nguyễn Thảo My',  N'Nữ',  '0912000002', N'Yoga - Mobility',    16000000, 1),
(N'Lê Văn Phúc',     N'Nam', '0912000003', N'Powerlifting',       22000000, 1),
(N'Trần Thanh Hà',   N'Nữ',  '0912000004', N'Cardio - HIIT',      17000000, 1),
(N'Võ Minh Quân',    N'Nam', '0912000005', N'Boxing Fitness',     19000000, 1),
(N'Đặng Mỹ Linh',    N'Nữ',  '0912000006', N'Pilates',            16500000, 0);
GO

/* =========================
   3) CALAM (5 dòng)
   ========================= */
INSERT INTO dbo.CaLam (TenCa, GioBatDau, GioKetThuc)
VALUES
(N'Ca Sáng 1', '06:00', '08:00'),
(N'Ca Sáng 2', '08:00', '10:00'),
(N'Ca Chiều 1','14:00', '16:00'),
(N'Ca Chiều 2','16:00', '18:00'),
(N'Ca Tối',    '18:00', '20:00');
GO

/* =========================
   4) GOITAP (6 dòng)
   ========================= */
INSERT INTO dbo.GoiTap (TenGoi, ThoiHan, Gia, MoTa, TrangThai)
VALUES
(N'Gói 1 Tháng Basic',   30,  450000,  N'Tập tự do - không PT', 1),
(N'Gói 3 Tháng Standard',90, 1200000,  N'Tập tự do + 2 buổi PT', 1),
(N'Gói 6 Tháng Premium', 180,2100000,  N'Tập tự do + 6 buổi PT', 1),
(N'Gói 12 Tháng VIP',    365,3800000,  N'Tập tự do + 12 buổi PT + locker', 1),
(N'Gói Yoga 1 Tháng',    30,  550000,  N'Yoga theo lớp', 1),
(N'Gói HIIT 1 Tháng',    30,  600000,  N'HIIT theo lớp', 0);
GO

/* =========================
   5) DANGKYGOI (8 dòng)
   - FK: MaHV, MaGoi phải tồn tại
   - Date: NgayHetHan >= NgayBatDau
   ========================= */
INSERT INTO dbo.DangKyGoi (MaHV, MaGoi, NgayBatDau, NgayHetHan, TrangThai, GhiChu)
VALUES
(1, 1, '2025-01-10', '2025-02-08', N'Hết hạn',        N'Đã gia hạn sau đó'),
(2, 2, '2025-02-01', '2025-05-01', N'Đang hoạt động', N'Tập tăng cơ'),
(3, 3, '2025-03-15', '2025-09-11', N'Đang hoạt động', N'Có PT'),
(4, 1, '2025-04-05', '2025-05-04', N'Hủy',            N'Đổi kế hoạch'),
(5, 5, '2025-04-10', '2025-05-09', N'Đang hoạt động', N'Yoga buổi tối'),
(6, 4, '2025-05-01', '2026-04-30', N'Đang hoạt động', N'VIP cả năm'),
(7, 2, '2025-06-01', '2025-08-30', N'Tạm dừng',       N'Tạm nghỉ 2 tuần'),
(8, 1, '2025-06-15', '2025-07-14', N'Hết hạn',        N'Trạng thái tài khoản HV = 0');
GO

/* =========================
   6) PHANCONG (7 dòng)
   - FK: MaHLV, MaDK, MaCa
   ========================= */
INSERT INTO dbo.PhanCong (MaHLV, MaDK, MaCa, NgayBatDau, NgayKetThuc, GhiChu)
VALUES
(1, 2, 1, '2025-02-01', '2025-03-01', N'Đánh giá form cơ bản'),
(1, 2, 2, '2025-03-02', '2025-05-01', N'Tăng khối lượng'),
(3, 3, 5, '2025-03-15', '2025-06-15', N'Chu kỳ strength'),
(4, 3, 3, '2025-06-16', '2025-09-11', N'Giảm mỡ cuối kỳ'),
(2, 5, 5, '2025-04-10', '2025-05-09', N'Yoga nhóm 10 người'),
(5, 6, 4, '2025-05-01', '2025-08-01', N'Boxing fitness'),
(5, 6, 5, '2025-08-02', '2026-04-30', N'Duy trì thể lực');
GO

/* =========================
   7) HOADON (8 dòng)
   - FK: MaDK phải tồn tại
   - SoTien > 0
   - HinhThucTT thuộc danh sách
   ========================= */
INSERT INTO dbo.HoaDon (MaDK, NgayThanhToan, SoTien, HinhThucTT, GhiChu)
VALUES
(1, '2025-01-10T09:10:00',  450000, N'Tiền mặt',     N'Thanh toán tại quầy'),
(2, '2025-02-01T18:30:00', 1200000, N'Chuyển khoản', N'CK MB Bank'),
(3, '2025-03-15T07:45:00', 2100000, N'Thẻ',          N'Quẹt thẻ'),
(4, '2025-04-05T10:05:00',  450000, N'Tiền mặt',     N'Hủy sau 2 ngày'),
(5, '2025-04-10T19:20:00',  550000, N'Chuyển khoản', N'CK Vietcombank'),
(6, '2025-05-01T08:00:00', 3800000, N'Thẻ',          N'Gói VIP'),
(7, '2025-06-01T17:15:00', 1200000, N'Khác',         N'Ví điện tử'),
(8, '2025-06-15T09:00:00',  450000, N'Tiền mặt',     N'Thanh toán thường');
GO

/* =========================
   8) QUYEN (10 dòng)
   - UNIQUE (TenBang, HanhDong)
   ========================= */
INSERT INTO dbo.Quyen (TenBang, HanhDong, MoTa)
VALUES
(N'HocVien',        N'VIEW',   N'Xem danh sách học viên'),
(N'HocVien',        N'INSERT', N'Thêm học viên'),
(N'HocVien',        N'UPDATE', N'Sửa học viên'),
(N'HocVien',        N'DELETE', N'Xóa học viên'),
(N'DangKyGoi',      N'VIEW',   N'Xem đăng ký gói'),
(N'DangKyGoi',      N'INSERT', N'Tạo đăng ký gói'),
(N'PhanCong',       N'VIEW',   N'Xem phân công PT'),
(N'PhanCong',       N'UPDATE', N'Sửa phân công'),
(N'HoaDon',         N'VIEW',   N'Xem hóa đơn'),
(N'HoaDon',         N'INSERT', N'Tạo hóa đơn');
GO

/* =========================
   9) TAIKHOAN (5 dòng)
   - Password = '123' cho tất cả tài khoản
   - Salt = UNIQUEIDENTIFIER (phù hợp schema DB)
   - Hash = SHA2_512('123' + '|' + CONVERT(varchar(50), Salt))
   ========================= */
DECLARE @s1 UNIQUEIDENTIFIER = NEWID();
DECLARE @s2 UNIQUEIDENTIFIER = NEWID();
DECLARE @s3 UNIQUEIDENTIFIER = NEWID();
DECLARE @s4 UNIQUEIDENTIFIER = NEWID();
DECLARE @s5 UNIQUEIDENTIFIER = NEWID();

INSERT INTO dbo.TaiKhoan (TenDangNhap, Salt, MatKhauHash, HoTen, VaiTro, TrangThai, TaoLuc)
VALUES
('admin',    @s1, HASHBYTES('SHA2_512', CONVERT(varbinary(200), '123' + '|' + CONVERT(varchar(50), @s1))), N'Quản trị hệ thống', N'Admin',    1, '2025-01-01T08:00:00'),
('nv01',     @s2, HASHBYTES('SHA2_512', CONVERT(varbinary(200), '123' + '|' + CONVERT(varchar(50), @s2))), N'Nguyễn Văn A',      N'NhanVien', 1, '2025-02-01T08:00:00'),
('nv02',     @s3, HASHBYTES('SHA2_512', CONVERT(varbinary(200), '123' + '|' + CONVERT(varchar(50), @s3))), N'Trần Văn B',        N'NhanVien', 1, '2025-03-01T08:00:00'),
('nv03',     @s4, HASHBYTES('SHA2_512', CONVERT(varbinary(200), '123' + '|' + CONVERT(varchar(50), @s4))), N'Lê Văn C',          N'NhanVien', 0, '2025-04-01T08:00:00'),
('quanly01', @s5, HASHBYTES('SHA2_512', CONVERT(varbinary(200), '123' + '|' + CONVERT(varchar(50), @s5))), N'Phạm Quản Lý',      N'Admin',    1, '2025-05-01T08:00:00');
GO

/* =========================
   10) TAIKHOAN_QUYEN (10 dòng)
   - PK (MaTK, MaQuyen)
   ========================= */
-- Giả sử: MaTK 1..5; MaQuyen 1..10 theo thứ tự insert ở trên
INSERT INTO dbo.TaiKhoan_Quyen (MaTK, MaQuyen)
VALUES
(1, 0),(1, 1),(1, 2),(1, 3),(1, 4),(1, 5),(1, 6),(1, 7),(1, 8),(1, 9);
GO
