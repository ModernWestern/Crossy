using System;
using Json2CSharp;
using UnityEngine;
using ModernWestern;

public class AstroModule : MonoBehaviour
{
    [SerializeField] private Gradient day;

    private Color dayLight;

    private Vector3 color;

    private void Awake()
    {
        // dayLight = light.color;
    }

    /// <summary>
    /// Altitude, Azimuth, Distance
    /// </summary>
    public Vector3 Position
    {
        set
        {
            var r = value.z;
            var phi = value.y * Mathf.Deg2Rad;
            var theta = Mathf.PI / 2 - value.x * Mathf.Deg2Rad;

            var x = r * Mathf.Sin(theta) * Mathf.Cos(phi);
            var y = r * Mathf.Cos(theta);
            var z = r * Mathf.Sin(theta) * Mathf.Sin(phi);

            Shader.SetGlobalVector(Constants.Light, new Vector3(x, y < 0f ? 0.5f : y, z));
        }
    }

    public Tuple<Weather.Description, int> Shade
    {
        set => Shader.SetGlobalColor(Constants.Tint, day.Evaluate(((float)value.Item2).Normalize(0, 23)));
    }
}