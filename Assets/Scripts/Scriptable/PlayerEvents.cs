using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Events", menuName = "ScriptableObjects/PlayerEvents", order = 0)]
public class PlayerEvents : ScriptableObject
{
    public event Action OnDamage, OnFinish, OnGameOver;

    public void Damage() => OnDamage?.Invoke();

    public void Finish() => OnFinish?.Invoke();

    public void GameOver() => OnGameOver?.Invoke();

    public void CleanAll()
    {
        OnDamage = null;

        OnFinish = null;

        OnGameOver = null;
    }
}