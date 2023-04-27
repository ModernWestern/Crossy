using System;
using UnityEngine;

public static class EnumExtensions
{
    /// <summary>
    /// Converts a string to an enum value of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of the enum to convert to.</typeparam>
    /// <param name="str">The string to convert to an enum value.</param>
    /// <returns>The enum value corresponding to the input string, or the default value of the enum type if the input string is not a valid enum value.</returns>
    public static T ToEnum<T>(this string str)
    {
        try
        {
            var result = (T)Enum.Parse(typeof(T), str);

            return Enum.IsDefined(typeof(T), result) ? result : default;
        }
        catch
        {
#if UNITY_EDITOR

            Debug.Log($"{str} is not a member of the {typeof(T)} enumValue");
#endif
            return default;
        }
    }


    /// <summary>
    /// Cast the enum value to the specified type, and returns it.
    /// </summary>
    /// <typeparam name="T">The type of the enum to match to.</typeparam>
    /// <param name="enumValue">The enum value to match.</param>
    /// <returns>The enum value cast to the specified type, or the default value of the specified type if the match is unsuccessful.</returns>
    public static T CastToEnumType<T>(this Enum enumValue)
    {
        if (enumValue.ToString().ToEnum<T>() is { } e)
        {
            return e;
        }
        else
        {
            return default;
        }
    }
}