using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Costumer : MonoBehaviour {
    [SerializeField] private Item desiredItem;
    [SerializeField] private ItemDesireDisplayer itemDisplay;

    [SerializeField] private GameObject happyEffect;
    [SerializeField] private GameObject unhappyEffect;

    private Coroutine expireCoroutine;

    private float timeToExpireItem = 8f;
    private int timeItemSettled; 

    private bool isCoolingDown;
     
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
        timeItemSettled = DateTime.Now.Second;
        yield return new WaitForSeconds(timeToExpireItem);
        CostumerManager.instance.RegisterCostumerHappiness(HappinessLevel.UNHAPPY);
        Instantiate(unhappyEffect, this.transform);
        RemoveDesiredItem();
    }

    public Item GetDesiredItem(){
        return desiredItem;
    }

    public bool HasDesiredItem(){
        return GetDesiredItem() != null;
    }
    public bool IsCoolingDown(){
        return isCoolingDown;
    }

     private void RemoveDesiredItem(){
        desiredItem = null;
        itemDisplay.CleanDisplay();
        StopCoroutine(expireCoroutine);
        expireCoroutine = null;
     }

     public void OnDesiredItemDeliver(){
        HappinessLevel happinessLevel = GetHappiness();
        LevelManager.instance.AddWhiskas(GetPaymentValue(happinessLevel));
        CostumerManager.instance.RegisterCostumerHappiness(happinessLevel);
        StartCoroutine(Cooldown());
        RemoveDesiredItem();
        if(happinessLevel >= HappinessLevel.HAPPY)
            Instantiate(happyEffect, this.transform);
    }

    private int GetPaymentValue(HappinessLevel happinessLevel){
        int goodValue =  ((Good) desiredItem).basePrice;
       
        switch(happinessLevel){
            case HappinessLevel.SUPER_HAPPY:
                return goodValue * 3;
            
            case HappinessLevel.HAPPY:
                return goodValue * 2;
            
            default:
                return goodValue;
        }
    }

    private HappinessLevel GetHappiness(){
        int timeItemDelivered = DateTime.Now.Second;
        float percentOfTime = 1 - (timeItemDelivered - timeItemSettled) / timeToExpireItem;
        Debug.Log("DeliveredAt:"+ percentOfTime);

        if(percentOfTime > 0.8f )
            return HappinessLevel.SUPER_HAPPY;
        
        if(percentOfTime > 0.6f)
            return HappinessLevel.HAPPY;

        return HappinessLevel.REGULAR;
    }

     private void LoadDesiredItem(){
        itemDisplay.ChangeItem(desiredItem);
        expireCoroutine = StartCoroutine(ExpireDesireItem());
     }

     
    private IEnumerator Cooldown(){
        Debug.Log("Cooling down");
        isCoolingDown = true;
        yield return new WaitForSeconds(LevelManager.instance.GetGameConfig().costumerCooldownTime);
        isCoolingDown = false;
        Debug.Log("NOT Cooling down");
    }

}
