using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    const int range = 4;

    private int steps;

    void Update()
    {
        SideMovement();
    }

    private void SideMovement()
    {
        if (Input.GetKeyDown(KeyCode.A) && steps > -range)
        {
            transform.Translate(Vector3.left);

            steps--;
        }

        if (Input.GetKeyDown(KeyCode.D) && steps < range)
        {
            transform.Translate(Vector3.right);

            steps++;
        }
    }

    public void Default()
    {
        steps = 0;
    }
}