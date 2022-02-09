using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerEvents playerEvents;

    [SerializeField] private PlayerMovement movement;

    private Vector3 defaultPosition = new Vector3(0.5f, 0, -3.5f);

    public void SetDamage()
    {
        movement.Default();

        playerEvents.OnDamage();

        transform.localPosition = defaultPosition;
    }
}