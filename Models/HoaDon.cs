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
        public int? MaNguoiLap { get; set; }
        public int? MaNguoiThanhToan { get; set; }

        // Display fields
        public string TenHV { get; set; } = "";
        public string TenGoi { get; set; } = "";
        public string TenNguoiLap { get; set; } = "";
        public string TenNguoiThanhToan { get; set; } = "";
    }
}
