namespace KMeansImageCompression;

/// <summary>
///     Struct to store pixel color values.
/// </summary>
/// <param name="r">Red color amount of this pixel.</param>
/// <param name="g">Green color amount of this pixel.</param>
/// <param name="b">Blue color amount of this pixel.</param>
public readonly struct Pixel(byte r, byte g, byte b)
{
    /// <summary>
    ///     Red-amount of this pixel.
    /// </summary>
    public byte R { get; } = r;

    /// <summary>
    ///     Green-amount of this pixel.
    /// </summary>
    public byte G { get; } = g;

    /// <summary>
    ///     Blue-amount of this pixel.
    /// </summary>
    public byte B { get; } = b;

    /// <summary>
    ///     Return a string representation if this <see cref="Pixel" />.
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $"Pixel({R}, {G}, {B})";
}