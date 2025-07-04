using UnityEngine;

[CreateAssetMenu(fileName = "DinoProfile", menuName = "VirtualPet/DinoProfile")]
public class DinoProfile : ScriptableObject
{
    public GameObject eggPrefab;
    public GameObject petPrefab;
    public string dinoName; // Optional, for display
}