# Zaya.Primitives

Fundamental types for the Zaya ecosystem — zero dependencies, cross-platform.

## Features

- **PixelFormat** — strongly-typed pixel format definitions (Bgra32, Rgb24, Bgr24, Gray8) with bytes-per-pixel metadata
- **IRawImage** — generic in-memory raw pixel image (Width, Height, Stride, Format, GetPixelData, ToByteArray)
- **Languages** — static list of 15 common languages as `EnumOption` with BCP-47 codes and localized names (en, ru)
- **LocalizedString** — localizable string with deferred resolver (.resx ResourceManager); no hardcoded defaults in code
- **LocalizedException** — base class for exceptions carrying a localization key; subclasses resolve via .resx
- **SettingDescriptor** — polymorphic hierarchy for describing configurable engine settings (string, int, bool, enum, file, directory, url, password)
- **EnumOption** — machine-readable value paired with a localizable display name for dropdown choices

## Installation

```xml
<PackageReference Include="Zaya.Primitives" Version="0.2.0" />
```

## Quick Start

### PixelFormat

```csharp
using Zaya.Primitives;

var format = PixelFormat.Gray8;
Console.WriteLine($"Format: {format.Name}, Bpp: {format.BytesPerPixel}");

// Equality works
Assert.True(PixelFormat.Bgra32 == PixelFormat.Bgra32);
```

### LocalizedString

```csharp
using Zaya.Primitives;

// Invariant (never translated)
var label = LocalizedString.Invariant("OK");

// Localized via .resx resolver — only a key, no English default in code
var engineName = new LocalizedString("Ocr_EngineName",
    culture => Properties.Resources.ResourceManager.GetString("Ocr_EngineName", culture)!);

var displayText = engineName.GetValue(new CultureInfo("ru-RU"));
```

### SettingDescriptor

```csharp
var sourceSetting = new EnumSettingDescriptor("source", engineName)
{
    Description = desc,
    DefaultValue = "snippingtool",
    IsRequired = static _ => true,
    Options =
    [
        new EnumOption("snippingtool", snippingLabel),
        new EnumOption("directory", directoryLabel),
    ]
};

var urlSetting = new UrlSettingDescriptor("downloadUrl", urlLabel)
{
    Description = urlDesc,
    IsVisible  = s => s.GetValueOrDefault("source") as string == "url",
    IsRequired = s => s.GetValueOrDefault("source") as string == "url",
};
```

Both `IsVisible` and `IsRequired` are predicates that receive the dictionary of current setting values
(keyed by `Key`). The host re-evaluates them whenever any value changes; `IsRequired` is only checked
for settings whose `IsVisible` returns `true`. A setting is always visible / never required when the
predicate returns `true` / `false` for all inputs — use `static _ => true` or `static _ => false`.

### LocalizedException

```csharp
public sealed class MyNotFoundException : LocalizedException
{
    public MyNotFoundException() : base("Err_NotFound") { }

    public override string GetLocalizedMessage(CultureInfo culture)
        => Resources.ResourceManager.GetString("Err_NotFound", culture)
           ?? base.GetLocalizedMessage(culture);
}

// Throw — no hardcoded strings
throw new MyNotFoundException();
```

### Languages

```csharp
using Zaya.Primitives;

// Static list of 15 common languages — use in EnumSettingDescriptor.Options
var langSetting = new EnumSettingDescriptor("language", desc)
{
    Options = Languages.All
};

// Find by BCP-47 code
var russian = Languages.Find("ru");
Console.WriteLine(russian!.Value);       // "ru"
Console.WriteLine(russian.DisplayName.GetValue(new CultureInfo("ru-RU"))); // "Русский"
```

## Supported Languages

| Code | English | Russian |
|------|---------|---------|
| `en` | English | Английский |
| `ru` | Russian | Русский |
| `de` | German | Немецкий |
| `fr` | French | Французский |
| `es` | Spanish | Испанский |
| `it` | Italian | Итальянский |
| `pt` | Portuguese | Португальский |
| `ja` | Japanese | Японский |
| `ko` | Korean | Корейский |
| `zh-Hans` | Chinese (Simplified) | Китайский (упрощённый) |
| `zh-Hant` | Chinese (Traditional) | Китайский (традиционный) |
| `ar` | Arabic | Арабский |
| `tr` | Turkish | Турецкий |
| `uk` | Ukrainian | Украинский |
| `pl` | Polish | Польский |

## Available Pixel Formats

| Format      | Bytes/Pixel | Description                                     |
|-------------|-------------|-------------------------------------------------|
| `Bgra32`    | 4           | 32-bit Blue-Green-Red-Alpha (default for DGXI)  |
| `Rgb24`     | 3           | 24-bit Red-Green-Blue, no alpha                 |
| `Bgr24`     | 3           | 24-bit Blue-Green-Red, no alpha (PaddleOCR)     |
| `Gray8`     | 1           | 8-bit grayscale, single channel                 |

## Setting Descriptors

| Type                          | Renders as            | Extra properties                  |
|-------------------------------|-----------------------|-----------------------------------|
| `StringSettingDescriptor`     | TextBox               | MinLength, MaxLength, RegexPattern |
| `IntegerSettingDescriptor`    | NumericUpDown         | MinValue, MaxValue                |
| `BooleanSettingDescriptor`    | CheckBox              | —                                 |
| `EnumSettingDescriptor`       | ComboBox              | Options                           |
| `FilePathSettingDescriptor`   | TextBox + Browse      | FileMustExist, FileFilter         |
| `DirectoryPathSettingDescriptor` | TextBox + Browse  | —                                 |
| `UrlSettingDescriptor`        | TextBox               | RegexPattern                      |
| `PasswordSettingDescriptor`   | Masked TextBox        | —                                 |

## Architecture

- **Zaya.Primitives** — fundamental types used across the Zaya ecosystem: pixel formats, plugin settings, localization

## Used By

- [Zaya.Screenshot](https://github.com/shtrasser-dev/Zaya.Screenshot) — screen capture with Direct3D 11
- [Zaya.OCR](https://github.com/shtrasser-dev/Zaya.OCR) — optical character recognition

## License

MIT
