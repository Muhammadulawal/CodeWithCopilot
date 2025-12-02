namespace PdfCompressor;

/// <summary>
/// Configuration options for PDF compression
/// </summary>
public class CompressionOptions
{
    /// <summary>
    /// Image quality level for downsampling
    /// </summary>
    public enum ImageQuality
    {
        High,    // 200 DPI
        Medium,  // 150 DPI
        Low      // 72 DPI
    }

    /// <summary>
    /// Quality level for image downsampling (default: Medium)
    /// </summary>
    public ImageQuality Quality { get; set; } = ImageQuality.Medium;

    /// <summary>
    /// JPEG compression quality (0-100, default: 75)
    /// </summary>
    public int JpegQuality { get; set; } = 75;

    /// <summary>
    /// Remove PDF metadata (default: true)
    /// </summary>
    public bool RemoveMetadata { get; set; } = true;

    /// <summary>
    /// Remove annotations (default: false)
    /// </summary>
    public bool RemoveAnnotations { get; set; } = false;

    /// <summary>
    /// Remove embedded thumbnails (default: true)
    /// </summary>
    public bool RemoveThumbnails { get; set; } = true;

    /// <summary>
    /// Remove unused fonts (default: true)
    /// </summary>
    public bool RemoveUnusedFonts { get; set; } = true;

    /// <summary>
    /// Remove JavaScript and embedded files (default: false)
    /// </summary>
    public bool RemoveEmbeddedFiles { get; set; } = false;

    /// <summary>
    /// Flatten form fields (default: false)
    /// </summary>
    public bool FlattenForms { get; set; } = false;

    /// <summary>
    /// Optimize PDF object streams (default: true)
    /// </summary>
    public bool OptimizeObjectStreams { get; set; } = true;

    /// <summary>
    /// Compress content streams using Flate compression (default: true)
    /// </summary>
    public bool CompressContentStreams { get; set; } = true;

    /// <summary>
    /// Get the target DPI based on quality setting
    /// </summary>
    public int GetTargetDpi()
    {
        return Quality switch
        {
            ImageQuality.High => 200,
            ImageQuality.Medium => 150,
            ImageQuality.Low => 72,
            _ => 150
        };
    }
}
