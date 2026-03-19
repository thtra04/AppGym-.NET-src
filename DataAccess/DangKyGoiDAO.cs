using AppGym.Models;
using Microsoft.Data.SqlClient;

namespace AppGym.DataAccess
{
    public class DangKyGoiDAO
    {
        public List<DangKyGoi> GetAll()
        {
            var list = new List<DangKyGoi>();
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                @"SELECT dk.*, hv.HoTen AS TenHV, gt.TenGoi
                  FROM DangKyGoi dk
                  JOIN HocVien hv ON dk.MaHV = hv.MaHV
                  JOIN GoiTap gt ON dk.MaGoi = gt.MaGoi
                  ORDER BY dk.MaDK DESC", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new DangKyGoi
                {
                    MaDK = reader.GetInt32(reader.GetOrdinal("MaDK")),
                    MaHV = reader.GetInt32(reader.GetOrdinal("MaHV")),
                    MaGoi = reader.GetInt32(reader.GetOrdinal("MaGoi")),
                    NgayBatDau = reader.IsDBNull(reader.GetOrdinal("NgayBatDau")) ? null : reader.GetDateTime(reader.GetOrdinal("NgayBatDau")),
                    NgayHetHan = reader.IsDBNull(reader.GetOrdinal("NgayHetHan")) ? null : reader.GetDateTime(reader.GetOrdinal("NgayHetHan")),
                    TrangThai = reader.IsDBNull(reader.GetOrdinal("TrangThai")) ? "" : reader.GetString(reader.GetOrdinal("TrangThai")),
                    GhiChu = reader.IsDBNull(reader.GetOrdinal("GhiChu")) ? "" : reader.GetString(reader.GetOrdinal("GhiChu")),
                    TenHV = reader.IsDBNull(reader.GetOrdinal("TenHV")) ? "" : reader.GetString(reader.GetOrdinal("TenHV")),
                    TenGoi = reader.IsDBNull(reader.GetOrdinal("TenGoi")) ? "" : reader.GetString(reader.GetOrdinal("TenGoi"))
                });
            }
            return list;
        }

        public bool Insert(DangKyGoi dk)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                @"INSERT INTO DangKyGoi (MaHV, MaGoi, NgayBatDau, NgayHetHan, TrangThai, GhiChu)
                  VALUES (@MaHV, @MaGoi, @NgayBatDau, @NgayHetHan, @TrangThai, @GhiChu)", conn);
            cmd.Parameters.AddWithValue("@MaHV", dk.MaHV);
            cmd.Parameters.AddWithValue("@MaGoi", dk.MaGoi);
            cmd.Parameters.AddWithValue("@NgayBatDau", (object?)dk.NgayBatDau ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@NgayHetHan", (object?)dk.NgayHetHan ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TrangThai", dk.TrangThai);
            cmd.Parameters.AddWithValue("@GhiChu", dk.GhiChu);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Update(DangKyGoi dk)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                @"UPDATE DangKyGoi SET MaHV=@MaHV, MaGoi=@MaGoi, NgayBatDau=@NgayBatDau,
                  NgayHetHan=@NgayHetHan, TrangThai=@TrangThai, GhiChu=@GhiChu
                  WHERE MaDK=@MaDK", conn);
            cmd.Parameters.AddWithValue("@MaDK", dk.MaDK);
            cmd.Parameters.AddWithValue("@MaHV", dk.MaHV);
            cmd.Parameters.AddWithValue("@MaGoi", dk.MaGoi);
            cmd.Parameters.AddWithValue("@NgayBatDau", (object?)dk.NgayBatDau ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@NgayHetHan", (object?)dk.NgayHetHan ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TrangThai", dk.TrangThai);
            cmd.Parameters.AddWithValue("@GhiChu", dk.GhiChu);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Delete(int maDK)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("DELETE FROM DangKyGoi WHERE MaDK=@MaDK", conn);
            cmd.Parameters.AddWithValue("@MaDK", maDK);
            return cmd.ExecuteNonQuery() > 0;
        }

        public int CountActive()
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("SELECT COUNT(*) FROM DangKyGoi WHERE TrangThai=N'Đang hoạt động'", conn);
            return (int)cmd.ExecuteScalar();
        }
    }
}
