using UnityEngine;
using System.Linq;
using ModernWestern;
using System.Collections.Generic;

public class TrunkSpawner : MonoBehaviour
{
    [SerializeField] private GameSettings settings;

    [SerializeField] private PlayerEvents events;

    [SerializeField] private Transform[] streams;

    [SerializeField] private Vector2 timeRange;

    private Queue<Transform> points;

    private bool zigzag;

    private void Start()
    {
        points = new Queue<Transform>(streams.Select(road => road.GetChild((zigzag = !zigzag) ? 0 : 1)));

        if (settings.spawnOnAwake)
        {
            Spawn();
        }

        events.OnCityChange += cityData =>
        {
            // var rain = cityData.Location.Rain;

            // rain.Remap(0f, rain <= 10f ? 10f : rain, 0.5f, 2.5f);
        };
        
        Debug.Log("Trunk Start");
    }

    public void Spawn()
    {
        var point = Point();

        var trunkType = Random2.Value() ? ObjectType.TrunkShort : ObjectType.TrunkLarge;

        var currentTrunk = PoolController.Shift(trunkType);

        if (!currentTrunk)
        {
            return;
        }
        
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