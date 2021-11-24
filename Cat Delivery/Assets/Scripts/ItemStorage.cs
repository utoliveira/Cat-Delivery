using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStorage : MonoBehaviour
{
    
    [SerializeField] private List<Item> items = new List<Item>();

    public bool isEmpty(){
        return items.Count < 1;
    }

    public void AddItem(Item item){
        items.Add(item);
    }

    public Item RemoveItem(string itemName){
        Item item = items.Find(item => item.name == itemName);
        items.Remove(item);
        return item;
    }

}
