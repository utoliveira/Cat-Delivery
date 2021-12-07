using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vendor : MonoBehaviour
{
    [SerializeField] private Text priceDisplay;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Good good;
    [SerializeField] private GameObject whiskasEffect;

    private void Start() {
        ConfigureGood(good);
    } 

    private void ConfigureGood(Good good){
        spriteRenderer.sprite = good.artwork;
        priceDisplay.text = good.basePrice.ToString(); 
    }

    public Good GetGood() {
        return good;
    }

    public void OnSellingGood(){
        Instantiate(whiskasEffect, this.transform.position, whiskasEffect.transform.rotation)
            .GetComponent<WhiskasEffect>()
            .Configure(-good.basePrice, WhiskasEffectColors.NEGATIVE);
    }

    public void SetGood(Good good){
        this.good = good;
        ConfigureGood(good);
    }

}
