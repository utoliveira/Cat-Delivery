using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStorage : MonoBehaviour
{
    
    [SerializeField] private List<Item> items = new List<Item>();
    
    private int storageLimit = 3; 

    public bool isEmpty(){
        return items.Count < 1;
    }

    public void AddItem(Item item){
        if(item == null) return;
        
        if(storageLimit == items.Count)
            RemoveFirstItem();
        
        items.Add(item);
        HUDManager.instance.AddItem(item);
    }

    public Item RemoveItem(Item itemToRemove){
        Item item = items.Find(item => item.name == itemToRemove.name);
        
        if(item){
            items.Remove(item);
            HUDManager.instance.RemoveItem(item);
            return item;
        }

        return null;
    }

    public void RemoveFirstItem(){
        if(items.Count == 0 ) return;
        
        Item item = items[0];
        HUDManager.instance.RemoveItem(item);
        items.Remove(item);
    }

}
