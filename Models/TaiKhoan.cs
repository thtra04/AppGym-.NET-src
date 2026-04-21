namespace AppGym.Models
{
    public class TaiKhoan
    {
        public int MaTK { get; set; }
        public string TenDangNhap { get; set; } = "";
        public string HoTen { get; set; } = "";
        public string VaiTro { get; set; } = "NhanVien";
        public bool HoatDong { get; set; } = true;
        public DateTime? TaoLuc { get; set; }

        public string TrangThaiText => HoatDong ? "Đang hoạt động" : "Tạm khóa";
    }
}
