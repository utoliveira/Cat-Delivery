using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantSpawner : MonoBehaviour
{
    public GameObject merchantPrefab;

    [SerializeField] private Transform[] spawners;

    [SerializeField] private Transform centralPosition;

    private List<Merchant> spawnedMerchants;
    private List<Good> availableGoods;

    private int lastSpawnIndex = 0;
    private int maxMerchants = 3;

    private void Start() {
        spawnedMerchants = new List<Merchant>();
        StartCoroutine(SpawnBoat());
        availableGoods = new List<Good>(LevelManager.instance.GetGameConfig().availableGoods);
    }
    IEnumerator SpawnBoat(){
        while(true){

            //Check what merchandize can be applyied
            if(spawnedMerchants.Count < maxMerchants){
                GameObject merchant = Instantiate(merchantPrefab, getNextSpawner().position, merchantPrefab.transform.rotation);
                ConfigureMerchant(merchant.GetComponent<Merchant>());    
            }
            yield return new WaitForSeconds(Random.Range(5, 10));
        }
    }

    private Transform getNextSpawner(){
        int spawnerIndex = 0;
        while(spawners.Length > 1 && spawnerIndex == lastSpawnIndex){
            spawnerIndex = Random.Range(0, spawners.Length);
            Debug.Log("Repeti o processo em pegar o next");
        }
        lastSpawnIndex = spawnerIndex;
        return spawners[spawnerIndex];
    }

    private void ConfigureMerchant(Merchant merchant){
        
        int speed = Random.Range(90, 260);
        Vector2 merchantPosition = merchant.transform.position;

        if(merchantPosition.x > centralPosition.position.x){
            merchant.transform.localScale = new Vector2(-1f, 1f);
            merchant.SetSpeed(-speed);
        }else{
           merchant.SetSpeed(speed);
        }
    
        spawnedMerchants.Add(merchant);
        merchant.ConfigureVendors(LevelManager.instance.GetGameConfig().availableGoods);

    }

    public void UnRegisterMerchant(Merchant merchant){
        spawnedMerchants.Remove(merchant);
    }

}
