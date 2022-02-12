using UnityEngine;

public class Vehicle : FroggerPoolObject
{
    [SerializeField] private float speed = 1f;

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().SetDamage();
        }
    }
}