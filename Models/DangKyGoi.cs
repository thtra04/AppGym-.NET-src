namespace AppGym.Models
{
    public class DangKyGoi
    {
        public int MaDK { get; set; }
        public int MaHV { get; set; }
        public int MaGoi { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayHetHan { get; set; }
        public string TrangThai { get; set; } = "Đang hoạt động";
        public string GhiChu { get; set; } = "";

        // Display fields
        public string TenHV { get; set; } = "";
        public string TenGoi { get; set; } = "";
    }
}
