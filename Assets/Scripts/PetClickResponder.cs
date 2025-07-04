using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PetClickResponder : MonoBehaviour, IPointerClickHandler
{
    public int hops = 3;
    public float hopHeight = 0.5f;
    public float hopDuration = 0.1f;
    public float pauseBetweenHops = 0.05f;

    private Vector3 originalPosition;

    public AudioSource roarAudioSource; // Drag this in or find it in Awake()
    private PetBehavior petBehavior;

    void Awake()
    {
        petBehavior = GetComponent<PetBehavior>();

        if (roarAudioSource == null)
            roarAudioSource = GetComponent<AudioSource>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!petBehavior.isHopping)
            StartCoroutine(HopAnimation());
    }

    private IEnumerator HopAnimation()
    {
        petBehavior.isHopping = true;
        originalPosition = transform.position;

        if (roarAudioSource != null && roarAudioSource.clip != null)
            roarAudioSource.Play();

        for (int i = 0; i < hops; i++)
        {
            // Jump up
            yield return MoveToY(originalPosition.y + hopHeight, hopDuration);

            // Jump down
            yield return MoveToY(originalPosition.y, hopDuration);

            yield return new WaitForSeconds(pauseBetweenHops);
        }

        petBehavior.isHopping = false;
    }

    private IEnumerator MoveToY(float targetY, float duration)
    {
        float elapsed = 0f;
        Vector3 startPos = transform.position;
        Vector3 targetPos = new Vector3(startPos.x, targetY, startPos.z);

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
    }
}
