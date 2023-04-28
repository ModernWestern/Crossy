using System;
using UnityEngine;

public static class NumberExtensions
{
    /// <summary>
    /// Divides a given value by a divisor and returns the result as an integer.
    /// </summary>
    /// <param name="value">The value to divide.</param>
    /// <param name="divisor">The divisor.</param>
    /// <returns>The result of the division, rounded to the nearest integer.</returns>
    public static int DivideByToNearestInt(this float value, float divisor)
    {
        return Mathf.RoundToInt(divisor <= 0f ? 0f : value / divisor);
    }

    /// <summary>
    /// Divides a given value by a divisor and returns the result as a float.
    /// </summary>
    /// <param name="value">The value to divide.</param>
    /// <param name="divisor">The divisor.</param>
    /// <returns>The result of the division as a float. If the divisor is less than or equal to 0, the method returns 0.</returns>
    public static float Divide(this float value, float divisor)
    {
        return divisor <= 0f ? 0f : value / divisor;
    }

    /// <summary>
    /// Normalizes a given value between a minimum and a maximum range, and returns the result as a float between 0 and 1.
    /// </summary>
    /// <param name="value">The value to normalize.</param>
    /// <param name="min">The minimum range.</param>
    /// <param name="max">The maximum range.</param>
    /// <returns>The normalized value as a float between 0 and 1.</returns>
    public static float Normalize(this float value, float min, float max)
    {
        var percentage = Mathf.InverseLerp(min, max, value);

        var mappedValue = Mathf.Lerp(0, 1, percentage);

        return Mathf.Clamp01(mappedValue);
    }

    /// <summary>
    /// Remaps a given value from one range to another and returns the result.
    /// </summary>
    /// <param name="value">The value to remap.</param>
    /// <param name="inMin">The minimum value of the input range.</param>
    /// <param name="inMax">The maximum value of the input range.</param>
    /// <param name="outMin">The minimum value of the output range.</param>
    /// <param name="outMax">The maximum value of the output range.</param>
    /// <returns>The remapped value.</returns>
    public static float Remap(this float value, float inMin, float inMax, float outMin, float outMax)
    {
        var percentage = Mathf.InverseLerp(inMin, inMax, value);

        var mappedValue = Mathf.Lerp(outMin, outMax, percentage);

        return mappedValue;
    }

    /// <summary>
    /// Remaps a value from the range [0,1] to the range [outMin, outMax].
    /// </summary>
    /// <param name="value">The value to remap, in the range [0,1].</param>
    /// <param name="outMin">The minimum value of the output range.</param>
    /// <param name="outMax">The maximum value of the output range.</param>
    /// <returns>The remapped value.</returns>
    public static float RemapToRange(this float value, float outMin, float outMax)
    {
        var mappedValue = Mathf.Lerp(outMin, outMax, value);

        return mappedValue;
    }

    /// <summary>
    /// Remaps a value from the range [inMin, inMax] to the range [outMin, outMax], with a logarithmic scaling factor.
    /// </summary>
    /// <param name="value">The value to remap, in the range [inMin, inMax].</param>
    /// <param name="inMin">The minimum value of the input range.</param>
    /// <param name="inMax">The maximum value of the input range.</param>
    /// <param name="outMin">The minimum value of the output range.</param>
    /// <param name="outMax">The maximum value of the output range.</param>
    /// <param name="exponent">The exponent to use for logarithmic scaling. Default is 2.</param>
    /// <returns>The remapped value.</returns>
    public static float LogarithmicRemapToRange(this float value, float inMin, float inMax, float outMin, float outMax, float exponent = 2)
    {
        var percentage = Mathf.InverseLerp(inMin, inMax, value);

        var mappedPercentage = Mathf.Pow(percentage, exponent);

        var mappedValue = Mathf.Lerp(outMin, outMax, mappedPercentage);

        return mappedValue;
    }

