using UnityEngine;

public class moveLeft : MonoBehaviour
{
    public float speed = 5f; // movement speed

    void Update()
    {
        // Move object to the left
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
}
