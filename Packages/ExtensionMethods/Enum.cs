using System;
using UnityEngine;

public static class EnumExtensions
{
    public static T ToEnum<T>(this string str)
    {
        try
        {
            T result = (T)Enum.Parse(typeof(T), str);

            if (Enum.IsDefined(typeof(T), result))
            {
                return result;
            }

            return default;
        }
        catch
        {
#if UNITY_EDITOR

            Debug.Log($"{str} is not a member of the {typeof(T)} enumeration");
#endif
            return default;
        }
    }

    public static T Match<T>(this Enum enumeration)
    {
        if (enumeration.ToString().ToEnum<T>() is { } e)
        {
            return e;
        }
        else
        {
            return default;
        }
    }
}