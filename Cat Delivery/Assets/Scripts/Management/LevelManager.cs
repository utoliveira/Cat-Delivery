using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance; 
    [SerializeField] private GameConfig gameConfig; //Change to LevelConfig
    [SerializeField] private DifficultyConfig currentDifficulty;
    [SerializeField] private int whiskas = 5;
    [SerializeField] private int catCookies = 0;
    
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
        HUDManager.instance.UpdateWhiskasLimit(currentDifficulty.whiskasToNextLevel);

        CostumerManager.instance.StartManagement(); //Change to stopAll with a Manager generic class Specific Manager
        MerchantManager.instance.StartManagement();
    }

    private void GoNextLevel(){
        AddCatCookies();
        if(currentDifficulty.hasNextLevel()){
            AudioManager.instance.Play(AudioCode.DIFFICULTY_CLEAR);
        }else{
            //LOAD new level info
            AudioManager.instance.Play(AudioCode.LEVEL_CLEAR);
            NotificationManager.instance.Notify("New level unlocked"); // TODO change it to Scriptable Objects if needed
            return;
        }
        
        this.currentDifficulty = currentDifficulty.nextDifficulty;
        HUDManager.instance.UpdateWhiskasLimit(currentDifficulty.whiskasToNextLevel);
        
        CostumerManager.instance.StartManagement(); //Change to stopAll with a Manager generic class Specific Manager
        MerchantManager.instance.StartManagement();
        //Reset the costumer manager level 
    }

    private void AddCatCookies(){
        catCookies++;
        HUDManager.instance.UpdateCatCookies(catCookies);
    }
    public void AddWhiskas(int whiskas, bool applyProfit = false){
        
        int amount = applyProfit ? whiskas * currentDifficulty.profitRate : whiskas;
        this.whiskas += Mathf.Abs(amount);
        HUDManager.instance.UpdateWhiskas(this.whiskas);
        
        if(isAbleToGoNextLevel())
            GoNextLevel();

        BuildingManager.instance.EvolveAnyBuildingIfPossible(this.whiskas);
        
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
        AudioManager.instance.Play(AudioCode.GAME_OVER);
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
