using System.Globalization;

namespace Zaya.Primitives;

/// <summary>
/// Represents a string that can be localized. Supports two modes:
/// <list type="bullet">
/// <item><description>Localized via a resolver function (typically a .resx <c>ResourceManager</c>).</description></item>
/// <item><description>Invariant — created via <see cref="Invariant"/> and never translated.</description></item>
/// </list>
/// All source strings live in resource files; no English defaults are embedded in code.
/// </summary>
public sealed class LocalizedString
{
    /// <summary>
    /// Gets the resource key used for localization. For invariant strings this equals the invariant text itself.
    /// </summary>
    public string Key { get; }

    private readonly Func<CultureInfo, string>? _resolver;
    private readonly string? _invariant;

    private LocalizedString(string invariant)
    {
        _invariant = invariant;
        Key = invariant;
    }

    /// <summary>
    /// Creates an invariant string that is never localized (e.g. log messages, internal IDs).
    /// </summary>
    /// <param name="text">The invariant text.</param>
    /// <returns>A <see cref="LocalizedString"/> that always returns <paramref name="text"/>.</returns>
    public static LocalizedString Invariant(string text) => new(text);

    /// <summary>
    /// Initializes a new instance of the <see cref="LocalizedString"/> class with a resolver.
    /// </summary>
    /// <param name="key">Resource key for lookup (e.g. "Ocr_EngineName"). Used as fallback when the resolver returns null or empty.</param>
    /// <param name="resolver">Function that resolves the key to a localized string for a given culture.</param>
    public LocalizedString(string key, Func<CultureInfo, string> resolver)
    {
        Key = key;
        _resolver = resolver;
    }

    /// <summary>
    /// Returns the localized value for the specified culture.
    /// Falls back to <see cref="Key"/> when the resolver returns null or empty.
    /// </summary>
    /// <param name="culture">The target culture.</param>
    /// <returns>The localized or invariant string.</returns>
    public string GetValue(CultureInfo culture)
    {
        if (_invariant is not null) return _invariant;
        var result = _resolver!(culture);
        return string.IsNullOrEmpty(result) ? Key : result;
    }
}
