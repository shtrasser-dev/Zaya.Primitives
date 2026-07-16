namespace Zaya.Primitives;

/// <summary>
/// Abstract base class for describing a configurable engine setting.
/// Subclasses carry type-specific properties (e.g. <see cref="StringSettingDescriptor.MinLength"/>,
/// <see cref="IntegerSettingDescriptor.MinValue"/>, <see cref="EnumSettingDescriptor.Options"/>).
/// Hosts use pattern matching on the concrete type to render the appropriate UI control.
/// </summary>
public abstract class SettingDescriptor
{
    /// <summary>
    /// Gets the machine-readable key for this setting (e.g. "source", "directoryPath").
    /// </summary>
    public string Key { get; }

    /// <summary>
    /// Gets the localizable display name shown as the field label in the UI.
    /// </summary>
    public LocalizedString DisplayName { get; }

    /// <summary>
    /// Gets or sets the optional localizable description (tooltip / help text).
    /// </summary>
    public LocalizedString? Description { get; init; }

    /// <summary>
    /// Gets or sets whether this setting must be filled before the engine can be initialized.
    /// </summary>
    public bool IsRequired { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingDescriptor"/> class.
    /// </summary>
    /// <param name="key">Machine-readable key for this setting.</param>
    /// <param name="displayName">Localizable UI label.</param>
    protected SettingDescriptor(string key, LocalizedString displayName)
    {
        Key = key;
        DisplayName = displayName;
    }
}

/// <summary>
/// Descriptor for a free-text string setting. Supports min/max length and optional regex validation.
/// </summary>
public sealed class StringSettingDescriptor : SettingDescriptor
{
    /// <summary>
    /// Gets or sets the default text value.
    /// </summary>
    public string? DefaultValue { get; init; }

    /// <summary>
    /// Gets or sets the minimum allowed string length (inclusive).
    /// </summary>
    public int? MinLength { get; init; }

    /// <summary>
    /// Gets or sets the maximum allowed string length (inclusive).
    /// </summary>
    public int? MaxLength { get; init; }

    /// <summary>
    /// Gets or sets an optional regex pattern for validation.
    /// </summary>
    public string? RegexPattern { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="StringSettingDescriptor"/> class.
    /// </summary>
    /// <param name="key">Machine-readable key for this setting.</param>
    /// <param name="displayName">Localizable UI label.</param>
    public StringSettingDescriptor(string key, LocalizedString displayName) : base(key, displayName) { }
}

/// <summary>
/// Descriptor for an integer numeric setting. Supports min/max range clamping.
/// </summary>
public sealed class IntegerSettingDescriptor : SettingDescriptor
{
    /// <summary>
    /// Gets or sets the default integer value.
    /// </summary>
    public int? DefaultValue { get; init; }

    /// <summary>
    /// Gets or sets the minimum allowed value (inclusive).
    /// </summary>
    public int? MinValue { get; init; }

    /// <summary>
    /// Gets or sets the maximum allowed value (inclusive).
    /// </summary>
    public int? MaxValue { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="IntegerSettingDescriptor"/> class.
    /// </summary>
    /// <param name="key">Machine-readable key for this setting.</param>
    /// <param name="displayName">Localizable UI label.</param>
    public IntegerSettingDescriptor(string key, LocalizedString displayName) : base(key, displayName) { }
}

/// <summary>
/// Descriptor for a boolean toggle setting. Rendered as a checkbox.
/// </summary>
public sealed class BooleanSettingDescriptor : SettingDescriptor
{
    /// <summary>
    /// Gets or sets the default boolean state.
    /// </summary>
    public bool DefaultValue { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BooleanSettingDescriptor"/> class.
    /// </summary>
    /// <param name="key">Machine-readable key for this setting.</param>
    /// <param name="displayName">Localizable UI label.</param>
    public BooleanSettingDescriptor(string key, LocalizedString displayName) : base(key, displayName) { }
}

/// <summary>
/// Descriptor for a dropdown / combo-box setting. The <see cref="Options"/> list defines the available choices.
/// </summary>
public sealed class EnumSettingDescriptor : SettingDescriptor
{
    /// <summary>
    /// Gets or sets the default selected option value (must match one of the <see cref="Options"/> values).
    /// </summary>
    public string? DefaultValue { get; init; }

    /// <summary>
    /// Gets or sets the list of available choices for this setting.
    /// </summary>
    public IReadOnlyList<EnumOption> Options { get; init; } = [];

    /// <summary>
    /// Initializes a new instance of the <see cref="EnumSettingDescriptor"/> class.
    /// </summary>
    /// <param name="key">Machine-readable key for this setting.</param>
    /// <param name="displayName">Localizable UI label.</param>
    public EnumSettingDescriptor(string key, LocalizedString displayName) : base(key, displayName) { }
}

/// <summary>
/// Descriptor for a file path setting. Rendered as a text box with a "Browse..." button.
/// </summary>
public sealed class FilePathSettingDescriptor : SettingDescriptor
{
    /// <summary>
    /// Gets or sets the default file path.
    /// </summary>
    public string? DefaultValue { get; init; }

    /// <summary>
    /// Gets or sets whether the file must already exist on disk before validation passes.
    /// </summary>
    public bool FileMustExist { get; init; }

    /// <summary>
    /// Gets or sets an optional file filter for the open-file dialog (e.g. "*.dll|*.exe").
    /// </summary>
    public string? FileFilter { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="FilePathSettingDescriptor"/> class.
    /// </summary>
    /// <param name="key">Machine-readable key for this setting.</param>
    /// <param name="displayName">Localizable UI label.</param>
    public FilePathSettingDescriptor(string key, LocalizedString displayName) : base(key, displayName) { }
}

/// <summary>
/// Descriptor for a directory path setting. Rendered as a text box with a "Browse..." button.
/// </summary>
public sealed class DirectoryPathSettingDescriptor : SettingDescriptor
{
    /// <summary>
    /// Gets or sets the default directory path.
    /// </summary>
    public string? DefaultValue { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DirectoryPathSettingDescriptor"/> class.
    /// </summary>
    /// <param name="key">Machine-readable key for this setting.</param>
    /// <param name="displayName">Localizable UI label.</param>
    public DirectoryPathSettingDescriptor(string key, LocalizedString displayName) : base(key, displayName) { }
}

/// <summary>
/// Descriptor for a URL setting. Rendered as a text box; the host may validate via <see cref="RegexPattern"/>.
/// </summary>
public sealed class UrlSettingDescriptor : SettingDescriptor
{
    /// <summary>
    /// Gets or sets the default URL value.
    /// </summary>
    public string? DefaultValue { get; init; }

    /// <summary>
    /// Gets or sets an optional regex pattern for URL validation.
    /// </summary>
    public string? RegexPattern { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UrlSettingDescriptor"/> class.
    /// </summary>
    /// <param name="key">Machine-readable key for this setting.</param>
    /// <param name="displayName">Localizable UI label.</param>
    public UrlSettingDescriptor(string key, LocalizedString displayName) : base(key, displayName) { }
}

/// <summary>
/// Descriptor for a password setting. Rendered as a masked text box.
/// </summary>
public sealed class PasswordSettingDescriptor : SettingDescriptor
{
    /// <summary>
    /// Gets or sets the default password value.
    /// </summary>
    public string? DefaultValue { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PasswordSettingDescriptor"/> class.
    /// </summary>
    /// <param name="key">Machine-readable key for this setting.</param>
    /// <param name="displayName">Localizable UI label.</param>
    public PasswordSettingDescriptor(string key, LocalizedString displayName) : base(key, displayName) { }
}
