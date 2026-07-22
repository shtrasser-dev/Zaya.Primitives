namespace Zaya.Primitives;

/// <summary>
/// Wraps a collection of <see cref="SettingDescriptor"/> and provides typed getters
/// (<see cref="GetValueAsString"/>, <see cref="GetValueAsInt"/>, <see cref="GetValueAsBool"/>)
/// that read from a bound user-provided dictionary and fall back to the descriptors' default values.
/// Plugins declare descriptors once, the host renders the UI, and the plugin consumes the values
/// without manual parsing or default-value handling.
/// </summary>
public sealed class SettingDescriptorList
{
    private readonly IReadOnlyList<SettingDescriptor> _descriptors;
    private readonly Dictionary<string, SettingDescriptor> _descriptorIndex;
    private IReadOnlyDictionary<string, object?>? _values;

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingDescriptorList"/> class
    /// with the specified descriptor list. Builds an internal index for O(1) key lookup.
    /// </summary>
    /// <param name="descriptors">The list of setting descriptors. Duplicate keys cause an <see cref="ArgumentException"/>.</param>
    public SettingDescriptorList(IReadOnlyList<SettingDescriptor> descriptors)
    {
        _descriptors = descriptors;
        _descriptorIndex = new Dictionary<string, SettingDescriptor>(descriptors.Count);
        foreach (var d in descriptors)
        {
            if (!_descriptorIndex.TryAdd(d.Key, d))
                throw new ArgumentException($"Duplicate setting key '{d.Key}'.", nameof(descriptors));
        }
    }

    /// <summary>
    /// Gets the original descriptor list in declaration order, used for UI rendering.
    /// </summary>
    public IReadOnlyList<SettingDescriptor> Descriptors => _descriptors;

    /// <summary>
    /// Binds the user-provided settings values to this list.
    /// After calling this method, the typed getters read from the provided dictionary and fall back
    /// to the descriptors' default values when a key is missing or cannot be parsed.
    /// </summary>
    /// <param name="values">User-provided settings (keyed by the same keys as the descriptors).</param>
    public void Bind(IReadOnlyDictionary<string, object?> values)
    {
        _values = values;
    }

    /// <summary>
    /// Gets the string value for the specified setting key.
    /// If the key is not present in the bound dictionary, returns the descriptor's <see cref="StringSettingDescriptor.DefaultValue"/>.
    /// Handles all string-based descriptor types: <see cref="StringSettingDescriptor"/>,
    /// <see cref="FilePathSettingDescriptor"/>, <see cref="DirectoryPathSettingDescriptor"/>,
    /// <see cref="UrlSettingDescriptor"/>, <see cref="PasswordSettingDescriptor"/>, and <see cref="EnumSettingDescriptor"/>.
    /// </summary>
    /// <param name="key">The setting key.</param>
    /// <returns>The string value, or default fallback (<c>""</c>).</returns>
public string GetValueAsString(string key)
    {
        if (!_descriptorIndex.TryGetValue(key, out var desc))
            throw new ArgumentException($"Setting '{key}' not found.", nameof(key));

        if (desc is IntegerSettingDescriptor or BooleanSettingDescriptor)
            throw new InvalidOperationException($"Setting '{key}' is not a string setting.");

        var defaultStr = desc switch
        {
            StringSettingDescriptor s => s.DefaultValue,
            FilePathSettingDescriptor f => f.DefaultValue,
            DirectoryPathSettingDescriptor d => d.DefaultValue,
            UrlSettingDescriptor u => u.DefaultValue,
            PasswordSettingDescriptor p => p.DefaultValue,
            EnumSettingDescriptor e => e.DefaultValue,
            _ => throw new InvalidOperationException($"Unexpected setting type '{desc.GetType().Name}' for key '{key}'.")
        };

        if (_values is not null && _values.TryGetValue(key, out var raw) && raw is not null)
        {
            if (raw is string rawStr)
                return rawStr;
            return raw.ToString() ?? defaultStr ?? "";
        }

        return defaultStr ?? "";
    }

    /// <summary>
    /// Gets the integer value for the specified setting key.
    /// If the key is not present in the bound dictionary, returns the descriptor's <see cref="IntegerSettingDescriptor.DefaultValue"/>.
    /// </summary>
    /// <param name="key">The setting key.</param>
    /// <returns>The integer value, or default fallback (<c>0</c>).</returns>
public int GetValueAsInt(string key)
    {
        if (!_descriptorIndex.TryGetValue(key, out var desc))
            throw new ArgumentException($"Setting '{key}' not found.", nameof(key));

        if (desc is not IntegerSettingDescriptor intDesc)
            throw new InvalidOperationException($"Setting '{key}' is not an integer setting.");

        if (_values is not null && _values.TryGetValue(key, out var raw) && raw is not null)
        {
            if (raw is int intValue)
                return intValue;
            if (raw is string s && int.TryParse(s, out var parsed))
                return parsed;
        }

        return intDesc.DefaultValue ?? 0;
    }

    /// <summary>
    /// Gets the boolean value for the specified setting key.
    /// If the key is not present in the bound dictionary, returns the descriptor's <see cref="BooleanSettingDescriptor.DefaultValue"/>.
    /// </summary>
    /// <param name="key">The setting key.</param>
    /// <returns>The boolean value, or default fallback (<c>false</c>).</returns>
public bool GetValueAsBool(string key)
    {
        if (!_descriptorIndex.TryGetValue(key, out var desc))
            throw new ArgumentException($"Setting '{key}' not found.", nameof(key));

        if (desc is not BooleanSettingDescriptor boolDesc)
            throw new InvalidOperationException($"Setting '{key}' is not a boolean setting.");

        if (_values is not null && _values.TryGetValue(key, out var raw) && raw is not null)
        {
            if (raw is bool boolValue)
                return boolValue;
            if (raw is string s && bool.TryParse(s, out var parsed))
                return parsed;
        }

        return boolDesc.DefaultValue;
    }
}
