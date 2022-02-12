using System;
using UnityEngine;

public class RendererEvents : MonoBehaviour
{
    public event Action OnVisible, OnInvisible;

    private void OnBecameVisible() => OnVisible?.Invoke();

    private void OnBecameInvisible() => OnInvisible?.Invoke();
}