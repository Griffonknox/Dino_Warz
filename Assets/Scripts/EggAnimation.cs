using UnityEngine;

public class EggAnimation : MonoBehaviour
{
    public Sprite[] eggFrames;
    public float frameRate = 0.5f;

    private SpriteRenderer sr;
    private int currentFrame;
    private float timer;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (eggFrames.Length == 0) return;

        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            timer = 0f;
            currentFrame = (currentFrame + 1) % eggFrames.Length;
            sr.sprite = eggFrames[currentFrame];
        }
    }
}
