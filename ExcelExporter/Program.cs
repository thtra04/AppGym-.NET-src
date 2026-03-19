using ClosedXML.Excel;
using ExcelExporterApp;

ExcelExporter.Run();

// ── Namespace wraps both the program class and the record ───────────────────
namespace ExcelExporterApp
{
    record TC(string Id, string TenTestCase, string BoduLieu, string KetQuaMongDoi,
              string Prerequisites = "", string TestData = "");

    static class ExcelExporter
    {
        static readonly XLColor ClrYellow    = XLColor.FromHtml("#FFFF99");
        static readonly XLColor ClrBlue      = XLColor.FromArgb(68, 114, 196);
        static readonly XLColor ClrLightBlue = XLColor.FromArgb(189, 215, 238);
        static readonly XLColor ClrOrange    = XLColor.FromArgb(248, 203, 173);
        static readonly XLColor ClrPurple    = XLColor.FromArgb(217, 225, 242);
        static readonly XLColor ClrPink      = XLColor.FromArgb(234, 209, 220);
        static readonly XLColor ClrMint      = XLColor.FromArgb(226, 239, 218);
        static readonly XLColor ClrLemon     = XLColor.FromArgb(255, 242, 204);
        static readonly XLColor ClrRed       = XLColor.FromArgb(255, 199, 206);
        static readonly XLColor ClrGray      = XLColor.FromArgb(242, 242, 242);

