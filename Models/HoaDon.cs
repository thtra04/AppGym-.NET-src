namespace AppGym.Models
{
    public class HoaDon
    {
        public int MaHD { get; set; }
        public int MaDK { get; set; }
        public DateTime? NgayThanhToan { get; set; }
        public decimal? SoTien { get; set; }
        public string HinhThucTT { get; set; } = "";
        public string GhiChu { get; set; } = "";

        // Display fields
        public string TenHV { get; set; } = "";
        public string TenGoi { get; set; } = "";
    }
}
