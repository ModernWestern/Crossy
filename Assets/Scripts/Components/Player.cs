using System;
using ModernWestern;
using UnityEngine;

public class Player : PoolObject
{
    private const string Water = "Water";

    private const string Trunk = "Trunk";

    private const string Finish = "Finish";

    private static readonly int IgnoreTint = Shader.PropertyToID("_IgnoreTint");

    public PlayerEvents Events { private get; set; }

    [SerializeField] private MeshRenderer mesh;

    [SerializeField] private ParticleSystem embers;

    private static Action<bool?> OnNightBody;

    private Vector3 defaultPosition = new(-3, 1, 0);

    private LTDescr tween;

    public override void Awake()
    {
        base.Awake();

        OnNightBody += NightBody;
    }

    public static void IsDay(bool? value) => OnNightBody?.Invoke(value);

    private void NightBody(bool? value)
    {
        mesh.materials.ForEach(material => material.SetFloat(IgnoreTint, value.HasValue ? value.Value ? 0 : 1 : 0));
    }

    private void OnEnable()
    {
        transform.localPosition = defaultPosition;

        transform.eulerAngles = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Finish))
        {
            transform.eulerAngles = Vector3.up * 180;

            Events.Finish();

            Events = null;
        }

        if (other.CompareTag(Trunk))
        {
            transform.SetParent(other.transform, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Trunk))
        {
            transform.SetParent(null);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(Water) && transform.parent == null)
        {
            SetDamage();
        }
    }

    public void SetDamage()
    {
        Events.Damage();

        embers.Play();

        OnEnable();
    }

    public override void OnBecameInvisible() => SetDamage();

    public void Up() => Movement(Vector3.forward, Vector3.zero);

    public void Left() => Movement(Vector3.left, Vector3.up * -90);

    public void Down() => Movement(Vector3.back, Vector3.up * 180);

    public void Right() => Movement(Vector3.right, Vector3.up * 90);

    private void Movement(Vector3 position, Vector3 rotation)
    {
        transform.Translate(position, Space.World);

        transform.eulerAngles = rotation;

        Animation();
    }

    public void Animation(bool loop = false)
    {
        if (tween != null)
        {
            LeanTween.cancel(gameObject, tween.uniqueId);

            transform.localScale = Vector3.one;

            tween = null;
        }

        tween = LeanTween.scaleY(gameObject, 0.9f, 1).setEase(LeanTweenType.punch).setIgnoreTimeScale(true);

        if (!loop)
        {
            return;
        }

        tween.setLoopPingPong();

        tween.time = 2;
    }
}