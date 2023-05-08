using System;
using UnityEngine;

public class Vehicle : PoolObject
{
    [SerializeField] private GameObject lights;

    [SerializeField] private float speed = 1f;
    
    private static Action<bool?> OnLightsSetActive;

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

        LightsSetActive(null);

        OnLightsSetActive += LightsSetActive;
    }

    private void Update()
    {
        transform.Translate(Vector3.right * (speed * Time.deltaTime), Space.Self);
    }

    private void LightsSetActive(bool? value)
    {
        lights.SetActive(!value ?? false);

        if (Type == ObjectType.Taxi || gameObject.activeSelf)
        {
            //                                      | noon/dawn | night | day | 
            OverrideSpeed = value.HasValue ? value.Value ? null : 25 : null;
        }
        else
        {
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

    public static void SetActiveLights(bool? value) => OnLightsSetActive?.Invoke(value);
}