using AppGym.Models;
using Microsoft.Data.SqlClient;
using System.Text;

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

        public List<ThanhToanReportItem> GetInvoiceReport(string? keyword = null)
        {
            var list = new List<ThanhToanReportItem>();
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();

            var key = keyword?.Trim() ?? string.Empty;
            var sql = new StringBuilder(@"
SELECT hd.MaHD, dk.MaDK, dk.MaHV, hv.HoTen, dk.MaGoi, gt.TenGoi,
       dk.NgayBatDau, dk.NgayHetHan, hd.NgayThanhToan, hd.SoTien, hd.HinhThucTT, hd.GhiChu
FROM HoaDon hd
JOIN DangKyGoi dk ON hd.MaDK = dk.MaDK
JOIN HocVien hv ON dk.MaHV = hv.MaHV
JOIN GoiTap gt ON dk.MaGoi = gt.MaGoi");
            if (!string.IsNullOrWhiteSpace(key))
            {
                sql.Append(@" WHERE hv.HoTen LIKE @kw OR gt.TenGoi LIKE @kw OR hv.SDT LIKE @kw
                              OR CAST(hd.MaHD AS NVARCHAR(20)) LIKE @kw OR CAST(dk.MaDK AS NVARCHAR(20)) LIKE @kw");
            }
            sql.Append(" ORDER BY hd.NgayThanhToan DESC, hd.MaHD DESC");

            using var cmd = new SqlCommand(sql.ToString(), conn);
            if (!string.IsNullOrWhiteSpace(key))
            {
                cmd.Parameters.AddWithValue("@kw", $"%{key}%");
            }

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new ThanhToanReportItem
                {
                    MaHD = reader.GetInt32(reader.GetOrdinal("MaHD")),
                    MaDK = reader.GetInt32(reader.GetOrdinal("MaDK")),
                    MaHV = reader.IsDBNull(reader.GetOrdinal("MaHV")) ? 0 : reader.GetInt32(reader.GetOrdinal("MaHV")),
                    HoTen = reader.IsDBNull(reader.GetOrdinal("HoTen")) ? "" : reader.GetString(reader.GetOrdinal("HoTen")),
                    MaGoi = reader.IsDBNull(reader.GetOrdinal("MaGoi")) ? 0 : reader.GetInt32(reader.GetOrdinal("MaGoi")),
                    TenGoi = reader.IsDBNull(reader.GetOrdinal("TenGoi")) ? "" : reader.GetString(reader.GetOrdinal("TenGoi")),
                    NgayBatDau = reader.IsDBNull(reader.GetOrdinal("NgayBatDau")) ? null : reader.GetDateTime(reader.GetOrdinal("NgayBatDau")),
                    NgayHetHan = reader.IsDBNull(reader.GetOrdinal("NgayHetHan")) ? null : reader.GetDateTime(reader.GetOrdinal("NgayHetHan")),
                    NgayThanhToan = reader.IsDBNull(reader.GetOrdinal("NgayThanhToan")) ? null : reader.GetDateTime(reader.GetOrdinal("NgayThanhToan")),
                    SoTien = reader.IsDBNull(reader.GetOrdinal("SoTien")) ? null : reader.GetDecimal(reader.GetOrdinal("SoTien")),
                    HinhThucTT = reader.IsDBNull(reader.GetOrdinal("HinhThucTT")) ? "" : reader.GetString(reader.GetOrdinal("HinhThucTT")),
                    GhiChu = reader.IsDBNull(reader.GetOrdinal("GhiChu")) ? "" : reader.GetString(reader.GetOrdinal("GhiChu"))
                });
            }
            return list;
        }

        public List<DangKyUnpaidItem> GetUnpaidRegistrations(string? keyword = null, bool includeNoInvoice = true, bool includeUnderpaid = true)
        {
            var list = new List<DangKyUnpaidItem>();
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();

            var key = keyword?.Trim() ?? string.Empty;
            var sql = new StringBuilder(@"
SELECT dk.MaDK, dk.MaHV, hv.HoTen, dk.MaGoi, gt.TenGoi,
       dk.NgayBatDau, dk.NgayHetHan,
       ISNULL(hdSum.TongDaThanhToan, 0) AS TongDaThanhToan,
       ISNULL(gt.Gia, 0) AS GiaGoi,
       ISNULL(gt.Gia, 0) - ISNULL(hdSum.TongDaThanhToan, 0) AS ConThieu
FROM DangKyGoi dk
JOIN HocVien hv ON dk.MaHV = hv.MaHV
JOIN GoiTap gt ON dk.MaGoi = gt.MaGoi
LEFT JOIN (
    SELECT MaDK, SUM(ISNULL(SoTien, 0)) AS TongDaThanhToan
    FROM HoaDon
    GROUP BY MaDK
) hdSum ON hdSum.MaDK = dk.MaDK");

            if (!includeNoInvoice && !includeUnderpaid)
            {
                sql.Append(@" WHERE 1 = 0");
            }
            else if (includeNoInvoice && !includeUnderpaid)
            {
                sql.Append(@" WHERE hdSum.MaDK IS NULL");
            }
            else if (!includeNoInvoice && includeUnderpaid)
            {
                sql.Append(@" WHERE hdSum.MaDK IS NOT NULL AND ISNULL(hdSum.TongDaThanhToan, 0) < ISNULL(gt.Gia, 0)");
            }
            else
            {
                sql.Append(@" WHERE hdSum.MaDK IS NULL OR ISNULL(hdSum.TongDaThanhToan, 0) < ISNULL(gt.Gia, 0)");
            }

            if (!string.IsNullOrWhiteSpace(key))
            {
                sql.Append(@" AND (hv.HoTen LIKE @kw OR gt.TenGoi LIKE @kw OR hv.SDT LIKE @kw)");
            }

            sql.Append(" ORDER BY dk.NgayBatDau DESC, dk.MaDK DESC");

            using var cmd = new SqlCommand(sql.ToString(), conn);
            if (!string.IsNullOrWhiteSpace(key))
            {
                cmd.Parameters.AddWithValue("@kw", $"%{key}%");
            }

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new DangKyUnpaidItem
                {
                    MaDK = reader.GetInt32(reader.GetOrdinal("MaDK")),
                    MaHV = reader.IsDBNull(reader.GetOrdinal("MaHV")) ? 0 : reader.GetInt32(reader.GetOrdinal("MaHV")),
                    HoTen = reader.IsDBNull(reader.GetOrdinal("HoTen")) ? "" : reader.GetString(reader.GetOrdinal("HoTen")),
                    MaGoi = reader.IsDBNull(reader.GetOrdinal("MaGoi")) ? 0 : reader.GetInt32(reader.GetOrdinal("MaGoi")),
                    TenGoi = reader.IsDBNull(reader.GetOrdinal("TenGoi")) ? "" : reader.GetString(reader.GetOrdinal("TenGoi")),
                    NgayBatDau = reader.IsDBNull(reader.GetOrdinal("NgayBatDau")) ? null : reader.GetDateTime(reader.GetOrdinal("NgayBatDau")),
                    NgayHetHan = reader.IsDBNull(reader.GetOrdinal("NgayHetHan")) ? null : reader.GetDateTime(reader.GetOrdinal("NgayHetHan")),
                    TongDaThanhToan = reader.IsDBNull(reader.GetOrdinal("TongDaThanhToan")) ? 0 : reader.GetDecimal(reader.GetOrdinal("TongDaThanhToan")),
                    GiaGoi = reader.IsDBNull(reader.GetOrdinal("GiaGoi")) ? 0 : reader.GetDecimal(reader.GetOrdinal("GiaGoi")),
                    ConThieu = reader.IsDBNull(reader.GetOrdinal("ConThieu")) ? 0 : reader.GetDecimal(reader.GetOrdinal("ConThieu"))
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
