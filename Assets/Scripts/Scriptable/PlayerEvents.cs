using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Events", menuName = "ScriptableObjects/PlayerEvents", order = 0)]
public class PlayerEvents : ScriptableObject
{
    public event Action Damage;

    public event Action GameOver;

    public void OnGameOver()
    {
        GameOver?.Invoke();
    }

    public void OnDamage()
    {
        Damage?.Invoke();
    }

    public void CleanAll()
    {
        GameOver = null;

        Damage = null;
    }
}