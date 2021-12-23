using System.Collections;
using UnityEngine;
using System;

public class Costumer : MonoBehaviour {
    [SerializeField] private Good desiredGood;
    [SerializeField] private GradientDisplayer itemDisplay;
    [SerializeField] private Animator expressionsAnimator;
    [SerializeField] private GameObject whiskasEffect;
    private Coroutine expireCoroutine;
    private SatisfactionEnum currentSatisfaction;
    private int timeItemSettled; 
    private bool isCoolingDown;

    [SerializeField] private bool applyExpireRoutine = true;

     public void SetDesiredItem(Good good){
        ClearExpireCoroutine();
        this.desiredGood = good;
        ConfigureDesiredGood();
    }

    private void ConfigureDesiredGood(){
        itemDisplay.ChangeItem(desiredGood, applyExpireRoutine);
        
        if(applyExpireRoutine)
            expireCoroutine = StartCoroutine(ExpireDesireItem());
    }

    private IEnumerator ExpireDesireItem(){
        timeItemSettled = DateTime.Now.Second;
        yield return new WaitForSeconds(desiredGood.desireTime);
        expressionsAnimator.SetTrigger(ExpressionsConstants.ANIM_GET_DISAPPOINTED);
        UpdateSatisfaction(SatisfactionEnum.UNHAPPY);
        RemoveDesiredGood();
        AudioManager.instance.Play(AudioCode.ITEM_DELIVERY_FAILURE);
        //CostumerManager.instance.CheckUnhappyCostumers();
    }

    private void UpdateSatisfaction(SatisfactionEnum newSatisfaction){
        int satisfaction = ((int)this.currentSatisfaction) + (int)newSatisfaction;
        satisfaction = Mathf.Clamp(satisfaction, (int)SatisfactionEnum.UNHAPPY, (int)SatisfactionEnum.SUPER_HAPPY);
        this.currentSatisfaction = (SatisfactionEnum) satisfaction;

        //TODO: Improve here
        if(this.currentSatisfaction == SatisfactionEnum.UNHAPPY)
            expressionsAnimator.SetBool(ExpressionsConstants.ANIM_IS_ANGRY, true);
        
        if(this.currentSatisfaction >= SatisfactionEnum.REGULAR)
            expressionsAnimator.SetBool(ExpressionsConstants.ANIM_IS_ANGRY, false);
        
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
        ClearExpireCoroutine();
        //StartCoroutine(Cooldown());
        expireCoroutine = null;
     }
    
    private void ClearExpireCoroutine(){
        if(expireCoroutine != null)
            StopCoroutine(expireCoroutine);
    }

     public void OnDesiredItemDeliver(){
        SatisfactionEnum satisfaction = GetSatisfactionOnDeliver();
        UpdateSatisfaction(satisfaction);
        
        int paymentValue = SatisFactionHelper.GetPaymentValue(satisfaction, this.desiredGood);
        LevelManager.instance.AddWhiskas(paymentValue);
        
        //if(satisfaction >= SatisfactionEnum.REGULAR)
        //    CostumerManager.instance.CheckHappyCostumers();
        
        expressionsAnimator.SetTrigger(ExpressionsConstants.ANIM_LAUGH);
        
        Instantiate(whiskasEffect, this.transform.position, whiskasEffect.transform.rotation, this.transform)
            .GetComponent<WhiskasEffect>()
            .Configure(paymentValue, GetWhiskasEffectColor(satisfaction));

        RemoveDesiredGood();
        AudioManager.instance.Play(AudioCode.ITEM_DELIVERING);
    }
    private SatisfactionEnum GetSatisfactionOnDeliver(){
        if(!applyExpireRoutine) 
            return SatisfactionEnum.HAPPY;

        int timeItemDelivered = DateTime.Now.Second;
        return SatisFactionHelper.GetDeliverSatisfaction(timeItemDelivered, timeItemSettled, desiredGood.desireTime);
    }

     /*
    private IEnumerator Cooldown(){
        isCoolingDown = true;
        yield return new WaitForSeconds(LevelManager.instance.GetDifficulty().costumerCooldownTime);
        isCoolingDown = false;
    }*/

    
    private WhiskasEffectColors GetWhiskasEffectColor(SatisfactionEnum satisfaction){
        return satisfaction > SatisfactionEnum.REGULAR ? WhiskasEffectColors.BONUS : WhiskasEffectColors.REGULAR;
    }


}
