using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorableDisplayer : ItemDisplayer
{
    [SerializeField] private Color initialColor;
    [SerializeField] private Color finalColor;
    [SerializeField] private Image panel;
    private bool isFinalColor;

    private void Start() {
        ChangePanelColor(initialColor);
    }

    public override void ChangeItem(Item item){
        base.ChangeItem(item);
        ChangePanelColor(initialColor);
        isFinalColor = false;
    }

    public void ChangeToFinalColor(){
        ChangePanelColor(finalColor);
        isFinalColor = true;
    }
    public void ResetColor(){
        ChangePanelColor(initialColor);
        isFinalColor = false;
    }

    private void ChangePanelColor(Color color){
        if(panel != null) panel.color = color;
    }

    public bool IsFinalColor(){
        return isFinalColor;
    }


    
}
