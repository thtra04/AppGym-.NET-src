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
    }
}
