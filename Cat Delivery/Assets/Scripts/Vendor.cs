using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vendor : MonoBehaviour
{
    [SerializeField] private Text priceDisplay;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Good good;

    private void Start() {
        spriteRenderer.sprite = good.artwork;
        priceDisplay.text = good.price.ToString(); 
    } 

    public Good GetGood() {
        return good;
    }

    public void OnSellingGood(){
        Debug.Log("TUTCHIM!");
    }

}
