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
    private SatisfactionEnum currentSatisfaction;
    private float timeToExpireItem = 8f;
    private int timeItemSettled; 
    private bool isCoolingDown;
     
     private void Start() {
         if(desiredItem){
            LoadDesiredItem();
         }
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
        Instantiate(unhappyEffect, this.transform);
        UpdateSatisfaction(SatisfactionEnum.UNHAPPY);
        RemoveDesiredItem();
        AudioManager.instance.Play(AudioCode.ITEM_DELIVERY_FAILURE);
        CostumerManager.instance.CheckUnhappyCostumers();
    }

    private void UpdateSatisfaction(SatisfactionEnum newSatisfaction){
        int satisfaction = ((int)this.currentSatisfaction) + (int)newSatisfaction;
        satisfaction = Mathf.Clamp(satisfaction, (int)SatisfactionEnum.UNHAPPY, (int)SatisfactionEnum.SUPER_HAPPY);
        this.currentSatisfaction = (SatisfactionEnum) satisfaction;
    }

    public Item GetDesiredItem(){
        return desiredItem;
    }

    public SatisfactionEnum GetSatisfaction(){
        return this.currentSatisfaction;
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
        SatisfactionEnum satisfaction = GetSatisfactionOnDeliver();
        UpdateSatisfaction(satisfaction);
        
        int paymentValue = SatisFactionHelper.GetPaymentValue(satisfaction, (Good)this.desiredItem);
        LevelManager.instance.AddWhiskas(paymentValue);
        
        if(satisfaction >= SatisfactionEnum.REGULAR)
            CostumerManager.instance.CheckHappyCostumers();
        
        Instantiate(whiskasEffect, this.transform)
            .GetComponent<WhiskasEffect>()
            .Configure(paymentValue, GetWhiskasEffectColor(satisfaction));

        RemoveDesiredItem();
        AudioManager.instance.Play(AudioCode.ITEM_DELIVERING);
    }
    private SatisfactionEnum GetSatisfactionOnDeliver(){
        int timeItemDelivered = DateTime.Now.Second;
        return SatisFactionHelper.GetDeliverSatisfaction(timeItemDelivered, timeItemSettled, timeToExpireItem);
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

    
    private WhiskasEffectColors GetWhiskasEffectColor(SatisfactionEnum satisfaction){
        return satisfaction > SatisfactionEnum.REGULAR ? WhiskasEffectColors.BONUS : WhiskasEffectColors.REGULAR;
    }


}
