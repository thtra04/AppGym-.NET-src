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
    TrangThai NVARCHAR(50) DEFAULT N'Đang hoạt động',
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

-- Insert default admin account (password: 123)
DECLARE @salt UNIQUEIDENTIFIER = NEWID();
INSERT INTO TaiKhoan (TenDangNhap, Salt, MatKhauHash, HoTen, VaiTro, TrangThai)
VALUES (
    'admin',
    @salt,
    HASHBYTES('SHA2_512', CONVERT(varbinary(200), '123' + '|' + CONVERT(varchar(50), @salt))),
    N'Quản trị viên',
    N'Admin',
    1
);
GO

-- Insert sample CaLam
INSERT INTO CaLam (TenCa, GioBatDau, GioKetThuc) VALUES (N'Ca sáng', '06:00', '12:00');
INSERT INTO CaLam (TenCa, GioBatDau, GioKetThuc) VALUES (N'Ca chiều', '12:00', '18:00');
INSERT INTO CaLam (TenCa, GioBatDau, GioKetThuc) VALUES (N'Ca tối', '18:00', '22:00');
GO

