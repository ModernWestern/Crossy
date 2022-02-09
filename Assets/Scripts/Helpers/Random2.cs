using UnityEngine;

public class Random2
{
    public static bool Value()
    {
        return Random.Range(0, 2) == 0;
    }
}