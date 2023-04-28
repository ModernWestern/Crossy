using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public static class CollectionsExtensions
{
    /// <summary>
    /// Applies a specified function to the elements of the collection and accumulates the result.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    /// <typeparam name="TAccumulate">The type of the accumulated value.</typeparam>
    /// <param name="collection">The collection to accumulate.</param>
    /// <param name="initialValue">The initial value of the accumulated result.</param>
    /// <param name="func">A function that takes the accumulated value (initial value) and an element of the collection, and returns the new accumulated value.</param>
    /// <returns>The accumulated value after applying the specified function to each element of the collection.</returns>
    /// <example>
    /// <code>
    /// { 1, 2, 3, 4, 5 }.Accumulate(8, (initialValue, n) => initialValue + n) // 23
    /// var b = { 't', 'e', 's', 't' }.Accumulate(new StringBuilder(), (builder, c) => { builder.Append(c); return builder} ); b.ToString() // "test"
    /// </code>
    /// </example>
    public static TAccumulate Accumulate<T, TAccumulate>(this IEnumerable<T> collection, TAccumulate initialValue, Func<TAccumulate, T, TAccumulate> func)
    {
        return collection.Aggregate(initialValue, func);
    }

    /// <summary>
    /// Returns the last element of a sequence, or the default value of <typeparamref name="T"/> if the sequence is empty.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
    /// <param name="collection">The sequence to return the last element of.</param>
    /// <returns>The last element in the sequence, or the default value of <typeparamref name="T"/> if the sequence is empty.</returns>
    public static T LastOrDefault<T>(this IEnumerable<T> collection)
    {
        if (collection.ToList() is { Count: > 0 } list)
        {
            return list[^1];
        }

        return default;
    }

    /// <summary>
    /// Performs the specified action on each element of the array.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    /// <param name="collection">The array to iterate over.</param>
    /// <param name="action">The action to perform on each element of the array.</param>
    public static void ForEach<T>(this T[] collection, Action<T> action)
    {
        Array.ForEach(collection, action);
    }

    /// <summary>
    /// Executes the specified action on each element of the array, passing the index of the current element as a parameter to the action.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    /// <param name="collection">The array to iterate over.</param>
    /// <param name="action">The action to perform on each element of the array, taking the index and the element as parameters.</param>
    public static void ForEach<T>(this T[] collection, Action<int, T> action)
    {
        for (int i = 0, l = collection.Length; i < l; ++i)
        {
            action?.Invoke(i, collection[i]);
        }
    }

    /// <summary>
    /// Iterates over the array and applies the specified action to each element until the predicate is true.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    /// <param name="collection">The array to iterate over.</param>
    /// <param name="predicate">The condition that determines whether the iteration should stop.</param>
    /// <param name="action">The action to perform on each element of the array.</param>
    public static void BreakableForEach<T>(this T[] collection, Func<bool> predicate, Action<T> action)
    {
        for (int i = 0, l = collection.Length; i < l && !predicate(); ++i)
        {
            action?.Invoke(collection[i]);
        }
    }

    /// <summary>
    /// Performs the specified action on each element of the collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection of elements to iterate over.</param>
    /// <param name="action">The action to perform on each element of the collection.</param>
    public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
    {
        foreach (var t in collection)
        {
            action?.Invoke(t);
        }
    }

    /// <summary>
    /// Executes the specified action on each element of the array, passing the index of the current element as a parameter to the action.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    /// <param name="collection">The sequence to iterate over.</param>
    /// <param name="action">The action to perform on each element of the array, taking the index and the element as parameters.</param>
    public static void ForEach<T>(this IEnumerable<T> collection, Action<int, T> action)
    {
        var index = 0;

        foreach (var t in collection)
        {
            action?.Invoke(index++, t);
        }
    }

    /// <summary>
    /// Iterates over each element of the collection, performing the specified action for each element.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection of elements to iterate over.</param>
    /// <param name="action">The action to perform for each element of the collection.</param>
    public static void Iterate<T>(this IEnumerable<T> collection, Action action)
    {
        foreach (var t in collection)
        {
            action?.Invoke();
        }
    }

    /// <summary>
    /// Performs the specified action on each element of the array and returns a new array with the updated elements.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    /// <param name="collection">The array of elements to iterate over.</param>
    /// <param name="action">The action to perform on each element of the array.</param>
    /// <returns>A new array with the updated elements.</returns>
    public static T[] UpdateElements<T>(this T[] collection, Action<T> action)
    {
        var clone = collection.CloneAll();

        Array.ForEach(clone, action);

        return clone;
    }

    /// <summary>
    /// Returns a new array that is a deep copy of the array.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    /// <param name="array">The array to clone.</param>
    /// <returns>A new array that is a deep copy of the array.</returns>
    public static T[] CloneAll<T>(this T[] array)
    {
        return array.Clone() as T[];
    }

    /// <summary>
    /// Searches the collection for a child object with a component of type T.
    /// </summary>
    /// <typeparam name="T">The type of component to search for.</typeparam>
    /// <param name="collection">The collection of child objects.</param>
    /// <param name="component">The component of type T attached to a child object if found, otherwise null.</param>
    /// <returns>True if a child object with a component of type T is found, otherwise false.</returns>
    public static bool TryGetComponentInChildren<T>(this IEnumerable<T> collection, out T component) where T : Component
    {
        component = collection.FirstOrDefault(c => c.GetType() == typeof(T));

        return component is { };
    }

    /// <summary>
    /// Checks if at least one element in the array matches the provided condition.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    /// <param name="collection">The array to search.</param>
    /// <param name="match">The condition to test elements against. e.g. t => t == x.</param>
    /// <returns>True if any element in the array matches the condition, otherwise false.</returns>
    /// <example>
    /// <code>
    /// bool hasEvenNumbers = { 1, 2, 3, 4, 5 }.Exist(num => num % 2 == 0) // true
    /// </code>
    /// </example>
    public static bool Exists<T>(this T[] collection, Predicate<T> match)
    {
        return Array.Exists(collection, match);
    }

    /// <summary>
    /// Converts all elements in the sequence to a list of a different type using the provided converter function.
    /// </summary>
    /// <typeparam name="TInput">The type of elements in the array.</typeparam>
    /// <typeparam name="TOutput">The type of elements in the output array.</typeparam>
    /// <param name="collection">The array to convert.</param>
    /// <param name="converter">The function that converts elements from TInput to TOutput. e.g. t => t as tu, t => (tu)t, t => t.tu.</param>
    /// <returns>A sequence of type TOutput containing all elements from the array converted using the provided converter function.</returns>
    /// <example>
    /// <code>
    /// {0, 1, 2, 3}.ConvertAll(num => num.ToString()) = { "0", "1", "2", "3" }
    /// </code>
    /// </example>
    public static IEnumerable<TOutput> ConvertAll<TInput, TOutput>(this IEnumerable<TInput> collection, Converter<TInput, TOutput> converter)
    {
        return Array.ConvertAll(collection.ToArray(), converter);
    }

    /// <summary>
    /// Determines whether all elements of a sequence satisfy a condition.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the array.</typeparam>
    /// <param name="array">The array to check.</param>
    /// <param name="match">The condition to test each element against. e.g. i => i > 0, b => !b</param>
    /// <returns>True if every element in the array satisfies the condition, false otherwise.</returns>
    /// <example>
    /// <code>
    /// {true, true, true}.All(element => !element) // false;
    /// {false, false, false}.All(element => !element) // true;
    /// </code>
    /// </example>
    public static bool All<T>(this IEnumerable<T> array, Func<T, bool> match)
    {
        return Enumerable.All(array, match);
    }

    /// <summary>
    /// Determines whether every element in an array matches the conditions defined by the specified predicate.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the array.</typeparam>
    /// <param name="array">The array to check.</param>
    /// <param name="match">The predicate function to test each element against. e.g. i => i > 0, b => !b</param>
    /// <returns>True if every element in the array matches the conditions defined by the specified predicate, false otherwise.</returns>
    /// <example>
    /// <code>
    /// {true, true, true}.All(element => !element) // false;
    /// {false, false, false}.All(element => !element) // true;
    /// </code>
    /// </example>
    public static bool All<T>(this T[] array, Predicate<T> match)
    {
        return Array.TrueForAll(array, match);
    }

    /// <summary>
    /// Determines whether two sequences are equal by comparing their elements using their default equality comparer.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the sequences.</typeparam>
    /// <param name="compare">The sequence to compare.</param>
    /// <param name="collection">The sequence to compare.</param>
    /// <returns>True if the two sequences are equal and in the same order, false otherwise.</returns>
    public static bool Compare<T>(this IEnumerable<T> compare, IEnumerable<T> collection)
    {
        return compare.SequenceEqual(collection);
    }

    /// <summary>
    /// Determines whether an enumerable object is not null and contains at least one element.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the enumerable object.</typeparam>
    /// <param name="collection">The enumerable object to check.</param>
    /// <returns>True if the enumerable object is not null and contains at least one element, false otherwise.</returns>
    public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> collection)
    {
        return collection?.Any() == true;
    }

    /// <summary>
    /// Determines whether an enumerable object is not null and contains at least one element.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the enumerable object.</typeparam>
    /// <param name="collection">The enumerable object to check.</param>
    /// <param name="result">The enumerable object.</param>
    /// <returns>True if the enumerable object is not null and contains at least one element, false otherwise.</returns>
    public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> collection, out IEnumerable<T> result)
    {
        var enumerable = collection as T[] ?? collection.ToArray();

        if (enumerable.Any())
        {
            result = enumerable;

            return true;
        }

        result = null;

        return false;
    }

    /// <summary>
    /// Determines whether an enumerable object is not null and contains at least one element.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the enumerable object.</typeparam>
    /// <param name="collection">The enumerable object to check.</param>
    /// <param name="result">The enumerable object as array.</param>
    /// <returns>True if the enumerable object is not null and contains at least one element, false otherwise.</returns>
    public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> collection, out T[] result)
    {
        result = collection.IsNotNullOrEmpty(out IEnumerable<T> sequence) ? sequence.ToArray() : null;

        return result is { };
    }

    /// <summary>
    /// Determines whether an enumerable object is not null and contains at least one element.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the enumerable object.</typeparam>
    /// <param name="collection">The enumerable object to check.</param>
    /// <param name="result">The enumerable object as list.</param>
    /// <returns>True if the enumerable object is not null and contains at least one element, false otherwise.</returns>
    public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> collection, out List<T> result)
    {
        result = collection.IsNotNullOrEmpty(out IEnumerable<T> sequence) ? sequence.ToList() : null;

        return result is { };
    }

    /// <summary>
    /// Splits an enumerable object into smaller chunks of a specified size.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the enumerable object.</typeparam>
    /// <param name="collection">The enumerable object to split.</param>
    /// <param name="size">The maximum size of each chunk.</param>
    /// <returns>An enumerable object containing the smaller chunks of the enumerable object.</returns>
    /// <example>
    /// <code>
    /// { 1, 2, 3, 4, 5 }.Split(2) = { { 1, 2 }, { 3, 4 }, { 5 } }
    /// </code>
    /// </example>
    public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> collection, int size)
    {
        var enumerable = collection.ToArray();

        var count = (float)enumerable.Length / size;

        for (var i = 0; i < count; i++)
        {
            yield return enumerable.Skip(i * size).Take(size);
        }
    }

    /// <summary>
    /// Splits an enumerable object into smaller chunks of a specified size.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the enumerable object.</typeparam>
    /// <param name="collection">The enumerable object to split.</param>
    /// <param name="size">The maximum size of each chunk.</param>
    /// <returns>An enumerable object containing the smaller chunks of the enumerable object.</returns>
    /// <example>
    /// <code>
    /// { 1, 2, 3, 4, 5 }.Split(2) = { { 1, 2 }, { 3, 4 }, { 5 } }
    /// </code>
    /// </example>
    public static IEnumerable<List<T>> SplitToList<T>(this IEnumerable<T> collection, int size)
    {
        return collection.Split(size).ConvertAll(ie => ie.ToList());
    }

    /// <summary>
    /// Splits an enumerable object into smaller chunks of a specified size.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the enumerable object.</typeparam>
    /// <param name="collection">The enumerable object to split.</param>
    /// <param name="size">The maximum size of each chunk.</param>
    /// <returns>An enumerable object containing the smaller chunks of the enumerable object.</returns>
    /// <example>
    /// <code>
    /// { 1, 2, 3, 4, 5 }.Split(2) = { { 1, 2 }, { 3, 4 }, { 5 } }
    /// </code>
    /// </example>
    public static IEnumerable<T[]> SplitToArray<T>(this IEnumerable<T> collection, int size)
    {
        return collection.Split(size).ConvertAll(ie => ie.ToArray());
    }
}