using UnityEngine;

public class Player : FroggerObject
{
    [SerializeField] private PlayerEvents playerEvents;

    private Vector3 defaultPosition = new Vector3(-3, 1, 0);

    public void SetDamage()
    {
        playerEvents.OnDamage();

        transform.localPosition = defaultPosition;
    }

    public override void OnBecameInvisible()
    {
        SetDamage();
    }
}