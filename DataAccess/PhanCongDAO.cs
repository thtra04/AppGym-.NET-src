using AppGym.Models;
using Microsoft.Data.SqlClient;

namespace AppGym.DataAccess
{
    public class PhanCongDAO
    {
        public List<PhanCong> GetAll()
        {
            var list = new List<PhanCong>();
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                @"SELECT pc.*, hlv.HoTen AS TenHLV, hv.HoTen AS TenHV, cl.TenCa
                  FROM PhanCong pc
                  JOIN HuanLuyenVien hlv ON pc.MaHLV = hlv.MaHLV
                  JOIN DangKyGoi dk ON pc.MaDK = dk.MaDK
                  JOIN HocVien hv ON dk.MaHV = hv.MaHV
                  LEFT JOIN CaLam cl ON pc.MaCa = cl.MaCa
                  ORDER BY pc.MaPC DESC", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new PhanCong
                {
                    MaPC = reader.GetInt32(reader.GetOrdinal("MaPC")),
                    MaHLV = reader.GetInt32(reader.GetOrdinal("MaHLV")),
                    MaDK = reader.GetInt32(reader.GetOrdinal("MaDK")),
                    MaCa = reader.IsDBNull(reader.GetOrdinal("MaCa")) ? null : reader.GetInt32(reader.GetOrdinal("MaCa")),
                    NgayBatDau = reader.IsDBNull(reader.GetOrdinal("NgayBatDau")) ? null : reader.GetDateTime(reader.GetOrdinal("NgayBatDau")),
                    NgayKetThuc = reader.IsDBNull(reader.GetOrdinal("NgayKetThuc")) ? null : reader.GetDateTime(reader.GetOrdinal("NgayKetThuc")),
                    GhiChu = reader.IsDBNull(reader.GetOrdinal("GhiChu")) ? "" : reader.GetString(reader.GetOrdinal("GhiChu")),
                    TenHLV = reader.IsDBNull(reader.GetOrdinal("TenHLV")) ? "" : reader.GetString(reader.GetOrdinal("TenHLV")),
                    TenHV = reader.IsDBNull(reader.GetOrdinal("TenHV")) ? "" : reader.GetString(reader.GetOrdinal("TenHV")),
                    TenCa = reader.IsDBNull(reader.GetOrdinal("TenCa")) ? "" : reader.GetString(reader.GetOrdinal("TenCa"))
                });
            }
            return list;
        }

        public List<PhanCong> Search(string keyword)
        {
            var list = new List<PhanCong>();
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                @"SELECT pc.*, hlv.HoTen AS TenHLV, hv.HoTen AS TenHV, cl.TenCa
                  FROM PhanCong pc
                  JOIN HuanLuyenVien hlv ON pc.MaHLV = hlv.MaHLV
                  JOIN DangKyGoi dk ON pc.MaDK = dk.MaDK
                  JOIN HocVien hv ON dk.MaHV = hv.MaHV
                  LEFT JOIN CaLam cl ON pc.MaCa = cl.MaCa
                  WHERE CAST(pc.MaPC AS NVARCHAR(20)) LIKE @kw
                     OR hlv.HoTen LIKE @kw
                     OR hv.HoTen LIKE @kw
                     OR ISNULL(cl.TenCa, N'') LIKE @kw
                     OR ISNULL(pc.GhiChu, N'') LIKE @kw
                     OR CONVERT(NVARCHAR(10), pc.NgayBatDau, 103) LIKE @kw
                     OR CONVERT(NVARCHAR(10), pc.NgayKetThuc, 103) LIKE @kw
                  ORDER BY pc.MaPC DESC", conn);
            cmd.Parameters.AddWithValue("@kw", $"%{keyword}%");
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new PhanCong
                {
                    MaPC = reader.GetInt32(reader.GetOrdinal("MaPC")),
                    MaHLV = reader.GetInt32(reader.GetOrdinal("MaHLV")),
                    MaDK = reader.GetInt32(reader.GetOrdinal("MaDK")),
                    MaCa = reader.IsDBNull(reader.GetOrdinal("MaCa")) ? null : reader.GetInt32(reader.GetOrdinal("MaCa")),
                    NgayBatDau = reader.IsDBNull(reader.GetOrdinal("NgayBatDau")) ? null : reader.GetDateTime(reader.GetOrdinal("NgayBatDau")),
                    NgayKetThuc = reader.IsDBNull(reader.GetOrdinal("NgayKetThuc")) ? null : reader.GetDateTime(reader.GetOrdinal("NgayKetThuc")),
                    GhiChu = reader.IsDBNull(reader.GetOrdinal("GhiChu")) ? "" : reader.GetString(reader.GetOrdinal("GhiChu")),
                    TenHLV = reader.IsDBNull(reader.GetOrdinal("TenHLV")) ? "" : reader.GetString(reader.GetOrdinal("TenHLV")),
                    TenHV = reader.IsDBNull(reader.GetOrdinal("TenHV")) ? "" : reader.GetString(reader.GetOrdinal("TenHV")),
                    TenCa = reader.IsDBNull(reader.GetOrdinal("TenCa")) ? "" : reader.GetString(reader.GetOrdinal("TenCa"))
                });
            }
            return list;
        }

        public bool Insert(PhanCong pc)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                @"INSERT INTO PhanCong (MaHLV, MaDK, MaCa, NgayBatDau, NgayKetThuc, GhiChu)
                  VALUES (@MaHLV, @MaDK, @MaCa, @NgayBatDau, @NgayKetThuc, @GhiChu)", conn);
            cmd.Parameters.AddWithValue("@MaHLV", pc.MaHLV);
            cmd.Parameters.AddWithValue("@MaDK", pc.MaDK);
            cmd.Parameters.AddWithValue("@MaCa", (object?)pc.MaCa ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@NgayBatDau", (object?)pc.NgayBatDau ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@NgayKetThuc", (object?)pc.NgayKetThuc ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@GhiChu", pc.GhiChu);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Update(PhanCong pc)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                @"UPDATE PhanCong SET MaHLV=@MaHLV, MaDK=@MaDK, MaCa=@MaCa,
                  NgayBatDau=@NgayBatDau, NgayKetThuc=@NgayKetThuc, GhiChu=@GhiChu
                  WHERE MaPC=@MaPC", conn);
            cmd.Parameters.AddWithValue("@MaPC", pc.MaPC);
            cmd.Parameters.AddWithValue("@MaHLV", pc.MaHLV);
            cmd.Parameters.AddWithValue("@MaDK", pc.MaDK);
            cmd.Parameters.AddWithValue("@MaCa", (object?)pc.MaCa ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@NgayBatDau", (object?)pc.NgayBatDau ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@NgayKetThuc", (object?)pc.NgayKetThuc ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@GhiChu", pc.GhiChu);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Delete(int maPC)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("DELETE FROM PhanCong WHERE MaPC=@MaPC", conn);
            cmd.Parameters.AddWithValue("@MaPC", maPC);
            return cmd.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// True if the given DangKyGoi already has a trainer assigned.
        /// Optionally exclude a PhanCong row (used when editing).
        /// </summary>
        public bool ExistsForDangKy(int maDK, int? excludeMaPC = null)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                "SELECT COUNT(1) FROM PhanCong WHERE MaDK=@MaDK AND (@ExcludeMaPC IS NULL OR MaPC<>@ExcludeMaPC)", conn);
            cmd.Parameters.AddWithValue("@MaDK", maDK);
            cmd.Parameters.AddWithValue("@ExcludeMaPC", (object?)excludeMaPC ?? DBNull.Value);
            return (int)cmd.ExecuteScalar() > 0;
        }

        /// <summary>
        /// Danh sách các đăng ký gói (học viên + gói) hiện CHƯA được phân công PT.
        /// </summary>
        public List<DangKyGoi> GetDangKyChuaPhanCong()
        {
            var list = new List<DangKyGoi>();
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                @"SELECT dk.MaDK, dk.MaHV, dk.MaGoi, dk.NgayBatDau, dk.NgayHetHan,
                         ISNULL(dk.GhiChu, N'') AS GhiChu,
                         hv.HoTen AS TenHV, gt.TenGoi AS TenGoi
                  FROM DangKyGoi dk
                  INNER JOIN HocVien hv ON dk.MaHV = hv.MaHV
                  INNER JOIN GoiTap gt ON dk.MaGoi = gt.MaGoi
                  WHERE NOT EXISTS (SELECT 1 FROM PhanCong pc WHERE pc.MaDK = dk.MaDK)
                  ORDER BY dk.MaDK DESC", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new DangKyGoi
                {
                    MaDK = reader.GetInt32(0),
                    MaHV = reader.GetInt32(1),
                    MaGoi = reader.GetInt32(2),
                    NgayBatDau = reader.IsDBNull(3) ? null : reader.GetDateTime(3),
                    NgayHetHan = reader.IsDBNull(4) ? null : reader.GetDateTime(4),
                    GhiChu = reader.GetString(5),
                    TenHV = reader.GetString(6),
                    TenGoi = reader.GetString(7)
                });
            }
            return list;
        }
    }
}
