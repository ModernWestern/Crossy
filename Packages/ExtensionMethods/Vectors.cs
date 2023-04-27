using System.Collections.Generic;
using UnityEngine;

public static class VectorsExtensions
{
    #region FROM SCREEN COORDINATES

    /// <summary>
    /// Screen coordinates clamp to screen. Padding amount in screen resolution.
    /// </summary>
    public static Vector3 SCClamp2Screen(this Vector3 vector, Vector2 padding)
    {
        const int MAX = 1;

        var viewport = Camera.main.ScreenToViewportPoint(vector);

        var x = (Screen.width - padding.x).Remap(0, padding.x, 0, 1);

        var y = (Screen.height - padding.y).Remap(0, padding.y, 0, 1);

        viewport.x = Mathf.Clamp(viewport.x, x, MAX - x);

        viewport.y = Mathf.Clamp(viewport.y, y, MAX - y);

        return Camera.main.ViewportToScreenPoint(viewport);
    }

    /// <summary>
    /// Screen coordinates clamp to screen. Padding amount between 0 and 1.
    /// </summary>
    public static Vector3 SCClamp2Screen(this Vector3 vector, float padding)
    {
        const int MAX = 1;

        var viewport = Camera.main.ScreenToViewportPoint(vector);

        viewport.x = Mathf.Clamp(viewport.x, padding, MAX - padding);

        viewport.y = Mathf.Clamp(viewport.y, padding, MAX - padding);

        return Camera.main.ViewportToScreenPoint(viewport);
    }

    /// <summary>
    /// Screen coordinates clamp to viewport. Padding amount between 0 and 1.
    /// </summary>
    public static Vector3 SCClamp2Viewport(this Vector3 vector, float padding)
    {
        const int MAX = 1;

        var viewport = Camera.main.ScreenToViewportPoint(vector);

        viewport.x = Mathf.Clamp(viewport.x, padding, MAX - padding);

        viewport.y = Mathf.Clamp(viewport.y, padding, MAX - padding);

        return viewport;
    }

    /// <summary>
    /// Screen coordinates clamp to viewport. Padding amount between 0 and 1.
    /// </summary>
    public static Vector3 SCClamp2Viewport(this Vector3 vector, Camera camera, float padding)
    {
        const int MAX = 1;

        var viewport = camera.ScreenToViewportPoint(vector);

        viewport.x = Mathf.Clamp(viewport.x, padding, MAX - padding);

        viewport.y = Mathf.Clamp(viewport.y, padding, MAX - padding);

        return viewport;
    }

    /// <summary>
    /// Clamp into the assigned camera frustum. Padding amount in screen resolution.
    /// </summary>
    public static Vector3 FromScreenCoordinatesClamped(this Vector3 vector, Camera camera, Vector2 padding)
    {
        const int MAX = 1;

        var position = camera.ScreenToViewportPoint(vector);

        var x = (Screen.width - padding.x).Remap(0, padding.x, 0, 1);

        var y = (Screen.height - padding.y).Remap(0, padding.y, 0, 1);

        position.x = Mathf.Clamp(position.x, x, MAX - x);

        position.y = Mathf.Clamp(position.y, y, MAX - y);

        return position;
    }

    /// <summary>
    /// Pixel coordinates to viewport coordinates.
    /// </summary>
    public static Vector3 ScreenToViewportPoint(this Vector3 position)
    {
        return Camera.main.ScreenToViewportPoint(position);
    }

    /// <summary>
    /// Pixel coordinates to viewport coordinates.
    /// </summary>
    public static Vector3 ScreenToViewportPoint(this Vector3 position, Camera camera)
    {
        return camera.ScreenToViewportPoint(position);
    }

    /// <summary>
    /// Pixel coordinates to viewport coordinates. Set padding to clamp between 0 and 1 (Viewport coordinates).
    /// </summary>
    public static Vector3 ScreenToViewportPoint(this Vector3 vector, float padding)
    {
        const int MAX = 1;

        var position = Camera.main.ScreenToViewportPoint(vector);

        position.x = Mathf.Clamp(position.x, padding, MAX - padding);

        position.y = Mathf.Clamp(position.y, padding, MAX - padding);

        return position;
    }

    /// <summary>
    /// Pixel coordinates to viewport coordinates. Set padding to clamp between 0 and 1 (Viewport coordinates).
    /// </summary>
    public static Vector3 ScreenToViewportPoint(this Vector3 vector, Vector2 padding)
    {
        const int MAX = 1;

        var position = Camera.main.ScreenToViewportPoint(vector);

        position.x = Mathf.Clamp(position.x, padding.x, MAX - padding.x);

        position.y = Mathf.Clamp(position.y, padding.y, MAX - padding.y);

        return position;
    }

    #endregion

