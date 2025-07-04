using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallBehavior : MonoBehaviour
{
    public float kickForce = 6f;
    private Rigidbody2D rb;
    public float lifeTime = 10f;
    private float lifeTimer = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pet"))
        {
            Vector2 kickDir = (transform.position - collision.transform.position).normalized;
            float force = Random.Range(kickForce * 0.9f, kickForce * 1.1f);
            rb.linearVelocity = kickDir * force;

            float torque = force * 10f; // tweak multiplier for how spinny it looks
            rb.angularVelocity = torque;
        }
    }

    void Update()
    {
        lifeTimer += Time.deltaTime;
        if (lifeTimer >= lifeTime)
        {
            Destroy(gameObject);
            return;
        }

        // Check if the ball is moving slowly
        if (rb.linearVelocity.magnitude < 0.2f)
        {
            // Gradually reduce spin
            rb.angularVelocity *= 0.9f;
            if (Mathf.Abs(rb.angularVelocity) < 1f)
            {
                rb.angularVelocity = 0f;
            }
        }
    }
}
