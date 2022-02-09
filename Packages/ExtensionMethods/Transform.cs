using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public static class TransformExtensions
{
    public static Transform[] Children(this Transform transform)
    {
        var child = new Transform[transform.childCount];

        for (int i = 0, l = transform.childCount; i < l; i++)
        {
            child[i] = transform.GetChild(i);
        }

        return child;
    }

    public static T[] Children<T>(this Transform transform)
    {
        var child = new Transform[transform.childCount];

        for (int i = 0, l = transform.childCount; i < l; i++)
        {
            child[i] = transform.GetChild(i);
        }

        return child.ConvertAll(c => c.GetComponent<T>());
    }

    public static Transform GetFirstActiveChild(this Transform transform)
    {
        var child = transform.Children().FirstOrDefault(c => c.gameObject.activeInHierarchy && c.gameObject.activeSelf);

        return child;
    }

    public static bool TryGetComponentInChildren<T>(this Transform transform, out T component)
    {
        component = transform.GetComponentInChildren<T>();

        return component is T;
    }

    public static bool TryGetComponentsInChildren<T>(this Transform transform, out T[] components)
    {
        components = transform.GetComponentsInChildren<T>();

        return components.Length > 0;
    }

    public static bool TryGetComponentInChildren<T>(this Transform transform, out T component, bool includeInactive)
    {
        component = transform.GetComponentInChildren<T>(includeInactive);

        return component is T;
    }

    public static bool TryGetComponentsInChildren<T>(this Transform transform, out T[] components, bool includeInactive)
    {
        components = transform.GetComponentsInChildren<T>(includeInactive);

        return components.Length > 0;
    }

    public static T[] GetComponentsInChildrenIgnoringParent<T>(this Transform transform)
    {
        var collection = new List<T>();

        for (int i = 0; i < transform.childCount; i++)
        {
            T[] components = transform.GetChild(i).GetComponentsInChildren<T>();

            if (components != null)
            {
                for (int j = 0, l = components.Length; j < l; j++)
                {
                    collection.Add(components[j]);
                }
            }
        }

        return collection.ToArray();
    }

    public static T[] GetComponentsInChildrenIgnoringParent<T>(this Transform transform, bool includeInactive)
    {
        var collection = new List<T>();

        for (int i = 0; i < transform.childCount; i++)
        {
            T[] components = transform.GetChild(i).GetComponentsInChildren<T>(includeInactive);

            if (components != null)
            {
                for (int j = 0, l = components.Length; j < l; j++)
                {
                    collection.Add(components[j]);
                }
            }
        }

        return collection.ToArray();
    }

    public static void FromWorldCoordinatesLookAt(this Transform transform, Vector3 target)
    {
        transform.rotation = Quaternion.AngleAxis(transform.position.GetDirection(target).GetAngleFromVector(), Vector3.forward);
    }

    public static void FromScreenCoordinatesLookAt(this Transform transform, Vector3 target)
    {
        transform.rotation = Quaternion.AngleAxis(transform.position.GetDirection(Camera.main.WorldToScreenPoint(target)).GetAngleFromVector(), Vector3.forward);
    }

    /// <summary>
    /// Creates a rotation which rotates angle in degrees around axis.
    /// </summary>
    public static void FromWorldCoordinatesLookAt(this Transform transform, Vector3 target, Vector3 axis)
    {
        transform.rotation = Quaternion.AngleAxis(transform.position.GetDirection(target).GetAngleFromVector(), axis);
    }

    /// <summary>
    /// Creates a rotation which rotates angle in degrees around axis.
    /// </summary>
    public static void FromScreenCoordinatesLookAt(this Transform transform, Vector3 target, Vector3 axis)
    {
        transform.rotation = Quaternion.AngleAxis(transform.position.GetDirection(Camera.main.WorldToScreenPoint(target)).GetAngleFromVector(), axis);
    }

    public static Vector3 PositionClampedToScreenPoint(this Transform target)
    {
        const int MAX = 1;

        var padding = Camera.main.WorldToViewportPoint(target.position);

        padding.x = Mathf.Clamp(padding.x, 0, MAX - 0);

        padding.y = Mathf.Clamp(padding.y, 0, MAX - 0);

        return Camera.main.ViewportToScreenPoint(padding);
    }

    public static Vector3 PositionClampedToScreenPoint(this Transform target, float amount)
    {
        const int MAX = 1;

        var padding = Camera.main.WorldToViewportPoint(target.position);

        padding.x = Mathf.Clamp(padding.x, amount, MAX - amount);

        padding.y = Mathf.Clamp(padding.y, amount, MAX - amount);

        return Camera.main.ViewportToScreenPoint(padding);
    }

    /// <summary>
    /// Returns world position clamped into the main camera frustum and turned into screen space (RectTransform.position). Set amount between 0 and 1.
    /// </summary>
    public static Vector3 PositionClampedToScreenPoint(this Transform target, Vector2 amount)
    {
        const int MAX = 1;

        var padding = Camera.main.WorldToViewportPoint(target.position);

        padding.x = Mathf.Clamp(padding.x, amount.x, MAX - amount.x);

        padding.y = Mathf.Clamp(padding.y, amount.y, MAX - amount.y);

        return Camera.main.ViewportToScreenPoint(padding);
    }

    ///// <summary>
    ///// Set padding amount between 0 and 1.
    ///// </summary>
    //public static Vector3 ScreenPointClamped(this RectTransform target, float amount)
    //{
    //    const int MAX = 1;

    //    float width = Screen.width / 2;

    //    float height = Screen.height / 2;

    //    float x = target.localPosition.x.Remap(-width, width, 0, Screen.width);

    //    float y = target.localPosition.x.Remap(-height, height, 0, Screen.height);

    //    var padding = Camera.main.ScreenToViewportPoint(new Vector3(x, y, target.localPosition.z));

    //    padding.x = Mathf.Clamp(padding.x, amount, MAX - amount);
    //    padding.y = Mathf.Clamp(padding.y, amount, MAX - amount);

    //    return Camera.main.ViewportToWorldPoint(padding);
    //}

    ///// <summary>
    ///// Set padding amount between 0 and 1.
    ///// </summary>
    //public static Vector3 ScreenToWorldPointClamp(this RectTransform target, float amount)
    //{
    //    const int MAX = 1;

    //    float width = Screen.width / 2;

    //    float height = Screen.height / 2;

    //    float x = target.localPosition.x.Remap(-width, width, 0, Screen.width);

    //    float y = target.localPosition.x.Remap(-height, height, 0, Screen.height);

    //    var padding = Camera.main.ScreenToViewportPoint(new Vector3(x, y, target.localPosition.z));

    //    //var padding = Camera.main.ScreenToViewportPoint(target.localPosition);

    //    padding.x = Mathf.Clamp(padding.x, amount, MAX - amount);
    //    padding.y = Mathf.Clamp(padding.y, amount, MAX - amount);

    //    return Camera.main.ViewportToWorldPoint(padding);
    //}
}