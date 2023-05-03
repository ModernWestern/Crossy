using TMPro;
using System;
using Json2CSharp;
using UnityEngine;
using ModernWestern;
using System.Collections.Generic;

public class ClockModule : MonoBehaviour
{
    [SerializeField] private RectTransform clock;

    [SerializeField] private TMP_Text text;

    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private float parentPadding = 20;

    private string current;
    
    private float clockHeight;

    private List<int> ids = new();

    public City City
    {
        set => UpdateClock(value);
    }
    
    private void Awake()
    {
        ClockPosition(parentPadding);

        clockHeight = clock.rect.height;
    }

    private void UpdateClock(City city)
    {
        canvasGroup.alpha = 1;

        if (ids is { Count: > 0 })
        {
            foreach (var id in ids)
            {
                LeanTween.cancel(gameObject, id);
            }

            ids.Clear();
        }

        if (current == city.name)
        {
            LeanTween.delayedCall(gameObject, 3, () => LeanTween.alphaCanvas(canvasGroup, 0, 3).StoreUniqueId(ref ids)).StoreUniqueId(ref ids);
            
            ClockPosition(-clockHeight);

            return;
        }

        current = city.name;

        LeanTween.move(clock, Vector3.up * parentPadding, 0.45f).setEase(LeanTweenType.easeInOutBack).setOnComplete(() =>
        {
            LeanTween.move(clock, Vector3.up * -clockHeight, 1).setEase(LeanTweenType.easeOutBack).setOnComplete(() =>
            {
                LeanTween.delayedCall(gameObject, 3, () => LeanTween.alphaCanvas(canvasGroup, 0, 3).StoreUniqueId(ref ids)).StoreUniqueId(ref ids);
                
            }).StoreUniqueId(ref ids);

            text.text = $"<b>{city.name.ToFirstUpper()},{city.country.ToUpper()}</b> {DateTime.UtcNow.AddHours(city.gmt):MMMM dd\nHH:mm}\n<size=18>GMT{city.gmt}</size>";
            
        }).StoreUniqueId(ref ids);
    }

    private void ClockPosition(float height)
    {
        var clockPosition = clock.anchoredPosition;

        clockPosition.y = height;

        clock.anchoredPosition = clockPosition;
    }
}