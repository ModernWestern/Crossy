using UnityEngine;

public class Life : MonoBehaviour
{
    [SerializeField] private GameSettings settings;

    [SerializeField] private PlayerEvents playerEvents;

    private void Awake()
    {
        transform.GetChildren().ForEach((i, heart) =>
        {
            heart.gameObject.SetActive(i < settings.lifes);
        });

        playerEvents.Damage += OnDamage;
    }

    private void OnDamage()
    {
        transform.GetFirstActiveChild().gameObject.SetActive(false);

        if (transform.GetChildren().All(heart => !heart.gameObject.activeInHierarchy))
        {
            transform.GetChildren().Last().gameObject.SetActive(true);

            playerEvents.OnGameOver();
        }
    }
}