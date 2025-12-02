#!/bin/bash

# Example usage script for PDF Compressor
# This script demonstrates various ways to use the PDF compression tool

echo "PDF Compressor - Usage Examples"
echo "================================"
echo ""

# Check if dotnet is installed
if ! command -v dotnet &> /dev/null; then
    echo "Error: .NET SDK is not installed"
    exit 1
fi

# Navigate to the application directory
cd "$(dirname "$0")/PdfCompressor"

echo "Building the application..."
dotnet build -c Release > /dev/null 2>&1

if [ $? -ne 0 ]; then
    echo "Error: Build failed"
    exit 1
fi

echo "Build successful!"
echo ""
echo "Usage examples:"
echo ""
echo "1. Basic compression with default settings:"
echo "   dotnet run -- input.pdf output.pdf"
echo ""
echo "2. Maximum compression (low quality, remove all extras):"
echo "   dotnet run -- input.pdf output.pdf --quality=low --jpeg-quality=60 --remove-embedded=true --flatten-forms=true"
echo ""
echo "3. High quality compression (preserve quality, optimize structure):"
echo "   dotnet run -- input.pdf output.pdf --quality=high --jpeg-quality=90"
echo ""
echo "4. Compress and remove annotations:"
echo "   dotnet run -- input.pdf output.pdf --remove-annotations=true"
echo ""
echo "5. Compress with form flattening:"
echo "   dotnet run -- input.pdf output.pdf --flatten-forms=true"
echo ""
echo "6. Minimal compression (preserve metadata):"
echo "   dotnet run -- input.pdf output.pdf --remove-metadata=false --remove-thumbnails=false"
echo ""
echo ""
echo "To use the application, replace 'input.pdf' and 'output.pdf' with your actual file paths."
echo "For more information, see the README.md file."
