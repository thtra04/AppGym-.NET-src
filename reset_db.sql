-- =====================================================
-- RESET: Xóa database c? (n?u có) và t?o l?i t? ??u
-- Ch?y script này trong SSMS v?i server: DESKTOP-05A1RCH\THANHTRA
-- =====================================================

USE master;
GO

-- Xóa DB c? n?u t?n t?i
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

CREATE TABLE TaiKhoan (
    MaTK INT IDENTITY(1,1) PRIMARY KEY,
    TenDangNhap NVARCHAR(50) NOT NULL UNIQUE,
    MatKhauHash VARBINARY(MAX),
    Salt UNIQUEIDENTIFIER DEFAULT NEWID(),
    HoTen NVARCHAR(100),
    VaiTro NVARCHAR(50) DEFAULT N'NhanVien',
    TrangThai BIT DEFAULT 1,
    TaoLuc DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE HocVien (
    MaHV INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    GioiTinh NVARCHAR(10),
    NgaySinh DATE,
    SDT NVARCHAR(20),
    Email NVARCHAR(100),
    NgayDangKy DATE DEFAULT GETDATE(),
    TrangThai BIT DEFAULT 1
);
GO

CREATE TABLE HuanLuyenVien (
    MaHLV INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    GioiTinh NVARCHAR(10),
    SDT NVARCHAR(20),
    ChuyenMon NVARCHAR(100),
    Luong DECIMAL(18,0),
    TrangThai BIT DEFAULT 1
);
GO

CREATE TABLE GoiTap (
    MaGoi INT IDENTITY(1,1) PRIMARY KEY,
    TenGoi NVARCHAR(100) NOT NULL,
    ThoiHan INT,
    Gia DECIMAL(18,0),
    MoTa NVARCHAR(500),
    TrangThai BIT DEFAULT 1
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
    TrangThai NVARCHAR(50) DEFAULT N'?ang ho?t ??ng',
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
    SoTien DECIMAL(18,0),
    HinhThucTT NVARCHAR(50),
    GhiChu NVARCHAR(500)
);
GO

-- =====================================================
-- T?o tài kho?n admin (m?t kh?u: 123)
-- =====================================================
DECLARE @salt UNIQUEIDENTIFIER = NEWID();
INSERT INTO TaiKhoan (TenDangNhap, Salt, MatKhauHash, HoTen, VaiTro, TrangThai)
VALUES (
    'admin',
    @salt,
    HASHBYTES('SHA2_512', CONVERT(varbinary(200), '123' + '|' + CONVERT(varchar(50), @salt))),
    N'Qu?n tr? viên',
    N'Admin',
    1
);
GO

-- =====================================================
-- Ca làm m?u
-- =====================================================
INSERT INTO CaLam (TenCa, GioBatDau, GioKetThuc) VALUES (N'Ca sáng', '06:00', '12:00');
INSERT INTO CaLam (TenCa, GioBatDau, GioKetThuc) VALUES (N'Ca chi?u', '12:00', '18:00');
INSERT INTO CaLam (TenCa, GioBatDau, GioKetThuc) VALUES (N'Ca t?i', '18:00', '22:00');
GO

-- =====================================================
-- D? li?u m?u: HocVien
-- =====================================================
INSERT INTO HocVien (HoTen, GioiTinh, NgaySinh, SDT, Email, NgayDangKy, TrangThai) VALUES
(N'Nguy?n V?n An', N'Nam', '1995-03-15', '0901234567', 'an.nguyen@gmail.com', GETDATE(), 1),
(N'Tr?n Th? Bình', N'N?', '1998-07-22', '0912345678', 'binh.tran@gmail.com', GETDATE(), 1),
(N'Lê Minh C??ng', N'Nam', '1993-11-10', '0923456789', 'cuong.le@gmail.com', GETDATE(), 1);
GO

-- =====================================================
-- D? li?u m?u: HuanLuyenVien
-- =====================================================
INSERT INTO HuanLuyenVien (HoTen, GioiTinh, SDT, ChuyenMon, Luong, TrangThai) VALUES
(N'Ph?m V?n D?ng', N'Nam', '0934567890', N'Yoga', 8000000, 1),
(N'Hoàng Th? Em', N'N?', '0945678901', N'Cardio', 7500000, 1);
GO

-- =====================================================
-- D? li?u m?u: GoiTap
-- =====================================================
INSERT INTO GoiTap (TenGoi, ThoiHan, Gia, MoTa, TrangThai) VALUES
(N'Gói 1 tháng', 30, 500000, N'Gói c? b?n 1 tháng', 1),
(N'Gói 3 tháng', 90, 1200000, N'Gói ti?t ki?m 3 tháng', 1),
(N'Gói 6 tháng', 180, 2000000, N'Gói ?u ?ãi 6 tháng', 1);
GO

-- =====================================================
-- D? li?u m?u: DangKyGoi
-- =====================================================
INSERT INTO DangKyGoi (MaHV, MaGoi, NgayBatDau, NgayHetHan, TrangThai, GhiChu) VALUES
(1, 1, '2025-01-01', '2025-01-31', N'?ang ho?t ??ng', N'??ng ký l?n ??u'),
(2, 2, '2025-01-15', '2025-04-15', N'?ang ho?t ??ng', N'Khách quen'),
(3, 3, '2024-12-01', '2025-05-31', N'?ang ho?t ??ng', N'Gói dài h?n');
GO

-- =====================================================
-- D? li?u m?u: HoaDon
-- =====================================================
INSERT INTO HoaDon (MaDK, NgayThanhToan, SoTien, HinhThucTT, GhiChu) VALUES
(1, '2025-01-01', 500000, N'Ti?n m?t', N'Thanh toán ??'),
(2, '2025-01-15', 1200000, N'Chuy?n kho?n', N'Thanh toán ??'),
(3, '2024-12-01', 2000000, N'Ti?n m?t', N'Thanh toán ??');
GO

-- =====================================================
-- D? li?u m?u: PhanCong
-- =====================================================
INSERT INTO PhanCong (MaHLV, MaDK, MaCa, NgayBatDau, NgayKetThuc, GhiChu) VALUES
(1, 1, 1, '2025-01-01', '2025-01-31', N'PT Yoga bu?i sáng'),
(2, 2, 2, '2025-01-15', '2025-04-15', N'PT Cardio bu?i chi?u');
GO

PRINT N'=== T?o database GymManagementDB thành công! ===';
PRINT N'??ng nh?p: admin / 123';
GO
