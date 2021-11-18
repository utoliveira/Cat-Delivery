using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Costumer : MonoBehaviour {
    [SerializeField] private Item desiredItem;
    [SerializeField] private Text itemDisplay;
     
     private void Start() {
         if(desiredItem){
            LoadDesiredItem();
         }
     }

     public void SetDesiredItem(Item item){
         Debug.Log("Chamou");
         desiredItem = item;
         LoadDesiredItem();
     }

    public Item GetDesiredItem(){
        return desiredItem;
    }

    public bool HasDesiredItem(){
        return GetDesiredItem() != null;
    }

     public void OnItemDeliver(){
         desiredItem = null;
         itemDisplay.text = null;
     }

     private void LoadDesiredItem(){
         itemDisplay.text = desiredItem.name;
     }

}
