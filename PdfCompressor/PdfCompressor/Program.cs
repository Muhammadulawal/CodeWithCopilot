using PdfCompressor;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("PDF Compression Tool - .NET 8");
        Console.WriteLine("================================\n");

        if (args.Length < 2)
        {
            ShowUsage();
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Parse compression options from command line arguments
        var options = ParseOptions(args);

        try
        {
            Console.WriteLine($"Input file: {inputPath}");
            Console.WriteLine($"Output file: {outputPath}");
            Console.WriteLine("\nCompression settings:");
            Console.WriteLine($"  Image Quality: {options.Quality}");
            Console.WriteLine($"  JPEG Quality: {options.JpegQuality}%");
            Console.WriteLine($"  Remove Metadata: {options.RemoveMetadata}");
            Console.WriteLine($"  Remove Annotations: {options.RemoveAnnotations}");
            Console.WriteLine($"  Remove Thumbnails: {options.RemoveThumbnails}");
            Console.WriteLine($"  Remove Unused Fonts: {options.RemoveUnusedFonts}");
            Console.WriteLine($"  Remove Embedded Files: {options.RemoveEmbeddedFiles}");
            Console.WriteLine($"  Flatten Forms: {options.FlattenForms}");
            Console.WriteLine($"  Optimize Object Streams: {options.OptimizeObjectStreams}");
            Console.WriteLine($"  Compress Content Streams: {options.CompressContentStreams}");
            Console.WriteLine("\nProcessing...");

            var compressor = new PdfCompressorEngine(options);
            var result = compressor.Compress(inputPath, outputPath);

            Console.WriteLine("\n✓ Compression completed successfully!\n");
            Console.WriteLine(result.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n✗ Error: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            Environment.Exit(1);
        }
    }

    static void ShowUsage()
    {
        Console.WriteLine("Usage: PdfCompressor <input.pdf> <output.pdf> [options]\n");
        Console.WriteLine("Options:");
        Console.WriteLine("  --quality=<high|medium|low>     Image quality (default: medium)");
        Console.WriteLine("  --jpeg-quality=<0-100>          JPEG compression quality (default: 75)");
        Console.WriteLine("  --remove-metadata=<true|false>  Remove metadata (default: true)");
        Console.WriteLine("  --remove-annotations=<true|false> Remove annotations (default: false)");
        Console.WriteLine("  --remove-thumbnails=<true|false> Remove thumbnails (default: true)");
        Console.WriteLine("  --remove-fonts=<true|false>     Remove unused fonts (default: true)");
        Console.WriteLine("  --remove-embedded=<true|false>  Remove embedded files & JS (default: false)");
        Console.WriteLine("  --flatten-forms=<true|false>    Flatten form fields (default: false)");
        Console.WriteLine("  --optimize-streams=<true|false> Optimize object streams (default: true)");
        Console.WriteLine("  --compress-content=<true|false> Compress content streams (default: true)");
        Console.WriteLine("\nExamples:");
        Console.WriteLine("  PdfCompressor input.pdf output.pdf");
        Console.WriteLine("  PdfCompressor input.pdf output.pdf --quality=low --jpeg-quality=60");
        Console.WriteLine("  PdfCompressor input.pdf output.pdf --remove-annotations=true --flatten-forms=true");
    }

    static CompressionOptions ParseOptions(string[] args)
    {
        var options = new CompressionOptions();

        foreach (var arg in args.Skip(2))
        {
            if (arg.StartsWith("--"))
            {
                var parts = arg.Substring(2).Split('=');
                if (parts.Length == 2)
                {
                    string key = parts[0].ToLower();
                    string value = parts[1].ToLower();

                    switch (key)
                    {
                        case "quality":
                            if (Enum.TryParse<CompressionOptions.ImageQuality>(value, true, out var quality))
                            {
                                options.Quality = quality;
                            }
                            break;
                        case "jpeg-quality":
                            if (int.TryParse(value, out int jpegQuality) && jpegQuality >= 0 && jpegQuality <= 100)
                            {
                                options.JpegQuality = jpegQuality;
                            }
                            break;
                        case "remove-metadata":
                            options.RemoveMetadata = ParseBool(value);
                            break;
                        case "remove-annotations":
                            options.RemoveAnnotations = ParseBool(value);
                            break;
                        case "remove-thumbnails":
                            options.RemoveThumbnails = ParseBool(value);
                            break;
                        case "remove-fonts":
                            options.RemoveUnusedFonts = ParseBool(value);
                            break;
                        case "remove-embedded":
                            options.RemoveEmbeddedFiles = ParseBool(value);
                            break;
                        case "flatten-forms":
                            options.FlattenForms = ParseBool(value);
                            break;
                        case "optimize-streams":
                            options.OptimizeObjectStreams = ParseBool(value);
                            break;
                        case "compress-content":
                            options.CompressContentStreams = ParseBool(value);
                            break;
                    }
                }
            }
        }

        return options;
    }

    static bool ParseBool(string value)
    {
        return value == "true" || value == "1" || value == "yes";
    }
}
