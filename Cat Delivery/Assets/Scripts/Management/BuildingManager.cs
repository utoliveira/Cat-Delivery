using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private List<Building> buildings;
    public static BuildingManager instance;
    private int minWhiskasToEvolve = 0;
    private bool allBuildingsEvolved;

    private void Awake() {
        if(instance){
            Destroy(this);
            return;
        }
        instance = this;
        buildings = Helper.GetAllComponentsByTag<Building>(Tags.BUILDING);
    }

    public void EvolveAnyBuildingIfPossible(int currentWhiskas){
        if(!IsAbleToEvolve(currentWhiskas)) 
            return;

        minWhiskasToEvolve = currentWhiskas;
        EvolveAnyBuilding();

    }

    private bool IsAbleToEvolve(int currentWhiskas){
        return  HasEnoughWhiskasToEvolve(currentWhiskas)  && !allBuildingsEvolved;
    }

    private bool HasEnoughWhiskasToEvolve(int whiskas){
        return whiskas > minWhiskasToEvolve && whiskas - minWhiskasToEvolve >= LevelManager.instance.GetDifficulty().whiskasToEvolveBuildings;
    }

    private void EvolveAnyBuilding(){

        Building evolvableBuilding = buildings.Find(building => building.hasNextLevel());
        
        if(evolvableBuilding == null) {
            allBuildingsEvolved = true;
            return;
        }
        
        EvolveBuilding(evolvableBuilding);
    }

    private void EvolveBuilding(Building building){
        if(!building || !building.hasNextLevel()) 
            return;

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
