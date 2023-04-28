using UnityEngine;

public static class GameObjectExtensions
{
    public static bool TryGetComponentInChildren<T>(this GameObject gameObject, out T component)
    {
        return gameObject.transform.TryGetComponentInChildren(out component);
    }

    public static bool TryGetComponentInChildren<T>(this GameObject gameObject, out T component, bool includeInactive)
    {
        return gameObject.transform.TryGetComponentInChildren(out component, includeInactive);
    }

    public static bool TryGetComponentsInChildren<T>(this GameObject gameObject, out T[] components)
    {
        return gameObject.transform.TryGetComponentsInChildren(out components);
    }

    public static bool TryGetComponentsInChildren<T>(this GameObject gameObject, out T[] components, bool includeInactive)
    {
        return gameObject.transform.TryGetComponentsInChildren(out components, includeInactive);
    }

    /// <summary>
    /// Try get a component attached to an object that could be null or empty at some point.
    /// </summary>
    public static bool? TryGetComponentCarefully<T>(this GameObject gameObject, out T component)
    {
        if (!gameObject)
        {
            component = default;

            return null;
        }
        else return gameObject.TryGetComponent(out component);
    }

    public static Bounds GetEncapsulatedBounds(this GameObject gameObject)
    {
        Bounds totalBounds = new Bounds();

        foreach (MeshRenderer renderer in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            if (totalBounds.size.magnitude == 0)
            {
                totalBounds = renderer.bounds;
            }
            else
            {
                totalBounds.Encapsulate(renderer.bounds);
            }
        }

        return totalBounds;
    }

    public static Vector3 GetEncapsulatedBoundsSize(this GameObject gameObject)
    {
        Bounds bounds = new Bounds();

        foreach (MeshRenderer renderer in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            if (bounds.size.magnitude == 0)
            {
                bounds = renderer.bounds;
            }
            else
            {
                bounds.Encapsulate(renderer.bounds);
            }
        }

        return Vector3.Scale(gameObject.transform.localScale, bounds.size);
    }

    public static bool TryGetEncapsulatedBounds(this GameObject gameObject, out Bounds bounds)
    {
        if (gameObject.TryGetComponent(out MeshRenderer renderer))
        {
            bounds = renderer.bounds;

            return true;
        }
        else
        {
            if (gameObject.TryGetComponentsInChildren(out MeshRenderer[] renderers))
            {
                bounds = new Bounds();

                for (int i = 0; i < renderers.Length; i++)
                {
                    bounds.Encapsulate(renderers[i].bounds);
                }

                return true;
            }
            else
            {
                bounds = default;

                return false;
            }
        }
    }

    public static bool TryGetEncapsulatedMeshFilterBounds(this GameObject gameObject, out Bounds bounds)
    {
        if (gameObject.TryGetComponent(out MeshFilter mesh))
        {
            bounds = mesh.sharedMesh.bounds;

            return true;
        }
        else
        {
            if (gameObject.TryGetComponentsInChildren(out MeshFilter[] meshes))
            {
                bounds = new Bounds();

                for (int i = 0; i < meshes.Length; i++)
                {
                    bounds.Encapsulate(meshes[i].sharedMesh.bounds);
                }

                return true;
            }
            else
            {
                bounds = default;

                return false;
            }
        }
    }

    /// <summary>
    ///  The total size of the box. This is always twice as large as the extents.
    /// </summary>
    public static bool TryGetEncapsulatedBoundsSize(this GameObject gameObject, out Vector3 size)
    {
        if (TryGetEncapsulatedBounds(gameObject, out Bounds bounds))
        {
            size = bounds.size;

            return true;
        }
        else
        {
            size = default;

            return false;
        }
    }

    /// <summary>
    ///  The extents of the Bounding Box. This is always half of the size of the Bounds.
    /// </summary>
    public static bool TryGetEncapsulatedBoundsExtents(this GameObject gameObject, out Vector3 extents)
    {
        if (TryGetEncapsulatedBounds(gameObject, out Bounds bounds))
        {
            extents = bounds.extents;

            return true;
        }
        else
        {
            extents = default;

            return false;
        }
    }
}