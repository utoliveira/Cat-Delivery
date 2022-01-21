using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ItemMaker : MonoBehaviour, IItemDelivearable{
    [SerializeField] private ItemMakerDisplayer displayer;
    [SerializeField] private List<ItemRecipe> recipies;
    private ItemRecipe currentRecipe;
    private bool itemReady;

    public void OnTryToDeliver(ItemStorage itemStorage){
        if(itemStorage == null  || itemStorage.isEmpty() || currentRecipe){
            AudioManager.instance.Play(AudioCode.ITEM_DELIVERY_FAILURE);
            return;
        }

        List<ItemRecipe> recipes = GetAllRecipes(itemStorage.GetItems());

        if(recipes.Count == 0){
            //Show animation
            AudioManager.instance.Play(AudioCode.ITEM_DELIVERY_FAILURE);
            return;
        }

        if(recipes.Count == 1){
            ReceiveItems(itemStorage.GetItems(), itemStorage);
            return;
        }
        
        HUDManager.instance.OpenMultiItemDeliver(this, itemStorage);
        
    }
    
    public bool ReceiveItems(List<Item> items, ItemStorage storage){
        List<ItemRecipe> recipes = GetAllRecipes(items); // In case of receiving by MultiItemDeliver, this is important
        
        if(recipes.Count < 1) {
            return false;
            //doesnt have any recipe, just put a bad sound
        }

        storage.RemoveItems(recipes[0].items);
        AudioManager.instance.Play(AudioCode.ITEM_COLLECTING);
        StartCoroutine(ProduceItem(recipes[0]));
        return true;
    }

    private IEnumerator ProduceItem(ItemRecipe recipe){
        displayer.Configure(recipe);
        this.currentRecipe = recipe;
        yield return new WaitForSeconds(recipe.timeToProduce);
        displayer.ChangeResultColor();
        itemReady = true;
    }

    public Item DeliverItem(){
        Item result = currentRecipe.result;
        Reset();
        return result;
    } 

    private List<ItemRecipe> GetAllRecipes(List<Item> items){
        return recipies == null ? new List<ItemRecipe>() : recipies.FindAll(recipe => recipe.hasExactRequirements(items));
    }

    public bool IsItemReady(){
        return itemReady;
    }

    private void Reset(){
        displayer.Reset();
        itemReady = false;
        currentRecipe = null;
    }


}
