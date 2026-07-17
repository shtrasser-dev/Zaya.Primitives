using System.Resources;

namespace Zaya.Primitives.Properties;

internal static class Resources
{
    private static readonly ResourceManager _rm =
        new("Zaya.Primitives.Properties.Resources", typeof(Resources).Assembly);

    public static ResourceManager ResourceManager => _rm;
}
