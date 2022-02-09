using UnityEngine;

public static class RectTransformextensions
{
    // https://stackoverflow.com/questions/42043017/check-if-ui-elements-recttransform-are-overlapping

    /// <summary>
    /// Returns true if the other rectangle overlaps this one.
    /// </summary>
    public static bool Overlaps(this RectTransform rt, RectTransform rectTransform)
    {
        return rt.WorldRect().Overlaps(rectTransform.WorldRect());
    }

    /// <summary>
    /// Returns true if the other rectangle overlaps this one. If allowInverse is present
    /// and true, the widths and heights of the Rects are allowed to take negative values
    /// (ie, the min value is greater than the max), and the setAsActive will still work.
    /// </summary>
    public static bool Overlaps(this RectTransform rt, RectTransform rectTransform, bool allowInverse)
    {
        return rt.WorldRect().Overlaps(rectTransform.WorldRect(), allowInverse);
    }

    /// <summary>
    /// Float the Rect of this Recttransform.
    /// </summary>
    public static Rect WorldRect(this RectTransform rt)
    {
        var sizeDelta = rt.sizeDelta;

        var rectTransformWidth = sizeDelta.x * rt.lossyScale.x;

        var rectTransformHeight = sizeDelta.y * rt.lossyScale.y;

        var position = rt.position;

        return new Rect(position.x - rectTransformWidth / 2f, position.y - rectTransformHeight / 2f, rectTransformWidth, rectTransformHeight);
    }

    public static void SetLeft(this RectTransform rect, float left)
    {
        rect.offsetMin = new Vector2(left, rect.offsetMin.y);
    }

    public static void SetRight(this RectTransform rect, float right)
    {
        rect.offsetMax = new Vector2(-right, rect.offsetMax.y);
    }

    public static void SetTop(this RectTransform rect, float top)
    {
        rect.offsetMax = new Vector2(rect.offsetMax.x, -top);
    }

    public static void SetBottom(this RectTransform rect, float bottom)
    {
        rect.offsetMin = new Vector2(rect.offsetMin.x, bottom);
    }
}