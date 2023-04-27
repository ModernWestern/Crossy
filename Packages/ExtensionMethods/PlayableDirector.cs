#if TIMELINE

using UnityEngine.Playables;

public static class PlayableDirectorExtensions
{
    public static void TruePlay(this PlayableDirector director)
    {
        director.Stop();

        director.Evaluate();

        director.Play();
    }

    public static void TrueStop(this PlayableDirector director)
    {
        director.Stop();

        director.Evaluate();
    }
}
#endif