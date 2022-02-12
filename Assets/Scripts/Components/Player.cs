using UnityEngine;

public class Player : FroggerObject
{
    public PlayerEvents Events { private get; set; }

    private Vector3 defaultPosition = new Vector3(-3, 1, 0);

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

    public void Up() => transform.Translate(Vector3.forward);

    public void Left() => transform.Translate(Vector3.left);

    public void Down() => transform.Translate(Vector3.back);

    public void Right() => transform.Translate(Vector3.right);

    private void OnTriggerEnter(Collider other)
    {
        const string Finish = "Finish";

        if (other.CompareTag(Finish))
        {
            Events.Finish();
        }
    }
}