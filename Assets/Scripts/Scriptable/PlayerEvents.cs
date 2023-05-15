using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Events", menuName = "ScriptableObjects/PlayerEvents", order = 0)]
public class PlayerEvents : ScriptableObject
{
    public event Action<GameplayData> OnCityChange;

    public event Action OnStart, OnDamage, OnFinish;

    public event Action<bool> OnGameOver;

    public void Start() => OnStart?.Invoke();
    
    public void Damage() => OnDamage?.Invoke();

    public void Finish() => OnFinish?.Invoke();

    public void GameOver(bool value) => OnGameOver?.Invoke(value);

    public void CityChange(GameplayData value) => OnCityChange?.Invoke(value);

    public void CleanAll()
    {
        OnStart = null;
        
        OnDamage = null;

        OnFinish = null;

        OnGameOver = null;

        OnCityChange = null;
    }
}