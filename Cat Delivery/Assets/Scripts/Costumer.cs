using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Costumer : MonoBehaviour {
    [SerializeField] private Item desiredItem;
    [SerializeField] private ItemDesireDisplayer itemDisplay;
    [SerializeField] private GameObject unhappyEffect;
    [SerializeField] private GameObject whiskasEffect;

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
        AudioManager.instance.Play(AudioCode.ITEM_DELIVERY_FAILURE);
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
        StartCoroutine(Cooldown());
        expireCoroutine = null;
     }

     public void OnDesiredItemDeliver(){
        HappinessLevel happinessLevel = GetHappiness();
        CostumerManager.instance.RegisterCostumerHappiness(happinessLevel);
        int paymentValue = GetPaymentValue(happinessLevel);
        LevelManager.instance.AddWhiskas(paymentValue);
        
        Instantiate(whiskasEffect, this.transform)
            .GetComponent<WhiskasEffect>()
            .Configure(paymentValue, GetWhiskasEffectColor(happinessLevel));

        RemoveDesiredItem();
        AudioManager.instance.Play(AudioCode.ITEM_DELIVERING);
    }
    private WhiskasEffectColors GetWhiskasEffectColor(HappinessLevel happinessLevel){
        return happinessLevel > HappinessLevel.REGULAR ? WhiskasEffectColors.BONUS : WhiskasEffectColors.REGULAR;
    }

//Remove it to a ProfitManager
    private int GetPaymentValue(HappinessLevel happinessLevel){
        int goodValue =  GetDesiredItemValue();
       
        switch(happinessLevel){
            case HappinessLevel.SUPER_HAPPY:
                return goodValue * 3;
            
            case HappinessLevel.HAPPY:
                return goodValue * 2;
            
            default:
                return goodValue;
        }
    }

    private int GetDesiredItemValue(){
        return ((Good) desiredItem).basePrice;
    }
    private HappinessLevel GetHappiness(){
        int timeItemDelivered = DateTime.Now.Second;
        float percentOfTime = 1 - (timeItemDelivered - timeItemSettled) / timeToExpireItem;
        Debug.Log("DeliveredAt:"+ percentOfTime);

        if(percentOfTime > 0.7f )
            return HappinessLevel.SUPER_HAPPY;
        
        if(percentOfTime > 0.3f)
            return HappinessLevel.HAPPY;

        return HappinessLevel.REGULAR;
    }

     private void LoadDesiredItem(){
        itemDisplay.ChangeItem(desiredItem);
        expireCoroutine = StartCoroutine(ExpireDesireItem());
     }

     
    private IEnumerator Cooldown(){
        isCoolingDown = true;
        yield return new WaitForSeconds(LevelManager.instance.GetDifficulty().costumerCooldownTime);
        isCoolingDown = false;
    }

}
