using UnityEngine;
using UnityEngine.InputSystem;

public class MenuSpawner : MonoBehaviour
{
    public GameObject ballPrefab;
    private GameObject currentBall;
    public Transform spawnPoint;
    public void SpawnBall()
    {
        //// Destroy existing ball if there is one
        //if (currentBall != null)
        //{
        //    Destroy(currentBall);
        //}
        //else
        //{
        //    //Vector3 spawnPos = spawnPoint != null ? spawnPoint.position : Vector3.zero;
        //    //currentBall = Instantiate(ballPrefab, spawnPos, Quaternion.identity);
        //    DragManager.Instance.BeginDrag(ballPrefab);

        //}
    }

    public GameObject bugPrefab;
    private GameObject currentBug;

    public void SpawnBug()
    {
        // Destroy existing bug if there is one
        if (currentBug != null)
        {
            Destroy(currentBug);
        }
        else
        {
            Vector3 spawnPos = spawnPoint != null ? spawnPoint.position : Vector3.zero;
            currentBug = Instantiate(bugPrefab, spawnPos, Quaternion.identity);

        }
    }

    public PetManager petManager;

    public void KillPet()
    {
        if (petManager != null)
        {
            petManager.KillPet();
        }
    }
}
