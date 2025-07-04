using UnityEngine;

public class BugBehavior : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float moveDuration = 0.5f;
    public float pauseDuration = 0.5f;
    public Sprite[] animationFrames;
    public float animationInterval = 0.2f;

    private Vector2 moveDirection;
    private float moveTimer;
    private float pauseTimer;
    private bool isMoving = false;

    private SpriteRenderer spriteRenderer;
    private int currentFrame = 0;
    private float animationTimer = 0f;

    public AudioClip biteSound;
    public AudioSource bugWalk;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (bugWalk == null)
        {
            bugWalk = GetComponent<AudioSource>();
        }

        ChooseNewDirection();
        pauseTimer = pauseDuration;
    }

    void Update()
    {
        HandleAnimation();
        HandleMovement();
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
            spriteRenderer.sprite = animationFrames[currentFrame];
        }
    }

    void HandleMovement()
    {
        if (isMoving)
        {
            bugWalk.Play();
            moveTimer -= Time.deltaTime;
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

            if (moveTimer <= 0f)
            {
                isMoving = false;
                pauseTimer = pauseDuration;
            }
        }
        else
        {
            pauseTimer -= Time.deltaTime;
            if (pauseTimer <= 0f)
            {
                ChooseNewDirection();
                isMoving = true;
                moveTimer = moveDuration;
            }
        }
    }

    void ChooseNewDirection()
    {
        moveDirection = Random.insideUnitCircle.normalized;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pet"))
        {
            PlayBiteSound();
            Destroy(gameObject);
        }
    }
    void PlayBiteSound()
    {
        GameObject tempAudio = new GameObject("TempAudio");
        AudioSource audio = tempAudio.AddComponent<AudioSource>();
        audio.clip = biteSound;
        audio.volume = 1f; // You can raise this to 2f or 3f if needed
        audio.spatialBlend = 0f; // 0 = 2D, 1 = 3D
        audio.Play();
        Destroy(tempAudio, biteSound.length);
    }
}
