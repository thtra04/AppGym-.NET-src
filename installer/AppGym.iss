; Inno Setup script for AppGym
; Produces a standalone Windows installer (setup .exe) that installs the
; self-contained published build into Program Files.
;
; HOW TO BUILD:
;   1. Run build_installer.ps1 (it publishes the app then invokes ISCC).
;   OR
;   2. dotnet publish -c Release -r win-x64 --self-contained true `
;        -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true `
;        -o publish\win-x64 AppGym.csproj
;   3. "C:\Program Files (x86)\Inno Setup 6\ISCC.exe" installer\AppGym.iss

#define MyAppName "AppGym - Quản lý phòng Gym"
#define MyAppShortName "AppGym"
#define MyAppVersion "1.14.0"
#define MyAppPublisher "AppGym Team"
#define MyAppExeName "AppGym.exe"

[Setup]
AppId={{6F3B0DB7-2C1E-4C5A-9B4C-0D7AAE5BFA77}}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={autopf}\{#MyAppShortName}
DefaultGroupName={#MyAppShortName}
DisableProgramGroupPage=yes
OutputDir=..\installer_output
OutputBaseFilename=AppGym_Setup_{#MyAppVersion}
Compression=lzma
SolidCompression=yes
WizardStyle=modern
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
PrivilegesRequired=admin
UninstallDisplayIcon={app}\{#MyAppExeName}

[Languages]
Name: "english";    MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "Táº¡o shortcut trÃªn mÃ n hÃ¬nh"; GroupDescription: "TÃ¹y chá»n bá»• sung:"; Flags: unchecked

[Files]
; Publish output must exist at ..\publish\win-x64 before running ISCC.
Source: "..\publish\win-x64\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
; Ship DB scripts alongside so admins can (re)create the database.
Source: "..\setup_db.sql";       DestDir: "{app}\sql"; Flags: ignoreversion skipifsourcedoesntexist
Source: "..\CodeTaoBang.sql";    DestDir: "{app}\sql"; Flags: ignoreversion skipifsourcedoesntexist
Source: "..\reset_db.sql";       DestDir: "{app}\sql"; Flags: ignoreversion skipifsourcedoesntexist

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\Gá»¡ cÃ i Ä‘áº·t {#MyAppShortName}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "Cháº¡y {#MyAppShortName} ngay"; Flags: nowait postinstall skipifsilent
