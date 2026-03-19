using AppGym.Models;
using Microsoft.Data.SqlClient;

namespace AppGym.DataAccess
{
    public class HoaDonDAO
    {
        public List<HoaDon> GetAll()
        {
            var list = new List<HoaDon>();
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                @"SELECT hd.*, hv.HoTen AS TenHV, gt.TenGoi
                  FROM HoaDon hd
                  JOIN DangKyGoi dk ON hd.MaDK = dk.MaDK
                  JOIN HocVien hv ON dk.MaHV = hv.MaHV
                  JOIN GoiTap gt ON dk.MaGoi = gt.MaGoi
                  ORDER BY hd.MaHD DESC", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new HoaDon
                {
                    MaHD = reader.GetInt32(reader.GetOrdinal("MaHD")),
                    MaDK = reader.GetInt32(reader.GetOrdinal("MaDK")),
                    NgayThanhToan = reader.IsDBNull(reader.GetOrdinal("NgayThanhToan")) ? null : reader.GetDateTime(reader.GetOrdinal("NgayThanhToan")),
                    SoTien = reader.IsDBNull(reader.GetOrdinal("SoTien")) ? null : reader.GetDecimal(reader.GetOrdinal("SoTien")),
                    HinhThucTT = reader.IsDBNull(reader.GetOrdinal("HinhThucTT")) ? "" : reader.GetString(reader.GetOrdinal("HinhThucTT")),
                    GhiChu = reader.IsDBNull(reader.GetOrdinal("GhiChu")) ? "" : reader.GetString(reader.GetOrdinal("GhiChu")),
                    TenHV = reader.IsDBNull(reader.GetOrdinal("TenHV")) ? "" : reader.GetString(reader.GetOrdinal("TenHV")),
                    TenGoi = reader.IsDBNull(reader.GetOrdinal("TenGoi")) ? "" : reader.GetString(reader.GetOrdinal("TenGoi"))
                });
            }
            return list;
        }

        public bool Insert(HoaDon hd)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                @"INSERT INTO HoaDon (MaDK, NgayThanhToan, SoTien, HinhThucTT, GhiChu)
                  VALUES (@MaDK, @NgayThanhToan, @SoTien, @HinhThucTT, @GhiChu)", conn);
            cmd.Parameters.AddWithValue("@MaDK", hd.MaDK);
            cmd.Parameters.AddWithValue("@NgayThanhToan", (object?)hd.NgayThanhToan ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@SoTien", (object?)hd.SoTien ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@HinhThucTT", hd.HinhThucTT);
            cmd.Parameters.AddWithValue("@GhiChu", hd.GhiChu);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Update(HoaDon hd)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                @"UPDATE HoaDon SET MaDK=@MaDK, NgayThanhToan=@NgayThanhToan,
                  SoTien=@SoTien, HinhThucTT=@HinhThucTT, GhiChu=@GhiChu
                  WHERE MaHD=@MaHD", conn);
            cmd.Parameters.AddWithValue("@MaHD", hd.MaHD);
            cmd.Parameters.AddWithValue("@MaDK", hd.MaDK);
            cmd.Parameters.AddWithValue("@NgayThanhToan", (object?)hd.NgayThanhToan ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@SoTien", (object?)hd.SoTien ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@HinhThucTT", hd.HinhThucTT);
            cmd.Parameters.AddWithValue("@GhiChu", hd.GhiChu);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Delete(int maHD)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("DELETE FROM HoaDon WHERE MaHD=@MaHD", conn);
            cmd.Parameters.AddWithValue("@MaHD", maHD);
            return cmd.ExecuteNonQuery() > 0;
        }

        public decimal TotalRevenue()
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("SELECT ISNULL(SUM(SoTien),0) FROM HoaDon", conn);
            return (decimal)cmd.ExecuteScalar();
        }
    }
}
