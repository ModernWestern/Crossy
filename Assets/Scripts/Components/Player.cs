using UnityEngine;

public class Player : FroggerObject
{
    const string Finish = "Finish";

    const string Trunk = "Trunk";

    const string Water = "Water";

    public PlayerEvents Events { private get; set; }

    [SerializeField] private ParticleSystem Embers;

    private Vector3 defaultPosition = new Vector3(-3, 1, 0);

    private LTDescr punch;

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

            Events?.Finish();

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

    public override void OnBecameInvisible() => SetDamage();

    private void OnDisable() => Events?.CleanAll();

    public void SetDamage()
    {
        Events?.Damage();

        Embers.Play();

        OnEnable();
    }

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
        if (punch != null)
        {
            LeanTween.cancel(gameObject, punch.uniqueId);

            transform.localScale = Vector3.one;

            punch = null;
        }

        punch = LeanTween.scaleY(gameObject, 0.9f, 1).setEase(LeanTweenType.punch).setIgnoreTimeScale(true);

        if (loop)
        {
            punch.setLoopPingPong();

            punch.time = 2;
        }
    }
}