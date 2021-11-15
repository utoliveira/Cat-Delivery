using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStorage : MonoBehaviour
{
    
    [SerializeField] private List<Item> items;
    [SerializeField] private Transform bagPosition; 
    
    private int itemsCapacity = 1;

    public bool IsFull(){
        return items.Count >= itemsCapacity;
    }

    public bool AddItem(Item item){
        if(IsFull()) return false;

        items.Add(item);
        return true;
    }

    public Item RemoveItem(string itemName){
        Item item = items.Find(item => item.name == itemName);
        items.Remove(item);
        return item;
    }

}
