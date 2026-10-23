# REN / Kaizen Modular - recolector corregido v03.1 (NO modifica el proyecto)
# Uso: powershell -ExecutionPolicy Bypass -File .\REN_RECOLECTAR_CODIGO_SOLO_LECTURA.ps1
#       -ProjectPath 'C:\Users\Gianfranco\Desktop\Kaizen Modular'
[CmdletBinding()]
param(
  [string]$ProjectPath = (Join-Path ([Environment]::GetFolderPath('Desktop')) 'Kaizen Modular'),
  [string]$OutputDirectory = [Environment]::GetFolderPath('Desktop')
)
Set-StrictMode -Version Latest
$ErrorActionPreference='Stop'
$root=(Resolve-Path -LiteralPath $ProjectPath).Path.TrimEnd('\')
$time=Get-Date -Format 'yyyyMMdd_HHmmss'
$temp=Join-Path $env:TEMP ("REN_Codigo_" + $time)
$zip=Join-Path $OutputDirectory ("REN_Codigo_Actual_" + $time + '.zip')
New-Item -ItemType Directory -Path $temp -Force | Out-Null
$codeExt=@('.cs','.csproj','.sql','.ts','.html','.scss','.css','.json','.md','.ps1','.yml','.yaml')
$blockedFileNames=@('appsettings.json','appsettings.Development.json','appsettings.Production.json','.env','secrets.json','launchSettings.json','credentials.json')
$blockedFolders=@('node_modules','bin','obj','dist','.git','.angular','.vs','.vscode','coverage','packages','wwwroot')
$copied=0
try {
  Write-Host "Revisando archivos de: $root" -ForegroundColor Cyan
  Get-ChildItem -LiteralPath $root -Recurse -File -ErrorAction Stop | ForEach-Object {
    $file=$_
    $relative=$file.FullName.Substring($root.Length).TrimStart('\','/')
    $segments=$relative -split '[\\/]'
    $name=$file.Name
    $ok=($codeExt -contains $file.Extension) -and (-not ($blockedFileNames -contains $name)) -and
        (-not ($name -match '(?i)(secret|password|token|private.?key|credential)')) -and
        (-not (@($segments | Where-Object { $blockedFolders -contains $_ }).Count -gt 0))
    if ($ok -and $file.Length -lt 1048576) {
      $to=Join-Path $temp $relative
      $dir=Split-Path $to -Parent
      New-Item -ItemType Directory -Path $dir -Force | Out-Null
      Copy-Item -LiteralPath $file.FullName -Destination $to
      $script:copied++
    }
  }
  if ($copied -eq 0) { throw "No se encontraron archivos de codigo para copiar. Verifique ProjectPath." }
  @"
Proyecto analizado: $root
Fecha: $(Get-Date -Format o)
Archivos copiados: $copied
Excluidos: .env, appsettings, credenciales, binarios, node_modules, bin, obj, dist y archivos grandes.
No se hicieron cambios en el proyecto o en la base de datos.
NOTA: revisar el ZIP antes de compartir por si el codigo contiene secretos escritos dentro de archivos permitidos.
"@ | Set-Content -LiteralPath (Join-Path $temp 'AUDITORIA_LEEME.txt') -Encoding UTF8
  Compress-Archive -Path (Join-Path $temp '*') -DestinationPath $zip -Force
  Write-Host "LISTO: $zip" -ForegroundColor Green
  Write-Host "Archivos: $copied. Revisar el ZIP antes de compartir."
}
finally { if(Test-Path -LiteralPath $temp){Remove-Item -LiteralPath $temp -Recurse -Force} }
