using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Pool
{
    [SerializeField] private FroggerPoolObject prefab;

    [SerializeField] private Transform container;

    [SerializeField] private int amount;

    public ObjectType type;

    private Queue<FroggerPoolObject> queue = new Queue<FroggerPoolObject>();

    public void Populate()
    {
        var length = amount;

        for (int i = 0; i < length; i++)
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
    /// if the pool has not objects availables it returns null.
    /// </summary>
    public FroggerPoolObject Shift(Transform parent)
    {
        if (queue.Count != 0 && queue.Dequeue() is FroggerPoolObject obj)
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
    public void Push(FroggerPoolObject obj)
    {
        if (obj.gameObject.activeInHierarchy && obj.gameObject.activeSelf)
        {
            obj.transform.SetParent(container);

            obj.SetActive(false);

            queue.Enqueue(obj);
        }
    }
}