using UnityEngine;

public class PetBehavior : MonoBehaviour
{
    // SPEED ACTIVITY
    public float wanderSpeed = 1f;
    public float lifeSpan = 15f;
    public float directionChangeInterval = 3f;

    private Vector2 direction;
    private float ageTimer;
    private float directionTimer;

    public Sprite[] animationFrames;
    public float animationInterval = 0.5f;
    private SpriteRenderer spriteRenderer;
    private int currentFrame;
    private float animationTimer;

    private bool isAlive = false;
    private System.Action deathCallback;

    // BALL ACTIVITY
    private GameObject ball;
    private bool isChasingBall = false;
    public float approachDistance = 0.5f;
    public float ballSeekSpeed = 1.5f;

    // BUG ACTIVITY
    private GameObject bug;
    private bool isChasingBug = false;
    public float bugApproachDistance = 0.4f;
    public float bugSeekSpeed = 1.8f;

    public bool isHopping = false;

    public AudioClip eggCrackSound;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!isAlive) return;

        if (!isHopping)
        {
            HandleBugChase(); // prioritize bug

            if (!isChasingBug)
            {
                HandleBallChase(); // chase ball if no bug
            }

            HandleAnimation();

            if (!isChasingBug && !isChasingBall)
            {
                MovePet();
                HandleDirectionChange();
            }
        }

        HandleAging();
    }

    public void StartLife(System.Action onDeathCallback)
    {
        isAlive = true;
        PlayEggCrackSound();
        ageTimer = 0f;
        directionTimer = 0f;
        PickRandomDirection();
        deathCallback = onDeathCallback;
    }

    void MovePet()
    {
        transform.Translate(direction * wanderSpeed * Time.deltaTime);

        if (direction.x < 0)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void HandleAging()
    {
        ageTimer += Time.deltaTime;
        if (ageTimer >= lifeSpan)
        {
            Die();
        }
    }

    void HandleDirectionChange()
    {
        directionTimer += Time.deltaTime;
        if (directionTimer >= directionChangeInterval)
        {
            directionTimer = 0f;
            PickRandomDirection();
        }
    }

    void HandleAnimation()
    {
        if (animationFrames == null || animationFrames.Length == 0)
            return;

        animationTimer += Time.deltaTime;
        if (animationTimer >= animationInterval)
        {
            animationTimer = 0f;
            currentFrame = (currentFrame + 1) % animationFrames.Length;

            if (currentFrame >= 0 && currentFrame < animationFrames.Length)
            {
                spriteRenderer.sprite = animationFrames[currentFrame];
            }
        }
    }

    void Die()
    {
        isAlive = false;
        deathCallback?.Invoke();
    }
    public void KillPet()
    {
        Die();
    }

    void HandleBallChase()
    {
        if (ball == null)
        {
            GameObject found = GameObject.FindWithTag("Ball");
            if (found != null)
            {
                ball = found;
                isChasingBall = true;
            }
            else
            {
                isChasingBall = false;
                return;
            }
        }

        float dist = Vector2.Distance(transform.position, ball.transform.position);

        if (dist > approachDistance)
        {
            Vector2 dir = (ball.transform.position - transform.position).normalized;
            transform.Translate(dir * ballSeekSpeed * Time.deltaTime);

            if (dir.x < 0)
                transform.localScale = new Vector3(1, 1, 1);
            else
                transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void HandleBugChase()
    {
        if (bug == null)
        {
            GameObject found = GameObject.FindWithTag("Bug");
            if (found != null)
            {
                bug = found;
                isChasingBug = true;
            }
            else
            {
                isChasingBug = false;
                return;
            }
        }

        float dist = Vector2.Distance(transform.position, bug.transform.position);

        if (dist > bugApproachDistance)
        {
            Vector2 dir = (bug.transform.position - transform.position).normalized;
            transform.Translate(dir * bugSeekSpeed * Time.deltaTime);

            if (dir.x < 0)
                transform.localScale = new Vector3(1, 1, 1);
            else
                transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            // Wait for bug to be destroyed (eaten) by collision
        }

        // If bug is destroyed externally, clear reference
        if (bug == null)
        {
            isChasingBug = false;
        }
    }
    void PlayEggCrackSound()
    {
        GameObject tempAudio = new GameObject("TempAudio");
        AudioSource audio = tempAudio.AddComponent<AudioSource>();
        audio.clip = eggCrackSound;
        audio.volume = 1f; // You can raise this to 2f or 3f if needed
        audio.spatialBlend = 0f; // 0 = 2D, 1 = 3D
        audio.Play();
        Destroy(tempAudio, eggCrackSound.length);
    }

    public Vector2 GetDirection() => direction;

    public void SetDirection(Vector2 newDirection) => direction = newDirection.normalized;

    public void ResetDirectionTimer() => directionTimer = 0f;

    public void PickRandomDirection() => direction = Random.insideUnitCircle.normalized;
}
