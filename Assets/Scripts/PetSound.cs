using UnityEngine;

public class PetSound : MonoBehaviour
{
    public AudioSource audioSource;

    [Header("Rawr Timing (Seconds)")]
    public float minInterval = 2f;
    public float maxInterval = 5f;

    private float timer;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        SetNewTimer();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            audioSource.Play();
            SetNewTimer();
        }
    }

    void SetNewTimer()
    {
        timer = Random.Range(minInterval, maxInterval);
    }
}