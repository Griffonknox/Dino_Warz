using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PetManager : MonoBehaviour
{
    public GameObject tombstonePrefab;
    public float eggDelay = 5f;

    private GameObject pet;
    private GameObject egg;
    private GameObject tombstone;

    private PetBehavior petBehavior;

    public DinoProfile defaultDino;

    private bool eggSkipped = false;

    void Start()
    {
        DinoProfile selected;
        if (GameManager.Instance == null)
        {
            selected = defaultDino;
        }
        else
        {
            selected = GameManager.Instance.selectedDino;
        }

        egg = Instantiate(selected.eggPrefab);
        pet = Instantiate(selected.petPrefab);
        petBehavior = pet.GetComponent<PetBehavior>();

        pet.SetActive(false);
        tombstone = Instantiate(tombstonePrefab);
        tombstone.SetActive(false);

        StartCoroutine(BeginLifeCycle());
    }

    IEnumerator BeginLifeCycle()
    {
        egg.SetActive(true);

        float elapsed = 0f;
        while (elapsed < eggDelay && !eggSkipped)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        egg.SetActive(false);
        pet.SetActive(true);

        petBehavior.StartLife(OnPetDied);
    }

    public void SkipEgg()
    {
        if (!eggSkipped)
        {
            eggSkipped = true;
            Debug.Log("Egg hatch skipped via click!");
        }
    }

    void OnPetDied()
    {
        pet.SetActive(false);
        tombstone.transform.position = pet.transform.position;
        tombstone.SetActive(true);

        StartCoroutine(EndGame());
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("StartScene");
    }

    public void KillPet()
    {
        petBehavior?.KillPet();
    }
}
