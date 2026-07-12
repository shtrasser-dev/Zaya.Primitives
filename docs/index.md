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

// Use predefined formats
var format = PixelFormat.Gray8;
Console.WriteLine($"{format.Name}: {format.BytesPerPixel} bytes/pixel");
```

## Available Formats

| Format   | Bpp | Description |
|----------|-----|-------------|
| `Bgra32` | 4   | Blue-Green-Red-Alpha (default for DirectX capture) |
| `Rgb24`  | 3   | Red-Green-Blue, no alpha |
| `Bgr24`  | 3   | Blue-Green-Red, no alpha (PaddleOCR native order) |
| `Gray8`  | 1   | Grayscale, single channel |

## Next Steps

- **Getting Started** — detailed usage guide
- **API Reference** — complete API documentation generated from source code
