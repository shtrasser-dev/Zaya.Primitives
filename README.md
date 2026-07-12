# Zaya.Primitives

Fundamental types for the Zaya ecosystem — zero dependencies, cross-platform.

## Features

- **PixelFormat** — strongly-typed pixel format definitions (Bgra32, Rgb24, Bgr24, Gray8) with bytes-per-pixel metadata

## Installation

```xml
<PackageReference Include="Zaya.Primitives" Version="0.1.0" />
```

## Quick Start

```csharp
using Zaya.Primitives;

var format = PixelFormat.Gray8;
Console.WriteLine($"Format: {format.Name}, Bpp: {format.BytesPerPixel}");

// Equality works
Assert.True(PixelFormat.Bgra32 == PixelFormat.Bgra32);
```

## Available Formats

| Format      | Bytes/Pixel | Description                                     |
|-------------|-------------|-------------------------------------------------|
| `Bgra32`    | 4           | 32-bit Blue-Green-Red-Alpha (default for DGXI)  |
| `Rgb24`     | 3           | 24-bit Red-Green-Blue, no alpha                 |
| `Bgr24`     | 3           | 24-bit Blue-Green-Red, no alpha (PaddleOCR)     |
| `Gray8`     | 1           | 8-bit grayscale, single channel                 |

## Architecture

- **Zaya.Primitives** — fundamental types used across the Zaya ecosystem: pixel formats, geometry primitives

## Used By

- [Zaya.Screenshot](https://github.com/shtrasser-dev/Zaya.Screenshot) — screen capture with Direct3D 11
- *Zaya.OCR (upcoming)* — optical character recognition

## License

MIT
