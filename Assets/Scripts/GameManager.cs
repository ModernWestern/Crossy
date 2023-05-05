using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerEvents playerEvents;

    [SerializeField] private GameSettings settings;

    void Awake()
    {
        Shader.SetGlobalVector(Constants.Light, Vector3.one / 2);
        
        Shader.SetGlobalColor(Constants.Tint, Color.white);

        playerEvents.OnGameOver += OnGameOver;
    }

    private void OnGameOver(bool value)
    {
        settings.GameOver = true;

        Time.timeScale = 0;
    }

    private void OnDestroy()
    {
        Shader.SetGlobalVector(Constants.Light, Vector3.one / 2);

        Shader.SetGlobalColor(Constants.Tint, Color.white);
    }
}