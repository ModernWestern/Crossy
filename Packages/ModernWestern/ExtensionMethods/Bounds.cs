using System.Text;
using UnityEngine;

public static class BoundsExtension
{
    /// <summary>
    /// Returns the bounding box that encapsulates all the MeshRenderers in the GameObject's children.
    /// </summary>
    /// <param name="transform">The GameObject to calculate the bounds from.</param>
    /// <returns>A Bounds object representing the total bounds of all the MeshRenderers in the children GameObjects.</returns>
    public static Bounds GetEncapsulatedBounds(this Transform transform)
    {
        var totalBounds = new Bounds();

        foreach (var renderer in transform.GetComponentsInChildren<MeshRenderer>())
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

    /// <summary>
    /// Returns the size of the bounding box that encapsulates all the MeshRenderers in the GameObject's children, scaled by the local scale of the GameObject.
    /// </summary>
    /// <param name="transform">The GameObject to calculate the bounds size from.</param>
    /// <returns>A Vector3 object representing the size of the total bounds of all the MeshRenderers in the children GameObjects, scaled by the local scale of the GameObject.</returns>
    public static Vector3 GetEncapsulatedBoundsSize(this Transform transform)
    {
        return Vector3.Scale(transform.localScale, transform.GetEncapsulatedBounds().size);
    }

    public static bool TryGetEncapsulatedBounds(this Transform transform, out Bounds bounds)
    {
        if (transform.TryGetComponent(out MeshRenderer renderer))
        {
            bounds = renderer.bounds;

            return true;
        }

        if (transform.TryGetComponentsInChildren(out MeshRenderer[] meshes))
        {
            bounds = meshes.Accumulate(new Bounds(), (b, m) =>
            {
                b.Encapsulate(m.bounds);

                return b;
            });

            return true;
        }

        bounds = default;

        return false;
    }


    public static bool TryGetEncapsulatedFromMeshFilter(this Transform transform, out Bounds bounds)
    {
        if (transform.TryGetComponent(out MeshFilter mesh))
        {
            bounds = mesh.sharedMesh.bounds;

            return true;
        }

        if (transform.TryGetComponentsInChildren(out MeshFilter[] meshes))
        {
            bounds = meshes.Accumulate(new Bounds(), (b, m) =>
            {
                b.Encapsulate(m.sharedMesh.bounds);

                return b;
            });

            return true;
        }

        bounds = default;

        return false;
    }

    /// <summary>
    ///  The total size of the box. This is always twice as large as the extents.
    /// </summary>
    public static bool TryGetEncapsulatedBoundsSize(this Transform transform, out Vector3 size)
    {
        if (TryGetEncapsulatedBounds(transform, out var bounds))
        {
            size = bounds.size;

            return true;
        }

        size = default;

        return false;
    }

    /// <summary>
    ///  The extents of the Bounding Box. This is always half of the size of the Bounds.
    /// </summary>
    public static bool TryGetEncapsulatedBoundsExtents(this Transform transform, out Vector3 extents)
    {
        if (TryGetEncapsulatedBounds(transform, out var bounds))
        {
            extents = bounds.extents;

            return true;
        }

        extents = default;

        return false;
    }
}