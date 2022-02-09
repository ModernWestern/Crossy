using UnityEngine;

public class Vehicle : PoolableObject
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().SetDamage();
        }
    }
}