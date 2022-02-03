using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ItemMaker : MonoBehaviour, IItemDelivearable{
    [SerializeField] private ItemMakerDisplayer displayer;
    [SerializeField] private List<ItemRecipe> recipies;
    protected ItemRecipe currentRecipe;
    protected bool itemReady;
    [SerializeField] UnityEvent onProduceStart;
    [SerializeField] UnityEvent onProduceEnd;

    public void OnTryToDeliver(ItemStorage storage){
        if(storage == null  || storage.isEmpty() || currentRecipe){
            AudioManager.instance.Play(AudioCode.ITEM_DELIVERY_FAILURE);
            return;
        }

        List<ItemRecipe> recipes = GetAllPossibleRecipes(storage.GetItems());
        Debug.Log("Recipes Available:" + recipes.Count);
        if(recipes.Count == 0){
            //Show animation
            AudioManager.instance.Play(AudioCode.ITEM_DELIVERY_FAILURE);
            return;
        }

        if(recipes.Count == 1){
            StartCoroutine(ProduceItem(recipes[0], storage));
            return;
        }
        
        HUDManager.instance.OpenMultiItemDeliver(this, storage);
        
    }

    public bool ReceiveItems(List<Item> items, ItemStorage storage){
        ItemRecipe recipe = GetRecipe(items); 
        
        if(!recipe) {
            AudioManager.instance.Play(AudioCode.ITEM_DELIVERY_FAILURE);
            return false;
        }

        StartCoroutine(ProduceItem(recipe, storage));
        return true;
    }

    private IEnumerator ProduceItem(ItemRecipe recipe, ItemStorage storage){
        StartItemProduction(recipe, storage);
        yield return new WaitForSeconds(recipe.timeToProduce);
        FinishItemProduction();
    }

    private void StartItemProduction(ItemRecipe recipe, ItemStorage storage){
        OnStartProduction(recipe);
        storage.RemoveItems(recipe.items);
        AudioManager.instance.Play(AudioCode.ITEM_COLLECTING);
        displayer.Configure(recipe);
        this.currentRecipe = recipe;
        if(onProduceStart != null) 
            onProduceStart.Invoke();
    }
    protected virtual void OnStartProduction(ItemRecipe recipe){}

    private void FinishItemProduction(){
        displayer.ShowResultItem();
        itemReady = true;

        if(onProduceEnd != null)
            onProduceEnd.Invoke();
    }

    public Item DeliverItem(){
        Item result = currentRecipe.result;
        Reset();
        return result;
    } 

    private List<ItemRecipe> GetAllPossibleRecipes(List<Item> items){
        return recipies == null ? new List<ItemRecipe>() : recipies.FindAll(recipe => recipe.hasAllRequirements(items));
    }

    private ItemRecipe GetRecipe(List<Item> items){
        return recipies == null ? null : recipies.Find(recipe => recipe.hasExactRequirements(items));
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
