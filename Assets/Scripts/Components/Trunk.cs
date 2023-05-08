using UnityEngine;

public class Trunk : PoolObject
{
    [SerializeField] private float speed = 1f;

    private void Update()
    {
        transform.Translate(Vector3.right * (speed * Time.deltaTime), Space.Self);
    }
}