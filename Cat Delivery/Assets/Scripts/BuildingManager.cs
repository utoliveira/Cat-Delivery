using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private List<Building> buildings;
    private bool hasEvolutionAvailable = false;

    private void Awake() {
        buildings = Helper.GetAllComponentsByTag<Building>(Tags.BUILDING);
    }

    private void Start() {
        StartCoroutine(EvolveBuildings());
    }

    private IEnumerator EvolveBuildings(){
        while(true){
            yield return new WaitForSeconds(15f);
            Building evolvableBuilding = buildings.Find(building => building.hasNextLevel());
            EvolveBuilding(evolvableBuilding);

        }
        
    }

    private void EvolveBuilding(Building building){
        if(!building || !building.hasNextLevel()) return;

        GameObject nextLevelPrefab = building.getNextLevelPrefab(); 
        
        Building newBuilding = Instantiate(
            nextLevelPrefab,
            building.transform.position,
            nextLevelPrefab.transform.rotation)
            .GetComponent<Building>();
            
        RegisterBuilding(newBuilding);
        
        if(newBuilding.GetSpawnPositions() != null){
            newBuilding.GetSpawnPositions()
                .ForEach(position => CostumerManager.instance.ConfigureNewCostumers(position));
        }

        UnregisterBuilding(building);
    } 
    
    private void RegisterBuilding(Building building){
        buildings.Add(building);
    }

    private void UnregisterBuilding(Building building){
        buildings.Remove(building);
        Destroy(building.gameObject);
    }




    //Building configs: => This one has the information of the available buildings 
    
    //EvolveOrCreateNewBuilding(){}
    //Check if there's any building spot available
    //  if yes create a new building
    //      get a random position on availableSpots => remove it afterwards
    // else evolve a building
    //  check the highest level of the buildings. if the building is at its highest, ignore it

    //EvolveAnyBuilding(){}
        //Check if there's any available building spot
            // - Randomize chances to evolve a house or a building spot.



    //Create a new Building(){}    


}
