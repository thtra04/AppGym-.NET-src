namespace AppGym.Models
{
    public class TaiKhoan
    {
        public int MaTK { get; set; }
        public string TenDangNhap { get; set; } = "";
        public string HoTen { get; set; } = "";
        public string VaiTro { get; set; } = "NhanVien";
        public bool TrangThai { get; set; } = true;
        public DateTime? TaoLuc { get; set; }
    }
}
