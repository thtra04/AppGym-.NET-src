using Microsoft.Data.SqlClient;

namespace AppGym.DataAccess
{
    public static class DatabaseSchemaSynchronizer
    {
        public static void EnsureSchemaUpToDate()
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();

            ExecuteNonQuery(conn,
                """
                IF OBJECT_ID(N'TaiKhoan', N'U') IS NULL
                    RETURN;
                """);

            ExecuteNonQuery(conn,
                """
                IF COL_LENGTH('TaiKhoan', 'HoatDong') IS NULL
                    ALTER TABLE TaiKhoan ADD HoatDong BIT NULL;
                """);

            ExecuteNonQuery(conn,
                """
                IF COL_LENGTH('DangKyGoi', 'MaNguoiLap') IS NULL
                    ALTER TABLE DangKyGoi ADD MaNguoiLap INT NULL;
                """);

            ExecuteNonQuery(conn,
                """
                IF COL_LENGTH('HoaDon', 'MaNguoiLap') IS NULL
                    ALTER TABLE HoaDon ADD MaNguoiLap INT NULL;
                """);

            ExecuteNonQuery(conn,
                """
                IF COL_LENGTH('HoaDon', 'MaNguoiThanhToan') IS NULL
                    ALTER TABLE HoaDon ADD MaNguoiThanhToan INT NULL;
                """);

            ExecuteNonQuery(conn,
                """
                IF NOT EXISTS (
                    SELECT 1
                    FROM sys.default_constraints dc
                    INNER JOIN sys.columns c
                        ON c.object_id = dc.parent_object_id
                       AND c.column_id = dc.parent_column_id
                    WHERE dc.parent_object_id = OBJECT_ID(N'TaiKhoan')
                      AND c.name = N'HoatDong')
                BEGIN
                    ALTER TABLE TaiKhoan
                    ADD CONSTRAINT DF_TaiKhoan_HoatDong DEFAULT ((1)) FOR HoatDong;
                END;
                """);

            ExecuteNonQuery(conn,
                """
                UPDATE TaiKhoan
                SET HoatDong = 1
                WHERE HoatDong IS NULL;
                """);

            ExecuteNonQuery(conn,
                """
                DECLARE @DefaultMaTK INT = (
                    SELECT TOP 1 MaTK
                    FROM TaiKhoan
                    ORDER BY CASE WHEN TenDangNhap = 'admin' THEN 0 ELSE 1 END, MaTK
                );

                IF @DefaultMaTK IS NOT NULL
                BEGIN
                    UPDATE DangKyGoi
                    SET MaNguoiLap = @DefaultMaTK
                    WHERE MaNguoiLap IS NULL;

                    UPDATE hd
                    SET MaNguoiLap = COALESCE(hd.MaNguoiLap, dk.MaNguoiLap, @DefaultMaTK),
                        MaNguoiThanhToan = COALESCE(hd.MaNguoiThanhToan, hd.MaNguoiLap, dk.MaNguoiLap, @DefaultMaTK)
                    FROM HoaDon hd
                    LEFT JOIN DangKyGoi dk ON dk.MaDK = hd.MaDK
                    WHERE hd.MaNguoiLap IS NULL OR hd.MaNguoiThanhToan IS NULL;
                END;
                """);

            ExecuteNonQuery(conn,
                """
                IF NOT EXISTS (
                    SELECT 1
                    FROM sys.foreign_key_columns fkc
                    INNER JOIN sys.columns c
                        ON c.object_id = fkc.parent_object_id
                       AND c.column_id = fkc.parent_column_id
                    WHERE fkc.parent_object_id = OBJECT_ID(N'DangKyGoi')
                      AND c.name = N'MaNguoiLap')
                BEGIN
                    ALTER TABLE DangKyGoi
                    WITH CHECK ADD CONSTRAINT FK_DangKyGoi_TaiKhoan_MaNguoiLap
                    FOREIGN KEY (MaNguoiLap) REFERENCES TaiKhoan(MaTK);
                END;
                """);

            ExecuteNonQuery(conn,
                """
                IF NOT EXISTS (
                    SELECT 1
                    FROM sys.foreign_key_columns fkc
                    INNER JOIN sys.columns c
                        ON c.object_id = fkc.parent_object_id
                       AND c.column_id = fkc.parent_column_id
                    WHERE fkc.parent_object_id = OBJECT_ID(N'HoaDon')
                      AND c.name = N'MaNguoiLap')
                BEGIN
                    ALTER TABLE HoaDon
                    WITH CHECK ADD CONSTRAINT FK_HoaDon_TaiKhoan_MaNguoiLap
                    FOREIGN KEY (MaNguoiLap) REFERENCES TaiKhoan(MaTK);
                END;
                """);

