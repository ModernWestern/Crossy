using System;
using UnityEngine;

public class RendererEvents : MonoBehaviour
{
    public event Action OnInvisible;

    private void OnBecameInvisible()
    {
        OnInvisible?.Invoke();
    }
}