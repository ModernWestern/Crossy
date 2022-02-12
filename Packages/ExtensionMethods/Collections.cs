using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public static class CollectionsExtensions
{
    public static T Last<T>(this IEnumerable<T> collection)
    {
        return collection.ElementAt(collection.Count() - 1);
    }

    public static void ForEach<T>(this T[] collection, Action<T> action)
    {
        Array.ForEach(collection, action);
    }

    public static void ForEach<T>(this T[] collection, Action<int, T> action)
    {
        for (int i = 0, l = collection.Length; i < l; ++i)
        {
            action?.Invoke(i, collection[i]);
        }
    }

    public static void BreakableForEach<T>(this T[] collection, Func<bool> predicate, Action<T> action)
    {
        for (int i = 0, l = collection.Length; i < l && !predicate(); ++i)
        {
            action?.Invoke(collection[i]);
        }
    }

    public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
    {
        foreach (T t in collection)
        {
            action?.Invoke(t);
        }
    }

    public static void ForEach<T>(this IEnumerable<T> collection, Action<int, T> action)
    {
        int index = 0;

        foreach (T t in collection)
        {
            action?.Invoke(index++, t);
        }
    }

    public static void Iterate<T>(this IEnumerable<T> collection, Action action)
    {
        foreach (T t in collection)
        {
            action?.Invoke();
        }
    }

    public static T[] Map<T>(this T[] collection, Action<T> action)
    {
        T[] clone = collection.CloneAll();

        Array.ForEach(clone, action);

        return clone;
    }

    public static T[] CloneAll<T>(this T[] array)
    {
        return array.Clone() as T[];
    }

    public static bool TryGetComponentInChildren<T>(this IEnumerable<T> collection, out T component) where T : Component
    {
        component = collection.FirstOrDefault(c => c.GetType().Equals(typeof(T)));

        return component is T;
    }

    /// <summary>
    /// Returns true if the criteria match, otherwise false.
    /// </summary>
    public static bool Exist<T>(this T[] collection, Predicate<T> match)
    {
        return Array.Exists(collection, match);
    }

    /// <summary>
    /// Converts an array of one type to an array of another type.
    /// </summary>
    public static TOutput[] ConvertAll<TInput, TOutput>(this TInput[] collection, Converter<TInput, TOutput> converter)
    {
        return Array.ConvertAll(collection, converter);
    }

    /// <summary>
    /// Converts a collection of one type to an array of another type.
    /// </summary>
    public static TOutput[] ConvertAll<TInput, TOutput>(this IEnumerable<TInput> collection, Converter<TInput, TOutput> converter)
    {
        return Array.ConvertAll(collection.ToArray(), converter);
    }

    /// <summary>
    /// Converts a collection of one type to a list of another type.
    /// </summary>
    public static List<TOutput> ConvertAllToList<TInput, TOutput>(this IEnumerable<TInput> collection, Converter<TInput, TOutput> converter)
    {
        return Array.ConvertAll(collection.ToArray(), converter).ToList();
    }

    /// <summary>
    /// Check if the whole array is true or false. (Linq NET 3.5)
    /// </summary>
    public static bool All<T>(this T[] collection, Func<T, bool> match)
    {
        return Enumerable.All(collection, match);
    }

    /// <summary>
    /// Check if the whole array is true or false. (System NET 2.0)
    /// </summary>
    public static bool TrueForAll<T>(this T[] collection, Predicate<T> match)
    {
        return Array.TrueForAll(collection, match);
    }

    /// <summary>
    /// Returns true if the collection to comparer is the same, otherwise false.
    /// </summary>
    public static bool Compare<T>(this T[] collection, T[] comparer)
    {
        return collection.SequenceEqual(comparer);
    }

    public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> collection)
    {
        if (collection?.Count() > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// If returns true the out result is the current collection else null.
    /// </summary>
    public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> collection, out IEnumerable<T> result)
    {
        if (collection?.Count() > 0)
        {
            result = collection;

            return true;
        }
        else
        {
            result = null;

            return false;
        }
    }

    /// <summary>
    /// If returns true the out result is the current collection, otherwise out is null.
    /// </summary>
    public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> collection, out T[] result)
    {
        if (collection?.Count() > 0)
        {
            result = collection.ToArray();

            return true;
        }
        else
        {
            result = null;

            return false;
        }
    }

    /// <summary>
    /// If returns true the out result is the current collection else null.
    /// </summary>
    public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> collection, out List<T> result)
    {
        if (collection?.Count() > 0)
        {
            result = collection.ToList();

            return true;
        }
        else
        {
            result = null;

            return false;
        }
    }

    // https://stackoverflow.com/questions/18986129/how-can-i-split-an-array-into-n-parts


    public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> collection, int size)
    {
        float count = (float)collection.Count() / size;

        for (int i = 0; i < count; i++)
        {
            yield return collection.Skip(i * size).Take(size);
        }
    }

    public static IEnumerable<List<T>> SplitToList<T>(this IEnumerable<T> collection, int size)
    {
        float count = (float)collection.Count() / size;

        for (int i = 0; i < count; i++)
        {
            yield return collection.Skip(i * size).Take(size).ToList();
        }
    }

    public static IEnumerable<T[]> SplitToArray<T>(this IEnumerable<T> collection, int size)
    {
        float count = (float)collection.Count() / size;

        for (int i = 0; i < count; i++)
        {
            yield return collection.Skip(i * size).Take(size).ToArray();
        }
    }
}