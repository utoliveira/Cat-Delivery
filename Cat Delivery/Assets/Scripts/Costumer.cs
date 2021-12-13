using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Costumer : MonoBehaviour {
    [SerializeField] private Good desiredGood;
    [SerializeField] private GradientDisplayer itemDisplay;
    [SerializeField] private GameObject unhappyEffect;
    [SerializeField] private GameObject whiskasEffect;
    private Coroutine expireCoroutine;
    private SatisfactionEnum currentSatisfaction;
    private int timeItemSettled; 
    private bool isCoolingDown;

     public void SetDesiredItem(Good good){

        if(expireCoroutine != null)
            StopCoroutine(expireCoroutine);
        
        this.desiredGood = good;
        ConfigureDesiredGood();
    }

    private void ConfigureDesiredGood(){
        itemDisplay.ChangeItem(desiredGood);
        expireCoroutine = StartCoroutine(ExpireDesireItem());
    }

    private IEnumerator ExpireDesireItem(){
        timeItemSettled = DateTime.Now.Second;
        yield return new WaitForSeconds(desiredGood.desireTime);
        Instantiate(unhappyEffect, this.transform);
        UpdateSatisfaction(SatisfactionEnum.UNHAPPY);
        RemoveDesiredGood();
        AudioManager.instance.Play(AudioCode.ITEM_DELIVERY_FAILURE);
        CostumerManager.instance.CheckUnhappyCostumers();
    }

    private void UpdateSatisfaction(SatisfactionEnum newSatisfaction){
        int satisfaction = ((int)this.currentSatisfaction) + (int)newSatisfaction;
        satisfaction = Mathf.Clamp(satisfaction, (int)SatisfactionEnum.UNHAPPY, (int)SatisfactionEnum.SUPER_HAPPY);
        this.currentSatisfaction = (SatisfactionEnum) satisfaction;
    }

    public Good GetDesiredGood(){
        return desiredGood;
    }

    public SatisfactionEnum GetSatisfaction(){
        return this.currentSatisfaction;
    }
    public bool HasDesiredItem(){
        return GetDesiredGood() != null;
    }
    public bool IsCoolingDown(){
        return isCoolingDown;
    }

     private void RemoveDesiredGood(){
        desiredGood = null;
        itemDisplay.CleanDisplay();
        StopCoroutine(expireCoroutine);
        StartCoroutine(Cooldown());
        expireCoroutine = null;
     }

     public void OnDesiredItemDeliver(){
        SatisfactionEnum satisfaction = GetSatisfactionOnDeliver();
        UpdateSatisfaction(satisfaction);
        
        int paymentValue = SatisFactionHelper.GetPaymentValue(satisfaction, this.desiredGood);
        LevelManager.instance.AddWhiskas(paymentValue);
        
        if(satisfaction >= SatisfactionEnum.REGULAR)
            CostumerManager.instance.CheckHappyCostumers();
        
        Instantiate(whiskasEffect, this.transform)
            .GetComponent<WhiskasEffect>()
            .Configure(paymentValue, GetWhiskasEffectColor(satisfaction));

        RemoveDesiredGood();
        AudioManager.instance.Play(AudioCode.ITEM_DELIVERING);
    }
    private SatisfactionEnum GetSatisfactionOnDeliver(){
        int timeItemDelivered = DateTime.Now.Second;
        return SatisFactionHelper.GetDeliverSatisfaction(timeItemDelivered, timeItemSettled, desiredGood.desireTime);
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
