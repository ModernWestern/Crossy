using System;
using UnityEngine;
using ModernWestern;

[Serializable]
public struct Panel
{
    public CanvasGroup[] panels;

    public void SetActive(bool value)
    {
        foreach (var panel in panels)
        {
            panel.SetVisibility(value);
        }
    }
}

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerEvents playerEvents;

    [SerializeField] private CanvasGroup[] gameoverPanels;

    [SerializeField] private Panel settings, gameplay;

    private void Awake()
    {
        settings.SetActive(true);

        gameplay.SetActive(false);

        playerEvents.OnStart += () =>
        {
            settings.SetActive(false);

            gameplay.SetActive(true);
        };

        playerEvents.OnGameOver += value => { gameoverPanels[value ? 0 : 1].SetActive(true, 1.5f, LeanTweenType.easeInOutSine); };
    }
}