using System;
using Json2CSharp;
using UnityEngine;
using ModernWestern;

public class AstroModule : MonoBehaviour
{
    [SerializeField] private Gradient day;

    [SerializeField] private Gradient rain;

    private Color dayLight;

    private Vector3 color;

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

    public (int Hour, float Rain) Shade
    {
        set
        {
            var rainCol = rain.Evaluate(value.Rain.Normalize(0, value.Rain <= 10 ? 10 : value.Rain));

            var dayCol = day.Evaluate(((float)value.Hour).Normalize(0, 23));

            Shader.SetGlobalColor(Constants.Tint, dayCol * rainCol);
        }
    }
}