        public static void Run()
        {
            var outPath = @"C:\BAITAP\t3-hk2-chieu-KiemThu\gk-kiemthu\AppGym\TestCases_AppGym.xlsx";

            var modules = new (string Name, string TcId, string Desc, XLColor RowColor, TC[] Cases)[]
            {
                ("Dang nhap", "TC_LOGIN", "Kiểm tra chức năng đăng nhập hệ thống", ClrLightBlue, new TC[]
                {
                    new("TC_LOGIN_01","Đăng nhập thành công với tài khoản hợp lệ",
                        "TenDangNhap=admin\nMatKhau=123",
                        "Đăng nhập thành công, hệ thống mở FormMain, hiển thị tên người dùng trên TopBar",
                        "Tài khoản admin đã tồn tại trong DB","TenDangNhap=admin | MatKhau=123"),
                    new("TC_LOGIN_02","Đăng nhập sai mật khẩu",
                        "TenDangNhap=admin\nMatKhau=sai123",
                        "Hiển thị thông báo lỗi 'Tên đăng nhập hoặc mật khẩu không đúng!'",
                        "","TenDangNhap=admin | MatKhau=sai123"),
                    new("TC_LOGIN_03","Để trống tên đăng nhập",
                        "TenDangNhap=''\nMatKhau=123",
                        "Hiển thị cảnh báo 'Vui lòng nhập đầy đủ thông tin!'",
                        "","TenDangNhap='' | MatKhau=123"),
                    new("TC_LOGIN_04","Để trống mật khẩu",
                        "TenDangNhap=admin\nMatKhau=''",
                        "Hiển thị cảnh báo 'Vui lòng nhập đầy đủ thông tin!'",
                        "","TenDangNhap=admin | MatKhau=''"),
                    new("TC_LOGIN_05","Tài khoản bị vô hiệu hóa (TrangThai=0)",
                        "TenDangNhap=user_bi_khoa\nMatKhau=123",
                        "Đăng nhập thất bại, không mở FormMain",
                        "Có tài khoản TrangThai=0 trong DB","TenDangNhap=user_bi_khoa | MatKhau=123"),
                    new("TC_LOGIN_06","Nhấn Enter ở ô mật khẩu để đăng nhập",
                        "TenDangNhap=admin\nMatKhau=123\nNhấn phím Enter",
                        "Đăng nhập thành công giống như nhấn nút ĐĂNG NHẬP",
                        "","TenDangNhap=admin | MatKhau=123"),
                    new("TC_LOGIN_07","Tên đăng nhập không tồn tại trong hệ thống",
                        "TenDangNhap=khongtontai999\nMatKhau=123",
                        "Hiển thị lỗi 'Tên đăng nhập hoặc mật khẩu không đúng!'",
                        "","TenDangNhap=khongtontai999 | MatKhau=123"),
                }),
                ("Hoc vien", "TC_HV", "Kiểm tra chức năng quản lý học viên", ClrMint, new TC[]
                {
                    new("TC_HV_01","Thêm học viên với đầy đủ thông tin hợp lệ",
                        "HoTen=Nguyen Van A\nGioiTinh=Nam\nNgaySinh=01/01/1995\nSDT=0901234567\nEmail=a@gmail.com",
                        "Lưu thành công, học viên xuất hiện trong danh sách",
                        "","HoTen=Nguyen Van A | SDT=0901234567"),
                    new("TC_HV_02","Thêm học viên để trống Họ tên",
                        "HoTen=''",
                        "Hiển thị cảnh báo 'Vui lòng nhập họ tên!'","","HoTen=''"),
                    new("TC_HV_03","Sửa thông tin học viên",
                        "Chọn học viên có sẵn\nSửa SDT=0987654321\nNhấn Lưu",
                        "Lưu thành công, SDT trong danh sách cập nhật",
                        "Có ít nhất 1 học viên trong DB","SDT mới=0987654321"),
                    new("TC_HV_04","Xóa học viên chưa có đăng ký gói",
                        "Chọn học viên chưa đăng ký gói\nXác nhận Yes",
                        "Xóa thành công, học viên không còn trong danh sách",
                        "Có học viên chưa đăng ký gói",""),
                    new("TC_HV_05","Xóa học viên đang có đăng ký gói (FK)",
                        "Chọn học viên đang có DangKyGoi\nXác nhận Yes",
                        "Hiển thị thông báo lỗi rõ ràng, không xóa được, app không crash",
                        "Có học viên đang có DangKyGoi",""),
                    new("TC_HV_06","Tìm kiếm học viên theo tên",
                        "Nhập 'Nguyen' vào ô tìm kiếm",
                        "Danh sách chỉ hiển thị học viên có tên chứa 'Nguyen'","","Keyword=Nguyen"),
                    new("TC_HV_07","Tìm kiếm học viên theo số điện thoại",
                        "Nhập SDT=0901234567 vào ô tìm kiếm",
                        "Danh sách lọc đúng theo SDT","","Keyword=0901234567"),
                    new("TC_HV_08","Tìm kiếm từ khóa không tồn tại",
                        "Nhập 'xyzxyz123' vào ô tìm kiếm",
                        "Danh sách rỗng, không có lỗi","","Keyword=xyzxyz123"),
                    new("TC_HV_09","Nhấn Làm mới sau khi tìm kiếm",
                        "Sau khi tìm kiếm, nhấn nút Làm mới",
                        "Ô tìm kiếm bị xóa, danh sách hiển thị toàn bộ","",""),
                    new("TC_HV_10","Nhấn Hủy trên form thêm học viên",
                        "Mở form thêm HV\nNhập dữ liệu\nNhấn Hủy",
                        "Form đóng, không lưu, danh sách không thay đổi","",""),
                }),
                ("Huan luyen vien", "TC_HLV", "Kiểm tra chức năng quản lý huấn luyện viên", ClrLemon, new TC[]
                {
                    new("TC_HLV_01","Thêm HLV với đầy đủ thông tin hợp lệ",
                        "HoTen=Tran Thi B\nGioiTinh=Nu\nSDT=0912345678\nChuyenMon=Yoga\nLuong=5000000",
                        "Lưu thành công, HLV xuất hiện trong danh sách",
                        "","HoTen=Tran Thi B | Luong=5000000"),
                    new("TC_HLV_02","Thêm HLV để trống Họ tên",
                        "HoTen=''",
                        "Hiển thị cảnh báo 'Vui lòng nhập họ tên!'","","HoTen=''"),
                    new("TC_HLV_03","Thêm HLV với lương không phải số",
                        "Luong='abc'",
                        "Lưu thành công, Luong lưu giá trị null (không crash)","","Luong=abc"),
                    new("TC_HLV_04","Xóa HLV đang có phân công (FK)",
                        "Chọn HLV đang có PhanCong\nXác nhận Xóa",
                        "Hiển thị lỗi FK rõ ràng, không xóa được, app không crash",
                        "Có HLV đang có PhanCong",""),
                    new("TC_HLV_05","Tìm kiếm HLV theo chuyên môn",
                        "Nhập 'Yoga' vào ô tìm kiếm",
                        "Danh sách lọc đúng, chỉ hiển thị HLV ChuyenMon chứa 'Yoga'","","Keyword=Yoga"),
                }),
                ("Goi tap", "TC_GT", "Kiểm tra chức năng quản lý gói tập", ClrOrange, new TC[]
                {
                    new("TC_GT_01","Thêm gói tập với đầy đủ thông tin hợp lệ",
                        "TenGoi=Goi 1 Thang\nThoiHan=30\nGia=500000\nMoTa=Co ban",
                        "Lưu thành công, gói tập xuất hiện trong danh sách",
                        "","TenGoi=Goi 1 Thang | Gia=500000"),
                    new("TC_GT_02","Thêm gói tập để trống tên gói",
                        "TenGoi=''",
                        "Hiển thị cảnh báo 'Vui lòng nhập tên gói!'","","TenGoi=''"),
                    new("TC_GT_03","Thêm gói với ThoiHan và Gia không phải số",
                        "ThoiHan='abc'\nGia='xyz'",
                        "Lưu thành công, ThoiHan/Gia lưu null (không crash)","","ThoiHan=abc | Gia=xyz"),
                    new("TC_GT_04","Xóa gói tập đang được đăng ký (FK)",
                        "Chọn gói đang có DangKyGoi\nXác nhận Xóa",
                        "Hiển thị lỗi FK rõ ràng, không xóa được",
                        "Có gói đang có DangKyGoi",""),
                    new("TC_GT_05","Sửa giá gói tập",
                        "Chọn gói\nSửa Gia=1000000\nNhấn Lưu",
                        "Lưu thành công, giá trong danh sách cập nhật",
                        "Có ít nhất 1 gói trong DB","Gia=1000000"),
                    new("TC_GT_06","[BUG#8] Tìm kiếm gói tập trên UI không hoạt động",
                        "Nhập từ khóa vào ô tìm kiếm trên trang Gói tập",
                        "[BUG] Danh sách KHÔNG thay đổi\nGoiTapDAO thiếu phương thức Search()",
                        "","Keyword=Goi"),
                }),
                ("Dang ky goi", "TC_DK", "Kiểm tra chức năng quản lý đăng ký gói", ClrPurple, new TC[]
                {
                    new("TC_DK_01","Thêm đăng ký gói hợp lệ",
                        "Chọn HocVien\nChọn GoiTap\nNgayBatDau=hôm nay\nNgayHetHan=+30 ngày\nTrangThai=Đang hoạt động",
                        "Lưu thành công, hiển thị trong danh sách",
                        "Có HocVien và GoiTap trong DB",""),
                    new("TC_DK_02","Thêm đăng ký không chọn học viên",
                        "cboHocVien=null (không chọn)",
                        "Hiển thị cảnh báo 'Vui lòng chọn đầy đủ thông tin!'","","cboHocVien=null"),
                    new("TC_DK_03","Thêm đăng ký không chọn gói tập",
                        "cboGoiTap=null (không chọn)",
                        "Hiển thị cảnh báo 'Vui lòng chọn đầy đủ thông tin!'","","cboGoiTap=null"),
                    new("TC_DK_04","Xóa đăng ký chưa có HoaDon/PhanCong",
                        "Chọn đăng ký sạch\nXác nhận Xóa",
                        "Xóa thành công","Có đăng ký chưa có HoaDon",""),
                    new("TC_DK_05","Xóa đăng ký đang có hóa đơn (FK)",
                        "Chọn đăng ký có HoaDon\nXác nhận Xóa",
                        "Hiển thị lỗi FK rõ ràng, không xóa được",
                        "Có đăng ký đang có HoaDon",""),
                    new("TC_DK_06","Sửa trạng thái đăng ký sang 'Hết hạn'",
                        "Chọn đăng ký\nĐổi TrangThai=Hết hạn\nNhấn Lưu",
                        "Lưu thành công, cột TrangThai cập nhật",
                        "Có ít nhất 1 đăng ký","TrangThai=Hết hạn"),
                    new("TC_DK_07","[BUG#9] Tìm kiếm đăng ký gói trên UI không hoạt động",
                        "Nhập từ khóa vào ô tìm kiếm trang Đăng ký gói",
                        "[BUG] Danh sách KHÔNG thay đổi\nDangKyGoiDAO thiếu phương thức Search()",
                        "","Keyword=Nguyen"),
                }),
                ("Phan cong PT", "TC_PC", "Kiểm tra chức năng quản lý phân công PT", ClrPink, new TC[]
                {
                    new("TC_PC_01","Thêm phân công hợp lệ",
                        "Chọn HLV\nChọn DangKyGoi\nChọn CaLam\nNgayBatDau=hôm nay\nNgayKetThuc=+30 ngày",
                        "Lưu thành công, hiển thị trong danh sách",
                        "Có HLV, DangKyGoi, CaLam trong DB",""),
                    new("TC_PC_02","Thêm phân công không chọn HLV",
                        "cboHLV=null (không chọn)",
                        "Hiển thị cảnh báo 'Vui lòng chọn đầy đủ thông tin!'","","cboHLV=null"),
                    new("TC_PC_03","Thêm phân công không chọn đăng ký gói",
                        "cboDangKy=null (không chọn)",
                        "Hiển thị cảnh báo 'Vui lòng chọn đầy đủ thông tin!'","","cboDangKy=null"),
                    new("TC_PC_04","Thêm phân công không chọn ca làm (optional)",
                        "cboCaLam=null\nCác trường bắt buộc đã chọn",
                        "Lưu thành công, MaCa lưu NULL trong DB","","cboCaLam=null"),
                    new("TC_PC_05","[BUG#5] GhiChu rỗng khi thêm phân công",
                        "GhiChu=''\nCác trường khác hợp lệ",
                        "[BUG] Ném ArgumentNullException tại cmd.Parameters.AddWithValue\nFix: (object?)pc.GhiChu ?? DBNull.Value",
                        "Có HLV, DangKyGoi trong DB","GhiChu=''"),
                    new("TC_PC_06","Xóa phân công",
                        "Chọn phân công\nXác nhận Xóa",
                        "Xóa thành công, không còn trong danh sách",
                        "Có ít nhất 1 phân công trong DB",""),
                }),
                ("Hoa don", "TC_HD", "Kiểm tra chức năng quản lý hóa đơn", ClrMint, new TC[]
                {
                    new("TC_HD_01","Thêm hóa đơn hợp lệ",
                        "Chọn DangKyGoi\nNgayTT=hôm nay\nSoTien=500000\nHinhThuc=Tiền mặt",
                        "Lưu thành công, hiển thị trong danh sách",
                        "Có DangKyGoi trong DB","SoTien=500000"),
                    new("TC_HD_02","Thêm hóa đơn không chọn đăng ký gói",
                        "cboDangKy=null (không chọn)",
                        "Hiển thị cảnh báo 'Vui lòng chọn đăng ký gói!'","","cboDangKy=null"),
                    new("TC_HD_03","Thêm hóa đơn với số tiền = 0",
                        "SoTien=0",
                        "Hiển thị cảnh báo 'Số tiền phải > 0!'","","SoTien=0"),
                    new("TC_HD_04","Thêm hóa đơn với số tiền âm",
                        "SoTien=-100",
                        "Hiển thị cảnh báo 'Số tiền phải > 0!'","","SoTien=-100"),
                    new("TC_HD_05","Thêm hóa đơn với số tiền không phải số",
                        "SoTien='abc'",
                        "Hiển thị cảnh báo 'Số tiền phải > 0!'","","SoTien=abc"),
                    new("TC_HD_06","[BUG#4] GhiChu rỗng khi thêm hóa đơn",
                        "GhiChu=''\nHinhThucTT=''",
                        "[BUG] Ném ArgumentNullException tại AddWithValue\nFix: (object?)hd.GhiChu ?? DBNull.Value",
                        "Có DangKyGoi trong DB","GhiChu='' | HinhThucTT=''"),
                    new("TC_HD_07","Xóa hóa đơn",
                        "Chọn hóa đơn\nXác nhận Xóa",
                        "Xóa thành công, không còn trong danh sách",
                        "Có ít nhất 1 hóa đơn",""),
                    new("TC_HD_08","Kiểm tra tổng doanh thu trên Dashboard",
                        "Có N hóa đơn với tổng SoTien=X trong DB",
                        "Card 'Doanh thu' trên Dashboard hiển thị đúng tổng X",
                        "Có hóa đơn trong DB",""),
                }),
                ("Ca lam", "TC_CL", "Kiểm tra chức năng quản lý ca làm", ClrLemon, new TC[]
                {
                    new("TC_CL_01","Thêm ca làm hợp lệ",
                        "TenCa=Ca sáng\nGioBatDau=06:00\nGioKetThuc=12:00",
                        "Lưu thành công, hiển thị trong danh sách",
                        "","TenCa=Ca sáng | GioBatDau=06:00 | GioKetThuc=12:00"),
                    new("TC_CL_02","Thêm ca làm để trống tên ca",
                        "TenCa=''",
                        "Hiển thị cảnh báo 'Vui lòng nhập tên ca!'","","TenCa=''"),
                    new("TC_CL_03","Sửa giờ kết thúc ca làm",
                        "Chọn ca\nSửa GioKetThuc=13:00\nNhấn Lưu",
                        "Lưu thành công, danh sách cập nhật giờ kết thúc mới",
                        "Có ít nhất 1 ca làm trong DB","GioKetThuc=13:00"),
                    new("TC_CL_04","Xóa ca làm đang có phân công (FK)",
                        "Chọn ca có PhanCong\nXác nhận Xóa",
                        "Hiển thị lỗi FK rõ ràng, không xóa được",
                        "Có ca làm đang có PhanCong",""),
                    new("TC_CL_05","Xóa ca làm không có phân công",
                        "Chọn ca sạch\nXác nhận Xóa",
                        "Xóa thành công","Có ca làm không có PhanCong",""),
                }),
                ("Dashboard", "TC_DB", "Kiểm tra hiển thị thống kê Dashboard", ClrLightBlue, new TC[]
                {
                    new("TC_DB_01","Hiển thị đúng số học viên đang hoạt động",
                        "Có N học viên TrangThai=1 trong DB",
                        "Card 'Học viên' hiển thị đúng số N",
                        "Có học viên trong DB","COUNT(HocVien WHERE TrangThai=1)=N"),
                    new("TC_DB_02","Hiển thị đúng số HLV đang hoạt động",
                        "Có M HLV TrangThai=1 trong DB",
                        "Card 'Huấn luyện viên' hiển thị đúng số M",
                        "Có HLV trong DB","COUNT(HuanLuyenVien WHERE TrangThai=1)=M"),
                    new("TC_DB_03","Hiển thị đúng số gói tập đang hoạt động",
                        "Có K gói tập TrangThai=1 trong DB",
                        "Card 'Gói tập' hiển thị đúng số K",
                        "Có gói tập trong DB","COUNT(GoiTap WHERE TrangThai=1)=K"),
                    new("TC_DB_04","Hiển thị đúng số đăng ký đang hoạt động",
                        "Có P đăng ký TrangThai='Đang hoạt động'",
                        "Card 'Đăng ký đang HĐ' hiển thị đúng số P",
                        "Có đăng ký trong DB","COUNT(DangKyGoi WHERE TrangThai='Đang hoạt động')=P"),
                    new("TC_DB_05","Bảng đăng ký gần đây chỉ hiển thị 10 bản ghi",
                        "Có hơn 10 đăng ký trong DB",
                        "Bảng chỉ hiển thị 10 bản ghi mới nhất",
                        "Có > 10 đăng ký trong DB",""),
                }),
                ("Bug Report", "TC_BUG", "Xác nhận các bug đã phát hiện trong hệ thống", ClrRed, new TC[]
                {
                    new("TC_BUG_01","[BUG#1] Tên admin hiển thị sai – lỗi encoding SQL",
                        "Đăng nhập admin\nXem tên hiển thị trên TopBar",
                        "[BUG] HoTen='Qu?n tr? viên' thay vì 'Quản trị viên'\nFix: sửa setup_db.sql thành N'Quản trị viên'",
                        "Tài khoản admin trong DB","TenDangNhap=admin | MatKhau=123"),
                    new("TC_BUG_02","[BUG#2] Xóa HocVien có DangKyGoi – lỗi không thân thiện",
                        "Chọn học viên đang có DangKyGoi\nXác nhận Xóa",
                        "[BUG] App crash SqlException thô\nFix: bắt SqlException → MessageBox 'Không thể xóa vì có dữ liệu liên quan'",
                        "Có học viên đang có DangKyGoi",""),
                    new("TC_BUG_03","[BUG#3] Xóa DangKyGoi có HoaDon – lỗi không thân thiện",
                        "Chọn đăng ký đang có HoaDon\nXác nhận Xóa",
                        "[BUG] App crash SqlException thô\nFix: bắt SqlException → MessageBox thân thiện",
                        "Có đăng ký đang có HoaDon",""),
                    new("TC_BUG_04","[BUG#4] HoaDonDAO.Insert – GhiChu null ném ArgumentNullException",
                        "Thêm hóa đơn\nĐể trống GhiChu và HinhThucTT",
                        "[BUG] ArgumentNullException tại AddWithValue\nFix: (object?)hd.GhiChu ?? DBNull.Value",
                        "Có DangKyGoi trong DB","GhiChu='' | HinhThucTT=''"),
                    new("TC_BUG_05","[BUG#5] PhanCongDAO.Insert – GhiChu null ném ArgumentNullException",
                        "Thêm phân công\nĐể trống GhiChu",
                        "[BUG] ArgumentNullException tại AddWithValue\nFix: (object?)pc.GhiChu ?? DBNull.Value",
                        "Có HLV và DangKyGoi trong DB","GhiChu=''"),
                    new("TC_BUG_06","[BUG#6] Form Đăng ký gói không tự tính NgayHetHan",
                        "Chọn GoiTap có ThoiHan=30\nNhập NgayBatDau=hôm nay",
                        "[BUG] NgayHetHan không tự điền = NgayBatDau + 30 ngày\nFix: gán dtpNgayHetHan.Value khi chọn GoiTap",
                        "","ThoiHan=30 | NgayBatDau=hôm nay"),
                    new("TC_BUG_07","[BUG#7] cboDangKy trong FormHoaDonDetail chỉ hiển thị TenHV",
                        "Mở form Thêm Hóa đơn\nXem dropdown cboDangKy khi HV có 2+ đăng ký",
                        "[BUG] Không phân biệt đăng ký của cùng 1 HV\nFix: DisplayMember='TenHV - TenGoi (NgayBatDau)'",
                        "Có HV đăng ký > 1 gói",""),
                    new("TC_BUG_08","[BUG#8] Tìm kiếm trang Gói tập không hoạt động",
                        "Nhập từ khóa vào ô tìm kiếm trang Gói tập",
                        "[BUG] Danh sách KHÔNG thay đổi\nGoiTapDAO thiếu phương thức Search()\nShowGoiTap() không gán txtSearch.TextChanged",
                        "","Keyword=Goi 1 thang"),
                    new("TC_BUG_09","[BUG#9] Tìm kiếm DK gói/PhanCong/HoaDon/CaLam không hoạt động",
                        "Nhập từ khóa vào các trang: Đăng ký gói, Phân công, Hóa đơn, Ca làm",
                        "[BUG] Danh sách KHÔNG thay đổi\nCác DAO tương ứng thiếu Search()",
                        "","Keyword=Nguyen"),
                    new("TC_BUG_10","[BUG#10] Dashboard – 5 cards bị tràn khi cửa sổ nhỏ",
                        "Kéo cửa sổ về kích thước tối thiểu 1100×650",
                        "[BUG] 5 cards × 235px = 1175px > 1100px → card cuối bị cắt\nFix: giảm cardWidth hoặc dùng FlowLayout",
                        "","Window width=1100px"),
                }),
            };

            using var wb = new XLWorkbook();
            foreach (var mod in modules)
                BuildSheet(wb, mod.Name, mod.TcId, mod.Desc, mod.RowColor, mod.Cases);

            wb.SaveAs(outPath);
            Console.WriteLine($"DONE: {outPath}");
            Console.WriteLine($"Sheets ({wb.Worksheets.Count()}): {string.Join(", ", wb.Worksheets.Select(w => w.Name))}");
        }

