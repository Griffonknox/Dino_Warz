using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class DraggableObject : MonoBehaviour, IPointerDownHandler
{
    private bool isDragging;
    private bool physicsActive;

    private Rigidbody2D rb;
    private MonoBehaviour[] aiScripts; // Optional: reference to AI behaviors you want to disable during drag

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // (Optional) cache references to your AI components here if you have any
        // aiScripts = GetComponents<YourAIScriptBase>();
    }

    public void SetDragging(bool dragging)
    {
        isDragging = dragging;

        if (rb != null)
            rb.simulated = !dragging;

        // Optional: disable AI or custom behavior while dragging
        if (aiScripts != null)
        {
            foreach (var ai in aiScripts)
                ai.enabled = !dragging;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (DragManager.Instance == null)
        {
            return;
        }

        if (DragManager.Instance.IsDragging)
        {
            return;
        }
        DragManager.Instance.PickUpExistingObject(gameObject);
    }
}
