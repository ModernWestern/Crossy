using System;
using UnityEngine;

public class PoolableObject : FroggerObject
{
    public event Action<PoolableObject> OnInvisible;

    public Vector3 Position
    {
        get => transform.localPosition;

        set => transform.localPosition = value;
    }

    public Quaternion Rotation
    {
        get => transform.localRotation;

        set => transform.localRotation = value;
    }

    [SerializeField] private float speedMultiplier = 1f;

    private void Update()
    {
        transform.Translate(Vector3.right * speedMultiplier * Time.deltaTime, Space.Self);
    }

    public override void OnBecameInvisible()
    {
        transform.position = Vector3.zero;

        OnInvisible?.Invoke(this);
    }

    public void SetActive(bool value)
    {
        transform.position = Vector3.zero;

        gameObject.SetActive(value);
    }
}