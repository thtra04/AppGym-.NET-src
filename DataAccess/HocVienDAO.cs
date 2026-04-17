using AppGym.Models;
using Microsoft.Data.SqlClient;

namespace AppGym.DataAccess
{
    public class HocVienDAO
    {
        public List<HocVien> GetAll()
        {
            var list = new List<HocVien>();
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("SELECT * FROM HocVien ORDER BY MaHV DESC", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new HocVien
                {
                    MaHV = reader.GetInt32(reader.GetOrdinal("MaHV")),
                    HoTen = reader.IsDBNull(reader.GetOrdinal("HoTen")) ? "" : reader.GetString(reader.GetOrdinal("HoTen")),
                    GioiTinh = reader.IsDBNull(reader.GetOrdinal("GioiTinh")) ? "" : reader.GetString(reader.GetOrdinal("GioiTinh")),
                    NgaySinh = reader.IsDBNull(reader.GetOrdinal("NgaySinh")) ? null : reader.GetDateTime(reader.GetOrdinal("NgaySinh")),
                    SDT = reader.IsDBNull(reader.GetOrdinal("SDT")) ? "" : reader.GetString(reader.GetOrdinal("SDT")),
                    Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? "" : reader.GetString(reader.GetOrdinal("Email")),
                    NgayDangKy = reader.IsDBNull(reader.GetOrdinal("NgayDangKy")) ? null : reader.GetDateTime(reader.GetOrdinal("NgayDangKy"))
                });
            }
            return list;
        }

        public List<HocVien> Search(string keyword)
        {
            var list = new List<HocVien>();
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                "SELECT * FROM HocVien WHERE HoTen LIKE @kw OR SDT LIKE @kw OR Email LIKE @kw ORDER BY MaHV DESC", conn);
            cmd.Parameters.AddWithValue("@kw", $"%{keyword}%");
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new HocVien
                {
                    MaHV = reader.GetInt32(reader.GetOrdinal("MaHV")),
                    HoTen = reader.IsDBNull(reader.GetOrdinal("HoTen")) ? "" : reader.GetString(reader.GetOrdinal("HoTen")),
                    GioiTinh = reader.IsDBNull(reader.GetOrdinal("GioiTinh")) ? "" : reader.GetString(reader.GetOrdinal("GioiTinh")),
                    NgaySinh = reader.IsDBNull(reader.GetOrdinal("NgaySinh")) ? null : reader.GetDateTime(reader.GetOrdinal("NgaySinh")),
                    SDT = reader.IsDBNull(reader.GetOrdinal("SDT")) ? "" : reader.GetString(reader.GetOrdinal("SDT")),
                    Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? "" : reader.GetString(reader.GetOrdinal("Email")),
                    NgayDangKy = reader.IsDBNull(reader.GetOrdinal("NgayDangKy")) ? null : reader.GetDateTime(reader.GetOrdinal("NgayDangKy"))
                });
            }
            return list;
        }

        public bool Insert(HocVien hv)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                @"INSERT INTO HocVien (HoTen, GioiTinh, NgaySinh, SDT, Email, NgayDangKy)
                  VALUES (@HoTen, @GioiTinh, @NgaySinh, @SDT, @Email, @NgayDangKy)", conn);
            cmd.Parameters.AddWithValue("@HoTen", hv.HoTen);
            cmd.Parameters.AddWithValue("@GioiTinh", hv.GioiTinh);
            cmd.Parameters.AddWithValue("@NgaySinh", (object?)hv.NgaySinh ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@SDT", hv.SDT);
            cmd.Parameters.AddWithValue("@Email", hv.Email);
            cmd.Parameters.AddWithValue("@NgayDangKy", (object?)hv.NgayDangKy ?? DBNull.Value);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Update(HocVien hv)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                @"UPDATE HocVien SET HoTen=@HoTen, GioiTinh=@GioiTinh, NgaySinh=@NgaySinh,
                  SDT=@SDT, Email=@Email, NgayDangKy=@NgayDangKy
                  WHERE MaHV=@MaHV", conn);
            cmd.Parameters.AddWithValue("@MaHV", hv.MaHV);
            cmd.Parameters.AddWithValue("@HoTen", hv.HoTen);
            cmd.Parameters.AddWithValue("@GioiTinh", hv.GioiTinh);
            cmd.Parameters.AddWithValue("@NgaySinh", (object?)hv.NgaySinh ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@SDT", hv.SDT);
            cmd.Parameters.AddWithValue("@Email", hv.Email);
            cmd.Parameters.AddWithValue("@NgayDangKy", (object?)hv.NgayDangKy ?? DBNull.Value);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Delete(int maHV)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("DELETE FROM HocVien WHERE MaHV=@MaHV", conn);
            cmd.Parameters.AddWithValue("@MaHV", maHV);
            return cmd.ExecuteNonQuery() > 0;
        }

        public int Count()
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("SELECT COUNT(*) FROM HocVien", conn);
            return (int)cmd.ExecuteScalar();
        }
    }
}
