using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerEvents playerEvents;

    [SerializeField] private CanvasGroup[] gameoverPanels;

    private void Awake()
    {
        playerEvents.OnGameOver += value =>
        {
            gameoverPanels[value ? 0 : 1].SetActive(true, 1.5f, LeanTweenType.easeInOutSine);

            Debug.Log(value);
        };
    }
}