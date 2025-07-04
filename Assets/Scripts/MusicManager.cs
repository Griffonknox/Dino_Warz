using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    // Optional: call this to stop music
    public void StopMusic()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
    }
}
