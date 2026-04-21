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
        }

        private static void ExecuteNonQuery(SqlConnection conn, string sql)
        {
            using var cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }
    }
}
