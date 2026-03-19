namespace AppGym.Models
{
    public class HuanLuyenVien
    {
        public int MaHLV { get; set; }
        public string HoTen { get; set; } = "";
        public string GioiTinh { get; set; } = "";
        public string SDT { get; set; } = "";
        public string ChuyenMon { get; set; } = "";
        public decimal? Luong { get; set; }
        public bool TrangThai { get; set; } = true;
    }
}
