using UnityEngine;

public static class IntExtensions
{
    public static int DivideByToInt(this int value, int divisor)
    {
        try
        {
            return Mathf.RoundToInt(value / divisor);
        }
#if UNITY_EDITOR

        catch (UnityException e)
        {
            Debug.Log($"<color=red>{value}/{divisor}\n{e.Message}</color>");

            return 0;
        }
#else
        catch
        {
            return 0;
        }
#endif
    }

    public static float DivideBy(this int value, int divisor)
    {
        try
        {
            return (float)value / divisor;
        }
#if UNITY_EDITOR

        catch (UnityException e)
        {
            Debug.Log($"<color=red>{value}/{divisor}\n{e.Message}</color>");

            return 0;
        }
#else
        catch
        {
            return 0;
        }
#endif
    }

    public static int Normalize(this int value, float min, float max)
    {
        var a = (value - min) / (max - min);

        var b = 0 + (1 - 0) * a;

        return Mathf.RoundToInt(b);
    }

    public static int Remap(this int value, float inMin, float inMax, float outMin, float outMax)
    {
        var a = (value - inMin) / (inMax - inMin);

        var b = outMin + (outMax - outMin) * a;

        return Mathf.RoundToInt(Mathf.Clamp(b, outMin, outMax));
    }
}