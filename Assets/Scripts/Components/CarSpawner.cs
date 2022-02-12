using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] roads;

    [SerializeField] private Vector2Int timeRange;

    private Queue<Transform> points;

    private void Start()
    {
        // All the points
        //points = new Queue<Transform>(roads.Select(road => road.GetChildren()).SelectMany(point => point).Distinct());

        // One point for road
        points = new Queue<Transform>(roads.Select(road => road.GetChild(Random2.Value() ? 0 : 1)));

        Spawn();
    }

    private void Spawn()
    {
        var point = Point();

        var vehicleType = (ObjectType)Random.Range(2, 5);

        var currentVehicle = PoolController.Shift(vehicleType);

        currentVehicle.Position = point.position;

        currentVehicle.Rotation = point.rotation;

        Invoke(nameof(Spawn), Random.Range(timeRange.x, timeRange.y + 1));
    }

    private Transform Point()
    {
        var point = points.Dequeue();

        points.Enqueue(point);

        return point;
    }
}