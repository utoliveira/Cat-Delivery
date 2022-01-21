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
 
    private void Awake() {
        if(!instance){
            instance = this;
            return;
        }
        Destroy(this);
    }
    

    public void StartManagement() {
        if(currentManagement != null)
            StopCoroutine(currentManagement);
            
        currentManagement = StartCoroutine(SpawnNewMerchants());
    }
   
    private IEnumerator SpawnNewMerchants() {
        
        while(true){
            if(currentMerchants.Count < LevelManager.GetMerchantConfig().maxMerchant){
                List<Good> availableGoods = GetAvailableGoodsToSpawn(LevelManager.GetGameConfig().goods);

                Merchant merchant = spawner.SpawnMerchant(availableGoods, LevelManager.GetGameConfig().merchantPrefab);
                RegisterMerchant(merchant);
            }
            yield return new WaitForSeconds(LevelManager.GetMerchantConfig().timeToSpawnMerchant);
        }
    }

    public void UnRegisterMerchant(Merchant merchant){
        currentMerchants.Remove(merchant);
    }

    private void RegisterMerchant(Merchant merchant){
        currentMerchants.Add(merchant);
    }

    public List<Good> GetAvailableGoodsToSpawn(List<Good> allGoods){
        HashSet<Good> alreadySpawnedGoods = GetAlreadySpawnedGoods();
        return allGoods.FindAll(good => !alreadySpawnedGoods.Contains(good));
    }

    public HashSet<Good> GetAlreadySpawnedGoods() {
        HashSet<Good> availableGoods = new HashSet<Good>(); 
        currentMerchants
            .FindAll(merchant =>  merchant.GetTraveledDistanceInPercent() < 0.75f)
            .ForEach(merchant => availableGoods.UnionWith(merchant.GetGoods()));

        return availableGoods;
    }

}
