using System;
using UnityEngine;
using ModernWestern;

public class Trunk : PoolObject
{
    [SerializeField] private float speed = 1f;

    private static Action<float> OnRainAmount;

    private float deltaSpeed;

    public override void Awake()
    {
        base.Awake();

        deltaSpeed = speed;

        OnRainAmount += RainAmount;
    }

    private void Update()
    {
        transform.Translate(Vector3.right * (deltaSpeed * Time.deltaTime), Space.Self);
    }

    private void RainAmount(float value)
    {
        deltaSpeed = speed * value.Remap(0f, value <= 10f ? 10f : value, 1, 5);
    }

    public static void SetRainAmount(float value) => OnRainAmount?.Invoke(value);
}