using Utils;
using UnityEngine;

public class CloudsModule : MonoBehaviour
{
    private static readonly int _Density = Shader.PropertyToID("_Density");
    
    private static readonly int _Speed = Shader.PropertyToID("_Speed");

    private const float MaxSpeed = 12.5f; // 45km/h or 12.5m/s

    public GameObject[] clouds;

    public AnimationCurve cloudsDensity, cloudsSpeed;

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
            foreach (var cloud in clouds)
            {
                cloud.GetComponent<Renderer>().material.SetFloat(_Speed, value.CurveMap(0, MaxSpeed, -0.2f, -1f, cloudsSpeed));
            }
        }
    }
    
    public float Density
    {
        set
        {
            foreach (var cloud in clouds)
            {
                cloud.GetComponent<Renderer>().material.SetFloat(_Density, value.CurveMap(0, 100, 2, 1, cloudsDensity));
            }
        }
    }
}