using UnityEngine;

public static class ParticleSystemExtension
{
    public static void PlayAll(this ParticleSystem particle)
    {
        particle.Play(true);
    }

    public static void StopAllAndClear(this ParticleSystem particle)
    {
        particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }
}