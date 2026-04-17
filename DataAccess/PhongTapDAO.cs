using AppGym.Models;
using Microsoft.Data.SqlClient;

namespace AppGym.DataAccess
{
    public class PhongTapDAO
    {
        public List<PhongTap> GetAll()
        {
            var list = new List<PhongTap>();
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("SELECT * FROM PhongTap ORDER BY MaPhong DESC", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new PhongTap
                {
                    MaPhong = reader.GetInt32(reader.GetOrdinal("MaPhong")),
                    TenPhong = reader.IsDBNull(reader.GetOrdinal("TenPhong")) ? "" : reader.GetString(reader.GetOrdinal("TenPhong")),
                    DiaChi = reader.IsDBNull(reader.GetOrdinal("DiaChi")) ? "" : reader.GetString(reader.GetOrdinal("DiaChi")),
                    SucChua = reader.IsDBNull(reader.GetOrdinal("SucChua")) ? null : reader.GetInt32(reader.GetOrdinal("SucChua")),
                    MoTa = reader.IsDBNull(reader.GetOrdinal("MoTa")) ? "" : reader.GetString(reader.GetOrdinal("MoTa"))
                });
            }
            return list;
        }

        public List<PhongTap> Search(string keyword)
        {
            var list = new List<PhongTap>();
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                "SELECT * FROM PhongTap WHERE TenPhong LIKE @kw OR DiaChi LIKE @kw ORDER BY MaPhong DESC", conn);
            cmd.Parameters.AddWithValue("@kw", $"%{keyword}%");
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new PhongTap
                {
                    MaPhong = reader.GetInt32(reader.GetOrdinal("MaPhong")),
                    TenPhong = reader.IsDBNull(reader.GetOrdinal("TenPhong")) ? "" : reader.GetString(reader.GetOrdinal("TenPhong")),
                    DiaChi = reader.IsDBNull(reader.GetOrdinal("DiaChi")) ? "" : reader.GetString(reader.GetOrdinal("DiaChi")),
                    SucChua = reader.IsDBNull(reader.GetOrdinal("SucChua")) ? null : reader.GetInt32(reader.GetOrdinal("SucChua")),
                    MoTa = reader.IsDBNull(reader.GetOrdinal("MoTa")) ? "" : reader.GetString(reader.GetOrdinal("MoTa"))
                });
            }
            return list;
        }

        public bool Insert(PhongTap pt)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                @"INSERT INTO PhongTap (TenPhong, DiaChi, SucChua, MoTa)
                  VALUES (@TenPhong, @DiaChi, @SucChua, @MoTa)", conn);
            cmd.Parameters.AddWithValue("@TenPhong", pt.TenPhong);
            cmd.Parameters.AddWithValue("@DiaChi", pt.DiaChi);
            cmd.Parameters.AddWithValue("@SucChua", (object?)pt.SucChua ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MoTa", pt.MoTa);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Update(PhongTap pt)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                @"UPDATE PhongTap SET TenPhong=@TenPhong, DiaChi=@DiaChi, SucChua=@SucChua,
                  MoTa=@MoTa
                  WHERE MaPhong=@MaPhong", conn);
            cmd.Parameters.AddWithValue("@MaPhong", pt.MaPhong);
            cmd.Parameters.AddWithValue("@TenPhong", pt.TenPhong);
            cmd.Parameters.AddWithValue("@DiaChi", pt.DiaChi);
            cmd.Parameters.AddWithValue("@SucChua", (object?)pt.SucChua ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MoTa", pt.MoTa);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Delete(int maPhong)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("DELETE FROM PhongTap WHERE MaPhong=@MaPhong", conn);
            cmd.Parameters.AddWithValue("@MaPhong", maPhong);
            return cmd.ExecuteNonQuery() > 0;
        }

        public int Count()
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("SELECT COUNT(*) FROM PhongTap", conn);
            return (int)cmd.ExecuteScalar();
        }
    }
}
