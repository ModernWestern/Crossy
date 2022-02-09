using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class TrunkSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private Vector2Int timeRange;

    private Queue<Vector3> points;

    private void Start()
    {
        points = new Queue<Vector3>(spawnPoints.Select(transform => transform.position));

        Spawn();
    }

    private void Spawn()
    {
        var trunkType = Random2.Value() ? ObjectType.TrunkShort : ObjectType.TrunkLarge;

        PoolController.Shift(trunkType).Position = Point();

        Invoke(nameof(Spawn), Random.Range(timeRange.x, timeRange.y + 1));
    }

    private Vector3 Point()
    {
        var point = points.Dequeue();

        points.Enqueue(point);

        return point;
    }
}