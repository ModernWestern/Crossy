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

    public static bool TryGetValues<K, V>(this Dictionary<K, V> collection, out V[] values, params K[] key)
    {
        var list = new List<V>();

        for (int i = 0, l = key.Length; i < l; ++i)
        {
            if (collection.TryGetValue(key[i], out V value))
            {
                list.Add(value);
            }
        }

        values = list.ToArray();

        return list?.Count > 0;
    }

    public static IEnumerable<T> GetKeys<T, U>(this Dictionary<T, U> collection)
    {
        return collection.Select(c => c.Key);
    }

    // https://stackoverflow.com/questions/141088/what-is-the-best-way-to-iterate-over-a-dictionary/31918117
    public static void Deconstruct<TKey, TVal>(this KeyValuePair<TKey, TVal> pair, out TKey key, out TVal value)
    {
        key = pair.Key;

        value = pair.Value;
    }

    public static void ForEach<T, U>(this Dictionary<T, U> d, Action<KeyValuePair<T, U>> a)
    {
        foreach (KeyValuePair<T, U> p in d)
        {
            a(p);
        }
    }

    public static void ForEach<T, U>(this Dictionary<T, U>.KeyCollection k, Action<T> a)
    {
        foreach (T t in k)
        {
            a(t);
        }
    }

    public static void ForEach<T, U>(this Dictionary<T, U>.ValueCollection v, Action<U> a)
    {
        foreach (U u in v)
        {
            a(u);
        }
    }

    // https://stackoverflow.com/questions/1028136/random-entry-from-dictionary
    public static TKey[] Shuffle<TKey, TValue>(this Dictionary<TKey, TValue> source)
    {
        System.Random r = new System.Random();

        TKey[] wviTKey = new TKey[source.Count];

        source.Keys.CopyTo(wviTKey, 0);

        for (int i = wviTKey.Length; i > 1; i--)
        {
            int k = r.Next(i);

            TKey temp = wviTKey[k];

            wviTKey[k] = wviTKey[i - 1];

            wviTKey[i - 1] = temp;
        }

        return wviTKey;
    }
}