using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private PlayerEvents events;

    private void Awake()
    {
        player.Events = events;

        events.OnFinish += () =>
        {
            player = Instantiate(player);

            player.Events = events;
        };
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            player.Up();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            player.Left();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            player.Down();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            player.Right();
        }
    }
}