using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public static class TransformExtensions
{
    /// <summary>
    /// Returns an array of the immediate children of the specified Transform.
    /// </summary>
    /// <param name="transform">The parent Transform.</param>
    /// <returns>An array of the immediate children of the specified Transform.</returns>
    public static Transform[] GetChildren(this Transform transform)
    {
        return transform.Cast<Transform>().ToArray();
    }

    /// <summary>
    /// Returns the last immediate child of the specified Transform, or null if it has no children.
    /// </summary>
    /// <param name="transform">The parent Transform.</param>
    /// <returns>The last immediate child of the specified Transform, or null if it has no children.</returns>
    public static Transform GetLastChildren(this Transform transform)
    {
        return transform.Cast<Transform>().LastOrDefault();
    }

    /// <summary>
    /// Returns an array of the immediate child GameObjects of the specified Transform.
    /// </summary>
    /// <param name="transform">The parent Transform.</param>
    /// <returns>An array of the immediate child GameObjects of the specified Transform.</returns>
    public static GameObject[] GetChildrenAsGameObjects(this Transform transform)
    {
        return transform.GetChildren().ConvertAll(child => child.gameObject).ToArray();
    }

    /// <summary>
    /// Returns an IEnumerable of the immediate child components of the specified Transform that are of the specified type T.
    /// </summary>
    /// <typeparam name="T">The type of component to search for.</typeparam>
    /// <param name="transform">The parent Transform.</param>
    /// <returns>An IEnumerable of the immediate child components of the specified Transform that are of the specified type T.</returns>
    public static IEnumerable<T> GetChildrenAs<T>(this Transform transform)
    {
        return transform.GetChildren().ConvertAll(child => child.TryGetComponent(out T component) ? component : default);
    }

    /// <summary>
    /// Returns the last immediate child of the specified Transform that has the specified component type.
    /// </summary>
    /// <typeparam name="T">The type of the component to search for.</typeparam>
    /// <param name="transform">The parent Transform.</param>
    /// <returns>The last immediate child of the specified Transform that has the specified component type.</returns>
    public static T GetLastChildrenAs<T>(this Transform transform)
    {
        return transform.GetChildren().ConvertAll(child => child.TryGetComponent(out T component) ? component : default).LastOrDefault();
    }

    /// <summary>
    /// Returns the first active child transform of the given parent transform.
    /// </summary>
    /// <param name="transform">The parent transform to search in.</param>
    /// <returns>The first active child transform or null if none found.</returns>
    /// <example>
    /// <code>
    /// Transform firstActiveChild = transform.GetFirstActiveChild();
    /// </code>
    /// </example>
    public static Transform GetFirstActiveChild(this Transform transform)
    {
        var child = transform.GetChildren().FirstOrDefault(child =>
        {
            var go = child.gameObject;

            return go.activeInHierarchy && go.activeSelf;
        });

        return child;
    }

    /// <summary>
    /// Attempts to get the first component of type T in the children of the transform.
    /// </summary>
    /// <typeparam name="T">The type of the component to get.</typeparam>
    /// <param name="transform">The transform to search for the component.</param>
    /// <param name="component">The component found.</param>
    /// <param name="includeInactive">Should inactive children be included in the search?</param>
    /// <returns>True if the component was found, false otherwise.</returns>
    public static bool TryGetComponentInChildren<T>(this Transform transform, out T component, bool includeInactive = false)
    {
        component = transform.GetComponentInChildren<T>(includeInactive);

        return component is { };
    }

    /// <summary>
    /// Attempts to get all components of type T in the child objects of the specified transform, and returns the results in an out parameter.
    /// </summary>
    /// <typeparam name="T">The type of the components to retrieve.</typeparam>
    /// <param name="transform">The transform whose child objects are to be searched.</param>
    /// <param name="components">If components of type T are found, returns an array containing those components; otherwise, returns an empty array.</param>
    /// <param name="includeInactive">If true, also include inactive child objects in the search.</param>
    /// <returns>True if components of type T are found in the child objects, false otherwise.</returns>
    public static bool TryGetComponentsInChildren<T>(this Transform transform, out T[] components, bool includeInactive = false)
    {
        components = transform.GetComponentsInChildren<T>(includeInactive);

        return components is { Length: > 0 };
    }


    // public static void FromWorldCoordinatesLookAt(this Transform transform, Vector3 target)
    // {
    //     transform.rotation = Quaternion.AngleAxis(transform.position.GetDirection(target).GetAngleFromVector(), Vector3.forward);
    // }
    //
    // public static void FromScreenCoordinatesLookAt(this Transform transform, Vector3 target)
    // {
    //     transform.rotation = Quaternion.AngleAxis(transform.position.GetDirection(Camera.main.WorldToScreenPoint(target)).GetAngleFromVector(), Vector3.forward);
    // }
    //
    // /// <summary>
    // /// Creates a rotation which rotates angle in degrees around axis.
    // /// </summary>
    // public static void FromWorldCoordinatesLookAt(this Transform transform, Vector3 target, Vector3 axis)
    // {
    //     transform.rotation = Quaternion.AngleAxis(transform.position.GetDirection(target).GetAngleFromVector(), axis);
    // }
    //
    // /// <summary>
    // /// Creates a rotation which rotates angle in degrees around axis.
    // /// </summary>
    // public static void FromScreenCoordinatesLookAt(this Transform transform, Vector3 target, Vector3 axis)
    // {
    //     transform.rotation = Quaternion.AngleAxis(transform.position.GetDirection(Camera.main.WorldToScreenPoint(target)).GetAngleFromVector(), axis);
    // }
    //
    // public static Vector3 PositionClampedToScreenPoint(this Transform target)
    // {
    //     const int MAX = 1;
    //
    //     var padding = Camera.main.WorldToViewportPoint(target.position);
    //
    //     padding.x = Mathf.Clamp(padding.x, 0, MAX - 0);
    //
    //     padding.y = Mathf.Clamp(padding.y, 0, MAX - 0);
    //
    //     return Camera.main.ViewportToScreenPoint(padding);
    // }
    //
    // public static Vector3 PositionClampedToScreenPoint(this Transform target, float amount)
    // {
    //     const int MAX = 1;
    //
    //     var padding = Camera.main.WorldToViewportPoint(target.position);
    //
    //     padding.x = Mathf.Clamp(padding.x, amount, MAX - amount);
    //
    //     padding.y = Mathf.Clamp(padding.y, amount, MAX - amount);
    //
    //     return Camera.main.ViewportToScreenPoint(padding);
    // }
    //
    // /// <summary>
    // /// Returns world position clamped into the main camera frustum and turned into screen space (RectTransform.position). Set amount between 0 and 1.
    // /// </summary>
    // public static Vector3 PositionClampedToScreenPoint(this Transform target, Vector2 amount)
    // {
    //     const int MAX = 1;
    //
    //     var padding = Camera.main.WorldToViewportPoint(target.position);
    //
    //     padding.x = Mathf.Clamp(padding.x, amount.x, MAX - amount.x);
    //
    //     padding.y = Mathf.Clamp(padding.y, amount.y, MAX - amount.y);
    //
    //     return Camera.main.ViewportToScreenPoint(padding);
    // }

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