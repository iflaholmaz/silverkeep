param([string]$RuntimeDirectory = '', [string]$BuildDirectory = '', [string]$OutputDirectory = '')
# xvv · Windows x64 installer build. Runtime and metadata stay outside Git.
$ErrorActionPreference = 'Stop'
$projectRoot = Split-Path -Parent $PSScriptRoot
$runtimeVersion = '44.7.0'
$buildRoot = if ($BuildDirectory) { [IO.Path]::GetFullPath($BuildDirectory) } else { Join-Path $projectRoot 'build' }
$outputRoot = if ($OutputDirectory) { [IO.Path]::GetFullPath($OutputDirectory) } else { Join-Path $projectRoot 'dist' }
$appRoot = [IO.Path]::GetFullPath((Join-Path $buildRoot 'Silverkeep'))
$compiler = Join-Path $env:WINDIR 'Microsoft.NET/Framework64/v4.0.30319/csc.exe'
if (!(Test-Path -LiteralPath $compiler)) { $compiler = Join-Path $env:WINDIR 'Microsoft.NET/Framework/v4.0.30319/csc.exe' }
if (!(Test-Path -LiteralPath $compiler)) { throw 'Windows .NET Framework C# compiler is required.' }
& node (Join-Path $PSScriptRoot 'data.cjs') --if-missing
if ($LASTEXITCODE -ne 0) { throw 'Game metadata generation failed.' }
New-Item -ItemType Directory -Force $buildRoot,$outputRoot | Out-Null
# Resolve and verify the one generated checkout folder before recursively replacing it.
if (!$appRoot.StartsWith([IO.Path]::GetFullPath($buildRoot) + [IO.Path]::DirectorySeparatorChar, [StringComparison]::OrdinalIgnoreCase)) { throw 'Unsafe build directory.' }
if (Test-Path -LiteralPath $appRoot) {
    $resolvedApp = (Resolve-Path -LiteralPath $appRoot).Path
    if ($resolvedApp -ne $appRoot -or ((Get-Item -LiteralPath $appRoot).Attributes -band [IO.FileAttributes]::ReparsePoint)) { throw 'Refusing to replace a redirected build folder.' }
    Remove-Item -LiteralPath $appRoot -Recurse -Force
}
New-Item -ItemType Directory -Force $appRoot | Out-Null
if ($RuntimeDirectory) {
    Get-ChildItem -LiteralPath ([IO.Path]::GetFullPath($RuntimeDirectory)) | Copy-Item -Destination $appRoot -Recurse -Force
} else {
    $filename = "electron-v$runtimeVersion-win32-x64.zip"
    $baseUrl = "https://github.com/electron/electron/releases/download/v$runtimeVersion"
    $archivePath = Join-Path $buildRoot $filename
    Invoke-WebRequest "$baseUrl/$filename" -OutFile $archivePath
    $checksums = (Invoke-WebRequest "$baseUrl/SHASUMS256.txt").Content
    $expected = (($checksums -split "`n" | Where-Object { $_.Trim().EndsWith($filename) }) -split '\s+')[0]
    if (!$expected -or (Get-FileHash -LiteralPath $archivePath -Algorithm SHA256).Hash.ToLower() -ne $expected.ToLower()) { throw 'Electron SHA-256 verification failed.' }
    Expand-Archive -LiteralPath $archivePath -DestinationPath $appRoot -Force
}
$runtimeExe = Join-Path $appRoot 'electron.exe'
if (Test-Path -LiteralPath $runtimeExe) { Rename-Item -LiteralPath $runtimeExe -NewName 'Silverkeep.exe' }
if (!(Test-Path -LiteralPath (Join-Path $appRoot 'Silverkeep.exe'))) { throw 'Electron executable is missing.' }
$appResources = Join-Path $appRoot 'resources/app'
New-Item -ItemType Directory -Force $appResources | Out-Null
Get-ChildItem -LiteralPath (Join-Path $projectRoot 'src') | Copy-Item -Destination $appResources -Recurse -Force
$iconPath = Join-Path $projectRoot 'assets/Silverkeep.ico'
Copy-Item -LiteralPath $iconPath -Destination (Join-Path $appRoot 'Silverkeep.ico') -Force
Copy-Item -LiteralPath $iconPath -Destination (Join-Path $appRoot 'Silverkeep-31.ico') -Force
$iconPatch = Join-Path $buildRoot 'IconPatch.exe'
& $compiler /nologo /target:exe "/out:$iconPatch" (Join-Path $PSScriptRoot 'IconPatch.cs')
if ($LASTEXITCODE -ne 0) { throw 'Icon helper compilation failed.' }
& $iconPatch (Join-Path $appRoot 'Silverkeep.exe') $iconPath
if ($LASTEXITCODE -ne 0) { throw 'Executable icon update failed.' }
$common = @('/nologo','/target:winexe',"/win32icon:$iconPath",'/reference:System.IO.Compression.dll','/reference:System.IO.Compression.FileSystem.dll','/reference:System.Windows.Forms.dll','/reference:System.Drawing.dll')
$uninstaller = Join-Path $buildRoot 'Silverkeep-Kaldir.exe'
& $compiler @common "/out:$uninstaller" (Join-Path $PSScriptRoot 'Installer.cs')
if ($LASTEXITCODE -ne 0) { throw 'Uninstaller compilation failed.' }
$payload = Join-Path $buildRoot 'payload.zip'
Compress-Archive -Path $appRoot -DestinationPath $payload -Force
$installer = Join-Path $outputRoot 'Silverkeep-Kurulum.exe'
& $compiler @common "/out:$installer" "/resource:$payload,payload.zip" "/resource:$uninstaller,uninstall.exe" "/resource:$iconPath,app.ico" (Join-Path $PSScriptRoot 'Installer.cs')
if ($LASTEXITCODE -ne 0) { throw 'Installer compilation failed.' }
$hash = (Get-FileHash -LiteralPath $installer -Algorithm SHA256).Hash.ToLower()
Set-Content -LiteralPath (Join-Path $outputRoot 'SHA256SUMS.txt') -Value "$hash  Silverkeep-Kurulum.exe" -Encoding utf8
Write-Output "Installer ready: $installer"
