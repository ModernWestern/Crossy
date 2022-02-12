using UnityEngine;

public class Life : MonoBehaviour
{
    [SerializeField] private GameSettings settings;

    [SerializeField] private PlayerEvents playerEvents;

    private void Awake()
    {
        SetHearts();

        playerEvents.OnDamage += OnDamage;

        playerEvents.OnFinish += SetHearts;
    }

    public void SetHearts()
    {
        transform.GetChildren().ForEach((i, heart) =>
        {
            heart.gameObject.SetActive(i < settings.lifes);
        });
    }

    private void OnDamage()
    {
        // Avoid code below if Life does not exist anymore
        if (this)
        {
            var firstHeart = transform.GetFirstActiveChild().gameObject;

            if (firstHeart && firstHeart.activeInHierarchy && firstHeart.activeSelf)
            {
                firstHeart.SetActive(false);
            }

            if (transform.GetChildren().All(heart => !heart.gameObject.activeInHierarchy))
            {
                transform.GetChildren().Last().gameObject.SetActive(true);

                playerEvents.GameOver();
            }
        }
    }
}