using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;
    [SerializeField] private Text WhiskasDisplay;
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
        WhiskasDisplay.text = whiskas.ToString();
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
}
