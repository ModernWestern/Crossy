using System;

#pragma warning disable IDE0059

public static class LeanTweenExtensions
{
#if LEANTWEEN_MODULE

    public static void Kill(this LTDescr descr)
    {
        if (descr != null)
        {
            LeanTween.cancel(descr.uniqueId);

            descr = null;
        }
    }

    public static void Kill(this LTDescr descr, UnityEngine.GameObject gameObject)
    {
        if (descr != null)
        {
            LeanTween.cancel(gameObject, descr.uniqueId);

            descr = null;
        }
    }

    public static void Kill(this LTDescr descr, Action completed)
    {
        if (descr != null)
        {
            LeanTween.cancel(descr.uniqueId);

            completed?.Invoke();

            descr = null;
        }
    }

    public static void Kill(this LTDescr descr, UnityEngine.GameObject gameObject, Action completed)
    {
        if (descr != null)
        {
            LeanTween.cancel(gameObject, descr.uniqueId);

            completed?.Invoke();

            descr = null;
        }
    }

    public static void Kill<T>(this LTDescr descr, T parameter, Action<T> completed)
    {
        if (descr != null)
        {
            LeanTween.cancel(descr.uniqueId);

            completed?.Invoke(parameter);

            descr = null;
        }
    }

    public static void Kill<T>(this LTDescr descr, UnityEngine.GameObject gameObject, T parameter, Action<T> completed)
    {
        if (descr != null)
        {
            LeanTween.cancel(gameObject, descr.uniqueId);

            completed?.Invoke(parameter);

            descr = null;
        }
    }

#endif
}