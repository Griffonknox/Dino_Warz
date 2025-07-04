using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EggSelector : MonoBehaviour
{
    public DinoProfile dinoProfile;

    public void SelectThisEgg()
    {
        GameManager.Instance.selectedDino = dinoProfile;
        SceneManager.LoadScene("GameScene");
    }

    public Sprite[] animationFrames;
    public float frameInterval = 0.5f;

    private Image imageComponent;
    private int currentFrame = 0;
    private float timer = 0f;

    void Start()
    {
        imageComponent = GetComponent<Image>();

        if (animationFrames != null && animationFrames.Length > 0)
        {
            imageComponent.sprite = animationFrames[0];
        }
    }


    void Update()
    {
        if (animationFrames == null || animationFrames.Length == 0) return;

        timer += Time.deltaTime;
        if (timer >= frameInterval)
        {
            timer = 0f;
            currentFrame = (currentFrame + 1) % animationFrames.Length;
            imageComponent.sprite = animationFrames[currentFrame];

            // Optional: Scale the button slightly to simulate bounce
            float pulse = (currentFrame % 2 == 0) ? 1.05f : 0.95f;
            transform.localScale = new Vector3(pulse, pulse, 1f);
        }
    }
}