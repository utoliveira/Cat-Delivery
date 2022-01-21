using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CostumerConfig {
    public int maxHappyCostumersToBoost = 3;
    public int maxUnhappyCostumersToGameOver = 3;
    public float costumerCooldownTime = 1f;
    public float timeToConfigureCostumer;
    public int profitRate = 2;

}

[System.Serializable]
public class MerchantConfig {

    [Range(1, 3)] 
    public float velocityFactor = 1f;
    public int maxMerchant;
    public float timeToSpawnMerchant;
}

[CreateAssetMenu(fileName ="New Difficulty Config", menuName ="Config/Difficulty Config")]
public class Difficulty : ScriptableObject {
    public Difficulty nextDifficulty;
    public int whiskasToNextLevel;
    public bool playerCanDie = true;
    public MerchantConfig merchants;
    public CostumerConfig costumers;

    public bool hasNextDifficulty(){
        return this.nextDifficulty != null;
    }


}
