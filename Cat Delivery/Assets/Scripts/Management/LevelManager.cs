using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance; 
    [SerializeField] private GameConfig gameConfig; //Change to LevelConfig
    [SerializeField] private DifficultyConfig currentDifficulty;
    [SerializeField] private int whiskas = 5;
    private int lastBuildingLevel;
    private void Awake() {
        if(instance){
            Destroy(this);
            return;
        }
        instance = this;
    }

    private void Start() {
        Time.timeScale = 1;
        whiskas = gameConfig.initialWhiskas;
        HUDManager.instance.UpdateWhiskas(whiskas);

        CostumerManager.instance.StartManagement(); //Change to stopAll with a Manager generic class Specific Manager
        MerchantManager.instance.StartManagement();
    }

    private void GoNextLevel(){
        if(currentDifficulty.nextDifficulty != null)
            this.currentDifficulty = currentDifficulty.nextDifficulty;
        
        CostumerManager.instance.StartManagement(); //Change to stopAll with a Manager generic class Specific Manager
        MerchantManager.instance.StartManagement();
        //Reset the costumer manager level 
    }
    public void AddWhiskas(int whiskas, bool applyProfit = false){
        
        int amount = applyProfit ? whiskas * currentDifficulty.profitRate : whiskas;
        
        this.whiskas += Mathf.Abs(amount);
        
        HUDManager.instance.UpdateWhiskas(this.whiskas);

        if(isAbleToGoNextLevel())
            GoNextLevel();

        if(isAbleToEvolveBuilding()){
            BuildingManager.instance.EvolveAnyBuilding();
            lastBuildingLevel = GetCurrentBuildingLevel();
        }
        
    }

    private bool isAbleToEvolveBuilding() {
        return lastBuildingLevel < GetCurrentBuildingLevel();
    }

    private int GetCurrentBuildingLevel(){
        return (int) (this.whiskas / currentDifficulty.whiskasToEvolveBuildings);
    }

    private bool isAbleToGoNextLevel(){
        return this.whiskas > currentDifficulty.whiskasToNextLevel;
    }

    public void RemoveWhiskas(int whiskas){
        if(whiskas <= this.whiskas){
            this.whiskas -= whiskas;
            HUDManager.instance.UpdateWhiskas(this.whiskas);
        } 
    }

    public void GameOver(){
        Time.timeScale = 0;
        HUDManager.instance.GameOver();
    }

    public int GetWhiskas(){
        return whiskas;
    }

    public GameConfig GetGameConfig(){
        return gameConfig;
    }
    public DifficultyConfig GetDifficulty(){
        return currentDifficulty;
    }

}
