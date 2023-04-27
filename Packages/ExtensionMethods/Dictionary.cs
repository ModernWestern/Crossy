using System;
using System.Linq;
using System.Collections.Generic;

public static class DictionaryExtensions
{
    public static bool TryAdd<K, V>(this Dictionary<K, V> collection, K key, V value)
    {
        if (collection.ContainsKey(key))
        {
            return false;
        }

        collection.Add(key, value);

        return true;
    }

    public static bool TryRemove<K, V>(this Dictionary<K, V> collection, K key)
    {
        if (collection.ContainsKey(key))
        {
            return false;
        }

        collection.Remove(key);

        return true;
    }

    public static bool TryGetValues<TKey, TValue>(this Dictionary<TKey, TValue> collection, out TValue[] values, params TKey[] key)
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
 
    public static IEnumerable<T> GetKeys<T, TU>(this Dictionary<T, TU> collection)
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

    public static void ForEach<T, TU>(this Dictionary<T, TU> d, Action<KeyValuePair<T, TU>> a)
    {
        foreach (var p in d)
        {
            a(p);
        }
    }

    public static void ForEach<T, TU>(this Dictionary<T, TU>.KeyCollection k, Action<T> a)
    {
        foreach (var t in k)
        {
            a(t);
        }
    }

    public static void ForEach<T, TU>(this Dictionary<T, TU>.ValueCollection v, Action<TU> a)
    {
        foreach (var u in v)
        {
            a(u);
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
        var r = new Random();

        var wviTKey = new TKey[source.Count];

        source.Keys.CopyTo(wviTKey, 0);

        for (var i = wviTKey.Length; i > 1; i--)
        {
            var k = r.Next(i);

            (wviTKey[k], wviTKey[i - 1]) = (wviTKey[i - 1], wviTKey[k]);
        }

        return wviTKey;
    }
}