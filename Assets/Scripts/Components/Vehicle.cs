using System;
using UnityEngine;

public class Vehicle : MovableObject
{
    private static Action<bool?> OnLightsSetActive;

    [SerializeField] private GameObject lights;

    public override void Awake()
    {
        base.Awake();

        OnLightsSetActive += LightsSetActive;
    }

    private void Update()
    {
        transform.Translate(Vector3.right * (SpeedMultiplier * Time.deltaTime), Space.Self);
    }

    private void LightsSetActive(bool? value)
    {
        lights.SetActive(!value ?? false);
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