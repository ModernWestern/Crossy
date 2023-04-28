using System;
using System.Linq;
using System.Collections.Generic;

public static class DictionaryExtensions
{
    /// <summary>
    /// Adds a key-value pair to a dictionary if the key does not already exist.
    /// </summary>
    /// <typeparam name="TKey">The type of the key in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the value in the dictionary.</typeparam>
    /// <param name="collection">The dictionary to add the key-value pair to.</param>
    /// <param name="key">The key to add to the dictionary.</param>
    /// <param name="value">The value to add to the dictionary.</param>
    /// <returns>True if the key-value pair was added successfully, false if the key already exists in the dictionary.</returns>
    public static bool AddIfKeyNotExists<TKey, TValue>(this Dictionary<TKey, TValue> collection, TKey key, TValue value)
    {
        if (collection.ContainsKey(key))
        {
            return false;
        }

        collection.Add(key, value);

        return true;
    }

    /// <summary>
    /// Removes a key-value pair from a dictionary if the key exists in the dictionary.
    /// </summary>
    /// <typeparam name="TKey">The type of the key in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the value in the dictionary.</typeparam>
    /// <param name="collection">The dictionary to remove the key-value pair from.</param>
    /// <param name="key">The key to remove from the dictionary.</param>
    /// <returns>True if the key-value pair was removed successfully, false if the key does not exist in the dictionary.</returns>
    public static bool RemoveIfExist<TKey, TValue>(this Dictionary<TKey, TValue> collection, TKey key)
    {
        if (!collection.ContainsKey(key))
        {
            return false;
        }

        collection.Remove(key);

        return true;
    }

    /// <summary>
    /// Gets the values associated with the specified keys from a dictionary.
    /// </summary>
    /// <typeparam name="TKey">The type of the key in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the value in the dictionary.</typeparam>
    /// <param name="collection">The dictionary to retrieve values from.</param>
    /// <param name="values">An array of values associated with the specified keys.</param>
    /// <param name="key">An array of keys to retrieve values for.</param>
    /// <returns>True if at least one value was found, false otherwise.</returns>
    public static bool TryGetValuesForKeys<TKey, TValue>(this Dictionary<TKey, TValue> collection, out TValue[] values, params TKey[] key)
    {
        var list = new List<TValue>();

        for (int i = 0, l = key.Length; i < l; ++i)
        {
            if (collection.TryGetValue(key[i], out var value))
            {
                list.Add(value);
            }
        }

        values = list.ToArray();

        return list.Count > 0;
    }

    /// <summary>
    /// Returns an enumerable collection of keys from a dictionary.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <param name="collection">The dictionary to retrieve keys from.</param>
    /// <returns>An enumerable collection of keys from the dictionary.</returns>
    public static IEnumerable<TKey> GetKeys<TKey, TValue>(this Dictionary<TKey, TValue> collection)
    {
        return collection.Select(c => c.Key);
    }

    /// <summary>
    /// Deconstructs a KeyValuePair into its key and value.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TVal">The type of the value.</typeparam>
    /// <param name="pair">The KeyValuePair to deconstruct.</param>
    /// <param name="key">The output key.</param>
    /// <param name="value">The output value.</param>
    public static void Deconstruct<TKey, TVal>(this KeyValuePair<TKey, TVal> pair, out TKey key, out TVal value)
    {
        key = pair.Key;

        value = pair.Value;
    }

    /// <summary>
    /// Performs the specified action on each element of the dictionary.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <param name="collection">The dictionary to iterate over.</param>
    /// <param name="action">The action to perform on each key-value pair.</param>
    public static void ForEach<TKey, TValue>(this Dictionary<TKey, TValue> collection, Action<KeyValuePair<TKey, TValue>> action)
    {
        foreach (var pair in collection)
        {
            action(pair);
        }
    }

    /// <summary>
    /// Performs the specified action on each key in the dictionary's KeyCollection.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <param name="keys">The KeyCollection of the dictionary to iterate over.</param>
    /// <param name="action">The action to perform on each key.</param>
    public static void ForEachKey<TKey, TValue>(this Dictionary<TKey, TValue>.KeyCollection keys, Action<TKey> action)
    {
        foreach (var key in keys)
        {
            action(key);
        }
    }
    
    /// <summary>
    /// Performs the specified action on each value in the dictionary's ValueCollection.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <param name="values">The ValueCollection of the dictionary to iterate over.</param>
    /// <param name="action">The action to perform on each value.</param>
    public static void ForEachValue<TKey, TValue>(this Dictionary<TKey, TValue>.ValueCollection values, Action<TValue> action)
    {
        foreach (var value in values)
        {
            action(value);
        }
    }
    
    /// <summary>
    /// Returns an array of keys from a dictionary in a random order.
    /// </summary>
    /// <typeparam name="TKey">The type of the dictionary keys.</typeparam>
    /// <typeparam name="TValue">The type of the dictionary values.</typeparam>
    /// <param name="source">The dictionary to shuffle.</param>
    /// <returns>An array of keys in a random order.</returns>
    public static TKey[] Shuffle<TKey, TValue>(this Dictionary<TKey, TValue> source)
    {
        var random = new Random();

        var keys = new TKey[source.Count];

        source.Keys.CopyTo(keys, 0);

        for (var i = keys.Length; i > 1; i--)
        {
            var key = random.Next(i);

            (keys[key], keys[i - 1]) = (keys[i - 1], keys[key]);
        }

        return keys;
    }
}