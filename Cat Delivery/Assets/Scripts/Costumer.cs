using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Costumer : MonoBehaviour {
    [SerializeField] private Item desiredItem;
    [SerializeField] private Text itemDisplay;
     
     private void Start() {
         LoadDesiredItem();
     }

     public void ChangeItem(Item item){
         desiredItem = item;
         LoadDesiredItem();
     }

    public Item GetDesiredItem(){
        return desiredItem;
    }

     public void OnItemDeliver(){
         desiredItem = null;
         itemDisplay.text = null;
     }

     private void LoadDesiredItem(){
         itemDisplay.text = desiredItem.name;
     }

}
