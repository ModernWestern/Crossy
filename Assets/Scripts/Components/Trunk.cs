using UnityEngine;

public class Trunk : MovableObject
{
    private void Update()
    {
        transform.Translate(Vector3.right * (SpeedMultiplier * Time.deltaTime), Space.Self);
    }
}