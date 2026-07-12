# Getting Started

## Using Pixel Formats

`PixelFormat` is a readonly struct with static predefined values and equality support.

```csharp
using Zaya.Primitives;

// Predefined formats
var bgra = PixelFormat.Bgra32;  // 4 bytes/pixel
var gray = PixelFormat.Gray8;   // 1 byte/pixel
var rgb  = PixelFormat.Rgb24;   // 3 bytes/pixel
var bgr  = PixelFormat.Bgr24;   // 3 bytes/pixel, PaddleOCR

Console.WriteLine(bgra.BytesPerPixel); // 4
Console.WriteLine(gray.Name);          // "Gray8"
```

## Equality

Formats are compared by name, not by value — `Bgra32 == Bgra32` is true.

```csharp
var a = PixelFormat.Bgra32;
var b = PixelFormat.Bgra32;
var c = PixelFormat.Gray8;

Assert.True(a == b);
Assert.False(a == c);
```

## Integration

`Zaya.Primitives` is a leaf dependency — it has zero external dependencies. It is designed to be referenced by all other Zaya packages:

- `Zaya.Screenshot` — `ICapturedFrame.Format` returns `PixelFormat`
- `Zaya.OCR` — `IOcrInput.Format` returns `PixelFormat`
