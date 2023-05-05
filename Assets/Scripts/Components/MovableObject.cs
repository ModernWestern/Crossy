using UnityEngine;

public class MovableObject : PoolObject
{
    [SerializeField] private float speed = 1f;

    private float multiplier = 1f;

    public float SpeedMultiplier
    {
        get => speed;
        set => multiplier = value;
    }
}