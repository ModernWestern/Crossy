public static class BoolExtensions
{
    public static bool Equals(this object obj, params object[] objects)
    {
        return objects.Exist(o => o.Equals(obj));
    }

    public static bool Equals<T>(this object obj, params T[] objects)
    {
        return objects.Exist(o => o.Equals(obj));
    }
}