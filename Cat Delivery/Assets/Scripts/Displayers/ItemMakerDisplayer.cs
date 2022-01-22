using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMakerDisplayer : MonoBehaviour
{
    private List<ColorableDisplayer> requiredItemsDisplayer = new List<ColorableDisplayer>();
    [SerializeField] private Transform requiredItemsPanel; 
    [SerializeField] private ColorableDisplayer resultItem;
    [SerializeField] private GameObject colorableDisplayerPrefab;

    public void Configure(ItemRecipe recipe){

        foreach(Transform child in requiredItemsPanel){
            Destroy(child.gameObject);
        }

        recipe.items.ForEach(item => SetupRequiredItems(item, colorableDisplayerPrefab));
        resultItem.ChangeItem(recipe.result);
        this.gameObject.SetActive(true);
    }

    private void SetupRequiredItems(Item item, GameObject prefab){
        ColorableDisplayer itemDisplayer =  Instantiate(prefab, requiredItemsPanel)
            .GetComponent<ColorableDisplayer>();
        
        requiredItemsDisplayer.Add(itemDisplayer);
        itemDisplayer.ChangeItem(item);
    }

    public void ChangeResultColor(){
        resultItem.ChangeToFinalColor();
    }

    public void Reset(){
        requiredItemsDisplayer.ForEach(item => item.ResetColor());
        resultItem.ResetColor();
        this.gameObject.SetActive(false);
    }

}
