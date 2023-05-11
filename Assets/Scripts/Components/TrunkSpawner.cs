using System.Linq;
using UnityEngine;
using ModernWestern;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class TrunkSpawner : MonoBehaviour
{
    [SerializeField] private GameSettings settings;

    [SerializeField] private PlayerEvents events;

    [SerializeField] private Transform[] streams;

    [SerializeField] private Vector2 timeRange;

    private Queue<Transform> points;

    private GameplayData data;

    private bool zigzag;

    private Loop loop;

    private void Start()
    {
        points = new Queue<Transform>(streams.Select(road => road.GetChild((zigzag = !zigzag) ? 0 : 1)));

        loop = new Loop(Random.Range(timeRange.x, timeRange.y), this);

        if (settings.spawnOnAwake)
        {
            loop.Start(Spawn);
        }

        events.OnCityChange += cityData => { data = cityData; };

        Debug.Log("Trunk Start");
    }

    public void Spawn()
    {
        var point = Point();

        var trunkType = ModernWestern.Random.RandomValue((ObjectType.TrunkSmall, 5f), (ObjectType.TrunkShort, 4f), (ObjectType.TrunkLarge, 1f));

        var currentTrunk = PoolController.Shift<Trunk>(trunkType);

        if (!currentTrunk)
        {
            return;
        }

        currentTrunk.Position = point.position;

        currentTrunk.Rotation = point.rotation;
    }

    private Transform Point()
    {
        var point = points.Dequeue();

        points.Enqueue(point);

        return point;
    }
}