using UnityEngine;

public class CloudMover : MonoBehaviour
{
    public float speed = 1f;
    public float resetX = -10f;
    public float startX = 10f;

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.x < resetX)
        {
            transform.position = new Vector3(startX, transform.position.y, transform.position.z);
        }
    }
}
