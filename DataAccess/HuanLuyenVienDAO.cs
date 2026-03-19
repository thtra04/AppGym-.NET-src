using AppGym.Models;
using Microsoft.Data.SqlClient;

namespace AppGym.DataAccess
{
    public class HuanLuyenVienDAO
    {
        public List<HuanLuyenVien> GetAll()
        {
            var list = new List<HuanLuyenVien>();
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("SELECT * FROM HuanLuyenVien ORDER BY MaHLV DESC", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new HuanLuyenVien
                {
                    MaHLV = reader.GetInt32(reader.GetOrdinal("MaHLV")),
                    HoTen = reader.IsDBNull(reader.GetOrdinal("HoTen")) ? "" : reader.GetString(reader.GetOrdinal("HoTen")),
                    GioiTinh = reader.IsDBNull(reader.GetOrdinal("GioiTinh")) ? "" : reader.GetString(reader.GetOrdinal("GioiTinh")),
                    SDT = reader.IsDBNull(reader.GetOrdinal("SDT")) ? "" : reader.GetString(reader.GetOrdinal("SDT")),
                    ChuyenMon = reader.IsDBNull(reader.GetOrdinal("ChuyenMon")) ? "" : reader.GetString(reader.GetOrdinal("ChuyenMon")),
                    Luong = reader.IsDBNull(reader.GetOrdinal("Luong")) ? null : reader.GetDecimal(reader.GetOrdinal("Luong")),
                    TrangThai = !reader.IsDBNull(reader.GetOrdinal("TrangThai")) && reader.GetBoolean(reader.GetOrdinal("TrangThai"))
                });
            }
            return list;
        }

        public List<HuanLuyenVien> Search(string keyword)
        {
            var list = new List<HuanLuyenVien>();
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                "SELECT * FROM HuanLuyenVien WHERE HoTen LIKE @kw OR ChuyenMon LIKE @kw OR SDT LIKE @kw ORDER BY MaHLV DESC", conn);
            cmd.Parameters.AddWithValue("@kw", $"%{keyword}%");
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new HuanLuyenVien
                {
                    MaHLV = reader.GetInt32(reader.GetOrdinal("MaHLV")),
                    HoTen = reader.IsDBNull(reader.GetOrdinal("HoTen")) ? "" : reader.GetString(reader.GetOrdinal("HoTen")),
                    GioiTinh = reader.IsDBNull(reader.GetOrdinal("GioiTinh")) ? "" : reader.GetString(reader.GetOrdinal("GioiTinh")),
                    SDT = reader.IsDBNull(reader.GetOrdinal("SDT")) ? "" : reader.GetString(reader.GetOrdinal("SDT")),
                    ChuyenMon = reader.IsDBNull(reader.GetOrdinal("ChuyenMon")) ? "" : reader.GetString(reader.GetOrdinal("ChuyenMon")),
                    Luong = reader.IsDBNull(reader.GetOrdinal("Luong")) ? null : reader.GetDecimal(reader.GetOrdinal("Luong")),
                    TrangThai = !reader.IsDBNull(reader.GetOrdinal("TrangThai")) && reader.GetBoolean(reader.GetOrdinal("TrangThai"))
                });
            }
            return list;
        }

        public bool Insert(HuanLuyenVien hlv)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                @"INSERT INTO HuanLuyenVien (HoTen, GioiTinh, SDT, ChuyenMon, Luong, TrangThai)
                  VALUES (@HoTen, @GioiTinh, @SDT, @ChuyenMon, @Luong, @TrangThai)", conn);
            cmd.Parameters.AddWithValue("@HoTen", hlv.HoTen);
            cmd.Parameters.AddWithValue("@GioiTinh", hlv.GioiTinh);
            cmd.Parameters.AddWithValue("@SDT", hlv.SDT);
            cmd.Parameters.AddWithValue("@ChuyenMon", hlv.ChuyenMon);
            cmd.Parameters.AddWithValue("@Luong", (object?)hlv.Luong ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TrangThai", hlv.TrangThai);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Update(HuanLuyenVien hlv)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand(
                @"UPDATE HuanLuyenVien SET HoTen=@HoTen, GioiTinh=@GioiTinh, SDT=@SDT,
                  ChuyenMon=@ChuyenMon, Luong=@Luong, TrangThai=@TrangThai
                  WHERE MaHLV=@MaHLV", conn);
            cmd.Parameters.AddWithValue("@MaHLV", hlv.MaHLV);
            cmd.Parameters.AddWithValue("@HoTen", hlv.HoTen);
            cmd.Parameters.AddWithValue("@GioiTinh", hlv.GioiTinh);
            cmd.Parameters.AddWithValue("@SDT", hlv.SDT);
            cmd.Parameters.AddWithValue("@ChuyenMon", hlv.ChuyenMon);
            cmd.Parameters.AddWithValue("@Luong", (object?)hlv.Luong ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TrangThai", hlv.TrangThai);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Delete(int maHLV)
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("DELETE FROM HuanLuyenVien WHERE MaHLV=@MaHLV", conn);
            cmd.Parameters.AddWithValue("@MaHLV", maHLV);
            return cmd.ExecuteNonQuery() > 0;
        }

        public int Count()
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = new SqlCommand("SELECT COUNT(*) FROM HuanLuyenVien WHERE TrangThai=1", conn);
            return (int)cmd.ExecuteScalar();
        }
    }
}
