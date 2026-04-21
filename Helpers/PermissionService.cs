using AppGym.DataAccess;
using AppGym.Models;

namespace AppGym.Helpers
{
    public static class Permissions
    {
        public const string TongQuan = "TongQuan";
        public const string HocVien = "HocVien";
        public const string HuanLuyenVien = "HuanLuyenVien";
        public const string GoiTap = "GoiTap";
        public const string DangKyGoi = "DangKyGoi";
        public const string HoaDon = "HoaDon";
        public const string CaLam = "CaLam";
        public const string PhanCong = "PhanCong";
        public const string TaiKhoan = "TaiKhoan";

        // Modules that admin grants to NhanVien / QuanLy
        public static readonly (string Code, string Display)[] CrudModules = new[]
        {
            (TongQuan,       "Tổng quan (Dashboard)"),
            (HocVien,        "Học viên"),
            (HuanLuyenVien,  "Huấn luyện viên"),
            (GoiTap,         "Gói tập"),
            (DangKyGoi,      "Đăng ký gói"),
            (HoaDon,         "Hóa đơn"),
            (CaLam,          "Ca làm"),
            (PhanCong,       "Phân công PT"),
        };

        // Modules that only support a View permission (no Add/Edit/Delete)
        public static readonly HashSet<string> ViewOnlyModules = new(StringComparer.OrdinalIgnoreCase)
        {
            TongQuan,
        };
    }

    public class PermissionService
    {
        private readonly TaiKhoan _user;
        private readonly Dictionary<string, QuyenTaiKhoan> _grants;

        public PermissionService(TaiKhoan user)
        {
            _user = user;
            _grants = new Dictionary<string, QuyenTaiKhoan>(StringComparer.OrdinalIgnoreCase);
            Reload();
        }

        public TaiKhoan User => _user;
        public bool IsAdmin => string.Equals(_user.VaiTro, "Admin", StringComparison.OrdinalIgnoreCase);
        public bool IsQuanLy => string.Equals(_user.VaiTro, "QuanLy", StringComparison.OrdinalIgnoreCase);

        public void Reload()
        {
            _grants.Clear();
            try
            {
                foreach (var q in new QuyenTaiKhoanDAO().GetByTaiKhoan(_user.MaTK))
                {
                    _grants[q.Module] = q;
                }
            }
            catch { /* table might not exist on first run; schema synchronizer handles it */ }
        }

        // Admin: implicit full. QuanLy: implicit full on every data module + TongQuan view + TaiKhoan management.
        // NhanVien: only the explicit grants stored in QuyenTaiKhoan.
        public bool CanView(string module) => Check(module, q => q.CanView);
        public bool CanAdd(string module) => Check(module, q => q.CanAdd);
        public bool CanEdit(string module) => Check(module, q => q.CanEdit);
        public bool CanDelete(string module) => Check(module, q => q.CanDelete);

        public bool CanManageAccounts => IsAdmin || IsQuanLy;
        public bool CanAssignPermissions => IsAdmin;

        // Admin can touch every account. QuanLy can only manage NhanVien accounts.
        public bool CanManageAccount(TaiKhoan target)
        {
            if (target == null) return false;
            if (IsAdmin) return true;
            if (IsQuanLy) return string.Equals(target.VaiTro, "NhanVien", StringComparison.OrdinalIgnoreCase);
            return false;
        }

        private bool Check(string module, Func<QuyenTaiKhoan, bool> selector)
        {
            if (IsAdmin) return true;
            if (IsQuanLy)
            {
                // QuanLy has implicit full access to operational data + dashboard + own account screen.
                if (string.Equals(module, Permissions.TaiKhoan, StringComparison.OrdinalIgnoreCase)) return true;
                if (string.Equals(module, Permissions.TongQuan, StringComparison.OrdinalIgnoreCase)) return true;
                // All CRUD modules listed in Permissions.CrudModules are implicitly granted to QuanLy.
                foreach (var (code, _) in Permissions.CrudModules)
                {
                    if (string.Equals(module, code, StringComparison.OrdinalIgnoreCase)) return true;
                }
            }
            return _grants.TryGetValue(module, out var q) && selector(q);
        }
    }
}
