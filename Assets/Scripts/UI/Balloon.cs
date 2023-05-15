using System;
using UnityEngine;
using ModernWestern;
using UnityEngine.UI;

public class Balloon : MonoBehaviour
{
    private static event Action<Player> SetPlayer;

    [SerializeField] private CanvasGroup group;

    [SerializeField] private float speed = 5f;

    [SerializeField, Min(0)] private int pollingRate = 60;

    [SerializeField] private Vector3 offset;

    [SerializeField] private Image image;

    [SerializeField] private Sprite[] sequence;

    private Poll poll, animationSequence;

    private new Camera camera;
    
    private bool isActive;

    private int? id;

    private void Awake()
    {
        camera = Camera.main;

        poll = new Poll(pollingRate, this);

        animationSequence = new Poll(5, this);

        SetPlayer += player =>
        {
            if (poll.IsRunning)
            {
                poll.Stop();
            }

            if (player)
            {
                poll.Start(() => !player.ChickenCrossTheRoad, () => SetVisible(true), () => OnSetPlayer(player), () => SetVisible(false));
            }
            else
            {
                SetVisible(false);
            }
        };
    }

    public static void SetActive(Player player) => SetPlayer?.Invoke(player);

    private void OnSetPlayer(Player player)
    {
        if (!player)
        {
            return;
        }

        var targetPos = camera.WorldToScreenPoint(player.transform.position + offset);

        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * (isActive ? speed : 100000));
    }

    private void SetVisible(bool value)
    {
        if (!group)
        {
            return;
        }

        if (id.HasValue)
        {
            LeanTween.cancel(id.Value);

            id = null;
        }

        id = LeanTween.alphaCanvas(group, value ? 1 : 0, value ? 0.25f : 0.1f).setIgnoreTimeScale(true).uniqueId;

        if (animationSequence.IsRunning)
        {
            return;
        }

        var flipFlop = false;

        animationSequence.Start(() => value, () =>
        {
            image.sprite = sequence[flipFlop ? 0 : 1];

            flipFlop = !flipFlop;
        });
    }
}