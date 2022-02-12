using UnityEngine;

public class Player : FroggerObject
{
    public PlayerEvents Events { private get; set; }

    private Vector3 defaultPosition = new Vector3(-3, 1, 0);

    private LTDescr punch;

    private void OnEnable()
    {
        transform.localPosition = defaultPosition;
    }

    public void SetDamage()
    {
        Events.Damage();

        OnEnable();
    }

    public override void OnBecameInvisible() => SetDamage();

    public void Up()
    {
        transform.Translate(Vector3.forward, Space.World);

        transform.eulerAngles = Vector3.zero;

        Animation();
    }

    public void Left()
    {
        transform.Translate(Vector3.left, Space.World);

        transform.eulerAngles = Vector3.up * -90;

        Animation();
    }

    public void Down()
    {
        transform.Translate(Vector3.back, Space.World);

        transform.eulerAngles = Vector3.up * 180;

        Animation();
    }

    public void Right()
    {
        transform.Translate(Vector3.right, Space.World);

        transform.eulerAngles = Vector3.up * 90;

        Animation();
    }

    private void Animation()
    {
        if (punch != null)
        {
            LeanTween.cancel(gameObject, punch.uniqueId);

            transform.localScale = Vector3.one;

            punch = null;
        }

        punch = LeanTween.scaleY(gameObject, 0.9f, 1).setEase(LeanTweenType.punch);
    }

    private void OnTriggerEnter(Collider other)
    {
        const string Finish = "Finish";

        if (other.CompareTag(Finish))
        {
            Events.Finish();
        }
    }
}