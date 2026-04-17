CREATE DATABASE GymManagementDB;
GO
USE GymManagementDB;
GO

CREATE TABLE PhongTap (
    MaPhong INT IDENTITY(1,1) PRIMARY KEY,
    TenPhong NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(200),
    SucChua INT CHECK (SucChua IS NULL OR SucChua > 0),
    MoTa NVARCHAR(300)
);
GO

CREATE TABLE TaiKhoan (
    MaTK INT IDENTITY(1,1) PRIMARY KEY,
    TenDangNhap NVARCHAR(50) NOT NULL UNIQUE,
    MatKhauHash VARBINARY(MAX),
    Salt UNIQUEIDENTIFIER DEFAULT NEWID(),
    HoTen NVARCHAR(100),
    VaiTro NVARCHAR(20) DEFAULT N'NhanVien' CHECK (VaiTro IN (N'Admin', N'NhanVien')),
    TaoLuc DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE HocVien (
    MaHV INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    GioiTinh NVARCHAR(10) CHECK (GioiTinh IN (N'Nam', N'Nữ', N'Khác')),
    NgaySinh DATE,
    SDT NVARCHAR(20),
    Email NVARCHAR(100),
    NgayDangKy DATE DEFAULT GETDATE()
);
GO

CREATE TABLE HuanLuyenVien (
    MaHLV INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    GioiTinh NVARCHAR(10) CHECK (GioiTinh IN (N'Nam', N'Nữ', N'Khác')),
    SDT NVARCHAR(20),
    ChuyenMon NVARCHAR(100),
    Luong DECIMAL(18,0)
);
GO

CREATE TABLE GoiTap (
    MaGoi INT IDENTITY(1,1) PRIMARY KEY,
    TenGoi NVARCHAR(100) NOT NULL,
    ThoiHan INT CHECK (ThoiHan IS NULL OR ThoiHan > 0),
    Gia DECIMAL(18,0) CHECK (Gia IS NULL OR Gia >= 0),
    MoTa NVARCHAR(500)
);
GO

CREATE TABLE CaLam (
    MaCa INT IDENTITY(1,1) PRIMARY KEY,
    TenCa NVARCHAR(50) NOT NULL,
    GioBatDau TIME,
    GioKetThuc TIME,
    CONSTRAINT CK_CaLam_Time CHECK (GioBatDau IS NULL OR GioKetThuc IS NULL OR GioKetThuc > GioBatDau)
);
GO

CREATE TABLE DangKyGoi (
    MaDK INT IDENTITY(1,1) PRIMARY KEY,
    MaHV INT NOT NULL FOREIGN KEY REFERENCES HocVien(MaHV),
    MaGoi INT NOT NULL FOREIGN KEY REFERENCES GoiTap(MaGoi),
    NgayBatDau DATE,
    NgayHetHan DATE,
    GhiChu NVARCHAR(500),
    CONSTRAINT CK_DangKyGoi_Date CHECK (NgayBatDau IS NULL OR NgayHetHan IS NULL OR NgayHetHan >= NgayBatDau)
);
GO

CREATE TABLE PhanCong (
    MaPC INT IDENTITY(1,1) PRIMARY KEY,
    MaHLV INT NOT NULL FOREIGN KEY REFERENCES HuanLuyenVien(MaHLV),
    MaDK INT NOT NULL FOREIGN KEY REFERENCES DangKyGoi(MaDK),
    MaCa INT FOREIGN KEY REFERENCES CaLam(MaCa),
    NgayBatDau DATE,
    NgayKetThuc DATE,
    GhiChu NVARCHAR(500),
    CONSTRAINT CK_PhanCong_Date CHECK (NgayBatDau IS NULL OR NgayKetThuc IS NULL OR NgayKetThuc >= NgayBatDau)
);
GO

CREATE TABLE HoaDon (
    MaHD INT IDENTITY(1,1) PRIMARY KEY,
    MaDK INT NOT NULL FOREIGN KEY REFERENCES DangKyGoi(MaDK),
    NgayThanhToan DATETIME DEFAULT GETDATE(),
    SoTien DECIMAL(18,0) CHECK (SoTien IS NULL OR SoTien > 0),
    HinhThucTT NVARCHAR(50) CHECK (HinhThucTT IN (N'Tiền mặt', N'Chuyển khoản', N'Thẻ', N'Khác')),
    GhiChu NVARCHAR(500)
);
GO

-- Insert default admin account (password: 123)
DECLARE @salt UNIQUEIDENTIFIER = NEWID();
INSERT INTO TaiKhoan (TenDangNhap, Salt, MatKhauHash, HoTen, VaiTro)
VALUES (
    'admin',
    @salt,
    HASHBYTES('SHA2_512', CONVERT(varbinary(200), '123' + '|' + CONVERT(varchar(50), @salt))),
    N'Quản trị viên',
    N'Admin'
);
GO

-- Insert default NhanVien account (password: 123)
DECLARE @salt2 UNIQUEIDENTIFIER = NEWID();
INSERT INTO TaiKhoan (TenDangNhap, Salt, MatKhauHash, HoTen, VaiTro)
VALUES (
    'nhanvien',
    @salt2,
    HASHBYTES('SHA2_512', CONVERT(varbinary(200), '123' + '|' + CONVERT(varchar(50), @salt2))),
    N'Nhân viên',
    N'NhanVien'
);
GO

-- Insert sample PhongTap
INSERT INTO PhongTap (TenPhong, DiaChi, SucChua, MoTa) VALUES (N'Phòng Gym chính', N'Tầng 1 - Tòa A', 50, N'Phòng tập chính với đầy đủ thiết bị');
INSERT INTO PhongTap (TenPhong, DiaChi, SucChua, MoTa) VALUES (N'Phòng Yoga', N'Tầng 2 - Tòa A', 20, N'Phòng tập yoga và thiền');
INSERT INTO PhongTap (TenPhong, DiaChi, SucChua, MoTa) VALUES (N'Phòng Cardio', N'Tầng 1 - Tòa B', 30, N'Máy chạy bộ, xe đạp');
GO

-- Insert sample CaLam
INSERT INTO CaLam (TenCa, GioBatDau, GioKetThuc) VALUES (N'Ca sáng', '06:00', '12:00');
INSERT INTO CaLam (TenCa, GioBatDau, GioKetThuc) VALUES (N'Ca chiều', '12:00', '18:00');
INSERT INTO CaLam (TenCa, GioBatDau, GioKetThuc) VALUES (N'Ca tối', '18:00', '22:00');
GO

