using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private Vector2Int timeRange;

    private Queue<Transform> points;

    private void Start()
    {
        points = new Queue<Transform>(spawnPoints);

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