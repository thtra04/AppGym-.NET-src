using AppGym.Models;
using Microsoft.Data.SqlClient;

namespace AppGym.DataAccess
{
    public class TaiKhoanDAO
    {
        public TaiKhoan? Login(string username, string password)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                @"SELECT MaTK, TenDangNhap, HoTen, VaiTro, TrangThai, TaoLuc
                  FROM TaiKhoan
                  WHERE TenDangNhap = @user
                    AND MatKhauHash = HASHBYTES('SHA2_512', CONVERT(varbinary(200), CAST(@pwd AS varchar(100)) + '|' + CONVERT(varchar(50), CAST(Salt AS UNIQUEIDENTIFIER))))
                    AND TrangThai = 1", conn);
            cmd.Parameters.AddWithValue("@user", username);
            cmd.Parameters.AddWithValue("@pwd", password);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new TaiKhoan
                {
                    MaTK = reader.GetInt32(0),
                    TenDangNhap = reader.GetString(1),
                    HoTen = reader.IsDBNull(2) ? "" : reader.GetString(2),
                    VaiTro = reader.IsDBNull(3) ? "" : reader.GetString(3),
                    TrangThai = !reader.IsDBNull(4) && reader.GetBoolean(4),
                    TaoLuc = reader.IsDBNull(5) ? null : reader.GetDateTime(5)
                };
            }
            return null;
        }
    }
}
