using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Events", menuName = "ScriptableObjects/PlayerEvents", order = 0)]
public class PlayerEvents : ScriptableObject
{
    public event Action OnDamage, OnFinish;

    public event Action<bool> OnGameOver;

    public void Damage() => OnDamage?.Invoke();

    public void Finish() => OnFinish?.Invoke();

    public void GameOver(bool value) => OnGameOver?.Invoke(value);

    public void CleanAll()
    {
        OnDamage = null;

        OnFinish = null;

        OnGameOver = null;
    }
}