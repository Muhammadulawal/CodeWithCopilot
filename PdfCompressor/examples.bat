@echo off
REM Example usage script for PDF Compressor
REM This script demonstrates various ways to use the PDF compression tool

echo PDF Compressor - Usage Examples
echo ================================
echo.

REM Check if dotnet is installed
where dotnet >nul 2>&1
if %ERRORLEVEL% NEQ 0 (
    echo Error: .NET SDK is not installed
    exit /b 1
)

REM Navigate to the application directory
cd /d "%~dp0\PdfCompressor"

echo Building the application...
dotnet build -c Release >nul 2>&1

if %ERRORLEVEL% NEQ 0 (
    echo Error: Build failed
    exit /b 1
)

echo Build successful!
echo.
echo Usage examples:
echo.
echo 1. Basic compression with default settings:
echo    dotnet run -- input.pdf output.pdf
echo.
echo 2. Maximum compression (low quality, remove all extras):
echo    dotnet run -- input.pdf output.pdf --quality=low --jpeg-quality=60 --remove-embedded=true --flatten-forms=true
echo.
echo 3. High quality compression (preserve quality, optimize structure):
echo    dotnet run -- input.pdf output.pdf --quality=high --jpeg-quality=90
echo.
echo 4. Compress and remove annotations:
echo    dotnet run -- input.pdf output.pdf --remove-annotations=true
echo.
echo 5. Compress with form flattening:
echo    dotnet run -- input.pdf output.pdf --flatten-forms=true
echo.
echo 6. Minimal compression (preserve metadata):
echo    dotnet run -- input.pdf output.pdf --remove-metadata=false --remove-thumbnails=false
echo.
echo.
echo To use the application, replace 'input.pdf' and 'output.pdf' with your actual file paths.
echo For more information, see the README.md file.
