using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MerchantManager : MonoBehaviour
{
    public static MerchantManager instance;
    private List<Merchant> currentMerchants  = new List<Merchant>();
    [SerializeField] private MerchantSpawner spawner;
    private Coroutine currentManagement;

    public void StartManagement() {
        if(currentManagement != null)
            StopCoroutine(currentManagement);
            
        currentManagement = StartCoroutine(SpawnNewMerchants());
    }
    
    private IEnumerator SpawnNewMerchants() {
        
        DifficultyConfig difficulty = LevelManager.instance.GetDifficulty();

        while(true){
            if(currentMerchants.Count < difficulty.maxMerchant){
                spawner.SpawnMerchant();
            }
            yield return new WaitForSeconds(difficulty.timeToSpawnMerchant);
        }
    }

    private void Awake() {
        if(!instance){
            instance = this;
            return;
        }
        Destroy(this);
    }
    

    public void UnRegisterMerchant(Merchant merchant){
        currentMerchants.Remove(merchant);
    }

    public void RegisterMerchant(Merchant merchant){
        currentMerchants.Add(merchant);
    }

    public HashSet<Good> GetAvailableGood() {
        HashSet<Good> availableGoods = new HashSet<Good>(); 
        currentMerchants
            .FindAll(merchant =>  merchant.GetTraveledDistanceInPercent() < 0.75f)
            .ForEach(merchant => availableGoods.UnionWith(merchant.GetGoods()));

        return availableGoods;
    }

}
