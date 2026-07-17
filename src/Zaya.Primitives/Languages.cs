using System.Globalization;

namespace Zaya.Primitives;

/// <summary>
/// Provides a static list of common languages as <see cref="EnumOption"/> values.
/// OCR, translation, and other engines reference <see cref="All"/> in their
/// <see cref="EnumSettingDescriptor.Options"/> to present a language dropdown in the host UI.
/// Each option's <see cref="EnumOption.Value"/> is a BCP-47 tag (e.g. "ru", "zh-Hans").
/// </summary>
public static class Languages
{
    private static readonly List<EnumOption> _all;
    private static readonly Dictionary<string, EnumOption> _byCode;

    static Languages()
    {
        var resolver = new Func<string, Func<CultureInfo, string>>(key =>
            culture => Properties.Resources.ResourceManager.GetString(key, culture)!);

        _all = new List<EnumOption>
        {
            new("en",       Loc("Lang_en", resolver)),
            new("ru",       Loc("Lang_ru", resolver)),
            new("de",       Loc("Lang_de", resolver)),
            new("fr",       Loc("Lang_fr", resolver)),
            new("es",       Loc("Lang_es", resolver)),
            new("it",       Loc("Lang_it", resolver)),
            new("pt",       Loc("Lang_pt", resolver)),
            new("ja",       Loc("Lang_ja", resolver)),
            new("ko",       Loc("Lang_ko", resolver)),
            new("zh-Hans",  Loc("Lang_zh-Hans", resolver)),
            new("zh-Hant",  Loc("Lang_zh-Hant", resolver)),
            new("ar",       Loc("Lang_ar", resolver)),
            new("tr",       Loc("Lang_tr", resolver)),
            new("uk",       Loc("Lang_uk", resolver)),
            new("pl",       Loc("Lang_pl", resolver)),
        };

        _byCode = _all.ToDictionary(o => o.Value, StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Gets the list of 15 common languages as <see cref="EnumOption"/> values
    /// with BCP-47 codes and localized display names.
    /// </summary>
    public static IReadOnlyList<EnumOption> All => _all;

    /// <summary>
    /// Finds a language by its BCP-47 code (case-insensitive).
    /// </summary>
    /// <param name="bcp47">The BCP-47 tag, e.g. "ru" or "zh-Hans".</param>
    /// <returns>The matching <see cref="EnumOption"/>, or <c>null</c> if not found.</returns>
    public static EnumOption? Find(string bcp47)
        => _byCode.TryGetValue(bcp47, out var option) ? option : null;

    private static LocalizedString Loc(string key, Func<string, Func<CultureInfo, string>> resolver)
        => new(key, resolver(key));
}
