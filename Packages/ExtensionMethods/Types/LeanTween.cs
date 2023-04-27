#if LEANTWEEN

using System.Collections.Generic;

public static class LeanTweenExtensions
{
    /// <summary>
    /// Cancels all LeanTween tweens in a list of IDs.
    /// </summary>
    /// <param name="ids">The list of unique IDs of the tweens to cancel.</param>
    public static void CancelTweenById(this List<int> ids)
    {
        if (ids is not { Count: > 0 }) return;

        foreach (var id in ids)
        {
            LeanTween.cancel(id);
        }
    }

    /// <summary>
    /// Add the unique ID of an LTDescr in a list.
    /// </summary>
    /// <param name="descr">The LTDescr object to get the unique ID from.</param>
    /// <param name="ids">The list of unique IDs to add the LTDescr's ID to.</param>
    public static void StoreTweenId(this LTDescr descr, ref List<int> ids)
    {
        ids.Add(descr.uniqueId);
    }
}

#endif