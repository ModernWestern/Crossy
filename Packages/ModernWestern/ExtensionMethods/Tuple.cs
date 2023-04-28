using System;

public static class TupleExtensions
{
    public static bool Compare<T1, T2>(this Tuple<T1, T2> value, T1 v1, T2 v2)
    {
        return value.Item1.Equals(v1) && value.Item2.Equals(v2);
    }

    public static bool Compare<T1, T2, T3>(this Tuple<T1, T2, T3> value, T1 v1, T2 v2, T3 v3)
    {
        return value.Item1.Equals(v1) && value.Item2.Equals(v2) && value.Item3.Equals(v3);
    }

    public static bool Compare<T1, T2, T3, T4>(this Tuple<T1, T2, T3, T4> value, T1 v1, T2 v2, T3 v3, T4 v4)
    {
        return value.Item1.Equals(v1) && value.Item2.Equals(v2) && value.Item3.Equals(v3) && value.Item4.Equals(v4);
    }

    public static bool Compare<T1, T2, T3, T4, T5>(this Tuple<T1, T2, T3, T4> value, T1 v1, T2 v2, T3 v3, T4 v4, T5 v5)
    {
        return value.Item1.Equals(v1) && value.Item2.Equals(v2) && value.Item3.Equals(v3) && value.Item4.Equals(v4) && value.Item4.Equals(v5);
    }
}