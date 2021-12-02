using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostumerHappinessHUD : MonoBehaviour
{
    [SerializeField] private Text happinessCounter;

    private int actualCount;

    private void Start() {
        UpdateText();
    }

    public void UpdateCounter(int newAmount){
        this.actualCount = newAmount;
        UpdateText();
    }

    private void UpdateText(){
        if(happinessCounter)
            happinessCounter.text = this.actualCount.ToString();
    }

}
