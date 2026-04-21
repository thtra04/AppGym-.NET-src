namespace AppGym.Models
{
    public class ThanhToanReportItem
    {
        public int MaHD { get; set; }
        public int MaDK { get; set; }
        public int MaHV { get; set; }
        public string HoTen { get; set; } = "";
        public int MaGoi { get; set; }
        public string TenGoi { get; set; } = "";
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayHetHan { get; set; }
        public DateTime? NgayThanhToan { get; set; }
        public decimal? SoTien { get; set; }
        public string HinhThucTT { get; set; } = "";
        public string TenNguoiLap { get; set; } = "";
        public string TenNguoiThanhToan { get; set; } = "";
        public string GhiChu { get; set; } = "";
    }
}
