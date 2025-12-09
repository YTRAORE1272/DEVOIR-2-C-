# Script pour exécuter le projet GesBanqueAspNet
# Utilise le runtime et SDK installés dans %USERPROFILE%\dotnet

param(
    [string]$Mode = "run",  # "run" ou "build"
    [switch]$Stop           # Arrêter l'application si elle tourne
)

$dotnetPath = "$env:USERPROFILE\dotnet\dotnet.exe"
$projectPath = ".\GesBanqueAspNet\GesBanqueAspNet.csproj"
$dllPath = ".\GesBanqueAspNet\bin\Debug\net8.0\GesBanqueAspNet.dll"

if ($Stop) {
    Write-Host "Arrêt de l'application..." -ForegroundColor Yellow
    Get-Process dotnet -ErrorAction SilentlyContinue | Stop-Process -Force
    exit 0
}

if ($Mode -eq "build") {
    Write-Host "Compilation du projet..." -ForegroundColor Cyan
    & $dotnetPath build $projectPath
    exit $LASTEXITCODE
}

# Mode "run" (par défaut)
Write-Host "Démarrage de l'application GesBanqueAspNet..." -ForegroundColor Green
Write-Host "URL d'accès: http://localhost:5000" -ForegroundColor Cyan
Write-Host "Appuyez sur Ctrl+C pour arrêter l'application." -ForegroundColor Yellow
Write-Host ""

& $dotnetPath $dllPath
