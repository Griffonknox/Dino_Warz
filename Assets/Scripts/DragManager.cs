using UnityEngine;
using UnityEngine.InputSystem;

public class DragManager : MonoBehaviour
{
    public static DragManager Instance;

    private GameObject draggingObject;
    private Camera mainCamera;

    private Vector3 lastMousePosition;
    private Vector3 mouseVelocity;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (draggingObject != null)
        {
            Vector3 currentMousePos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            currentMousePos.z = 0f;

            // Update velocity
            mouseVelocity = (currentMousePos - lastMousePosition) / Time.deltaTime;

            // Move dragged object to cursor
            draggingObject.transform.position = currentMousePos;

            // Drop on mouse button release
            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                DropObjectWithThrow();
            }

            lastMousePosition = currentMousePos;
        }
        else
        {
            // Update lastMousePosition to prevent a huge velocity jump when dragging starts
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mousePos.z = 0f;
            lastMousePosition = mousePos;
        }
    }

    public void BeginDrag(GameObject prefab)
    {
        if (draggingObject != null) return;

        Vector3 spawnPos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        spawnPos.z = 0f;

        draggingObject = Instantiate(prefab, spawnPos, Quaternion.identity);

        SetDraggingState(true);
    }

    public void PickUpExistingObject(GameObject obj)
    {
        if (draggingObject != null) return;

        draggingObject = obj;

        SetDraggingState(true);
    }

    private void DropObjectWithThrow()
    {
        if (draggingObject == null) return;

        Rigidbody2D rb = draggingObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = mouseVelocity;
        }

        SetDraggingState(false);

        draggingObject = null;
    }

    private void SetDraggingState(bool isDragging)
    {
        if (draggingObject == null) return;

        DraggableObject draggable = draggingObject.GetComponent<DraggableObject>();
        if (draggable != null)
        {
            draggable.SetDragging(isDragging);
            draggable.SetPhysicsActive(!isDragging);
        }
        else
        {
            // If DraggableObject component is missing, still toggle physics
            Rigidbody2D rb = draggingObject.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.simulated = !isDragging;
        }
    }

    public bool IsDragging => draggingObject != null;
}
