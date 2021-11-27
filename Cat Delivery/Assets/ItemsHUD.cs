using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsHUD : MonoBehaviour
{
    private List<ItemDisplayer> itemsOnDisplay = new List<ItemDisplayer>();
    [SerializeField] private GameObject displayerPrefab;

    public void AddItem(Item item){
        ItemDisplayer displayer = Instantiate(displayerPrefab, this.transform).GetComponent<ItemDisplayer>();
        displayer.ChangeItem(item);
        itemsOnDisplay.Add(displayer);
    }

    public void RemoveItem(Item item){
        if(!item) return;

        ItemDisplayer itemFound = itemsOnDisplay.Find(displayer => displayer.isDisplaying(item));
        
        if(itemFound){
            itemsOnDisplay.Remove(itemFound);
            Destroy(itemFound.gameObject);
        }
    }
}
