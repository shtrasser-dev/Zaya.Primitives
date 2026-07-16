using System.Globalization;

namespace Zaya.Primitives;

/// <summary>
/// Base class for exceptions that carry a localizable user-facing message.
/// Subclasses override <see cref="GetLocalizedMessage"/> to resolve translations.
/// The constructor receives only a localization key; the default culture resource file is the source of strings.
/// </summary>
public class LocalizedException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LocalizedException"/> class.
    /// </summary>
    /// <param name="key">Localization resource key (e.g. "Ocr_Err_SnippingToolNotFound").</param>
    protected LocalizedException(string key) : base(key) { }

    /// <summary>
    /// Returns the user-facing localized message for the specified culture.
    /// The base implementation returns the key as a fallback.
    /// Override to resolve via a resource manager.
    /// </summary>
    /// <param name="culture">The target culture for localization.</param>
    /// <returns>The localized error message.</returns>
    public virtual string GetLocalizedMessage(CultureInfo culture)
        => Message;
}
