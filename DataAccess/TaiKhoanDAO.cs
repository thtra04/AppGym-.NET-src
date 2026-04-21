using AppGym.Models;
using Microsoft.Data.SqlClient;

namespace AppGym.DataAccess
{
    public class TaiKhoanDAO
    {
        private static TaiKhoan MapTaiKhoan(SqlDataReader reader)
        {
            return new TaiKhoan
            {
                MaTK = reader.GetInt32(reader.GetOrdinal("MaTK")),
                TenDangNhap = reader.IsDBNull(reader.GetOrdinal("TenDangNhap")) ? "" : reader.GetString(reader.GetOrdinal("TenDangNhap")),
                HoTen = reader.IsDBNull(reader.GetOrdinal("HoTen")) ? "" : reader.GetString(reader.GetOrdinal("HoTen")),
                VaiTro = reader.IsDBNull(reader.GetOrdinal("VaiTro")) ? "" : reader.GetString(reader.GetOrdinal("VaiTro")),
                HoatDong = reader.IsDBNull(reader.GetOrdinal("HoatDong")) || reader.GetBoolean(reader.GetOrdinal("HoatDong")),
                TaoLuc = reader.IsDBNull(reader.GetOrdinal("TaoLuc")) ? null : reader.GetDateTime(reader.GetOrdinal("TaoLuc"))
            };
        }

        public TaiKhoan? Login(string username, string password)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                @"SELECT MaTK, TenDangNhap, HoTen, VaiTro, HoatDong, TaoLuc
                  FROM TaiKhoan
                  WHERE TenDangNhap = @user
                    AND ISNULL(HoatDong, 1) = 1
                    AND MatKhauHash = HASHBYTES('SHA2_512', CONVERT(varbinary(200), CAST(@pwd AS varchar(100)) + '|' + CONVERT(varchar(50), CAST(Salt AS UNIQUEIDENTIFIER))))", conn);
            cmd.Parameters.AddWithValue("@user", username);
            cmd.Parameters.AddWithValue("@pwd", password);
            using var reader = cmd.ExecuteReader();
            return reader.Read() ? MapTaiKhoan(reader) : null;
        }

