using System;
using UnityEngine;

public class PoolableObject : MonoBehaviour
{
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

    public event Action<PoolableObject> OnInvisible;

    [SerializeField] private float speedMultiplier = 0.001f;

    [SerializeField] private RendererEvents rendererEvents;

    private void Awake()
    {
        if (rendererEvents)
        {
            rendererEvents.OnInvisible += OnBecameInvisible;
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.left * speedMultiplier, Space.Self);
    }

    private void OnBecameInvisible()
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