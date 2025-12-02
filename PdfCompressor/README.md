# PDF Compressor - .NET 8 Application

A comprehensive .NET 8 console application that performs true PDF compression through actual PDF structure optimization, not just ZIP/Gzip/LZMA compression.

## Features

### Core Compression Capabilities

1. **Image Optimization**
   - Downscale embedded images with configurable quality levels (High/Medium/Low)
   - JPEG recompression with adjustable quality (0-100)
   - Intelligent DPI-based downsampling

2. **Resource Cleanup**
   - Remove PDF metadata (author, title, creation date, etc.)
   - Remove annotations (optional)
   - Remove embedded thumbnails
   - Remove unused fonts
   - Remove JavaScript and embedded files (optional)

3. **Form Optimization**
   - Flatten form fields to reduce file size (optional)

4. **PDF Structure Optimization**
   - Optimize PDF object streams using full compression mode
   - Compress content streams using Flate compression
   - Remove unnecessary PDF objects

## Requirements

- .NET 8.0 SDK or later
- iText7 library (automatically installed via NuGet)

## Installation

1. Clone the repository
2. Navigate to the PdfCompressor directory
3. Build the project:
   ```bash
   cd PdfCompressor/PdfCompressor
   dotnet build
   ```

## Usage

### Basic Usage

```bash
dotnet run -- <input.pdf> <output.pdf>
```

### Advanced Usage with Options

```bash
dotnet run -- <input.pdf> <output.pdf> [options]
```

### Command Line Options

| Option | Description | Default | Values |
|--------|-------------|---------|--------|
| `--quality` | Image quality level for downsampling | medium | high, medium, low |
| `--jpeg-quality` | JPEG compression quality | 75 | 0-100 |
| `--remove-metadata` | Remove PDF metadata | true | true, false |
| `--remove-annotations` | Remove annotations | false | true, false |
| `--remove-thumbnails` | Remove embedded thumbnails | true | true, false |
| `--remove-fonts` | Remove unused fonts | true | true, false |
| `--remove-embedded` | Remove embedded files & JavaScript | false | true, false |
| `--flatten-forms` | Flatten form fields | false | true, false |
| `--optimize-streams` | Optimize PDF object streams | true | true, false |
| `--compress-content` | Compress content streams | true | true, false |

### Quality Levels

- **High**: 200 DPI - Best quality, larger file size
- **Medium**: 150 DPI - Balanced quality and size
- **Low**: 72 DPI - Smallest size, lower quality

## Examples

### Example 1: Basic compression with default settings
```bash
dotnet run -- input.pdf output.pdf
```

### Example 2: Maximum compression
```bash
dotnet run -- input.pdf output.pdf --quality=low --jpeg-quality=60 --remove-embedded=true --flatten-forms=true
```

### Example 3: High quality with annotation removal
```bash
dotnet run -- input.pdf output.pdf --quality=high --remove-annotations=true
```

### Example 4: Preserve most features, only optimize structure
```bash
dotnet run -- input.pdf output.pdf --remove-metadata=false --remove-thumbnails=false
```

## How It Works

### 1. Image Downsampling
The application processes all images in the PDF and downsamples them based on the selected quality level. Images are intelligently scaled to the target DPI to reduce file size while maintaining readability.

### 2. Metadata Cleanup
Removes or clears document metadata including:
- Author, Title, Subject, Keywords
- Creator and Producer information
- Creation and modification dates
- XMP metadata streams

### 3. Resource Removal
Removes unnecessary resources that increase file size:
- Page thumbnails
- Unused font definitions
- JavaScript actions
- Embedded files
- Annotations (if enabled)

### 4. PDF Structure Optimization
Optimizes the internal PDF structure:
- **Object Stream Compression**: Uses PDF 1.5+ object streams to compress multiple PDF objects together
- **Content Stream Compression**: Applies Flate (deflate) compression to page content streams
- **Full Compression Mode**: Enables maximum compression for the entire PDF structure

### 5. Form Flattening
Optionally converts interactive form fields into static content, removing the form structure and reducing file size.

## Output

After compression, the application displays:
- Original file size
- Compressed file size
- Compression ratio (percentage)
- Space saved

Example output:
```
✓ Compression completed successfully!

Original Size: 5.42 MB
Compressed Size: 2.15 MB
Compression Ratio: 60.33%
Space Saved: 3.27 MB
```

## Technical Details

### Libraries Used
- **iText7**: Industry-standard PDF manipulation library
- **BouncyCastle**: Cryptography provider for PDF operations

### Compression Techniques

This application performs **true PDF optimization**, not simple ZIP compression:

1. **Structural Optimization**: Reorganizes PDF objects for better compression
2. **Stream Compression**: Uses Flate (deflate) algorithm on content streams
3. **Resource Deduplication**: Removes duplicate or unused resources
4. **Image Optimization**: Downsamples and recompresses embedded images
5. **Object Pooling**: Uses object streams to group similar objects

### Differences from ZIP Compression

Unlike tools that simply ZIP compress a PDF:
- Modifies the actual PDF structure
- Removes unused PDF objects
- Optimizes image quality and resolution
- Restructures content streams
- Applies PDF-specific compression techniques

## License

This project uses iText7, which has its own licensing terms. Please refer to the iText7 license for commercial use.

## Contributing

Contributions are welcome! Please submit pull requests or open issues for bugs and feature requests.

## Limitations

- Some heavily encrypted or protected PDFs may not be compressible
- Form flattening is irreversible - keep original PDFs if you need editable forms
- Very large PDFs may require significant processing time
- Some proprietary PDF features may not be fully supported

## Troubleshooting

### Build Errors
Ensure you have .NET 8.0 SDK installed:
```bash
dotnet --version
```

### Runtime Errors
- Verify input PDF file exists and is readable
- Ensure you have write permissions for the output directory
- Check that the PDF is not password-protected or encrypted

## Support

For issues or questions, please open an issue on the GitHub repository.
