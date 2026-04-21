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
                @"SELECT dk.*, hv.HoTen AS TenHV, gt.TenGoi,
                         COALESCE(NULLIF(tk.HoTen, N''), tk.TenDangNhap, N'Không rõ') AS TenNguoiLap,
                         ISNULL(gt.Gia, 0) AS GiaGoi,
                         ISNULL(hdSum.DaThanhToan, 0) AS DaThanhToan,
                         CASE
                             WHEN ISNULL(gt.Gia, 0) - ISNULL(hdSum.DaThanhToan, 0) > 0
                                 THEN ISNULL(gt.Gia, 0) - ISNULL(hdSum.DaThanhToan, 0)
                             ELSE 0
                         END AS ConThieu,
                         CASE
                             WHEN ISNULL(hdSum.DaThanhToan, 0) <= 0 THEN N'Chưa thanh toán'
                             WHEN ISNULL(hdSum.DaThanhToan, 0) < ISNULL(gt.Gia, 0) THEN N'Thanh toán một phần'
                             ELSE N'Đã thanh toán đủ'
                         END AS TrangThaiThanhToan
                  FROM DangKyGoi dk
                  JOIN HocVien hv ON dk.MaHV = hv.MaHV
                  JOIN GoiTap gt ON dk.MaGoi = gt.MaGoi
                  LEFT JOIN TaiKhoan tk ON dk.MaNguoiLap = tk.MaTK
                  LEFT JOIN (
                      SELECT MaDK, SUM(ISNULL(SoTien, 0)) AS DaThanhToan
                      FROM HoaDon
                      GROUP BY MaDK
                  ) hdSum ON hdSum.MaDK = dk.MaDK
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
                    GhiChu = reader.IsDBNull(reader.GetOrdinal("GhiChu")) ? "" : reader.GetString(reader.GetOrdinal("GhiChu")),
                    MaNguoiLap = reader.IsDBNull(reader.GetOrdinal("MaNguoiLap")) ? null : reader.GetInt32(reader.GetOrdinal("MaNguoiLap")),
                    TenHV = reader.IsDBNull(reader.GetOrdinal("TenHV")) ? "" : reader.GetString(reader.GetOrdinal("TenHV")),
                    TenGoi = reader.IsDBNull(reader.GetOrdinal("TenGoi")) ? "" : reader.GetString(reader.GetOrdinal("TenGoi")),
                    TenNguoiLap = reader.IsDBNull(reader.GetOrdinal("TenNguoiLap")) ? "" : reader.GetString(reader.GetOrdinal("TenNguoiLap")),
                    GiaGoi = reader.IsDBNull(reader.GetOrdinal("GiaGoi")) ? 0 : reader.GetDecimal(reader.GetOrdinal("GiaGoi")),
                    DaThanhToan = reader.IsDBNull(reader.GetOrdinal("DaThanhToan")) ? 0 : reader.GetDecimal(reader.GetOrdinal("DaThanhToan")),
                    ConThieu = reader.IsDBNull(reader.GetOrdinal("ConThieu")) ? 0 : reader.GetDecimal(reader.GetOrdinal("ConThieu")),
                    TrangThaiThanhToan = reader.IsDBNull(reader.GetOrdinal("TrangThaiThanhToan")) ? "" : reader.GetString(reader.GetOrdinal("TrangThaiThanhToan"))
                });
            }
            return list;
        }

        public bool Insert(DangKyGoi dk)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                @"INSERT INTO DangKyGoi (MaHV, MaGoi, NgayBatDau, NgayHetHan, GhiChu, MaNguoiLap)
                  VALUES (@MaHV, @MaGoi, @NgayBatDau, @NgayHetHan, @GhiChu, @MaNguoiLap)", conn);
            cmd.Parameters.AddWithValue("@MaHV", dk.MaHV);
            cmd.Parameters.AddWithValue("@MaGoi", dk.MaGoi);
            cmd.Parameters.AddWithValue("@NgayBatDau", (object?)dk.NgayBatDau ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@NgayHetHan", (object?)dk.NgayHetHan ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@GhiChu", dk.GhiChu);
            cmd.Parameters.AddWithValue("@MaNguoiLap", (object?)dk.MaNguoiLap ?? DBNull.Value);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Update(DangKyGoi dk)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                @"UPDATE DangKyGoi SET MaHV=@MaHV, MaGoi=@MaGoi, NgayBatDau=@NgayBatDau,
                  NgayHetHan=@NgayHetHan, GhiChu=@GhiChu, MaNguoiLap=@MaNguoiLap
                  WHERE MaDK=@MaDK", conn);
            cmd.Parameters.AddWithValue("@MaDK", dk.MaDK);
            cmd.Parameters.AddWithValue("@MaHV", dk.MaHV);
            cmd.Parameters.AddWithValue("@MaGoi", dk.MaGoi);
            cmd.Parameters.AddWithValue("@NgayBatDau", (object?)dk.NgayBatDau ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@NgayHetHan", (object?)dk.NgayHetHan ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@GhiChu", dk.GhiChu);
            cmd.Parameters.AddWithValue("@MaNguoiLap", (object?)dk.MaNguoiLap ?? DBNull.Value);
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
            using var cmd = new SqlCommand("SELECT COUNT(*) FROM DangKyGoi", conn);
            return (int)cmd.ExecuteScalar();
        }
    }
}
