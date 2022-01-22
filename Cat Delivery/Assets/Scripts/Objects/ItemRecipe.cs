using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item Recipe Config", menuName ="Config/Item Recipe")]
public class ItemRecipe : ScriptableObject
{
    public List<Item> items;
    public Item result;
    public float timeToProduce = 1f;

    public bool hasAllRequirements(List<Item> items){
        List<Item> requiredItems = new List<Item>(this.items);

        items.ForEach(item => {
            requiredItems.Remove(item);
        });
        
        return requiredItems.Count == 0 ;
    }

    
    public bool hasExactRequirements(List<Item> itemsToCompare){
        int usableItemsCount = 0; 
        List<Item> requiredItems = new List<Item>(this.items);
        itemsToCompare.ForEach(item => {
            usableItemsCount += requiredItems.Remove(item) ? 1 : 0;
        });

        return requiredItems.Count == 0 && usableItemsCount == itemsToCompare.Count ;
    }

}
