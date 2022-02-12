using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    void Update()
    {
        SideMovement();
    }

    private void SideMovement()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.Translate(Vector3.forward);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Translate(Vector3.left);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.Translate(Vector3.back);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Translate(Vector3.right);
        }
    }
}