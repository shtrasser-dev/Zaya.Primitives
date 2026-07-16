namespace Zaya.Primitives;

/// <summary>
/// Represents a single option in an enum-style setting, pairing a machine-readable value
/// with a localizable display name and optional description.
/// </summary>
public class EnumOption
{
    /// <summary>
    /// Gets the machine-readable value (e.g. "snippingtool", "directory").
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Gets the localizable display name shown in the UI.
    /// </summary>
    public LocalizedString DisplayName { get; }

    /// <summary>
    /// Gets the optional localizable description (tooltip / help text).
    /// </summary>
    public LocalizedString? Description { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="EnumOption"/> class.
    /// </summary>
    /// <param name="value">Machine-readable option value.</param>
    /// <param name="displayName">UI display name.</param>
    /// <param name="description">Optional description (tooltip).</param>
    public EnumOption(string value, LocalizedString displayName, LocalizedString? description = null)
    {
        Value = value;
        DisplayName = displayName;
        Description = description;
    }
}
