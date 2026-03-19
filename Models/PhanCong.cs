namespace AppGym.Models
{
    public class PhanCong
    {
        public int MaPC { get; set; }
        public int MaHLV { get; set; }
        public int MaDK { get; set; }
        public int? MaCa { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string GhiChu { get; set; } = "";

        // Display fields
        public string TenHLV { get; set; } = "";
        public string TenHV { get; set; } = "";
        public string TenCa { get; set; } = "";
    }
}
