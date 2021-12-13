using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMakerDisplayer : MonoBehaviour
{
    private List<ColorableDisplayer> requiredItemsDisplayer = new List<ColorableDisplayer>();
    [SerializeField] private Transform requiredItemsPanel; 
    [SerializeField] private ColorableDisplayer resultItem;
    [SerializeField] private Text valueText;

    public void Configure(ItemMakerConfig config){

        foreach(Transform child in requiredItemsPanel){
            Destroy(child.gameObject);
        }

        config.requiredItems.ForEach(item => SetupRequiredItems(item, config.colorableDisplayerPrefab));
        resultItem.ChangeItem(config.result);
        ChangeValue(config.serviceValue);
    }

    private void SetupRequiredItems(Item item, GameObject prefab){
        ColorableDisplayer itemDisplayer =  Instantiate(prefab, requiredItemsPanel).GetComponent<ColorableDisplayer>();
        requiredItemsDisplayer.Add(itemDisplayer);
        itemDisplayer.ChangeItem(item);
    }

    private void ChangeValue(int value){
        valueText.text = "+ " + value.ToString(); 
    }

    public void ChangeRequiredItemColor(Item item){
       ColorableDisplayer foundDisplayer = requiredItemsDisplayer
            .Find(displayer => !displayer.IsFinalColor() && displayer.isDisplaying(item));
            
       if(foundDisplayer != null) 
            foundDisplayer.ChangeToFinalColor();
    }

    public void ChangeResultColor(){
        resultItem.ChangeToFinalColor();
    }

    public void Reset(){
        requiredItemsDisplayer.ForEach(item => item.ResetColor());
        resultItem.ResetColor();
    }

}
