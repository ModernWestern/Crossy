using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Events", menuName = "ScriptableObjects/PlayerEvents", order = 0)]
public class PlayerEvents : ScriptableObject
{
    public event Action OnDamage, OnGameOver, OnFinish;

    public void GameOver() => OnGameOver?.Invoke();

    public void Damage() => OnDamage?.Invoke();

    public void Finish() => OnFinish?.Invoke();

    private void OnEnable() => CleanAll();

    public void CleanAll()
    {
        OnGameOver = null;

        OnDamage = null;

        OnFinish = null;
    }
}