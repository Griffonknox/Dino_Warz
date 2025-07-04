using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BackgroundResizer : MonoBehaviour
{
    void Start()
    {
        ResizeToCamera();
    }

    void ResizeToCamera()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        Vector2 spriteSize = sr.sprite.bounds.size;
        transform.localScale = new Vector3(
            worldScreenWidth / spriteSize.x,
            worldScreenHeight / spriteSize.y,
            1f
        );
    }
}
