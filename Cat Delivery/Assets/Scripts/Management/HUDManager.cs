using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;
    [SerializeField] private WhiskasCounter whiskasCounter;
    [SerializeField] private CatCookiesCounter catCookiesCounter;
    [SerializeField] private GameObject GameOverMenu;
    [SerializeField] private ItemsHUD itemsHUD;

    private void Awake() {
        if(instance){
            Destroy(this.gameObject);
            return;
        }
        
        instance = this;
    }

    public void UpdateWhiskas(int whiskas){
        if(Helper.isDefined(whiskasCounter)){
            whiskasCounter.UpdateCurrentWhiskas(whiskas);
        }
    }
    
    public void UpdateWhiskasLimit(int limit){
        if(!whiskasCounter){
            Debug.LogWarning("Whiskas counter undefined");
        }
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

    public void UpdateCostumerHappinessCounter(int actualAmount){
        //costumerHappinessHUD.UpdateCounter(actualAmount);
    }

    public void UpdateCatCookies(int cookiesAmount, bool resetWhiskas = false){
        if(resetWhiskas)
            UpdateWhiskas(0);
        
        catCookiesCounter.UpdateCounter(cookiesAmount);
    }

}
