using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private int currentLevel = 1;
    [SerializeField] private GameObject nextLevelPrefab;
    [SerializeField] List<Transform>  costumerSpawnPositions;


    public int getCurrentLevel() {
        return currentLevel;
    }

    public bool hasNextLevel() {
        return nextLevelPrefab != null;
    }
    

    public GameObject getNextLevelPrefab(){
        return nextLevelPrefab;
    }

    public List<Transform> GetSpawnPositions() {
        return costumerSpawnPositions;
    }

}
