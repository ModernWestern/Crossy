using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class TrunkSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] streams;

    [SerializeField] private Vector2 timeRange;

    private Queue<Transform> points;

    private bool zigzag;

    private void Start()
    {
        points = new Queue<Transform>(streams.Select(road => road.GetChild((zigzag = !zigzag) ? 0 : 1)));

        Spawn();
    }

    private void Spawn()
    {
        var point = Point();

        var trunkType = Random2.Value() ? ObjectType.TrunkShort : ObjectType.TrunkLarge;

        var currentTrunk = PoolController.Shift(trunkType);

        currentTrunk.Position = point.position;

        currentTrunk.Rotation = point.rotation;

        Invoke(nameof(Spawn), Random.Range(timeRange.x, timeRange.y));
    }

    private Transform Point()
    {
        var point = points.Dequeue();

        points.Enqueue(point);

        return point;
    }
}