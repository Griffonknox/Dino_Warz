using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DayNightCycle : MonoBehaviour
{
    public float cycleDuration = 40f;         // Total time for full cycle (e.g., 20 seconds)
    public float nightDarkness = 0.4f;        // Min brightness (0 = black, 1 = no change)

    private SpriteRenderer backgroundRenderer;
    private float timer = 0f;

    void Start()
    {
        //timer = cycleDuration / 2f;
    }

    void Awake()
    {
        backgroundRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > cycleDuration)
            timer -= cycleDuration;

        // Calculate time as 0 ? 2?
        float t = (timer / cycleDuration) * Mathf.PI * 2f;

        // Sine wave brightness: peaks at midday, lowest at midnight
        float brightness = Mathf.Lerp(nightDarkness, 1f, (Mathf.Sin(t - Mathf.PI / 2f) + 1f) / 2f);

        backgroundRenderer.color = new Color(brightness, brightness, brightness, 1f);
    }
}
