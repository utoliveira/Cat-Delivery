using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantSpawner : MonoBehaviour
{
    [SerializeField] private TrajectoryInitialPoint[] spawners;
    private int lastSpawnIndex = 0;
    
    public Merchant SpawnMerchant(List<Good> availableGoods, GameObject merchantPrefab){
        TrajectoryInitialPoint spawnerPoint =  getNextSpawnerPoint();
        
        Merchant merchant = Instantiate(
            merchantPrefab,
            spawnerPoint.transform.position,
            merchantPrefab.transform.rotation
        ).GetComponent<Merchant>();
        
        merchant.Configure(spawnerPoint, availableGoods); //Change it to randomized and avoiding the last one 
        return merchant;
    }

    private TrajectoryInitialPoint getNextSpawnerPoint(){
        int spawnerIndex = 0;
        
        while(spawners.Length > 1 && spawnerIndex == lastSpawnIndex){
            spawnerIndex = Random.Range(0, spawners.Length);
        }

        lastSpawnIndex = spawnerIndex;
        return spawners[spawnerIndex];
    }

}
