namespace AppGym.Models
{
    public class DangKyGoi
    {
        public int MaDK { get; set; }
        public int MaHV { get; set; }
        public int MaGoi { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayHetHan { get; set; }
        public string GhiChu { get; set; } = "";
        public int? MaNguoiLap { get; set; }

        // Display fields
        public string TenHV { get; set; } = "";
        public string TenGoi { get; set; } = "";
        public string TenNguoiLap { get; set; } = "";
        public decimal GiaGoi { get; set; }
        public decimal DaThanhToan { get; set; }
        public decimal ConThieu { get; set; }
        public string TrangThaiThanhToan { get; set; } = "";
    }
}
