namespace AppGym.Models
{
    public class GoiTap
    {
        public int MaGoi { get; set; }
        public string TenGoi { get; set; } = "";
        public int? ThoiHan { get; set; }
        public decimal? Gia { get; set; }
        public string MoTa { get; set; } = "";
        public bool TrangThai { get; set; } = true;
    }
}
