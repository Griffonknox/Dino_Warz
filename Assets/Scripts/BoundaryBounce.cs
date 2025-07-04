using UnityEngine;

public class BoundaryBounce : MonoBehaviour
{
    public float minX = 0.1f;
    public float maxX = 0.9f;
    public float minY = 0.1f;
    public float maxY = 0.5f;

    private Rigidbody2D rb;
    private Vector2 direction;  // for non-physics objects (like the pet)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // For pets without Rigidbody2D, we keep their direction vector here (optional)
        if (rb == null)
        {
            var petBehavior = GetComponent<PetBehavior>();
            if (petBehavior != null)
            {
                direction = petBehavior.GetDirection(); // we'll add this getter in pet script
            }
        }
    }

    void Update()
    {
        if (rb != null && rb.bodyType == RigidbodyType2D.Dynamic)
        {
            HandlePhysicsBounce();
        }
        else
        {
            HandleKinematicBounce();
        }
    }

    void HandleKinematicBounce()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        bool bounced = false;

        if (pos.x < minX || pos.x > maxX)
        {
            bounced = true;
        }

        if (pos.y < minY || pos.y > maxY)
        {
            bounced = true;
        }

        if (bounced)
        {
            // Clamp position inside viewport
            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);
            transform.position = Camera.main.ViewportToWorldPoint(pos);

            var petBehavior = GetComponent<PetBehavior>();
            if (petBehavior != null)
            {
                petBehavior.PickRandomDirection(); // Make this method public in PetBehavior.cs
                petBehavior.ResetDirectionTimer(); // Optional helper you can add
            }
        }
    }

    void HandlePhysicsBounce()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        Vector2 velocity = rb.linearVelocity;
        bool bounced = false;

        if (pos.x < minX)
        {
            pos.x = minX;
            velocity.x = Mathf.Abs(velocity.x);
            bounced = true;
        }
        else if (pos.x > maxX)
        {
            pos.x = maxX;
            velocity.x = -Mathf.Abs(velocity.x);
            bounced = true;
        }

        if (pos.y < minY)
        {
            pos.y = minY;
            velocity.y = Mathf.Abs(velocity.y);
            bounced = true;
        }
        else if (pos.y > maxY)
        {
            pos.y = maxY;
            velocity.y = -Mathf.Abs(velocity.y);
            bounced = true;
        }

        if (bounced)
        {
            transform.position = Camera.main.ViewportToWorldPoint(pos);
            rb.linearVelocity = velocity;
        }
    }
}
