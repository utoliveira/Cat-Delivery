using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item Maker Config", menuName ="Config/Item Maker")]
public class ItemRecipe : ScriptableObject
{
    public List<Item> items;
    public Item result;
    public int serviceValue;
    public GameObject colorableDisplayerPrefab;

    public float timeToProduce = 1f;

    public bool hasExactRequirements(List<Item> items){
        List<Item> requiredItems = new List<Item>(this.items);
        int usableItemsCount = 0;
        items.ForEach(item => {
            requiredItems.Remove(item);
            usableItemsCount++;
        });
        
        return requiredItems.Count == 0 && items.Count == usableItemsCount;
    }

}
