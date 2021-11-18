using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostumerManager : MonoBehaviour
{
    public static CostumerManager instance;

    [SerializeField] private List<Costumer> costumers = new List<Costumer>();

    private void Awake() {
        if(!instance){
            instance = this;
            costumers = Helper.GetAllComponentsByTag<Costumer>(Tags.COSTUMER);
            return;
        }
        Destroy(this);
    }

    private void Start() {
        StartCoroutine(ConfigureCostumersDesires());
    }

    private IEnumerator ConfigureCostumersDesires(){
        while(true){
            yield return new WaitForSeconds(5f);
            //Configure if there's none
            
            Costumer availableCostumer = costumers.Find(costumer => !costumer.HasDesiredItem());
            if(!availableCostumer) continue;
            
            Good good = Helper.GetRandomized<Good>(LevelManager.instance.GetGameConfig().availableGoods);
            
            availableCostumer.SetDesiredItem(good);
            //Get which merchant has goods in time to player get it
            //How many players can have desired Item?
        }
    }

    public void ConfigureNewCostumers(Transform spawnPosition) {

        GameObject randomCostumer = Helper.GetRandomized<GameObject>(
            LevelManager.instance.GetGameConfig().availableCostumers);
        
        Costumer newCostumer = Instantiate(
            randomCostumer, 
            spawnPosition.position, 
            spawnPosition.rotation
        ).GetComponent<Costumer>();

        RegisterCostumer(newCostumer);
    }


    public void RegisterCostumer(Costumer costumer){
        this.costumers.Add(costumer);
    }
    public void RegisterCostumer(List<Costumer> costumers){
        Debug.Log(costumers);
        this.costumers.AddRange(costumers);
    }

    public void UnregisterCostumer(Costumer costumer){
        this.costumers.Remove(costumer);
    }
    public void UnregisterCostumer(List<Costumer> toRemove){
        this.costumers.RemoveAll( costumer => toRemove.Contains(costumer));
    }
}