            ExecuteNonQuery(conn,
                """
                IF NOT EXISTS (
                    SELECT 1
                    FROM sys.foreign_key_columns fkc
                    INNER JOIN sys.columns c
                        ON c.object_id = fkc.parent_object_id
                       AND c.column_id = fkc.parent_column_id
                    WHERE fkc.parent_object_id = OBJECT_ID(N'HoaDon')
                      AND c.name = N'MaNguoiThanhToan')
                BEGIN
                    ALTER TABLE HoaDon
                    WITH CHECK ADD CONSTRAINT FK_HoaDon_TaiKhoan_MaNguoiThanhToan
                    FOREIGN KEY (MaNguoiThanhToan) REFERENCES TaiKhoan(MaTK);
                END;
                """);

            ExecuteNonQuery(conn,
                """
                UPDATE HoaDon
                SET HinhThucTT = N'Tiền mặt'
                WHERE HinhThucTT IN (N'Tiá»n máº·t', N'TiÃ¡Â»Ân mÃ¡ÂºÂ·t');

                UPDATE HoaDon
                SET HinhThucTT = N'Chuyển khoản'
                WHERE HinhThucTT IN (N'Chuyá»ƒn khoáº£n', N'ChuyÃ¡Â»Æ’n khoÃ¡ÂºÂ£n');

                UPDATE HoaDon
                SET HinhThucTT = N'Thẻ'
                WHERE HinhThucTT IN (N'Tháº»', N'ThÃ¡ÂºÂ»');
                """);

            // === Permission system ===
            // Allow new role 'QuanLy' on the VaiTro CHECK constraint.
            ExecuteNonQuery(conn,
                """
                DECLARE @ck SYSNAME = (
                    SELECT TOP 1 cc.name
                    FROM sys.check_constraints cc
                    INNER JOIN sys.columns c
                        ON c.object_id = cc.parent_object_id
                       AND c.column_id = cc.parent_column_id
                    WHERE cc.parent_object_id = OBJECT_ID(N'TaiKhoan')
                      AND c.name = N'VaiTro'
                );
                IF @ck IS NOT NULL
                BEGIN
                    EXEC('ALTER TABLE TaiKhoan DROP CONSTRAINT ' + @ck);
                END;
                ALTER TABLE TaiKhoan
                    ADD CONSTRAINT CK_TaiKhoan_VaiTro
                    CHECK (VaiTro IN (N'Admin', N'QuanLy', N'NhanVien'));
                """);

            // Create QuyenTaiKhoan table for granular per-user CRUD permissions.
            ExecuteNonQuery(conn,
                """
                IF OBJECT_ID(N'QuyenTaiKhoan', N'U') IS NULL
                BEGIN
                    CREATE TABLE dbo.QuyenTaiKhoan (
                        MaTK INT NOT NULL,
                        Module NVARCHAR(50) NOT NULL,
                        CanView BIT NOT NULL DEFAULT 0,
                        CanAdd BIT NOT NULL DEFAULT 0,
                        CanEdit BIT NOT NULL DEFAULT 0,
                        CanDelete BIT NOT NULL DEFAULT 0,
                        CONSTRAINT PK_QuyenTaiKhoan PRIMARY KEY (MaTK, Module),
                        CONSTRAINT FK_QuyenTaiKhoan_TaiKhoan FOREIGN KEY (MaTK)
                            REFERENCES TaiKhoan(MaTK) ON DELETE CASCADE
                    );
                END;
                """);

            // Enforce one PT per DangKyGoi (1 HV - 1 gói chỉ phân công 1 PT).
            ExecuteNonQuery(conn,
                """
                IF NOT EXISTS (
                    SELECT 1 FROM sys.indexes
                    WHERE name = N'UX_PhanCong_MaDK' AND object_id = OBJECT_ID(N'PhanCong'))
                BEGIN
                    -- Remove duplicates first (keep newest MaPC)
                    ;WITH ranked AS (
                        SELECT MaPC, ROW_NUMBER() OVER (PARTITION BY MaDK ORDER BY MaPC DESC) AS rn
                        FROM PhanCong
                    )
                    DELETE FROM PhanCong WHERE MaPC IN (SELECT MaPC FROM ranked WHERE rn > 1);

                    CREATE UNIQUE INDEX UX_PhanCong_MaDK ON PhanCong(MaDK);
                END;
                """);
        }

        private static void ExecuteNonQuery(SqlConnection conn, string sql)
        {
            using var cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }
    }
}
