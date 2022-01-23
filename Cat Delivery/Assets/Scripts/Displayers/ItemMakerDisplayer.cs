using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ItemMakerDisplayer : MonoBehaviour
{
    private List<ColorableDisplayer> requiredItemsDisplayer = new List<ColorableDisplayer>();
    [SerializeField] private Transform requiredItemsPanel; 
    [SerializeField] private ColorableDisplayer resultItem;
    [SerializeField] private GameObject colorableDisplayerPrefab;

    public void Configure(ItemRecipe recipe){

        foreach(Transform child in requiredItemsPanel){
            Destroy(child.gameObject);
            DOTween.Kill(child);
        }

        recipe.items.ForEach(item => SetupRequiredItems(item, recipe.timeToProduce, colorableDisplayerPrefab));
        resultItem.ChangeItem(recipe.result);
        this.gameObject.SetActive(true);
        resultItem.gameObject.SetActive(false);
        requiredItemsPanel.gameObject.SetActive(true);
    }

    private void SetupRequiredItems(Item item, float productionTime, GameObject prefab){
        ColorableDisplayer itemDisplayer =  Instantiate(prefab, requiredItemsPanel)
            .GetComponent<ColorableDisplayer>();
        requiredItemsDisplayer.Add(itemDisplayer);
        itemDisplayer.ChangeColor(Color.yellow, productionTime);
        ApplyRotateAnimation(itemDisplayer.transform);
        itemDisplayer.ChangeItem(item);
    }

    public void ShowResultItem(){
        resultItem.gameObject.SetActive(true);
        requiredItemsPanel.gameObject.SetActive(false);
        resultItem.ChangeColor(Color.yellow);
        resultItem.transform.DOShakeScale(0.15f, 0.4f, 6);
    }

    public void Reset(){
        requiredItemsDisplayer.ForEach(item => item.ResetColor());
        resultItem.ResetColor();
        this.gameObject.SetActive(false);
    }


    private void ApplyRotateAnimation(Transform targetTransform){
        targetTransform
            .DORotate(new Vector3(0,0,10), 1f)
            .From(new Vector3(0,0,-10))
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

}
