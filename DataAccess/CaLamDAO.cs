using AppGym.Models;
using Microsoft.Data.SqlClient;

namespace AppGym.DataAccess
{
    public class CaLamDAO
    {
        public List<CaLam> GetAll()
        {
            var list = new List<CaLam>();
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("SELECT * FROM CaLam ORDER BY MaCa", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new CaLam
                {
                    MaCa = reader.GetInt32(reader.GetOrdinal("MaCa")),
                    TenCa = reader.IsDBNull(reader.GetOrdinal("TenCa")) ? "" : reader.GetString(reader.GetOrdinal("TenCa")),
                    GioBatDau = reader.IsDBNull(reader.GetOrdinal("GioBatDau")) ? null : reader.GetTimeSpan(reader.GetOrdinal("GioBatDau")),
                    GioKetThuc = reader.IsDBNull(reader.GetOrdinal("GioKetThuc")) ? null : reader.GetTimeSpan(reader.GetOrdinal("GioKetThuc"))
                });
            }
            return list;
        }

        public bool Insert(CaLam ca)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                @"INSERT INTO CaLam (TenCa, GioBatDau, GioKetThuc)
                  VALUES (@TenCa, @GioBatDau, @GioKetThuc)", conn);
            cmd.Parameters.AddWithValue("@TenCa", ca.TenCa);
            cmd.Parameters.AddWithValue("@GioBatDau", (object?)ca.GioBatDau ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@GioKetThuc", (object?)ca.GioKetThuc ?? DBNull.Value);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Update(CaLam ca)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                @"UPDATE CaLam SET TenCa=@TenCa, GioBatDau=@GioBatDau, GioKetThuc=@GioKetThuc
                  WHERE MaCa=@MaCa", conn);
            cmd.Parameters.AddWithValue("@MaCa", ca.MaCa);
            cmd.Parameters.AddWithValue("@TenCa", ca.TenCa);
            cmd.Parameters.AddWithValue("@GioBatDau", (object?)ca.GioBatDau ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@GioKetThuc", (object?)ca.GioKetThuc ?? DBNull.Value);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Delete(int maCa)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("DELETE FROM CaLam WHERE MaCa=@MaCa", conn);
            cmd.Parameters.AddWithValue("@MaCa", maCa);
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
