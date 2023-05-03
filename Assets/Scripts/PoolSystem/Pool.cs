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

            obj.name = $"{type}_{i}";

            obj.OnInvisible += Push;

            obj.SetActive(false);

            queue.Enqueue(obj);
        }
    }

    /// <summary>
    /// Removes and returns the object at the beginning of the pool,
    /// if the pool has not objects available it returns null.
    /// </summary>
    public PoolObject Shift(Transform parent)
    {
        if (queue.Count != 0 && queue.Dequeue() is { } obj)
        {
            obj.transform.SetParent(parent);

            obj.SetActive(true);

            return obj;
        }
        else return null;
    }

    /// <summary>
    /// Adds an object to the end of the pool.
    /// </summary>
    public void Push(PoolObject obj)
    {
        if (obj.gameObject.activeInHierarchy && obj.gameObject.activeSelf)
        {
            obj.transform.SetParent(container);

            obj.SetActive(false);

            queue.Enqueue(obj);
        }
    }
}