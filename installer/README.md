# AppGym – Đóng gói cài đặt (Setup .exe)

Hướng dẫn tạo bộ cài đặt `AppGym_Setup_<version>.exe` cho người dùng cuối.

## Yêu cầu
1. **.NET 8 SDK** (đã cài sẵn trên máy build).
2. **Inno Setup 6** – tải tại https://jrsoftware.org/isinfo.php. Sau khi cài,
   chương trình biên dịch (`ISCC.exe`) mặc định nằm tại:
   `C:\Program Files (x86)\Inno Setup 6\ISCC.exe`.

## Các bước
Mở PowerShell trong thư mục `AppGym/`, rồi chạy:

```powershell
./build_installer.ps1
```

Script sẽ:
1. `dotnet publish` phát hành bản **self-contained** (`win-x64`, single-file, đã
   gộp runtime .NET) vào `publish\win-x64\`.
2. Gọi Inno Setup để tạo `installer_output\AppGym_Setup_<version>.exe`.

Nếu `ISCC.exe` cài ở vị trí khác, truyền tham số:
```powershell
./build_installer.ps1 -IsccPath "D:\Tools\InnoSetup\ISCC.exe"
```

## Yêu cầu môi trường người dùng cuối
- Windows 10/11 64-bit.
- SQL Server (Express/LocalDB/Developer) với database `GymManagementDB` đã chạy
  `setup_db.sql` (đi kèm thư mục `sql` trong bản cài).
- Cho phép kết nối Windows Authentication tới `(local)\SQLEXPRESS` (có thể đổi
  chuỗi kết nối trong `DataAccess/DatabaseHelper.cs` nếu khác).

## Cấu trúc sau khi cài
```
C:\Program Files\AppGym\
├─ AppGym.exe
├─ (runtime + libs)
└─ sql\
   ├─ setup_db.sql
   ├─ CodeTaoBang.sql
   └─ reset_db.sql
```

## Phiên bản
Đổi `#define MyAppVersion` trong `installer/AppGym.iss` để tăng phiên bản bản cài.
