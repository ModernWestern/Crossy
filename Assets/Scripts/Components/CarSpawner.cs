using System.Linq;
using UnityEngine;
using ModernWestern;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private GameSettings settings;

    [SerializeField] private PlayerEvents events;

    [SerializeField] private Transform[] roads;

    [SerializeField] private Vector2 timeRange;

    private Queue<Transform> points;

    private GameplayData data;

    private Loop loop;

    private void Start()
    {
        // All the points
        //points = new Queue<Transform>(roads.Select(road => road.GetChildren()).SelectMany(point => point).Distinct());

        // One point for road
        points = new Queue<Transform>(roads.Select(road => road.GetChild(ModernWestern.Random.Boolean() ? 0 : 1)));

        loop = new Loop(Random.Range(timeRange.x, timeRange.y), this);

        if (settings.spawnOnAwake)
        {
            loop.Start(Spawn);
        }

        events.OnCityChange += cityData =>
        {
            loop.Stop();

            data = cityData;

            var randomTime = Random.Range(timeRange.x, timeRange.y);
            //                                                                |     noon/dawn     |     night      |    day    | 
            loop.Start(Spawn, cityData.IsDay.HasValue ? cityData.IsDay.Value ? 0.5f * randomTime : 4 * randomTime : randomTime);
        };
    }

    public void Spawn()
    {
        var point = Point();

        var randomCarType = Random.Range(ObjectType.Taxi.ToInt(), ObjectType.Count.ToInt());
        //                                                                                                    |  noon/dawn  |          night         |       day      | 
        var vehicleType = (ObjectType)(data == null ? randomCarType : data.IsDay.HasValue ? data.IsDay.Value ? randomCarType : ObjectType.Taxi.ToInt() : randomCarType);

        var currentVehicle = PoolController.Shift(vehicleType);

        if (!currentVehicle)
        {
            return;
        }
        
        currentVehicle.Position = point.position;

        currentVehicle.Rotation = point.rotation;

        loop.Stop();

        var randomTime = Random.Range(timeRange.x, timeRange.y);
        //                                                                                    |     noon/dawn     |       night       |    day     | 
        loop.Start(Spawn, data == null ? randomTime : data.IsDay.HasValue ? data.IsDay.Value ? 0.75f * randomTime : randomTime * 1.5f : randomTime);
    }

    private Transform Point()
    {
        var point = points.Dequeue();

        points.Enqueue(point);

        return point;
    }
}