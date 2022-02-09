using UnityEngine;

public class Life : MonoBehaviour
{
    [SerializeField] private GameSettings settings;

    [SerializeField] private PlayerEvents playerEvents;

    private void Awake()
    {
        transform.Children().ForEach((i, heart) =>
        {
            heart.gameObject.SetActive(i < settings.lifes);
        });

        playerEvents.Damage += OnDamage;
    }

    private void OnDamage()
    {
        transform.GetFirstActiveChild().gameObject.SetActive(false);

        if (transform.Children().All(heart => !heart.gameObject.activeInHierarchy))
        {
            transform.Children().Last().gameObject.SetActive(true);

            playerEvents.OnGameOver();
        }
    }
}