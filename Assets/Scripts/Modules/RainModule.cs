using Utils;
using UnityEngine;

public class RainModule : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] particles;

    [SerializeField] private AnimationCurve rainStrength;

    private void Awake()
    {
        foreach (var particle in particles)
        {
            particle.Stop(true);
            particle.gameObject.SetActive(false);
        }
    }

    public float OneHour
    {
        set
        {
            foreach (var particle in particles)
            {
                particle.Stop(true);
                particle.gameObject.SetActive(false);
            }

            var rain = value.Remap(0, value <= 10 ? 10 : value, 0, 5);

            switch (rain)
            {
                case <= 1 and > 0:
                    Play(ref particles[0], rain);
                    break;
                case <= 2 and >= 1:
                    Play(ref particles[0], rain);
                    Play(ref particles[1], rain);
                    break;
                case <= 3 and >= 2:
                    Play(ref particles[0], rain);
                    Play(ref particles[1], rain);
                    Play(ref particles[2], rain / 2);
                    break;
                case <= 4 and >= 3:
                    Play(ref particles[0], rain);
                    Play(ref particles[1], rain);
                    Play(ref particles[2], rain);
                    break;
                case >= 4:
                    Play(ref particles[0]);
                    Play(ref particles[1]);
                    Play(ref particles[2]);
                    Play(ref particles[3]);
                    break;
            }
        }
    }

    private void Play(ref ParticleSystem particle, float? emission = null)
    {
        if (!particle)
        {
            return;
        }

        particle.gameObject.SetActive(true);
        
        var module = particle.emission;

        module.rateOverTime = emission.HasValue ? Mathf.CeilToInt(emission.Value.RemapWithCurve(0, 5, 0, 1000, rainStrength)) : 1000;

        particle.Play(true);
    }
}