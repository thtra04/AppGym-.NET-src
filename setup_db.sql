/* =========================================================================
   AppGym - Setup database script
   - Tạo database GymManagementDB nếu chưa có
   - Tạo toàn bộ bảng + khóa ngoại + ràng buộc cần thiết cho ứng dụng
   - Seed sẵn 1 tài khoản admin (TenDangNhap=admin / MatKhau=admin)
   - Seed một vài Ca làm mặc định
   Cách chạy:
     sqlcmd -S (local)\SQLEXPRESS -E -i setup_db.sql
   Hoặc mở trong SSMS rồi Execute.
   ========================================================================= */

IF DB_ID(N'GymManagementDB') IS NULL
BEGIN
    CREATE DATABASE GymManagementDB;
END
GO

USE GymManagementDB;
GO

SET NOCOUNT ON;
GO

/* ---------- TaiKhoan ---------- */
IF OBJECT_ID(N'dbo.TaiKhoan', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.TaiKhoan (
        MaTK            INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        TenDangNhap     NVARCHAR(50)   NOT NULL UNIQUE,
        Salt            UNIQUEIDENTIFIER NOT NULL CONSTRAINT DF_TaiKhoan_Salt DEFAULT (NEWID()),
        MatKhauHash     VARBINARY(64)  NOT NULL,
        HoTen           NVARCHAR(100)  NULL,
        VaiTro          NVARCHAR(20)   NOT NULL CONSTRAINT DF_TaiKhoan_VaiTro DEFAULT (N'NhanVien'),
        HoatDong        BIT            NOT NULL CONSTRAINT DF_TaiKhoan_HoatDong DEFAULT (1),
        TaoLuc          DATETIME       NOT NULL CONSTRAINT DF_TaiKhoan_TaoLuc DEFAULT (GETDATE()),
        CONSTRAINT CK_TaiKhoan_VaiTro CHECK (VaiTro IN (N'Admin', N'QuanLy', N'NhanVien'))
    );
END
GO

/* ---------- HocVien ---------- */
IF OBJECT_ID(N'dbo.HocVien', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.HocVien (
        MaHV         INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        HoTen        NVARCHAR(100) NOT NULL,
        GioiTinh     NVARCHAR(10)  NULL,
        NgaySinh     DATE          NULL,
        SDT          NVARCHAR(20)  NULL,
        Email        NVARCHAR(100) NULL,
        NgayDangKy   DATE          NULL CONSTRAINT DF_HocVien_NgayDangKy DEFAULT (CAST(GETDATE() AS DATE))
    );
END
GO

/* ---------- HuanLuyenVien ---------- */
IF OBJECT_ID(N'dbo.HuanLuyenVien', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.HuanLuyenVien (
        MaHLV       INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        HoTen       NVARCHAR(100) NOT NULL,
        GioiTinh    NVARCHAR(10)  NULL,
        SDT         NVARCHAR(20)  NULL,
        ChuyenMon   NVARCHAR(100) NULL,
        Luong       DECIMAL(18,2) NULL
    );
END
GO

/* ---------- GoiTap ---------- */
IF OBJECT_ID(N'dbo.GoiTap', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.GoiTap (
        MaGoi    INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        TenGoi   NVARCHAR(100) NOT NULL,
        ThoiHan  INT           NULL,           -- số ngày
        Gia      DECIMAL(18,2) NULL,
        MoTa     NVARCHAR(500) NULL
    );
END
GO

/* ---------- CaLam ---------- */
IF OBJECT_ID(N'dbo.CaLam', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.CaLam (
        MaCa        INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        TenCa       NVARCHAR(50) NOT NULL,
        GioBatDau   TIME          NULL,
        GioKetThuc  TIME          NULL
    );
END
GO

/* ---------- DangKyGoi ---------- */
IF OBJECT_ID(N'dbo.DangKyGoi', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.DangKyGoi (
        MaDK         INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        MaHV         INT NOT NULL,
        MaGoi        INT NOT NULL,
        NgayBatDau   DATE NULL,
        NgayHetHan   DATE NULL,
        GhiChu       NVARCHAR(500) NULL,
        MaNguoiLap   INT NULL,
        CONSTRAINT FK_DangKyGoi_HocVien      FOREIGN KEY (MaHV)       REFERENCES dbo.HocVien(MaHV),
        CONSTRAINT FK_DangKyGoi_GoiTap       FOREIGN KEY (MaGoi)      REFERENCES dbo.GoiTap(MaGoi),
        CONSTRAINT FK_DangKyGoi_TaiKhoan_MaNguoiLap
            FOREIGN KEY (MaNguoiLap) REFERENCES dbo.TaiKhoan(MaTK)
    );
END
GO

/* ---------- HoaDon ---------- */
IF OBJECT_ID(N'dbo.HoaDon', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.HoaDon (
        MaHD              INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        MaDK              INT NOT NULL,
        NgayThanhToan     DATETIME      NULL CONSTRAINT DF_HoaDon_NgayThanhToan DEFAULT (GETDATE()),
        SoTien            DECIMAL(18,2) NULL,
        HinhThucTT        NVARCHAR(50)  NULL,
        GhiChu            NVARCHAR(500) NULL,
        MaNguoiLap        INT NULL,
        MaNguoiThanhToan  INT NULL,
        CONSTRAINT FK_HoaDon_DangKyGoi
            FOREIGN KEY (MaDK) REFERENCES dbo.DangKyGoi(MaDK),
        CONSTRAINT FK_HoaDon_TaiKhoan_MaNguoiLap
            FOREIGN KEY (MaNguoiLap) REFERENCES dbo.TaiKhoan(MaTK),
        CONSTRAINT FK_HoaDon_TaiKhoan_MaNguoiThanhToan
            FOREIGN KEY (MaNguoiThanhToan) REFERENCES dbo.TaiKhoan(MaTK)
    );
END
GO

/* ---------- PhanCong (1 đăng ký - tối đa 1 PT) ---------- */
IF OBJECT_ID(N'dbo.PhanCong', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.PhanCong (
        MaPC         INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        MaHLV        INT NOT NULL,
        MaDK         INT NOT NULL,
        MaCa         INT NULL,
        NgayBatDau   DATE NULL,
        NgayKetThuc  DATE NULL,
        GhiChu       NVARCHAR(500) NULL,
        CONSTRAINT FK_PhanCong_HuanLuyenVien FOREIGN KEY (MaHLV) REFERENCES dbo.HuanLuyenVien(MaHLV),
        CONSTRAINT FK_PhanCong_DangKyGoi     FOREIGN KEY (MaDK)  REFERENCES dbo.DangKyGoi(MaDK),
        CONSTRAINT FK_PhanCong_CaLam         FOREIGN KEY (MaCa)  REFERENCES dbo.CaLam(MaCa)
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'UX_PhanCong_MaDK' AND object_id = OBJECT_ID(N'dbo.PhanCong'))
    CREATE UNIQUE INDEX UX_PhanCong_MaDK ON dbo.PhanCong(MaDK);
GO

/* ---------- QuyenTaiKhoan (per-user CRUD permissions) ---------- */
IF OBJECT_ID(N'dbo.QuyenTaiKhoan', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.QuyenTaiKhoan (
        MaTK       INT NOT NULL,
        Module     NVARCHAR(50) NOT NULL,
        CanView    BIT NOT NULL CONSTRAINT DF_Quyen_CanView   DEFAULT (0),
        CanAdd     BIT NOT NULL CONSTRAINT DF_Quyen_CanAdd    DEFAULT (0),
        CanEdit    BIT NOT NULL CONSTRAINT DF_Quyen_CanEdit   DEFAULT (0),
        CanDelete  BIT NOT NULL CONSTRAINT DF_Quyen_CanDelete DEFAULT (0),
        CONSTRAINT PK_QuyenTaiKhoan PRIMARY KEY (MaTK, Module),
        CONSTRAINT FK_QuyenTaiKhoan_TaiKhoan FOREIGN KEY (MaTK)
            REFERENCES dbo.TaiKhoan(MaTK) ON DELETE CASCADE
    );
END
GO

/* =========================================================================
   SEED DATA
   ========================================================================= */

/* Tài khoản admin mặc định: admin / admin */
IF NOT EXISTS (SELECT 1 FROM dbo.TaiKhoan WHERE TenDangNhap = N'admin')
BEGIN
    DECLARE @salt UNIQUEIDENTIFIER = NEWID();
    INSERT INTO dbo.TaiKhoan (TenDangNhap, Salt, MatKhauHash, HoTen, VaiTro, HoatDong)
    VALUES (
        N'admin',
        @salt,
        HASHBYTES('SHA2_512',
            CONVERT(varbinary(200),
                CAST(N'admin' AS varchar(100)) + '|' + CONVERT(varchar(50), @salt))),
        N'Quản trị hệ thống',
        N'Admin',
        1
    );
END
GO

/* Ca làm mặc định */
IF NOT EXISTS (SELECT 1 FROM dbo.CaLam)
BEGIN
    INSERT INTO dbo.CaLam (TenCa, GioBatDau, GioKetThuc) VALUES
        (N'Ca Sáng 1',  '06:00', '09:00'),
        (N'Ca Sáng 2',  '09:00', '12:00'),
        (N'Ca Chiều 1', '14:00', '17:00'),
        (N'Ca Chiều 2', '17:00', '20:00'),
        (N'Ca Tối',     '20:00', '22:00');
END
GO

/* Gói tập mẫu */
IF NOT EXISTS (SELECT 1 FROM dbo.GoiTap)
BEGIN
    INSERT INTO dbo.GoiTap (TenGoi, ThoiHan, Gia, MoTa) VALUES
        (N'Gói 1 tháng',  30,   500000, N'Tập tự do trong 1 tháng'),
        (N'Gói 3 tháng',  90,  1350000, N'Tập tự do trong 3 tháng'),
        (N'Gói 6 tháng', 180,  2400000, N'Tập tự do trong 6 tháng'),
        (N'Gói 1 năm',   365,  4200000, N'Tập tự do trong 1 năm'),
        (N'Gói PT 10 buổi', 60, 3000000, N'Kèm PT riêng 10 buổi');
END
GO

PRINT N'AppGym: setup_db.sql hoàn tất. Đăng nhập mặc định: admin / admin';
GO
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
    VaiTro NVARCHAR(20) DEFAULT N'NhanVien' CHECK (VaiTro IN (N'Admin', N'QuanLy', N'NhanVien')),
    HoatDong BIT NOT NULL DEFAULT 1,
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
    MaNguoiLap INT NULL FOREIGN KEY REFERENCES TaiKhoan(MaTK),
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
    GhiChu NVARCHAR(500),
    MaNguoiLap INT NULL FOREIGN KEY REFERENCES TaiKhoan(MaTK),
    MaNguoiThanhToan INT NULL FOREIGN KEY REFERENCES TaiKhoan(MaTK)
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

