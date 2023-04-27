using UnityEngine;

public static class UIExtensions
{
    /// <summary>
    /// Sets the visibility and interaction of a CanvasGroup.
    /// </summary>
    /// <param name="canvasGroup">The CanvasGroup to set visibility and interaction.</param>
    /// <param name="value">True to make the CanvasGroup visible and interactive, false to hide it and make it non-interactive.</param>
    public static void SetVisibility(this CanvasGroup canvasGroup, bool value)
    {
        if (!canvasGroup) return;

        canvasGroup.alpha = value ? 1 : 0;

        canvasGroup.interactable = value;

        canvasGroup.blocksRaycasts = value;
    }

    /// <summary>
    /// Sets the visibility, transparency and interaction of a CanvasGroup.
    /// </summary>
    /// <param name="canvasGroup">The CanvasGroup to set visibility, transparency and interaction.</param>
    /// <param name="value">True to make the CanvasGroup visible and interactive, false to hide it and make it non-interactive.</param>
    /// <param name="alpha">The transparency to set when hiding the CanvasGroup.</param>
    public static void SetVisibility(this CanvasGroup canvasGroup, bool value, float alpha)
    {
        if (!canvasGroup) return;

        canvasGroup.alpha = value ? 1 : alpha;

        canvasGroup.interactable = value;

        canvasGroup.blocksRaycasts = value;
    }

#if LEANTWEEN

    /// <summary>
    /// Sets the active state of a CanvasGroup with a fade animation.
    /// </summary>
    /// <param name="canvasGroup">The CanvasGroup component to modify.</param>
    /// <param name="value">The target active state of the CanvasGroup.</param>
    /// <param name="time">The duration of the fade animation.</param>
    /// <param name="ease">The easing function to use for the fade animation.</param>
    /// <returns>The object that represents the fade animation.</returns>
    public static LTDescr SetActive(this CanvasGroup canvasGroup, bool value, float time, LeanTweenType? ease = null)
    {
        if (canvasGroup)
        {
            return LeanTween.value(canvasGroup.gameObject, a =>
            {
                canvasGroup.alpha = a;

                canvasGroup.interactable = value;

                canvasGroup.blocksRaycasts = value;
                
            }, canvasGroup.alpha, value ? 1 : 0, time).setEase(ease ?? LeanTweenType.easeInOutSine).setIgnoreTimeScale(true);
        }

        return null;
    }

#endif
}