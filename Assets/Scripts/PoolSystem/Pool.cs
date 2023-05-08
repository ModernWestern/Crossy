using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Pool
{
    [SerializeField] private PoolObject prefab;

    [SerializeField] private Transform container;

    [SerializeField] private int amount;

    public ObjectType type;

    private Queue<PoolObject> queue = new();

    public void Populate()
    {
        var length = amount;

        for (var i = 0; i < length; i++)
        {
            var obj = Object.Instantiate(prefab, container);

            obj.Type = type;

            obj.name = $"{type}_{i}";

            obj.OnInvisible += AddToPool;

            obj.SetActive(false);

            queue.Enqueue(obj);
        }
    }

    /// <summary>
    /// Retrieves and activates the first PoolObject from the pool, sets its parent to the specified transform, and returns it.
    /// </summary>
    /// <param name="parent">The transform to set as the parent of the retrieved object.</param>
    /// <returns>The retrieved object, or null if the pool is empty.</returns>
    public PoolObject RetrievesFromPool(Transform parent)
    {
        if (queue.Count != 0 && queue.Dequeue() is { } obj)
        {
            obj.transform.SetParent(parent);

            obj.SetActive(true);

            return obj;
        }

        return null;
    }

    /// <summary>
    /// Adds a PoolObject to the object pool, sets its parent to its container, deactivates it, and enqueues it for later use. If the PoolObject is already inactive, it is not added to the pool
    /// </summary>
    /// <param name="obj">The PoolObject to add to the pool.</param>
    public void AddToPool(PoolObject obj)
    {
        if (obj.gameObject.activeInHierarchy && obj.gameObject.activeSelf)
        {
            obj.transform.SetParent(container);

            obj.SetActive(false);

            queue.Enqueue(obj);
        }
    }
}