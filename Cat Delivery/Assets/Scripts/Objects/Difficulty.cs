using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Difficulty Config", menuName ="Config/Difficulty Config")]
public class Difficulty : ScriptableObject {
    public Difficulty nextDifficulty;
    public int whiskasToNextLevel;

    public bool playerCanDie = true;
    
    [Header("Merchant")] [Space]
    public int maxMerchant;
    public float timeToSpawnMerchant;
    
    [Header("Costumers")] [Space]
    public float timeToConfigureCostumer;

    public int maxHappyCostumersToBoost = 3;
    public int maxUnhappyCostumersToGameOver = -3;
    
    public float costumerCooldownTime = 1f;

    public int profitRate = 2;
   
    [Range(1, 3)] 
    public float velocityFactor = 1f;

    [Header("Buildings")]
    public float whiskasToEvolveBuildings;

    public bool hasNextDifficulty(){
        return this.nextDifficulty != null;
    }


}
