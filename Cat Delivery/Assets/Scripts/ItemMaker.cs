using System.Collections.Generic;
using UnityEngine;

public class ItemMaker : MonoBehaviour
{
    [SerializeField] private ItemMakerDisplayer displayer;
    [SerializeField] private ItemMakerConfig config;

    private bool itemReady;

    private List<Item> currentRequiredItems;

    private void Start() {
        displayer.Configure(config);
        currentRequiredItems = new List<Item>(config.requiredItems);
    }


    public void ReceiveItems(ItemStorage itemStorage){
        if(itemStorage == null  || itemStorage.isEmpty()) 
            return;
        
        List<Item> removedItems = currentRequiredItems
            .FindAll(item => itemStorage.RemoveItem(item) != null);
        
        if(removedItems.Count < 1) return;

        removedItems.ForEach(item => {
            displayer.ChangeRequiredItemColor(item);
            currentRequiredItems.Remove(item);
        });

        AudioManager.instance.Play(AudioCode.ITEM_COLLECTING);
        
        if(CheckAllRequirementsComplete()){
            displayer.ChangeResultColor();
            itemReady = true;
        }
        
    }

    public Item DeliverItem(){
        if(!itemReady) 
            return null; 

        AudioManager.instance.Play(AudioCode.ITEM_COLLECTING);
        Reset();
        return config.result;
    } 

    public bool IsItemReady(){
        return itemReady;
    }

    private void Reset(){
        currentRequiredItems = new List<Item>(config.requiredItems);
        displayer.Reset();
        itemReady = false;
    }

    private bool CheckAllRequirementsComplete(){
        return currentRequiredItems.Count < 1;
    }


}
