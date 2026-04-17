namespace AppGym.Models
{
    public class DangKyUnpaidItem
    {
        public int MaDK { get; set; }
        public int MaHV { get; set; }
        public string HoTen { get; set; } = "";
        public int MaGoi { get; set; }
        public string TenGoi { get; set; } = "";
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayHetHan { get; set; }
        public decimal TongDaThanhToan { get; set; }
        public decimal GiaGoi { get; set; }
        public decimal ConThieu { get; set; }
    }
}
