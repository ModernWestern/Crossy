using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 1)]
public class GameSettings : ScriptableObject
{
    public bool GameOver { get; set; }

    public int goalsAmount = 5;

    [Tooltip("Time in seconds")]
    public float time = 300;

    public TimeType timeType;

    [Range(1, 6)]
    public int lifes = 4;

    private void OnEnable()
    {
        GameOver = false;
    }
}