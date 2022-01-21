using UnityEngine;
using UnityEngine.Events;
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance; 
    [SerializeField] private LevelConfig config;
    [SerializeField] UnityEvent OnLevelClearEvents;
    private Difficulty currentDifficulty;
    private int whiskas;
    private int catCookies;
    [SerializeField] private Transform playerSpawn;
    public bool playOnAwake;
    private void Awake() {
        if(instance){
            Destroy(this);
            return;
        }
        instance = this;
    }

    private void Start() {
        if(playOnAwake){
            StartManager();
        }
    }

    public void StartManager(){
        Time.timeScale = 1;
        
        if(!config) {
            Debug.LogWarning("Level without config");
            return;
        }

        SetupConfig();
        StartManagers();
    }

    private void SetupConfig(){
        whiskas = config.initialWhiskas;
        HUDManager.instance.UpdateWhiskas(whiskas);
        
        currentDifficulty = config.initialDifficulty;
        HUDManager.instance.UpdateWhiskasLimit(currentDifficulty.whiskasToNextLevel);
    }

    private void StartManagers() {
        if(MerchantManager.instance) MerchantManager.instance.StartManagement();
        if(CostumerManager.instance) CostumerManager.instance.StartManagement();
    }

    private void OnLevelClear(){
        AudioManager.instance.Play(AudioCode.LEVEL_CLEAR);
        OnLevelClearEvents.Invoke();
    }

    private void SetupNewDifficulty(){
        AudioManager.instance.Play(AudioCode.DIFFICULTY_CLEAR);
        this.currentDifficulty = currentDifficulty.nextDifficulty;
        HUDManager.instance.UpdateWhiskasLimit(currentDifficulty.whiskasToNextLevel);
        StartManagers();
    }

    private void AddCatCookies(){
        catCookies++;
        HUDManager.instance.UpdateCatCookies(catCookies);
    }
    public void AddWhiskas(int whiskas, bool applyProfit = false){
        int amount = applyProfit ? whiskas * currentDifficulty.costumers.profitRate : whiskas;
        this.whiskas += Mathf.Abs(amount);
        HUDManager.instance.UpdateWhiskas(this.whiskas);
        
        if(isAbleToGoNextLevel())
            GoNextLevel();
    }
    
    private void GoNextLevel(){
        if(currentDifficulty.hasNextDifficulty()){
            SetupNewDifficulty();
            AddCatCookies();
            return;
        }

        OnLevelClear();
    }   

    private bool isAbleToGoNextLevel(){
        return this.whiskas >= currentDifficulty.whiskasToNextLevel;
    }

    public bool RemoveWhiskas(int whiskas){
        if(whiskas > this.whiskas) 
            return false;

        this.whiskas -= whiskas;
        HUDManager.instance.UpdateWhiskas(this.whiskas);
        return true;
    }

    public void OnPlayerDies(GameObject player){
        if(currentDifficulty.playerCanDie){
            Destroy(player);
            GameOver();
            return;
        }

        player.transform.position = playerSpawn.position;

    }

    public void GameOver(){
        Time.timeScale = 0;
        AudioManager.instance.Play(AudioCode.GAME_OVER);
    }

    public int GetWhiskas(){
        return whiskas;
    }

    public static LevelConfig GetGameConfig(){
        return instance.config;
    }
    public static Difficulty GetCurrentDifficulty(){
        return instance.currentDifficulty;
    }

    public static MerchantConfig GetMerchantConfig(){
        return GetCurrentDifficulty().merchants;
    }
    public static CostumerConfig GetCostumerConfig(){
        return GetCurrentDifficulty().costumers;
    }

}
