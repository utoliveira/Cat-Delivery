using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Difficulty Config", menuName ="Config/Difficulty Config")]
public class DifficultyConfig : ScriptableObject {
    public DifficultyConfig nextDifficulty;
    public int whiskasToNextLevel;
    
    [Header("Merchant")] [Space]
    public int maxMerchant;
    public float timeToSpawnMerchant;
    
    [Header("Costumers")] [Space]
    public float timeToConfigureCostumer;

    public int profitRate = 2;
   
    [Range(1, 3)] 
    public float velocityFactor = 1f;

    [Header("Buildings")]
    public float whiskasToEvolveBuildings;


}
