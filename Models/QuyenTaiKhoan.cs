namespace AppGym.Models
{
    public class QuyenTaiKhoan
    {
        public int MaTK { get; set; }
        public string Module { get; set; } = "";
        public bool CanView { get; set; }
        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
    }
}
