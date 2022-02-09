using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class PoolController : MonoBehaviour
{
    [SerializeField] private List<Pool> pools;

    private static event Action<ObjectType, PoolableObject> PushStaticHelper;

    private static event Func<ObjectType, Transform, PoolableObject> ShiftStaticHelper;

    private void Awake()
    {
        pools.ForEach(pool => pool.Populate());

        PushStaticHelper += (type, obj) => pools.LastOrDefault(pool => pool.type == type)?.Push(obj);

        ShiftStaticHelper += (type, parent) => pools.LastOrDefault(pool => pool.type == type)?.Shift(parent);
    }

    public static void Push(ObjectType type, PoolableObject obj)
    {
        PushStaticHelper?.Invoke(type, obj);
    }

    public static PoolableObject Shift(ObjectType type)
    {
        return ShiftStaticHelper?.Invoke(type, null);
    }

    public static PoolableObject Shift(ObjectType type, Transform parent)
    {
        return ShiftStaticHelper?.Invoke(type, parent);
    }
}