        static void BuildSheet(XLWorkbook wb, string name, string tcId, string desc,
                               XLColor rowColor, TC[] cases)
        {
            string sheetName = name.Length > 31 ? name[..31] : name;
            var ws = wb.Worksheets.Add(sheetName);

            ws.Column(1).Width = 14;
            ws.Column(2).Width = 38;
            ws.Column(3).Width = 30;
            ws.Column(4).Width = 40;
            ws.Column(5).Width = 30;
            ws.Column(6).Width = 22;

            // ── ROW 1 ──────────────────────────────────────────────────────
            Label(ws, 1, 1, 2, "Test Case ID");
            ws.Cell(1, 3).Value = tcId;
            Border(ws.Cell(1,3).AsRange());
            Label(ws, 1, 4, 5, "Test Case Description");
            ws.Cell(1, 6).Value = desc;
            ws.Cell(1, 6).Style.Alignment.WrapText = true;
            Border(ws.Cell(1,6).AsRange());
            ws.Row(1).Height = 18;

            // ── ROW 2 ──────────────────────────────────────────────────────
            Label(ws, 2, 1, 2, "Created By");
            ws.Cell(2, 3).Value = "Sinh viên thực hiện";
            Border(ws.Cell(2,3).AsRange());
            Label(ws, 2, 4, 5, "Reviewed By");
            ws.Cell(2, 6).Value = "Giảng viên hướng dẫn";
            Border(ws.Cell(2,6).AsRange());
            ws.Row(2).Height = 18;

            // ── ROW 3: spacer ──────────────────────────────────────────────
            ws.Row(3).Height = 6;

            // ── ROW 4: QA Tester's Log ─────────────────────────────────────
            ws.Range(4,1,4,2).Merge();
            ws.Cell(4,1).Value = "QA Tester's Log";
            ws.Range(4,1,4,2).Style.Fill.BackgroundColor = ClrYellow;
            ws.Range(4,3,4,6).Style.Fill.BackgroundColor = ClrGray;
            Border(ws.Range(4,1,4,6));
            ws.Row(4).Height = 18;

            // ── ROW 5: spacer ──────────────────────────────────────────────
            ws.Row(5).Height = 6;

            // ── ROW 6: Tester's Name / Date Tested ─────────────────────────
            Label(ws, 6, 1, 2, "Tester's Name");
            ws.Cell(6, 3).Value = "";
            Border(ws.Cell(6,3).AsRange());
            Label(ws, 6, 4, 5, "Date Tested");
            ws.Cell(6, 6).Value = "Test Case (Pass/Fail/Not Executed)";
            ws.Cell(6,6).Style.Fill.BackgroundColor = ClrYellow;
            ws.Cell(6,6).Style.Alignment.WrapText   = true;
            ws.Cell(6,6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell(6,6).Style.Alignment.Vertical   = XLAlignmentVerticalValues.Center;
            Border(ws.Cell(6,6).AsRange());
            ws.Row(6).Height = 30;

            // ── ROW 7: spacer ──────────────────────────────────────────────
            ws.Row(7).Height = 6;

            // ── ROW 8: Prerequisites header ────────────────────────────────
            ws.Cell(8,1).Value = "S #";
            ws.Range(8,2,8,4).Merge();
            ws.Cell(8,2).Value = "Prerequisites";
            ws.Range(8,1,8,4).Style.Fill.BackgroundColor = ClrYellow;
            ws.Range(8,1,8,4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Range(8,1,8,4).Style.Alignment.Vertical   = XLAlignmentVerticalValues.Center;
            Border(ws.Range(8,1,8,4));
            ws.Row(8).Height = 18;

            var prereqs = cases.Select(c => c.Prerequisites)
                .Where(p => !string.IsNullOrWhiteSpace(p)).Distinct().Take(3).ToList();
            for (int i = 0; i < 3; i++)
            {
                ws.Cell(9+i,1).Value = i+1;
                ws.Cell(9+i,1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Range(9+i,2,9+i,4).Merge();
                ws.Cell(9+i,2).Value = i < prereqs.Count ? prereqs[i] : "";
                ws.Cell(9+i,2).Style.Alignment.WrapText = true;
                Border(ws.Range(9+i,1,9+i,4));
                ws.Row(9+i).Height = 18;
            }

            // ── ROW 12: spacer ─────────────────────────────────────────────
            ws.Row(12).Height = 6;

            // ── ROW 13: Test Data header ───────────────────────────────────
            ws.Cell(13,1).Value = "S #";
            ws.Range(13,2,13,4).Merge();
            ws.Cell(13,2).Value = "Test Data";
            ws.Range(13,1,13,4).Style.Fill.BackgroundColor = ClrYellow;
            ws.Range(13,1,13,4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Range(13,1,13,4).Style.Alignment.Vertical   = XLAlignmentVerticalValues.Center;
            Border(ws.Range(13,1,13,4));
            ws.Row(13).Height = 18;

            var testDatas = cases.Select(c => c.TestData)
                .Where(p => !string.IsNullOrWhiteSpace(p)).Distinct().Take(3).ToList();
            for (int i = 0; i < 3; i++)
            {
                ws.Cell(14+i,1).Value = i+1;
                ws.Cell(14+i,1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Range(14+i,2,14+i,4).Merge();
                ws.Cell(14+i,2).Value = i < testDatas.Count ? testDatas[i] : "";
                ws.Cell(14+i,2).Style.Alignment.WrapText = true;
                Border(ws.Range(14+i,1,14+i,4));
                ws.Row(14+i).Height = 18;
            }

            // ── ROW 17: spacer ─────────────────────────────────────────────
            ws.Row(17).Height = 6;

            // ── ROW 18: Table header ───────────────────────────────────────
            const int HDR = 18;
            string[] hdrCols = ["id", "Test Case", "Bộ dữ liệu", "Kết quả mong đợi", "Kết quả thực tế", "Kết quả (Pass/Fail)"];
            for (int c = 1; c <= 6; c++)
            {
                var cell = ws.Cell(HDR, c);
                cell.Value = hdrCols[c-1];
                cell.Style.Font.Bold            = true;
                cell.Style.Font.FontColor       = XLColor.White;
                cell.Style.Fill.BackgroundColor = ClrBlue;
                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                cell.Style.Alignment.Vertical   = XLAlignmentVerticalValues.Center;
                cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            }
            ws.Row(HDR).Height = 22;

            // ── ROW 19+: Data rows ─────────────────────────────────────────
            int dr = HDR + 1;
            foreach (var tc in cases)
            {
                ws.Cell(dr,1).Value = tc.Id;
                ws.Cell(dr,2).Value = tc.TenTestCase;
                ws.Cell(dr,3).Value = tc.BoduLieu;
                ws.Cell(dr,4).Value = tc.KetQuaMongDoi;
                ws.Cell(dr,5).Value = "";
                ws.Cell(dr,6).Value = "";

                for (int c = 1; c <= 6; c++)
                {
                    ws.Cell(dr,c).Style.Fill.BackgroundColor = rowColor;
                    ws.Cell(dr,c).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.Cell(dr,c).Style.Alignment.WrapText   = true;
                    ws.Cell(dr,c).Style.Alignment.Vertical   = XLAlignmentVerticalValues.Center;
                }
                ws.Cell(dr,1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell(dr,6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Row(dr).Height = 52;
                dr++;
            }

            ws.SheetView.FreezeRows(HDR);
        }

        static void Label(IXLWorksheet ws, int row, int c1, int c2, string text)
        {
            ws.Range(row, c1, row, c2).Merge();
            ws.Cell(row, c1).Value = text;
            ws.Range(row, c1, row, c2).Style.Fill.BackgroundColor = ClrYellow;
            ws.Range(row, c1, row, c2).Style.Alignment.Vertical   = XLAlignmentVerticalValues.Center;
            Border(ws.Range(row, c1, row, c2));
        }

        static void Border(IXLRange r)
        {
            r.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            r.Style.Border.InsideBorder  = XLBorderStyleValues.Thin;
        }
    }
}
