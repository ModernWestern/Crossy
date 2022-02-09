#if TMP_MODULE

using TMPro;

#endif

using UnityEngine;

public static class ColorExtensions
{
    public static Color[] ToColor(this Color32[] colors)
    {
        return colors.ConvertAll(c => (Color)c);
    }

    public static bool IsDefault(this Color color)
    {
        return color == Color.clear;
    }

#if TMP_MODULE

    public static Color Tint(this Color c, Color color)
    {
        return ((Color32)c).Tint(color);
    }

    public static Color Multiply(this Color c, Color color)
    {
        return ((Color32)c).Multiply(color);
    }

#endif
}