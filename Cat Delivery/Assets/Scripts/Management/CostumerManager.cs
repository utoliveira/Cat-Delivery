using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostumerManager : MonoBehaviour
{
    public static CostumerManager instance;

    [SerializeField] private List<Costumer> costumers = new List<Costumer>();

    private Coroutine currentManagement; //Replace for abstract
    private void Awake() {
        if(!instance){
            instance = this;
            costumers = Helper.GetAllComponentsByTag<Costumer>(Tags.COSTUMER);
            return;
        }
        Destroy(this);
    }

    public void StartManagement() {
        if(currentManagement != null)
            StopCoroutine(currentManagement);

        currentManagement = StartCoroutine(ConfigureCostumersDesires());
    }

    private IEnumerator ConfigureCostumersDesires(){
        DifficultyConfig difficulty = LevelManager.instance.GetDifficulty();
       
        while(true){
            yield return new WaitForSeconds(difficulty.timeToConfigureCostumer);
            
            Costumer costumer = Helper.GetRandomized<Costumer>(GetAvailableCostumers());
            
            if(!costumer) continue;
            
            Good good = Helper.GetRandomized<Good>(MerchantManager.instance.GetAvailableGood());

            if(good){
                costumer.SetDesiredItem(good);
            }
        }
    }

    public List<Costumer> GetAvailableCostumers () {
        return costumers.FindAll(costumer => !costumer.HasDesiredItem());
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
