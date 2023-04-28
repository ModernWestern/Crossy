using System.Linq;
using UnityEngine;

public static class ColorExtensions
{
    public static Color[] ToColor(this Color32[] colors)
    {
        return colors.ConvertAll(c => (Color)c).ToArray();
    }

    public static bool IsDefault(this Color color)
    {
        return color.Equals(default);
    }

    public static Color Multiply(this Color32 c, Color32 color)
    {
        var r = (byte)((c.r / 255f) * (color.r / 255f) * 255);
        var g = (byte)((c.g / 255f) * (color.g / 255f) * 255);
        var b = (byte)((c.b / 255f) * (color.b / 255f) * 255);
        var a = (byte)((c.a / 255f) * (color.a / 255f) * 255);

        return new Color32(r, g, b, a);
    }

    public static Color Tint(this Color32 c, Color32 color)
    {
        var r = (byte)((c.r / 255f) * (color.r / 255f) * 255);
        var g = (byte)((c.g / 255f) * (color.g / 255f) * 255);
        var b = (byte)((c.b / 255f) * (color.b / 255f) * 255);
        var a = (byte)((c.a / 255f) * (color.a / 255f) * 255);

        return new Color32(r, g, b, a);
    }
}