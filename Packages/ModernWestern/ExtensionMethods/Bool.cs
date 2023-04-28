public static class BoolExtensions
{
    /// <summary>
    /// Determines if the given object is equal to any of the specified objects.
    /// </summary>
    /// <param name="obj">The object to compare.</param>
    /// <param name="objects">The objects to compare against.</param>
    /// <returns>True if the object is equal to any of the specified objects, false otherwise.</returns>
    public static bool Equals(this object obj, params object[] objects)
    {
        return objects.Exists(o => o.Equals(obj));
    }

    /// <summary>
    /// Determines if the given object is equal to any of the specified objects of type T.
    /// </summary>
    /// <typeparam name="T">The type of objects to compare against.</typeparam>
    /// <param name="obj">The object to compare.</param>
    /// <param name="objects">The objects to compare against.</param>
    /// <returns>True if the object is equal to any of the specified objects, false otherwise.</returns>
    public static bool Equals<T>(this object obj, params T[] objects)
    {
        return objects.Exists(o => o.Equals(obj));
    }
}