using System.IO.Compression;

namespace FileCompressor
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                ShowUsage();
                return;
            }

            string command = args[0].ToLower();

            try
            {
                switch (command)
                {
                    case "compress":
                    case "c":
                        if (args.Length < 3)
                        {
                            Console.WriteLine("Error: Missing arguments for compress command.");
                            ShowUsage();
                            return;
                        }
                        CompressFile(args[1], args[2]);
                        break;

                    case "decompress":
                    case "d":
                        if (args.Length < 3)
                        {
                            Console.WriteLine("Error: Missing arguments for decompress command.");
                            ShowUsage();
                            return;
                        }
                        DecompressFile(args[1], args[2]);
                        break;

                    case "zip":
                    case "z":
                        if (args.Length < 3)
                        {
                            Console.WriteLine("Error: Missing arguments for zip command.");
                            ShowUsage();
                            return;
                        }
                        CreateZipArchive(args[1], args[2]);
                        break;

                    case "unzip":
                    case "u":
                        if (args.Length < 3)
                        {
                            Console.WriteLine("Error: Missing arguments for unzip command.");
                            ShowUsage();
                            return;
                        }
                        ExtractZipArchive(args[1], args[2]);
                        break;

                    case "help":
                    case "h":
                    case "-h":
                    case "--help":
                        ShowUsage();
                        break;

                    default:
                        Console.WriteLine($"Error: Unknown command '{command}'");
                        ShowUsage();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Environment.Exit(1);
            }
        }

        static void ShowUsage()
        {
            Console.WriteLine("File Compressor - A .NET application for compressing and decompressing files");
            Console.WriteLine();
            Console.WriteLine("Usage:");
            Console.WriteLine("  FileCompressor <command> <source> <destination>");
            Console.WriteLine();
            Console.WriteLine("Commands:");
            Console.WriteLine("  compress (c)    - Compress a single file using GZip");
            Console.WriteLine("  decompress (d)  - Decompress a GZip compressed file");
            Console.WriteLine("  zip (z)         - Create a zip archive from a file or directory");
            Console.WriteLine("  unzip (u)       - Extract a zip archive to a directory");
            Console.WriteLine("  help (h)        - Show this help message");
            Console.WriteLine();
            Console.WriteLine("Examples:");
            Console.WriteLine("  FileCompressor compress input.txt input.txt.gz");
            Console.WriteLine("  FileCompressor decompress input.txt.gz output.txt");
            Console.WriteLine("  FileCompressor zip MyFolder archive.zip");
            Console.WriteLine("  FileCompressor unzip archive.zip ExtractedFolder");
        }

        static void CompressFile(string sourceFile, string compressedFile)
        {
            if (!File.Exists(sourceFile))
            {
                throw new FileNotFoundException($"Source file not found: {sourceFile}");
            }

            Console.WriteLine($"Compressing '{sourceFile}' to '{compressedFile}'...");

            using (FileStream sourceStream = File.OpenRead(sourceFile))
            using (FileStream targetStream = File.Create(compressedFile))
            using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
            {
                sourceStream.CopyTo(compressionStream);
            }

            FileInfo sourceInfo = new FileInfo(sourceFile);
            FileInfo compressedInfo = new FileInfo(compressedFile);
            double compressionRatio = (1 - (double)compressedInfo.Length / sourceInfo.Length) * 100;

            Console.WriteLine($"Compression completed successfully!");
            Console.WriteLine($"Original size: {sourceInfo.Length:N0} bytes");
            Console.WriteLine($"Compressed size: {compressedInfo.Length:N0} bytes");
            Console.WriteLine($"Compression ratio: {compressionRatio:F2}%");
        }

        static void DecompressFile(string compressedFile, string targetFile)
        {
            if (!File.Exists(compressedFile))
            {
                throw new FileNotFoundException($"Compressed file not found: {compressedFile}");
            }

            Console.WriteLine($"Decompressing '{compressedFile}' to '{targetFile}'...");

            using (FileStream sourceStream = File.OpenRead(compressedFile))
            using (FileStream targetStream = File.Create(targetFile))
            using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
            {
                decompressionStream.CopyTo(targetStream);
            }

            Console.WriteLine("Decompression completed successfully!");
        }

        static void CreateZipArchive(string sourcePath, string zipFile)
        {
            if (!File.Exists(sourcePath) && !Directory.Exists(sourcePath))
            {
                throw new FileNotFoundException($"Source path not found: {sourcePath}");
            }

            if (File.Exists(zipFile))
            {
                File.Delete(zipFile);
            }

            Console.WriteLine($"Creating zip archive '{zipFile}' from '{sourcePath}'...");

            if (Directory.Exists(sourcePath))
            {
                ZipFile.CreateFromDirectory(sourcePath, zipFile, CompressionLevel.Optimal, false);
            }
            else
            {
                // For single file, create a zip with the file inside
                using (ZipArchive archive = ZipFile.Open(zipFile, ZipArchiveMode.Create))
                {
                    archive.CreateEntryFromFile(sourcePath, Path.GetFileName(sourcePath), CompressionLevel.Optimal);
                }
            }

            FileInfo zipInfo = new FileInfo(zipFile);
            Console.WriteLine($"Zip archive created successfully!");
            Console.WriteLine($"Archive size: {zipInfo.Length:N0} bytes");
        }

        static void ExtractZipArchive(string zipFile, string extractPath)
        {
            if (!File.Exists(zipFile))
            {
                throw new FileNotFoundException($"Zip file not found: {zipFile}");
            }

            if (!Directory.Exists(extractPath))
            {
                Directory.CreateDirectory(extractPath);
            }

            Console.WriteLine($"Extracting '{zipFile}' to '{extractPath}'...");

            ZipFile.ExtractToDirectory(zipFile, extractPath, true);

            Console.WriteLine("Extraction completed successfully!");
        }
    }
}
