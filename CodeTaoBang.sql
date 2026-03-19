/* =========================
   DATABASE
   ========================= */
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

/* =========================
   1) HOCVIEN
   ========================= */
CREATE TABLE dbo.HocVien
(
    MaHV        INT IDENTITY(1,1) PRIMARY KEY,
    HoTen       NVARCHAR(100) NULL,
    GioiTinh    NVARCHAR(10) NULL CHECK (GioiTinh IN (N'Nam', N'Nữ', N'Khác')),
    NgaySinh    DATE NULL,
    SDT         VARCHAR(20) NULL UNIQUE,
    Email       VARCHAR(100) NULL UNIQUE,
    NgayDangKy  DATE NULL DEFAULT (GETDATE()),
    TrangThai   BIT NULL DEFAULT (1)
);
GO

/* =========================
   2) HUANLUYENVIEN
   ========================= */
CREATE TABLE dbo.HuanLuyenVien
(
    MaHLV       INT IDENTITY(1,1) PRIMARY KEY,
    HoTen       NVARCHAR(100) NULL,
    GioiTinh    NVARCHAR(10) NULL CHECK (GioiTinh IN (N'Nam', N'Nữ', N'Khác')),
    SDT         VARCHAR(20) NULL UNIQUE,
    ChuyenMon   NVARCHAR(100) NULL,
    Luong       DECIMAL(18,2) NULL CHECK (Luong IS NULL OR Luong >= 0),
    TrangThai   BIT NULL DEFAULT (1)
);
GO

/* =========================
   3) CALAM
   ========================= */
CREATE TABLE dbo.CaLam
(
    MaCa        INT IDENTITY(1,1) PRIMARY KEY,
    TenCa       NVARCHAR(50) NULL UNIQUE,
    GioBatDau   TIME NULL,
    GioKetThuc  TIME NULL,
    CONSTRAINT CK_CaLam_Time CHECK (
        GioBatDau IS NULL OR GioKetThuc IS NULL OR GioKetThuc > GioBatDau
    )
);
GO

/* =========================
   4) GOITAP
   ========================= */
CREATE TABLE dbo.GoiTap
(
    MaGoi       INT IDENTITY(1,1) PRIMARY KEY,
    TenGoi      NVARCHAR(100) NULL UNIQUE,
    ThoiHan     INT NULL CHECK (ThoiHan IS NULL OR ThoiHan > 0),
    Gia         DECIMAL(18,2) NULL CHECK (Gia IS NULL OR Gia >= 0),
    MoTa        NVARCHAR(300) NULL,
    TrangThai   BIT NULL DEFAULT (1)
);
GO

/* =========================
   5) DANGKYGOI
   ========================= */
CREATE TABLE dbo.DangKyGoi
(
    MaDK        INT IDENTITY(1,1) PRIMARY KEY,
    MaHV        INT NOT NULL,
    MaGoi       INT NOT NULL,
    NgayBatDau  DATE NULL,
    NgayHetHan  DATE NULL,
    TrangThai   NVARCHAR(20) NULL DEFAULT(N'Đang hoạt động')
        CHECK (TrangThai IN (N'Đang hoạt động', N'Hết hạn', N'Tạm dừng', N'Hủy')),
    GhiChu      NVARCHAR(300) NULL,

    CONSTRAINT FK_DangKyGoi_HocVien FOREIGN KEY (MaHV) REFERENCES dbo.HocVien(MaHV),
    CONSTRAINT FK_DangKyGoi_GoiTap  FOREIGN KEY (MaGoi) REFERENCES dbo.GoiTap(MaGoi),
    CONSTRAINT CK_DangKyGoi_Date CHECK (
        NgayBatDau IS NULL OR NgayHetHan IS NULL OR NgayHetHan >= NgayBatDau
    )
);
GO

/* =========================
   6) PHANCONG
   ========================= */
CREATE TABLE dbo.PhanCong
(
    MaPC        INT IDENTITY(1,1) PRIMARY KEY,
    MaHLV       INT NOT NULL,
    MaDK        INT NOT NULL,
    MaCa        INT NULL,
    NgayBatDau  DATE NULL,
    NgayKetThuc DATE NULL,
    GhiChu      NVARCHAR(300) NULL,

    CONSTRAINT FK_PhanCong_HLV FOREIGN KEY (MaHLV) REFERENCES dbo.HuanLuyenVien(MaHLV),
    CONSTRAINT FK_PhanCong_DK  FOREIGN KEY (MaDK) REFERENCES dbo.DangKyGoi(MaDK),
    CONSTRAINT FK_PhanCong_Ca  FOREIGN KEY (MaCa) REFERENCES dbo.CaLam(MaCa),
    CONSTRAINT CK_PhanCong_Date CHECK (
        NgayBatDau IS NULL OR NgayKetThuc IS NULL OR NgayKetThuc >= NgayBatDau
    )
);
GO

/* =========================
   7) HOADON
   ========================= */
CREATE TABLE dbo.HoaDon
(
    MaHD            INT IDENTITY(1,1) PRIMARY KEY,
    MaDK            INT NOT NULL,
    NgayThanhToan   DATETIME NULL DEFAULT (GETDATE()),
    SoTien          DECIMAL(18,2) NULL CHECK (SoTien IS NULL OR SoTien > 0),
    HinhThucTT      NVARCHAR(30) NULL
        CHECK (HinhThucTT IN (N'Tiền mặt', N'Chuyển khoản', N'Thẻ', N'Khác')),
    GhiChu          NVARCHAR(300) NULL,

    CONSTRAINT FK_HoaDon_DangKyGoi FOREIGN KEY (MaDK) REFERENCES dbo.DangKyGoi(MaDK)
);
GO

/* =========================
   8) QUYEN
   ========================= */
CREATE TABLE dbo.Quyen
(
    MaQuyen     INT IDENTITY(1,1) PRIMARY KEY,
    TenBang     NVARCHAR(80) NULL,
    HanhDong    NVARCHAR(30) NULL,
    MoTa        NVARCHAR(200) NULL,
    CONSTRAINT UQ_Quyen UNIQUE (TenBang, HanhDong)
);
GO

/* =========================
   9) TAIKHOAN
   ========================= */
CREATE TABLE dbo.TaiKhoan
(
    MaTK        INT IDENTITY(1,1) PRIMARY KEY,
    TenDangNhap VARCHAR(50) NOT NULL UNIQUE,
    MatKhauHash VARBINARY(64) NOT NULL,
    Salt        VARBINARY(32) NOT NULL,
    HoTen       NVARCHAR(100) NULL,
    VaiTro      NVARCHAR(20) NULL DEFAULT(N'NhanVien')
        CHECK (VaiTro IN (N'Admin', N'NhanVien')),
    TrangThai   BIT NULL DEFAULT (1),
    TaoLuc      DATETIME NULL DEFAULT (GETDATE())
);
GO

/* =========================
   10) TAIKHOAN_QUYEN
   ========================= */
CREATE TABLE dbo.TaiKhoan_Quyen
(
    MaTK        INT NOT NULL,
    MaQuyen     INT NOT NULL,

    CONSTRAINT PK_TaiKhoan_Quyen PRIMARY KEY (MaTK, MaQuyen),
    CONSTRAINT FK_TKQ_TaiKhoan FOREIGN KEY (MaTK) REFERENCES dbo.TaiKhoan(MaTK),
    CONSTRAINT FK_TKQ_Quyen    FOREIGN KEY (MaQuyen) REFERENCES dbo.Quyen(MaQuyen)
);
GO
