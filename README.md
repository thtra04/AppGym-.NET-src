# AppGym

AppGym la ung dung WinForms quan ly phong gym, ho tro dang nhap, hoc vien, huan luyen vien, goi tap, dang ky goi, phan cong PT, hoa don va dong goi cai dat bang Inno Setup.

## Yeu Cau

- Windows 10/11.
- .NET 8 SDK neu chay tu source.
- SQL Server Express, LocalDB hoac SQL Server Developer.

## Cai Dat Bang Bo Setup

1. Mo file `installer_output\\AppGym_Setup_<version>.exe`.
2. Cai ung dung theo wizard.
3. Mo AppGym sau khi cai xong.
4. Neu app chua ket noi duoc database, man hinh cau hinh SQL Server se hien ra.
5. Chon `Server / Instance` phu hop, vi du:
   - `(local)\\SQLEXPRESS`
   - `.\\SQLEXPRESS`
   - `(localdb)\\MSSQLLocalDB`
6. Giu ten database mac dinh `GymManagementDB` neu khong co yeu cau khac.
7. Bam `Kiem tra` de thu ket noi.
8. Neu server ket noi duoc nhung chua co database, bam `Tao DB moi`.
9. Sau khi tao xong, bam `Luu & Tiep tuc`.

Tai khoan mac dinh sau khi tao database moi:

- `admin`
- `admin`

## Chay Tu Source

1. Mo PowerShell tai thu muc goc repo `AppGym`.
2. Restore va build:

```powershell
dotnet restore
dotnet build AppGym.csproj
```

3. Chay ung dung:

```powershell
dotnet run --project AppGym.csproj
```

Lan chay dau, neu chua co cau hinh ket noi phu hop, app se yeu cau ban cau hinh SQL Server va co the tu tao database tu `setup_db.sql`.

## File Database

Repo hien co cac script ho tro database:

- `setup_db.sql`: tao cau truc va du lieu khoi tao.
- `reset_db.sql`: reset lai du lieu mau.

## Dong Goi Setup

Neu muon tao lai bo cai:

```powershell
./build_installer.ps1
```

Neu `ISCC.exe` khong nam o duong dan mac dinh, truyen them tham so:

```powershell
./build_installer.ps1 -IsccPath "C:\\Users\\Admin\\AppData\\Local\\Programs\\Inno Setup 6\\ISCC.exe"
```

Chi tiet hon co the xem them trong `installer\\README.md`.
