using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class ColorableItemMaker : ItemMaker
{
    [SerializeField] SpriteRenderer spriteRenderer;
    protected override void OnStartProduction(ItemRecipe recipe){
        if(!spriteRenderer) return;
        Color initialColor = spriteRenderer.color;
        
        spriteRenderer
            .DOColor(recipe.result.mainColor, recipe.timeToProduce)
            .SetAutoKill(true)
            .OnComplete(() => {
                spriteRenderer.color =  initialColor;
            });
    }


}
