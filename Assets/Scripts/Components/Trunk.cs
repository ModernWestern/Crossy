using System;
using System.Linq;
using UnityEngine;
using ModernWestern;

public class Trunk : PoolObject
{
    [SerializeField] private float speed = 1f;

    [SerializeField] private Transform[] places;

    private static Action<float> OnRainAmount;

    private float deltaSpeed;

    private float length;

    public Vector3? GetPlace(Vector3 playerPosition)
    {
        var place = places.OrderBy(t => Vector3.Distance(t.position, playerPosition)).FirstOrDefault();

        return places.Length <= 0 ? null : place != null ? place.position : transform.position;
    }

    public override void Awake()
    {
        base.Awake();

        deltaSpeed = speed;

        OnRainAmount += RainAmount;

        length = GetComponentInChildren<Renderer>().bounds.size.x / 2;
    }

    private void Update()
    {
        transform.Translate(Vector3.right * (deltaSpeed * Time.deltaTime), Space.Self);

        RayCast();
    }

    private void RayCast()
    {
        var tr = transform;

        var origin = tr.position;

        origin.x = tr.rotation.eulerAngles.y == 180 ? origin.x - length : origin.x + length;

        var direction = tr.right;

        if (Physics.Raycast(origin, direction, out var hit, 1f, LayerMask.GetMask("Trunk")))
        {
            var hitObject = hit.collider.gameObject;

            hitObject.GetComponent<Trunk>().deltaSpeed = deltaSpeed;
        }
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //
    //     var tr = transform;
    //     
    //     var origin = tr.position;
    //
    //     origin.x = tr.rotation.eulerAngles.y == 180 ? origin.x - length : origin.x + length;
    //
    //     Gizmos.DrawRay(origin, tr.right);
    // }

    private void RainAmount(float value)
    {
        deltaSpeed = speed * value.Remap(0f, value <= 10f ? 10f : value, 1, 5);
    }

    public static void SetRainAmount(float value) => OnRainAmount?.Invoke(value);
}