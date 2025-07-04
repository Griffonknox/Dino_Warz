using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public DinoProfile selectedDino;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep across scenes
        }
        else
        {
            Destroy(gameObject); // Only one manager allowed
        }
    }
}
