using System;
using UnityEngine;

public static class FloatExtensions
{
    public static int DivideByToInt(this float value, float divisor)
    {
        return Mathf.RoundToInt(divisor <= 0 ? 0 : value / divisor);
    }

    public static float DivideBy(this float value, float divisor)
    {
        return divisor <= 0 ? 0 : value / divisor;
    }

    public static float Normalize(this float value, float min, float max)
    {
        var a = (value - min) / (max - min);

        var b = 0 + (1 - 0) * a;

        return Mathf.Clamp01(b);
    }

    public static float Remap(this float value, float inMin, float inMax, float outMin, float outMax)
    {
        var a = (value - inMin) / (inMax - inMin);

        var b = outMin + (outMax - outMin) * a;

        return Mathf.Clamp(b, outMin, outMax);
    }

    public static float Remap01(this float value, float inMin, float inMax, float outMin, float outMax)
    {
        var a = (value - inMin) / (inMax - inMin);

        var b = outMin + (outMax - outMin) * a;

        return Mathf.Clamp01(b);
    }

    public static float Truncate(this float value)
    {
        var mult = Math.Pow(10.0, 1);

        var result = Math.Truncate(mult * value) / mult;

        return (float)result;
    }

    public static float Truncate(this float value, int digits)
    {
        var mult = Math.Pow(10.0, digits);

        var result = Math.Truncate(mult * value) / mult;

        return (float)result;
    }

    public static float Normalize360(this float value)
    {
        return value += Mathf.Ceil(-value / 360) * 360;
    }

    public static int RoundToInt(this float value) => Mathf.RoundToInt(value);
}