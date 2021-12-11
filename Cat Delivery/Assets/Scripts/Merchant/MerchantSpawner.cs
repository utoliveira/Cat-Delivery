using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantSpawner : MonoBehaviour
{
    [SerializeField] private TrajectoryInitialPoint[] spawners;
    
    public GameObject merchantPrefab; //TODO: Change to variable merchants and boats
    private int lastSpawnIndex = 0;

    public Merchant SpawnMerchant(){
        TrajectoryInitialPoint spawnerPoint =  getNextSpawnerPoint();
        
        Merchant merchant = Instantiate(
            merchantPrefab,
            spawnerPoint.transform.position,
            merchantPrefab.transform.rotation
        ).GetComponent<Merchant>();
        
        //Get the available Goods;
        merchant.Configure(spawnerPoint, GetAvailableGoods()); //Change it to randomized and avoiding the last one 
        MerchantManager.instance.RegisterMerchant(merchant);
        return merchant;
    }



    private TrajectoryInitialPoint getNextSpawnerPoint(){
        int spawnerIndex = 0;
        
        while(spawners.Length > 1 && spawnerIndex == lastSpawnIndex){
            //Maybe not that bad to keep the previous one
            Debug.Log("GET RANDOM SPAWN");
            spawnerIndex = Random.Range(0, spawners.Length);
        }

        lastSpawnIndex = spawnerIndex;
        return spawners[spawnerIndex];
    }

    private List<Good> GetAvailableGoods(){
        HashSet<Good> alreadySpawnedGoods = MerchantManager.instance.GetAvailableGood();
        return LevelManager.instance.GetGameConfig().availableGoods.FindAll(good => !alreadySpawnedGoods.Contains(good));
         
    }

}
