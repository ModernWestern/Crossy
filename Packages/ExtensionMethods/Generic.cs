using UnityEngine;

public static class GenericExtensions
{
    public static T SafeDestroy<T>(this T obj) where T : UnityEngine.Object
    {
        if (Application.isEditor)
        {
            UnityEngine.Object.DestroyImmediate(obj);
        }
        else
        {
            UnityEngine.Object.Destroy(obj);
        }

        return null;
    }
    public static T SafeDestroyGameObject<T>(this T component) where T : Component
    {
        if (component != null)
        {
            SafeDestroy(component.gameObject);
        }

        return null;
    }
}