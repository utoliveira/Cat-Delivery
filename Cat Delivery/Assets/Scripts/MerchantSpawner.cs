using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantSpawner : MonoBehaviour
{
    public GameObject merchantPrefab;

    [SerializeField] private Transform[] spawners;

    [SerializeField] private Transform centralPosition;

    private void Start() {
        StartCoroutine(SpawnBoat());
    }
    IEnumerator SpawnBoat(){
        while(true){
            GameObject merchant = Instantiate(merchantPrefab, getNextSpawner().position, merchantPrefab.transform.rotation);
            ConfigureMerchant(merchant);
            yield return new WaitForSeconds(Random.Range(5, 15));
        }
    }

    private Transform getNextSpawner(){
        int spawnerIndex = Random.Range(0, spawners.Length);
        return spawners[spawnerIndex];
    }

    private void ConfigureMerchant(GameObject merchant){

        int speed = Random.Range(40, 250);
        Vector2 merchantPosition = merchant.transform.position;

        if(merchantPosition.x > centralPosition.position.x){
            merchant.transform.localScale = new Vector2(-1f, 1f);
            merchant.GetComponent<Merchant>().SetSpeed(-speed);
        }else{
           merchant.GetComponent<Merchant>().SetSpeed(speed);
        }
    }

}
