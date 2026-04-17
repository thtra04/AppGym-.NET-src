-- =====================================================
-- RESET: Xoa database cu (neu co) va tao lai tu dau
-- =====================================================

USE master;
GO

-- Xoa DB cu neu ton tai
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'GymManagementDB')
BEGIN
    ALTER DATABASE GymManagementDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE GymManagementDB;
END
GO

CREATE DATABASE GymManagementDB;
GO
USE GymManagementDB;
GO

CREATE TABLE PhongTap (
    MaPhong INT IDENTITY(1,1) PRIMARY KEY,
    TenPhong NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(200),
    SucChua INT,
    MoTa NVARCHAR(500)
);
GO

CREATE TABLE TaiKhoan (
    MaTK INT IDENTITY(1,1) PRIMARY KEY,
    TenDangNhap NVARCHAR(50) NOT NULL UNIQUE,
    MatKhauHash VARBINARY(MAX),
    Salt UNIQUEIDENTIFIER DEFAULT NEWID(),
    HoTen NVARCHAR(100),
    VaiTro NVARCHAR(50) DEFAULT N'NhanVien' CHECK (VaiTro IN (N'Admin', N'NhanVien')),
    TaoLuc DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE HocVien (
    MaHV INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    GioiTinh NVARCHAR(10) CHECK (GioiTinh IN (N'Nam', N'N\u1eef')),
    NgaySinh DATE,
    SDT NVARCHAR(20),
    Email NVARCHAR(100),
    NgayDangKy DATE DEFAULT GETDATE()
);
GO

CREATE TABLE HuanLuyenVien (
    MaHLV INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    GioiTinh NVARCHAR(10) CHECK (GioiTinh IN (N'Nam', N'N\u1eef')),
    SDT NVARCHAR(20),
    ChuyenMon NVARCHAR(100),
    Luong DECIMAL(18,0)
);
GO

CREATE TABLE GoiTap (
    MaGoi INT IDENTITY(1,1) PRIMARY KEY,
    TenGoi NVARCHAR(100) NOT NULL,
    ThoiHan INT CHECK (ThoiHan > 0),
    Gia DECIMAL(18,0) CHECK (Gia >= 0),
    MoTa NVARCHAR(500)
);
GO

CREATE TABLE CaLam (
    MaCa INT IDENTITY(1,1) PRIMARY KEY,
    TenCa NVARCHAR(50) NOT NULL,
    GioBatDau TIME,
    GioKetThuc TIME
);
GO

CREATE TABLE DangKyGoi (
    MaDK INT IDENTITY(1,1) PRIMARY KEY,
    MaHV INT NOT NULL FOREIGN KEY REFERENCES HocVien(MaHV),
    MaGoi INT NOT NULL FOREIGN KEY REFERENCES GoiTap(MaGoi),
    NgayBatDau DATE,
    NgayHetHan DATE,
    GhiChu NVARCHAR(500)
);
GO

CREATE TABLE PhanCong (
    MaPC INT IDENTITY(1,1) PRIMARY KEY,
    MaHLV INT NOT NULL FOREIGN KEY REFERENCES HuanLuyenVien(MaHLV),
    MaDK INT NOT NULL FOREIGN KEY REFERENCES DangKyGoi(MaDK),
    MaCa INT FOREIGN KEY REFERENCES CaLam(MaCa),
    NgayBatDau DATE,
    NgayKetThuc DATE,
    GhiChu NVARCHAR(500)
);
GO

CREATE TABLE HoaDon (
    MaHD INT IDENTITY(1,1) PRIMARY KEY,
    MaDK INT NOT NULL FOREIGN KEY REFERENCES DangKyGoi(MaDK),
    NgayThanhToan DATE DEFAULT GETDATE(),
    SoTien DECIMAL(18,0) CHECK (SoTien >= 0),
    HinhThucTT NVARCHAR(50),
    GhiChu NVARCHAR(500)
);
GO

-- =====================================================
-- Tao tai khoan admin (mat khau: 123)
-- =====================================================
DECLARE @salt UNIQUEIDENTIFIER = NEWID();
INSERT INTO TaiKhoan (TenDangNhap, Salt, MatKhauHash, HoTen, VaiTro)
VALUES (
    'admin',
    @salt,
    HASHBYTES('SHA2_512', CONVERT(varbinary(200), '123' + '|' + CONVERT(varchar(50), @salt))),
    N'Qu\u1EA3n tr\u1ECB vi\u00EAn',
    N'Admin'
);
GO

-- Tao tai khoan nhan vien (mat khau: 123)
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

-- =====================================================
-- Du lieu mau: PhongTap
-- =====================================================
INSERT INTO PhongTap (TenPhong, DiaChi, SucChua, MoTa) VALUES
(N'Ph\u00F2ng Gym ch\u00EDnh', N'T\u1EA7ng 1', 50, N'Ph\u00F2ng t\u1EADp ch\u00EDnh v\u1EDBi \u0111\u1EA7y \u0111\u1EE7 thi\u1EBFt b\u1ECB'),
(N'Ph\u00F2ng Yoga', N'T\u1EA7ng 2', 30, N'Ph\u00F2ng t\u1EADp Yoga v\u00E0 thi\u1EC1n'),
(N'Ph\u00F2ng Cardio', N'T\u1EA7ng 1', 40, N'Ph\u00F2ng ch\u1EA1y b\u1ED9 v\u00E0 \u0111\u1EA1p xe');
GO

-- =====================================================
-- Du lieu mau: CaLam
-- =====================================================
INSERT INTO CaLam (TenCa, GioBatDau, GioKetThuc) VALUES (N'Ca s\u00E1ng', '06:00', '12:00');
INSERT INTO CaLam (TenCa, GioBatDau, GioKetThuc) VALUES (N'Ca chi\u1EC1u', '12:00', '18:00');
INSERT INTO CaLam (TenCa, GioBatDau, GioKetThuc) VALUES (N'Ca t\u1ED1i', '18:00', '22:00');
GO

-- =====================================================
-- Du lieu mau: HocVien
-- =====================================================
INSERT INTO HocVien (HoTen, GioiTinh, NgaySinh, SDT, Email, NgayDangKy) VALUES
(N'Nguy\u1EC5n V\u0103n An', N'Nam', '1995-03-15', '0901234567', 'an.nguyen@gmail.com', GETDATE()),
(N'Tr\u1EA7n Th\u1ECB B\u00ECnh', N'N\u1EEF', '1998-07-22', '0912345678', 'binh.tran@gmail.com', GETDATE()),
(N'L\u00EA Minh C\u01B0\u1EDDng', N'Nam', '1993-11-10', '0923456789', 'cuong.le@gmail.com', GETDATE());
GO

-- =====================================================
-- Du lieu mau: HuanLuyenVien
-- =====================================================
INSERT INTO HuanLuyenVien (HoTen, GioiTinh, SDT, ChuyenMon, Luong) VALUES
(N'Ph\u1EA1m V\u0103n D\u0169ng', N'Nam', '0934567890', N'Yoga', 8000000),
(N'Ho\u00E0ng Th\u1ECB Em', N'N\u1EEF', '0945678901', N'Cardio', 7500000);
GO

-- =====================================================
-- Du lieu mau: GoiTap
-- =====================================================
INSERT INTO GoiTap (TenGoi, ThoiHan, Gia, MoTa) VALUES
(N'G\u00F3i 1 th\u00E1ng', 30, 500000, N'G\u00F3i c\u01A1 b\u1EA3n 1 th\u00E1ng'),
(N'G\u00F3i 3 th\u00E1ng', 90, 1200000, N'G\u00F3i ti\u1EBFt ki\u1EC7m 3 th\u00E1ng'),
(N'G\u00F3i 6 th\u00E1ng', 180, 2000000, N'G\u00F3i \u01B0u \u0111\u00E3i 6 th\u00E1ng');
GO

-- =====================================================
-- Du lieu mau: DangKyGoi
-- =====================================================
INSERT INTO DangKyGoi (MaHV, MaGoi, NgayBatDau, NgayHetHan, GhiChu) VALUES
(1, 1, '2025-01-01', '2025-01-31', N'\u0110ang ho\u1EA1t \u0111\u1ED9ng', N'\u0110\u0103ng k\u00FD l\u1EA7n \u0111\u1EA7u'),
(2, 2, '2025-01-15', '2025-04-15', N'\u0110ang ho\u1EA1t \u0111\u1ED9ng', N'Kh\u00E1ch quen'),
(3, 3, '2024-12-01', '2025-05-31', N'\u0110ang ho\u1EA1t \u0111\u1ED9ng', N'G\u00F3i d\u00E0i h\u1EA1n');
GO

-- =====================================================
-- Du lieu mau: HoaDon
-- =====================================================
INSERT INTO HoaDon (MaDK, NgayThanhToan, SoTien, HinhThucTT, GhiChu) VALUES
(1, '2025-01-01', 500000, N'Ti\u1EC1n m\u1EB7t', N'Thanh to\u00E1n \u0111\u1EE7'),
(2, '2025-01-15', 1200000, N'Chuy\u1EC3n kho\u1EA3n', N'Thanh to\u00E1n \u0111\u1EE7'),
(3, '2024-12-01', 2000000, N'Ti\u1EC1n m\u1EB7t', N'Thanh to\u00E1n \u0111\u1EE7');
GO

-- =====================================================
-- Du lieu mau: PhanCong
-- =====================================================
INSERT INTO PhanCong (MaHLV, MaDK, MaCa, NgayBatDau, NgayKetThuc, GhiChu) VALUES
(1, 1, 1, '2025-01-01', '2025-01-31', N'PT Yoga bu\u1ED5i s\u00E1ng'),
(2, 2, 2, '2025-01-15', '2025-04-15', N'PT Cardio bu\u1ED5i chi\u1EC1u');
GO

PRINT N'=== Tao database GymManagementDB thanh cong! ===';
PRINT N'\u0110\u0103ng nh\u1EADp: admin / 123 ho\u1EB7c nhanvien / 123';
GO


