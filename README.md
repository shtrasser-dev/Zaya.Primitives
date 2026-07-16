# Zaya.Primitives

Fundamental types for the Zaya ecosystem — zero dependencies, cross-platform.

## Features

- **PixelFormat** — strongly-typed pixel format definitions (Bgra32, Rgb24, Bgr24, Gray8) with bytes-per-pixel metadata
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
    IsRequired = true,
    Options =
    [
        new EnumOption("snippingtool", snippingLabel),
        new EnumOption("directory", directoryLabel),
    ]
};
```

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
