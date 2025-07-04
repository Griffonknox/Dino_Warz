using UnityEngine;
using UnityEngine.EventSystems;

public class EggClickResponder : MonoBehaviour, IPointerClickHandler
{
    private PetManager petManager;

    void Start()
    {
        petManager = FindFirstObjectByType<PetManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("click!");
        if (petManager != null)
        {
            Debug.Log("skip");
            petManager.SkipEgg(); // NEW METHOD we’ll add next
        }
    }
}
