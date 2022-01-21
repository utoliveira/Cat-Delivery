using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStorage : MonoBehaviour
{
    
    private List<Item> items = new List<Item>();
    
    private int storageLimit = 3; 

    public bool isEmpty(){
        return items.Count < 1;
    }

    public List<Item> GetItems(){
        return items;
    }

    public void AddItem(Item item){
        if(item == null) return;
        
        if(storageLimit == items.Count)
            RemoveFirstItem();
        
        items.Add(item);
        HUDManager.instance.AddItem(item);
    }

    public bool RemoveItem(Item item){
        if(!item) return false;

        items.Remove(item);
        HUDManager.instance.RemoveItem(item);
        return true;
    }

    public bool RemoveItems(List<Item> items){  
        if(items == null || items.Count < 1 ) return false;

        items.ForEach(item => RemoveItem(item));
        return true;
    }


    public void RemoveFirstItem(){
        if(items.Count == 0 ) return;
        
        Item item = items[0];
        HUDManager.instance.RemoveItem(item);
        items.Remove(item);
    }

}
