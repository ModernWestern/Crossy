using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Vector3 directionLight;

    [SerializeField] private GameSettings settings;

    [SerializeField] private PlayerEvents playerEvents;

    void Awake()
    {
#if UNITY_EDITOR && !UNITY_WEBGL

        Application.targetFrameRate = 60;
#endif
        Shader.SetGlobalVector(Constants.Light, directionLight);

        playerEvents.OnGameOver += OnGameOver;
    }

    private void OnGameOver()
    {
        settings.GameOver = true;

        Time.timeScale = 0;
    }
}