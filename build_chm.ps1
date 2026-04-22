param(
    [string]$CompilerPath = "$env:TEMP\htmlhelp_extract\hhc.exe"
)

$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $MyInvocation.MyCommand.Path
$sourceDir = Join-Path $root "help\chm"
$outputDir = Join-Path $root "help_output"
$projectFile = Join-Path $sourceDir "AppGymHelp.hhp"
$builderScript = Join-Path $root "tools\build_chm_help.py"

if (!(Test-Path $CompilerPath)) {
    throw "Khong tim thay CHM compiler tai: $CompilerPath"
}

if (!(Test-Path $builderScript)) {
    throw "Khong tim thay script build help: $builderScript"
}

if (!(Test-Path $outputDir)) {
    New-Item -ItemType Directory -Path $outputDir | Out-Null
}

Write-Host "==> Generating HTML help pages ..." -ForegroundColor Cyan
py $builderScript

Write-Host "==> Building CHM ..." -ForegroundColor Cyan
Push-Location $sourceDir
try {
    & $CompilerPath $projectFile | Out-Host
}
finally {
    Pop-Location
}

$chmPath = Join-Path $outputDir "AppGym_Help.chm"
if (!(Test-Path $chmPath)) {
    throw "Build CHM that bai. Khong tao duoc file: $chmPath"
}

Write-Host "==> Done." -ForegroundColor Green
Write-Host "    $chmPath"
