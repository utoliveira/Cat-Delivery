using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Costumer : MonoBehaviour {
    [SerializeField] private Item desiredItem;
    [SerializeField] private Text itemDisplay;

    private Coroutine expireCoroutine;

    private float timeToExpireItem = 8f;
     
     private void Start() {
         if(desiredItem){
            LoadDesiredItem();
         }
     }
     private void Update() {
         StopCoroutine(ExpireDesireItem());
     }

     public void SetDesiredItem(Item item){

        if(expireCoroutine != null){
            Debug.Log("Coroutine: " + expireCoroutine);
            StopCoroutine(expireCoroutine);
        }

        desiredItem = item;
        LoadDesiredItem();
    }

    private IEnumerator ExpireDesireItem(){
        yield return new WaitForSeconds(timeToExpireItem);
        RemoveDesiredItem();
        expireCoroutine = null;
    }

    public Item GetDesiredItem(){
        return desiredItem;
    }

    public bool HasDesiredItem(){
        return GetDesiredItem() != null;
    }

     public void RemoveDesiredItem(){
         desiredItem = null;
         itemDisplay.text = null;
     }

     private void LoadDesiredItem(){
        itemDisplay.text = desiredItem.name;
        expireCoroutine = StartCoroutine(ExpireDesireItem());
     }

}
