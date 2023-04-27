public static class ReflectionExtensions
{
    /// <summary>
    /// Gets a reference to the object field with the specified name.
    /// </summary>
    /// <typeparam name="T">The type of the object field to retrieve.</typeparam>
    /// <param name="obj">The object that contains the field to retrieve.</param>
    /// <param name="fieldName">The name of the field to retrieve.</param>
    /// <returns>The reference to the object field with the specified name.</returns>
    public static T GetFieldValueAsReference<T>(this object obj, string fieldName) where T : class
    {
        return GetFieldValue(obj, fieldName) as T;
    }

    /// <summary>
    /// Gets the value of the struct field with the specified name.
    /// </summary>
    /// <typeparam name="T">The type of the struct field to retrieve.</typeparam>
    /// <param name="obj">The object that contains the field to retrieve.</param>
    /// <param name="fieldName">The name of the field to retrieve.</param>
    /// <returns>The value of the struct field with the specified name.</returns>
    public static T GetStructFieldValue<T>(this object obj, string fieldName) where T : struct
    {
        return (T)GetFieldValue(obj, fieldName);
    }

    /// <summary>
    /// Sets the value of a field on an object using reflection.
    /// </summary>
    /// <param name="obj">The object whose field to set.</param>
    /// <param name="fieldName">The name of the field to set.</param>
    /// <param name="newValue">The new value to set for the field.</param>
    public static void SetFieldValue(this object obj, string fieldName, object newValue)
    {
        var info = obj.GetType().GetField(fieldName);

        if (info != null)
        {
            info.SetValue(obj, newValue);
        }
    }

    /// <summary>
    /// Gets the value of a field on an object using reflection.
    /// </summary>
    /// <param name="obj">The object whose field to get.</param>
    /// <param name="fieldName">The name of the field to get.</param>
    /// <returns>The value of the specified field on the object, or null if the field does not exist.</returns>
    private static object GetFieldValue(object obj, string fieldName)
    {
        object ret = null;

        var info = obj.GetType().GetField(fieldName);

        if (info != null)
        {
            ret = info.GetValue(obj);
        }

        return ret;
    }
}