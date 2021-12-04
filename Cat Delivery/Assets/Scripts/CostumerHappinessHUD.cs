using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostumerHappinessHUD : MonoBehaviour {

    private const string ANIMATOR_PULSE_TRIGGER = "pulse"; 
    private const string ANIMATOR_IS_PULSING = "isPulsing"; 
    private const string ANIMATOR_IS_SHAKING = "isShaking"; 
    [SerializeField] private Animator animator;
    [SerializeField] private Sprite unhappySprite;
    [SerializeField] private Sprite regularMoodSprite;
    [SerializeField] private Sprite happySprite;
    [SerializeField] private Image imageComponent;

    private int happinessAmount;

    public void UpdateCounter(int happinessAmount, HappinessLevel costumerReaction){
        this.happinessAmount = happinessAmount;
        imageComponent.sprite = GetSpriteBasedOnReaction(costumerReaction);
        animator.SetTrigger(ANIMATOR_PULSE_TRIGGER);
    }

    /*Called in animation fast_pulse*/
    private void ConfigureCurrentCostumerHappiness(){
        DifficultyConfig currentDifficulty = LevelManager.instance.GetDifficulty();
        animator.SetBool(ANIMATOR_IS_PULSING, false);
        animator.SetBool(ANIMATOR_IS_SHAKING, false);

        if(happinessAmount == currentDifficulty.maxCostumerHappiness){
            SetSpriteInHUD(happySprite);
            animator.SetBool(ANIMATOR_IS_PULSING, true);
            return;
        }

        if(happinessAmount == currentDifficulty.minCostumerHappiness){
            SetSpriteInHUD(unhappySprite);
            animator.SetBool(ANIMATOR_IS_SHAKING, true);
            return;
        }

        SetSpriteInHUD(regularMoodSprite);
    }

    private void SetSpriteInHUD(Sprite sprite){
        imageComponent.sprite = sprite;
    }

    private Sprite GetSpriteBasedOnReaction(HappinessLevel costumerReaction){
        switch(costumerReaction){
            case HappinessLevel.HAPPY:
                return happySprite;
            case HappinessLevel.UNHAPPY:
                return unhappySprite;
            default:
                return regularMoodSprite;
        }
    }

}
