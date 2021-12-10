using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;
    [SerializeField] private WhiskasCounter whiskasCounter;
    [SerializeField] private CatCookiesCounter catCookiesCounter;
    [SerializeField] private GameObject GameOverMenu;
    [SerializeField] private ItemsHUD itemsHUD;
    [SerializeField] private CostumerHappinessHUD costumerHappinessHUD;

    private void Awake() {
        if(instance){
            Destroy(this.gameObject);
            return;
        }
        
        instance = this;
    }


    public void UpdateWhiskas(int whiskas){
        whiskasCounter.UpdateCurrentWhiskas(whiskas);
    }
    
    public void UpdateWhiskasLimit(int limit){
        whiskasCounter.UpdateLimit(limit);
    }

    public void GameOver(){
        GameOverMenu.SetActive(true);
    }
    
    public void RemoveItem(Item item){
        itemsHUD.RemoveItem(item);
    }
    public void AddItem(Item item){
        itemsHUD.AddItem(item);
    }

    public void UpdateCostumerHappinessCounter(int actualAmount, HappinessLevel happinessLevel){
        costumerHappinessHUD.UpdateCounter(actualAmount, happinessLevel);
    }

    public void UpdateCatCookies(int cookiesAmount, bool resetWhiskas = false){
        if(resetWhiskas)
            UpdateWhiskas(0);
        
        catCookiesCounter.UpdateCounter(cookiesAmount);
    }

}
