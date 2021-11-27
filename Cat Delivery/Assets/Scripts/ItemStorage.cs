using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStorage : MonoBehaviour
{
    
    [SerializeField] private List<Item> items = new List<Item>();
    
    private int storageLimit = 3; //TODO implementar limite

    public bool isEmpty(){
        return items.Count < 1;
    }

    public void AddItem(Item item){
        items.Add(item);
        HUDManager.instance.AddItem(item);
    }

    public Item RemoveItem(string itemName){
        Item item = items.Find(item => item.name == itemName);
        if(item){
            items.Remove(item);
            HUDManager.instance.RemoveItem(item);
            return item;
        }

        return null;
    }

}
