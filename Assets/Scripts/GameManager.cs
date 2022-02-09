using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Vector3 directionLight;

    [SerializeField] private GameSettings settings;

    [SerializeField] private PlayerEvents playerEvents;

    void Awake()
    {
        Shader.SetGlobalVector(Constants.Light, directionLight);

        playerEvents.GameOver += OnGameOver;
    }

    private void OnGameOver()
    {
        settings.GameOver = true;

        Time.timeScale = 0;
    }
}