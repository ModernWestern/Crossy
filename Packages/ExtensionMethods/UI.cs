using UnityEngine;

public static class UIExtensions
{
    public static void SetActive(this CanvasGroup canvasGroup, bool value)
    {
        if (canvasGroup)
        {
            canvasGroup.alpha = value ? 1 : 0;

            canvasGroup.interactable = value;

            canvasGroup.blocksRaycasts = value;
        }
    }

    public static void SetActive(this CanvasGroup canvasGroup, bool value, float alpha)
    {
        if (canvasGroup)
        {
            canvasGroup.alpha = value ? 1 : alpha;

            canvasGroup.interactable = value;

            canvasGroup.blocksRaycasts = value;
        }
    }

#if LEANTWEEN_MODULE

    public static LTDescr SetActive(this CanvasGroup canvasGroup, bool value, float time, LeanTweenType? ease)
    {
        if (canvasGroup)
        {
            return LeanTween.value(canvasGroup.gameObject, a =>
            {
                canvasGroup.alpha = a;

                canvasGroup.interactable = value;

                canvasGroup.blocksRaycasts = value;

            }, canvasGroup.alpha, value ? 1 : 0, time).setEase(ease ?? LeanTweenType.linear);
        }

        return null;
    }

#endif
}