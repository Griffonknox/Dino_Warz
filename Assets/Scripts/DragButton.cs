using UnityEngine;
using UnityEngine.EventSystems;

public class DragButton : MonoBehaviour, IPointerDownHandler
{
    public GameObject prefabToDrag;

    public void OnPointerDown(PointerEventData eventData)
    {
        DragManager.Instance.BeginDrag(prefabToDrag);
    }
}
