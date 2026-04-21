# Build installer: publishes AppGym self-contained and builds the Inno Setup installer.
# Prereqs:
#   - .NET 8 SDK
#   - Inno Setup 6 (https://jrsoftware.org/isinfo.php). ISCC.exe must exist.
param(
    [string]$Configuration = "Release",
    [string]$Runtime = "win-x64",
    [string]$IsccPath = "C:\Program Files (x86)\Inno Setup 6\ISCC.exe"
)

$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $MyInvocation.MyCommand.Path
Set-Location $root

Write-Host "==> Restoring & publishing AppGym ($Configuration / $Runtime) ..." -ForegroundColor Cyan
$publishDir = Join-Path $root "publish\$Runtime"
if (Test-Path $publishDir) { Remove-Item $publishDir -Recurse -Force }

dotnet publish "AppGym.csproj" `
    -c $Configuration `
    -r $Runtime `
    --self-contained true `
    -p:PublishSingleFile=true `
    -p:IncludeNativeLibrariesForSelfExtract=true `
    -p:EnableCompressionInSingleFile=true `
    -o $publishDir
if ($LASTEXITCODE -ne 0) { throw "dotnet publish failed with code $LASTEXITCODE" }

if (-not (Test-Path $IsccPath)) {
    Write-Warning "Inno Setup compiler not found at: $IsccPath"
    Write-Warning "Install Inno Setup 6 from https://jrsoftware.org/isinfo.php,"
    Write-Warning "or pass -IsccPath <full path to ISCC.exe>."
    Write-Host "Published files are ready at: $publishDir"
    exit 1
}

Write-Host "==> Building installer with Inno Setup ..." -ForegroundColor Cyan
$outDir = Join-Path $root "installer_output"
if (-not (Test-Path $outDir)) { New-Item -ItemType Directory -Path $outDir | Out-Null }

& $IsccPath "installer\AppGym.iss"
if ($LASTEXITCODE -ne 0) { throw "ISCC failed with code $LASTEXITCODE" }

Write-Host "==> Done. Installer is under: $outDir" -ForegroundColor Green
Get-ChildItem $outDir -Filter *.exe | ForEach-Object { Write-Host "    $($_.FullName)" -ForegroundColor Green }
