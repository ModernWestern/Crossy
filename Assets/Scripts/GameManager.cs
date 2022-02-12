using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerEvents playerEvents;

    [SerializeField] private GameSettings settings;

    [SerializeField] private Vector3 customLight;


    void Awake()
    {
#if UNITY_EDITOR && !UNITY_WEBGL

        Application.targetFrameRate = 60;
#endif
        Shader.SetGlobalVector(Constants.Light, customLight);

        playerEvents.OnGameOver += OnGameOver;
    }

    private void OnGameOver(bool value)
    {
        settings.GameOver = true;

        Time.timeScale = 0;
    }
}