        public TaiKhoan? GetByUsername(string username)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                @"SELECT MaTK, TenDangNhap, HoTen, VaiTro, HoatDong, TaoLuc
                  FROM TaiKhoan
                  WHERE TenDangNhap = @user", conn);
            cmd.Parameters.AddWithValue("@user", username);
            using var reader = cmd.ExecuteReader();
            return reader.Read() ? MapTaiKhoan(reader) : null;
        }

        public TaiKhoan? GetById(int maTK)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                @"SELECT MaTK, TenDangNhap, HoTen, VaiTro, HoatDong, TaoLuc
                  FROM TaiKhoan
                  WHERE MaTK = @maTK", conn);
            cmd.Parameters.AddWithValue("@maTK", maTK);
            using var reader = cmd.ExecuteReader();
            return reader.Read() ? MapTaiKhoan(reader) : null;
        }

        public List<TaiKhoan> GetAll()
        {
            var list = new List<TaiKhoan>();
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                @"SELECT MaTK, TenDangNhap, HoTen, VaiTro, HoatDong, TaoLuc
                  FROM TaiKhoan
                  ORDER BY ISNULL(HoatDong, 1) DESC,
                           CASE WHEN VaiTro = N'Admin' THEN 0 ELSE 1 END,
                           MaTK", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(MapTaiKhoan(reader));
            }
            return list;
        }

        public List<TaiKhoan> Search(string keyword)
        {
            var list = new List<TaiKhoan>();
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                @"SELECT MaTK, TenDangNhap, HoTen, VaiTro, HoatDong, TaoLuc
                  FROM TaiKhoan
                  WHERE CAST(MaTK AS NVARCHAR(20)) LIKE @kw
                     OR TenDangNhap LIKE @kw
                     OR ISNULL(HoTen, N'') LIKE @kw
                     OR VaiTro LIKE @kw
                     OR CASE WHEN ISNULL(HoatDong, 1) = 1 THEN N'Đang hoạt động' ELSE N'Tạm khóa' END LIKE @kw
                  ORDER BY ISNULL(HoatDong, 1) DESC,
                           CASE WHEN VaiTro = N'Admin' THEN 0 ELSE 1 END,
                           MaTK", conn);
            cmd.Parameters.AddWithValue("@kw", $"%{keyword}%");
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(MapTaiKhoan(reader));
            }
            return list;
        }

        public bool Insert(TaiKhoan taiKhoan, string password)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                @"DECLARE @salt UNIQUEIDENTIFIER = NEWID();
                  INSERT INTO TaiKhoan (TenDangNhap, Salt, MatKhauHash, HoTen, VaiTro, HoatDong)
                  VALUES (
                      @TenDangNhap,
                      @salt,
                      HASHBYTES('SHA2_512', CONVERT(varbinary(200), CAST(@pwd AS varchar(100)) + '|' + CONVERT(varchar(50), @salt))),
                      @HoTen,
                      @VaiTro,
                      @HoatDong)", conn);
            cmd.Parameters.AddWithValue("@TenDangNhap", taiKhoan.TenDangNhap);
            cmd.Parameters.AddWithValue("@HoTen", string.IsNullOrWhiteSpace(taiKhoan.HoTen) ? DBNull.Value : taiKhoan.HoTen);
            cmd.Parameters.AddWithValue("@VaiTro", taiKhoan.VaiTro);
            cmd.Parameters.AddWithValue("@HoatDong", taiKhoan.HoatDong);
            cmd.Parameters.AddWithValue("@pwd", password);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Update(TaiKhoan taiKhoan)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                @"UPDATE TaiKhoan
                  SET TenDangNhap = @TenDangNhap,
                      HoTen = @HoTen,
                      VaiTro = @VaiTro,
                      HoatDong = @HoatDong
                  WHERE MaTK = @MaTK", conn);
            cmd.Parameters.AddWithValue("@MaTK", taiKhoan.MaTK);
            cmd.Parameters.AddWithValue("@TenDangNhap", taiKhoan.TenDangNhap);
            cmd.Parameters.AddWithValue("@HoTen", string.IsNullOrWhiteSpace(taiKhoan.HoTen) ? DBNull.Value : taiKhoan.HoTen);
            cmd.Parameters.AddWithValue("@VaiTro", taiKhoan.VaiTro);
            cmd.Parameters.AddWithValue("@HoatDong", taiKhoan.HoatDong);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Delete(int maTK)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("DELETE FROM TaiKhoan WHERE MaTK = @maTK", conn);
            cmd.Parameters.AddWithValue("@maTK", maTK);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool HasOtherAdmin(int excludedMaTK)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                @"SELECT COUNT(*)
                  FROM TaiKhoan
                  WHERE VaiTro = N'Admin' AND MaTK <> @maTK", conn);
            cmd.Parameters.AddWithValue("@maTK", excludedMaTK);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        public bool HasOtherActiveAdmin(int excludedMaTK)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                @"SELECT COUNT(*)
                  FROM TaiKhoan
                  WHERE VaiTro = N'Admin'
                    AND ISNULL(HoatDong, 1) = 1
                    AND MaTK <> @maTK", conn);
            cmd.Parameters.AddWithValue("@maTK", excludedMaTK);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        public bool HasLinkedOperations(int maTK)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                @"SELECT
                      (SELECT COUNT(*) FROM DangKyGoi WHERE MaNguoiLap = @maTK)
                    + (SELECT COUNT(*) FROM HoaDon WHERE MaNguoiLap = @maTK OR MaNguoiThanhToan = @maTK)", conn);
            cmd.Parameters.AddWithValue("@maTK", maTK);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        public void ChangePassword(int maTK, string newPassword)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                @"UPDATE TaiKhoan SET MatKhauHash = HASHBYTES('SHA2_512',
                  CONVERT(varbinary(200), CAST(@pwd AS varchar(100)) + '|' + CONVERT(varchar(50), CAST(Salt AS UNIQUEIDENTIFIER))))
                  WHERE MaTK = @maTK", conn);
            cmd.Parameters.AddWithValue("@maTK", maTK);
            cmd.Parameters.AddWithValue("@pwd", newPassword);
            cmd.ExecuteNonQuery();
        }
    }
}
