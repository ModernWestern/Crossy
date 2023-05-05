using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private GameSettings settings;

    [SerializeField] private PlayerEvents events;

    [SerializeField] private Transform[] roads;

    [SerializeField] private Vector2 timeRange;

    private Queue<Transform> points;

    private float multiplier = 1;

    private void Start()
    {
        // All the points
        //points = new Queue<Transform>(roads.Select(road => road.GetChildren()).SelectMany(point => point).Distinct());

        // One point for road
        points = new Queue<Transform>(roads.Select(road => road.GetChild(Random2.Value() ? 0 : 1)));

        if (settings.spawnOnAwake)
        {
            Spawn();
        }

        events.OnCityChange += cityData =>
        {
            multiplier = cityData.IsDay.HasValue ? cityData.IsDay.Value ? 1f : 0.5f : 1f;

            Debug.Log("Vehicle: " + multiplier);
        };
    }

    public void Spawn()
    {
        var point = Point();

        var vehicleType = (ObjectType)Random.Range(2, 5);

        var currentVehicle = PoolController.Shift<MovableObject>(vehicleType);

        if (!currentVehicle)
        {
            return;
        }
        
        currentVehicle.SpeedMultiplier = multiplier;
        
        currentVehicle.Position = point.position;

        currentVehicle.Rotation = point.rotation;

        Invoke(nameof(Spawn), Random.Range(timeRange.x, timeRange.y));
    }

    private Transform Point()
    {
        var point = points.Dequeue();

        points.Enqueue(point);

        return point;
    }
}