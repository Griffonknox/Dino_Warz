using UnityEngine;

public class SkyOrb : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite sunSprite;
    public Sprite moonSprite;

    public float cycleDuration = 10f; // Total time for one pass (sun or moon)
    public float xRange = 10f;        // From -xRange to +xRange
    public float yOffset = 3f;      // Base Y height
    public float arcHeight = 1f;      // How high the arc goes

    private float timer = 0f;
    private bool isDay = true;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= cycleDuration)
        {
            timer = 0f;
            isDay = !isDay;
            spriteRenderer.sprite = isDay ? sunSprite : moonSprite;
        }

        float t = timer / cycleDuration; // 0 ? 1

        // X moves left to right (or reverse if you want)
        float x = Mathf.Lerp(-xRange, xRange, t);

        // Y creates an arc using sine wave
        float y = yOffset + Mathf.Sin(t * Mathf.PI) * arcHeight;

        transform.position = new Vector3(x, y, 0f);
    }
}
