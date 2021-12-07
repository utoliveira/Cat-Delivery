using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum WhiskasEffectColors {
    REGULAR,
    NEGATIVE,
    BONUS
}

public class WhiskasEffect : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Animator animator;

    [SerializeField] private Color regularColor;
    [SerializeField] private Color negativeColor;
    [SerializeField] private Color bonusColor;
    

    public void Configure(int whiskas, WhiskasEffectColors color){
        text.text = whiskas.ToString();
        text.color = GetColor(color);
        
        if(whiskas > 0) 
            text.text = "+" + text.text;
        
        animator.enabled = true;
    }

    private Color GetColor(WhiskasEffectColors color){
        switch(color){
            case WhiskasEffectColors.BONUS:
                return bonusColor;
            case WhiskasEffectColors.NEGATIVE:
                return negativeColor;
            case WhiskasEffectColors.REGULAR:
                return regularColor;
            default:
                return regularColor;
        }
    }
}