    /// <summary>
    /// Remaps a value from the range [inMin, inMax] to the range [outMin, outMax], with an exponential scaling factor.
    /// </summary>
    /// <param name="value">The value to remap, in the range [inMin, inMax].</param>
    /// <param name="inMin">The minimum value of the input range.</param>
    /// <param name="inMax">The maximum value of the input range.</param>
    /// <param name="outMin">The minimum value of the output range.</param>
    /// <param name="outMax">The maximum value of the output range.</param>
    /// <param name="exponent">The exponent to use for exponential scaling. Default is 2.</param>
    /// <returns>The remapped value.</returns>
    public static float ExponentialRemapToRange(this float value, float inMin, float inMax, float outMin, float outMax, float exponent = 2)
    {
        var percentage = Mathf.InverseLerp(inMin, inMax, value);

        var mappedPercentage = Mathf.Pow(1f - percentage, exponent);

        var mappedValue = Mathf.Lerp(outMin, outMax, mappedPercentage);

        return mappedValue;
    }

    /// <summary>
    /// Remaps a value from one range to another using an AnimationCurve as a remapping function.
    /// </summary>
    /// <param name="value">The value to remap.</param>
    /// <param name="inMin">The minimum value of the input range.</param>
    /// <param name="inMax">The maximum value of the input range.</param>
    /// <param name="outMin">The minimum value of the output range.</param>
    /// <param name="outMax">The maximum value of the output range.</param>
    /// <param name="curve">The AnimationCurve to use as a remapping function.</param>
    /// <returns>The remapped value.</returns>
    public static float RemapWithCurve(this float value, float inMin, float inMax, float outMin, float outMax, AnimationCurve curve)
    {
        var percentage = Mathf.InverseLerp(inMin, inMax, value);

        var curveValue = curve.Evaluate(percentage);

        var mappedValue = Mathf.Lerp(outMin, outMax, curveValue);

        return mappedValue;
    }

    /// <summary>
    /// Truncates a float value to one decimal point.
    /// </summary>
    /// <param name="value">The float value to truncate.</param>
    /// <returns>The truncated float value.</returns>
    public static float TruncateToSingleDecimal(this float value)
    {
        var mult = Math.Pow(10.0, 1);

        var result = Math.Truncate(mult * value) / mult;

        return (float)result;
    }

    /// <summary>
    /// Truncates a float value to the specified number of decimal points.
    /// </summary>
    /// <param name="value">The float value to truncate.</param>
    /// <param name="digits">The number of decimal points to keep.</param>
    /// <returns>The truncated float value.</returns>
    public static float TruncateToDecimalPlaces(this float value, int digits)
    {
        var mult = Math.Pow(10.0, digits);

        var result = Math.Truncate(mult * value) / mult;

        return (float)result;
    }

    /// <summary>
    /// Wrap an angle in degrees to the range of [0, 360).
    /// </summary>
    /// <param name="value">The angle in degrees to normalize.</param>
    /// <returns>The normalized angle in degrees.</returns>
    public static float WrapAngleTo360(this float value)
    {
        return value += Mathf.Ceil(-value / 360) * 360;
    }

    /// <summary>
    /// Rounds a float value to the nearest integer.
    /// </summary>
    /// <param name="value">The float value to round.</param>
    /// <returns>The rounded integer value.</returns>
    public static int Round(this float value) => Mathf.RoundToInt(value);

    /// <summary>
    /// Determines if the given value is even.
    /// </summary>
    /// <typeparam name="T">The type of the value to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is even, false otherwise.</returns>
    public static bool IsEven<T>(this T value) where T : struct, IComparable, IConvertible, IEquatable<T>
    {
        if (typeof(T) == typeof(decimal))
        {
#if UNITY_EDITOR
            Debug.LogError("Decimal type is not supported.");
#endif
            return false;
        }

        var intValue = Convert.ToInt32(value);

        return (intValue & 1) == 0;
    }
}