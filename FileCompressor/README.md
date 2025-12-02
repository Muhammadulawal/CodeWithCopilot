# File Compressor

A .NET console application for compressing and decompressing files and directories.

## Features

- **GZip Compression**: Compress individual files using GZip compression algorithm
- **GZip Decompression**: Decompress GZip compressed files
- **Zip Archives**: Create zip archives from files or entire directories
- **Extract Archives**: Extract zip archives to specified directories
- **Compression Statistics**: View original size, compressed size, and compression ratio
- **Cross-platform**: Works on Windows, macOS, and Linux

## Requirements

- .NET 10.0 SDK or later

## Building the Application

```bash
cd FileCompressor
dotnet build
```

## Running the Application

```bash
dotnet run -- <command> <source> <destination>
```

Or after building, run the executable directly:

```bash
./bin/Debug/net10.0/FileCompressor <command> <source> <destination>
```

## Commands

### Compress a File (GZip)

Compress a single file using GZip compression:

```bash
dotnet run -- compress input.txt input.txt.gz
```

or using the short form:

```bash
dotnet run -- c input.txt input.txt.gz
```

### Decompress a File (GZip)

Decompress a GZip compressed file:

```bash
dotnet run -- decompress input.txt.gz output.txt
```

or using the short form:

```bash
dotnet run -- d input.txt.gz output.txt
```

### Create a Zip Archive

Create a zip archive from a file or directory:

```bash
dotnet run -- zip MyFolder archive.zip
dotnet run -- zip myfile.txt myfile.zip
```

or using the short form:

```bash
dotnet run -- z MyFolder archive.zip
```

### Extract a Zip Archive

Extract a zip archive to a directory:

```bash
dotnet run -- unzip archive.zip ExtractedFolder
```

or using the short form:

```bash
dotnet run -- u archive.zip ExtractedFolder
```

### Display Help

Show the help message with all available commands:

```bash
dotnet run -- help
```

## Examples

### Example 1: Compress a Text File

```bash
# Create a sample file
echo "This is a test file for compression." > sample.txt

# Compress the file
dotnet run -- compress sample.txt sample.txt.gz

# Output:
# Compressing 'sample.txt' to 'sample.txt.gz'...
# Compression completed successfully!
# Original size: 38 bytes
# Compressed size: 58 bytes
# Compression ratio: -52.63%
```

### Example 2: Create and Extract a Zip Archive

```bash
# Create a directory with files
mkdir MyDocuments
echo "Document 1" > MyDocuments/doc1.txt
echo "Document 2" > MyDocuments/doc2.txt

# Create a zip archive
dotnet run -- zip MyDocuments documents.zip

# Extract to a new location
dotnet run -- unzip documents.zip ExtractedDocs
```

### Example 3: Compress and Decompress a File

```bash
# Compress
dotnet run -- compress largefile.txt largefile.txt.gz

# Decompress
dotnet run -- decompress largefile.txt.gz restored.txt
```

## Error Handling

The application includes comprehensive error handling:

- Validates that source files/directories exist
- Provides clear error messages for missing arguments
- Handles file system exceptions gracefully
- Returns non-zero exit codes on errors

## Technical Details

- **GZip Compression**: Uses `System.IO.Compression.GZipStream` for efficient compression
- **Zip Archives**: Uses `System.IO.Compression.ZipFile` for creating and extracting archives
- **Compression Level**: Uses optimal compression level for best compression ratios
- **Overwrite Behavior**: Zip extraction overwrites existing files by default

## Performance

The application efficiently handles files of various sizes:

- Small files (< 1 MB): Near-instantaneous compression
- Medium files (1-100 MB): Processes in seconds
- Large files (> 100 MB): Streams data for memory efficiency

## Limitations

- GZip compression works best on text files and structured data
- Already compressed files (images, videos) may not compress further
- Very small files may result in larger compressed sizes due to metadata overhead

## License

This project is part of the CodeWithCopilot repository.