    /// <summary>
    /// Returns world position clamped into the main camera frustum. Set padding between 0 and 1.
    /// </summary>
    public static Vector3 PositionClamped(this Vector3 target, float padding)
    {
        const int MAX = 1;

        var position = Camera.main.WorldToViewportPoint(target);

        position.x = Mathf.Clamp(position.x, padding, MAX - padding);

        position.y = Mathf.Clamp(position.y, padding, MAX - padding);

        return Camera.main.ViewportToWorldPoint(position);
    }

    /// <summary>
    /// Returns world position clamped into the main camera frustum. Set padding between 0 and 1.
    /// </summary>
    public static Vector3 PositionClamped(this Vector3 target, Vector2 padding)
    {
        const int MAX = 1;

        var position = Camera.main.WorldToViewportPoint(target);

        position.x = Mathf.Clamp(position.x, padding.x, MAX - padding.x);

        position.y = Mathf.Clamp(position.y, padding.y, MAX - padding.y);

        return Camera.main.ViewportToWorldPoint(position);
    }

    /// <summary>
    /// Returns world position clamped into the main camera frustum and turned into screen space (RectTransform.position). Set padding between 0 and 1.
    /// </summary>
    public static Vector3 PositionClampedToScreenPoint(this Vector2 padding)
    {
        const int MAX = 1;

        var position = Camera.main.WorldToViewportPoint(padding);

        position.x = Mathf.Clamp(position.x, 0, MAX - 0);

        position.y = Mathf.Clamp(position.y, 0, MAX - 0);

        return Camera.main.ViewportToScreenPoint(position);
    }

    /// <summary>
    /// Returns world position clamped into the main camera frustum. Set padding between 0 and 1.
    /// </summary>
    public static Vector3 PositionClampedToViewportPoint(this Vector2 padding)
    {
        const int MAX = 1;

        var position = Camera.main.WorldToViewportPoint(padding);

        position.x = Mathf.Clamp(position.x, 0, MAX - 0);

        position.y = Mathf.Clamp(position.y, 0, MAX - 0);

        return position;
    }

    /// <summary>
    /// Returns world position clamped into the main camera frustum and turned into screen space (RectTransform.position). Set padding between 0 and 1.
    /// </summary>
    public static Vector3 PositionClampedToScreenPoint(this Vector3 padding)
    {
        const int MAX = 1;

        var position = Camera.main.WorldToViewportPoint(padding);

        position.x = Mathf.Clamp(position.x, 0, MAX - 0);

        position.y = Mathf.Clamp(position.y, 0, MAX - 0);

        return Camera.main.ViewportToScreenPoint(position);
    }

    public static Vector3 PositionClampedToScreenPoint(this Vector3 target, float padding)
    {
        const int MAX = 1;

        var position = Camera.main.WorldToViewportPoint(target);

        position.x = Mathf.Clamp(position.x, padding, MAX - padding);

        position.y = Mathf.Clamp(position.y, padding, MAX - padding);

        return Camera.main.ViewportToScreenPoint(position);
    }

    /// <summary>
    /// Returns world position clamped into the main camera frustum and turned into screen space (RectTransform.position). Set padding between 0 and 1.
    /// </summary>
    public static Vector3 PositionClampedToScreenPoint(this Vector3 target, Vector2 padding)
    {
        const int MAX = 1;

        var position = Camera.main.WorldToViewportPoint(target);

        position.x = Mathf.Clamp(position.x, padding.x, MAX - padding.x);

        position.y = Mathf.Clamp(position.y, padding.y, MAX - padding.y);

        return Camera.main.ViewportToScreenPoint(position);
    }

    public static Vector3 Round(this Vector3 v, bool x = true, bool y = true, bool z = true)
    {
        return new Vector3
        {
            x = x ? Mathf.Round(v.x) : v.x,
            y = y ? Mathf.Round(v.y) : v.y,
            z = z ? Mathf.Round(v.z) : v.z
        };
    }

    public static Vector3 Abs(this Vector3 v, bool x = true, bool y = true, bool z = true)
    {
        return new Vector3
        {
            x = x ? Mathf.Abs(v.x) : v.x,
            y = y ? Mathf.Abs(v.y) : v.y,
            z = z ? Mathf.Abs(v.z) : v.z
        };
    }

    public static bool Approximately(this Vector3 a, Vector3 b)
    {
        return a.sqrMagnitude.Round().Equals(b.sqrMagnitude.Round());
    }

    public static Vector3 GetDirection(this Vector3 v, Vector3 target)
    {
        return (target - v).normalized;
    }

    public static float GetAngleFromVector(this Vector3 v)
    {
        v = v.normalized;

        float n = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;

        if (n < 0) n += 360;

        return n;
    }

    public static Vector2 GetDirection(this Vector2 v, Vector2 target)
    {
        return (target - v).normalized;
    }

    public static float GetAngleFromVector(this Vector2 v)
    {
        v = v.normalized;

        float n = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;

        if (n < 0) n += 360;

        return n;
    }

    public static float GetAngleFromVector(this Vector2 v, Vector2 target)
    {
        return (float)(Mathf.Atan2(target.y - v.y, target.x - v.x) * (180 / Mathf.PI));
    }
}