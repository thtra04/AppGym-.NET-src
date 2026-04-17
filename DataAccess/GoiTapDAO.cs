using AppGym.Models;
using Microsoft.Data.SqlClient;

namespace AppGym.DataAccess
{
    public class GoiTapDAO
    {
        public List<GoiTap> GetAll()
        {
            var list = new List<GoiTap>();
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("SELECT * FROM GoiTap ORDER BY MaGoi DESC", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new GoiTap
                {
                    MaGoi = reader.GetInt32(reader.GetOrdinal("MaGoi")),
                    TenGoi = reader.IsDBNull(reader.GetOrdinal("TenGoi")) ? "" : reader.GetString(reader.GetOrdinal("TenGoi")),
                    ThoiHan = reader.IsDBNull(reader.GetOrdinal("ThoiHan")) ? null : reader.GetInt32(reader.GetOrdinal("ThoiHan")),
                    Gia = reader.IsDBNull(reader.GetOrdinal("Gia")) ? null : reader.GetDecimal(reader.GetOrdinal("Gia")),
                    MoTa = reader.IsDBNull(reader.GetOrdinal("MoTa")) ? "" : reader.GetString(reader.GetOrdinal("MoTa"))
                });
            }
            return list;
        }

        public bool Insert(GoiTap gt)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                @"INSERT INTO GoiTap (TenGoi, ThoiHan, Gia, MoTa)
                  VALUES (@TenGoi, @ThoiHan, @Gia, @MoTa)", conn);
            cmd.Parameters.AddWithValue("@TenGoi", gt.TenGoi);
            cmd.Parameters.AddWithValue("@ThoiHan", (object?)gt.ThoiHan ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Gia", (object?)gt.Gia ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MoTa", gt.MoTa);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Update(GoiTap gt)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                @"UPDATE GoiTap SET TenGoi=@TenGoi, ThoiHan=@ThoiHan, Gia=@Gia, MoTa=@MoTa
                  WHERE MaGoi=@MaGoi", conn);
            cmd.Parameters.AddWithValue("@MaGoi", gt.MaGoi);
            cmd.Parameters.AddWithValue("@TenGoi", gt.TenGoi);
            cmd.Parameters.AddWithValue("@ThoiHan", (object?)gt.ThoiHan ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Gia", (object?)gt.Gia ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MoTa", gt.MoTa);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Delete(int maGoi)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("DELETE FROM GoiTap WHERE MaGoi=@MaGoi", conn);
            cmd.Parameters.AddWithValue("@MaGoi", maGoi);
            return cmd.ExecuteNonQuery() > 0;
        }

        public int Count()
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("SELECT COUNT(*) FROM GoiTap", conn);
            return (int)cmd.ExecuteScalar();
        }
    }
}
