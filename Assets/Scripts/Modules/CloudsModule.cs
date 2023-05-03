using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class CloudsModule : MonoBehaviour
{
    private static readonly int _Color = Shader.PropertyToID("_CloudColor");

    private static readonly int _Density = Shader.PropertyToID("_Density");

    private static readonly int _Speed = Shader.PropertyToID("_Speed");

    private const float MaxSpeed = 12.5f; // 45km/h or 12.5m/s

    [SerializeField] private GameObject[] clouds;

    [SerializeField] private AnimationCurve cloudsDensity, cloudsSpeed;

    [SerializeField] private Color day, mid, night;

    private List<Material> materials;

    private void Awake()
    {
        materials = clouds.ConvertAll(cloud => cloud.GetComponent<Renderer>().material).ToList();

        Density = 0;

        Speed = 0;
    }

    public float Direction
    {
        set
        {
            var current = transform.rotation.eulerAngles;

            current.y = value;

            transform.rotation = Quaternion.Euler(current);
        }
    }

    public float Speed
    {
        set
        {
            foreach (var material in materials)
            {
                material.SetFloat(_Speed, value.RemapWithCurve(0, MaxSpeed, -0.1f, -0.5f, cloudsSpeed));
            }
        }
    }

    public float Density
    {
        set
        {
            foreach (var material in materials)
            {
                material.SetFloat(_Density, value.RemapWithCurve(0, 100, 2, 1, cloudsDensity));
            }
        }
    }

    public bool? IsDay
    {
        set
        {
            if (value.HasValue)
            {
                foreach (var material in materials)
                {
                    material.SetColor(_Color, value.Value ? mid : night);
                }
            }
            else
            {
                foreach (var material in materials)
                {
                    material.SetColor(_Color, day);
                }
            }
        }
    }
}