namespace Zaya.Primitives;

/// <summary>
/// Represents a pixel format with conversion methods.
/// </summary>
public readonly struct PixelFormat
{
    private readonly int _bytesPerPixel;
    private readonly string _name;

    private PixelFormat(int bytesPerPixel, string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        _bytesPerPixel = bytesPerPixel;
        _name = name;
    }

    /// <summary>
    /// Gets the number of bytes per pixel.
    /// </summary>
    public int BytesPerPixel => _bytesPerPixel;

    /// <summary>
    /// Gets the name of the format.
    /// </summary>
    public string Name => _name;

    /// <summary>
    /// 32-bit BGRA: 8 bits each for Blue, Green, Red, Alpha.
    /// Standard format returned by DirectX capture.
    /// </summary>
    public static readonly PixelFormat Bgra32 = new(4, "Bgra32");

    /// <summary>
    /// 24-bit RGB: 8 bits each for Red, Green, Blue. No alpha.
    /// Common format for OCR and image processing.
    /// </summary>
    public static readonly PixelFormat Rgb24 = new(3, "Rgb24");

    /// <summary>
    /// 24-bit BGR: 8 bits each for Blue, Green, Red. No alpha.
    /// Some OCR libraries (like PaddleOCR) expect BGR order.
    /// </summary>
    public static readonly PixelFormat Bgr24 = new(3, "Bgr24");

    /// <summary>
    /// 8-bit grayscale: 1 byte per pixel.
    /// Fastest for OCR, reduces memory usage by 75% compared to BGRA.
    /// </summary>
    public static readonly PixelFormat Gray8 = new(1, "Gray8");

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is PixelFormat other && _name == other._name;
    /// <inheritdoc/>
    public override int GetHashCode() => _name.GetHashCode();
    /// <summary>
    /// Determines whether two <see cref="PixelFormat"/> values are equal.
    /// </summary>
    public static bool operator ==(PixelFormat left, PixelFormat right) => left.Equals(right);
    /// <summary>
    /// Determines whether two <see cref="PixelFormat"/> values are not equal.
    /// </summary>
    public static bool operator !=(PixelFormat left, PixelFormat right) => !left.Equals(right);
}
