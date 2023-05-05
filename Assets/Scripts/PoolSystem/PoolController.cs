using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class PoolController : MonoBehaviour
{
    [SerializeField] private List<Pool> pools;

    private static event Action<ObjectType, PoolObject> PushStaticHelper;

    private static event Func<ObjectType, Transform, PoolObject> ShiftStaticHelper;

    private void Awake()
    {
        pools.ForEach(pool => pool.Populate());

        PushStaticHelper += (type, obj) => pools.LastOrDefault(pool => pool.type == type)?.AddToPool(obj);

        ShiftStaticHelper += (type, parent) => pools.LastOrDefault(pool => pool.type == type)?.RetrievesFromPool(parent);
    }

    public static void Push(ObjectType type, PoolObject obj)
    {
        PushStaticHelper?.Invoke(type, obj);
    }

    public static PoolObject Shift(ObjectType type)
    {
        return ShiftStaticHelper?.Invoke(type, null);
    }

    public static T Shift<T>(ObjectType type) where T : PoolObject
    {
        if (ShiftStaticHelper?.Invoke(type, null) is { } obj)
        {
            return obj as T;
        }

        return null;
    }

    public static PoolObject Shift(ObjectType type, Transform parent)
    {
        return ShiftStaticHelper?.Invoke(type, parent);
    }
}