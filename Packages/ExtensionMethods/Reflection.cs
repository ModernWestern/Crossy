using System.Reflection;

public static class ReflectionExtensions
{
    public static T GetReference<T>(this object obj, string fieldName) where T : class
    {
        return GetField(obj, fieldName) as T;
    }

    public static T GetValue<T>(this object obj, string fieldName) where T : struct
    {
        return (T)GetField(obj, fieldName);
    }

    public static void SetField(this object obj, string fieldName, object newValue)
    {
        FieldInfo info = obj.GetType().GetField(fieldName);

        if (info != null)
        {
            info.SetValue(obj, newValue);
        }
    }

    private static object GetField(object obj, string fieldName)
    {
        object ret = null;

        FieldInfo info = obj.GetType().GetField(fieldName);

        if (info != null)
        {
            ret = info.GetValue(obj);
        }

        return ret;
    }
}