using System;
using UnityEngine;

public class Vehicle : PoolObject
{
    [SerializeField] private GameObject lights;

    [SerializeField] private float speed = 1f;

    private static Action<bool?> OnTimeOfDay;

    private float? previousSpeed;

    private float? OverrideSpeed
    {
        set
        {
            previousSpeed ??= speed;

            speed = value ?? previousSpeed.Value;
        }
    }

    public override void Awake()
    {
        base.Awake();

        TimeOfDay(null);

        OnTimeOfDay += TimeOfDay;
    }

    private void Update()
    {
        transform.Translate(Vector3.right * (speed * Time.deltaTime), Space.Self);
    }

    private void TimeOfDay(bool? value)
    {
        if (value.HasValue)
        {
            lights.SetActive(!value.Value);

            OverrideSpeed = value.Value ? 5 : 25;
        }
        else
        {
            lights.SetActive(false);
            
            OverrideSpeed = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().SetDamage();
        }
    }

    public static void SetActiveLights(bool? value) => OnTimeOfDay?.Invoke(value);
}