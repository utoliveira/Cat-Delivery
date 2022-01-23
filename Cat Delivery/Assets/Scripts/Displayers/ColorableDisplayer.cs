using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ColorableDisplayer : ItemDisplayer
{
    [SerializeField] private Color initialColor;
    [SerializeField] private Image panel;

    private void Start() {
        ChangeColor(initialColor);
    }

    public override void ChangeItem(Item item){
        base.ChangeItem(item);
    }

    public void ResetColor(){
        ChangeColor(initialColor);
    }

    public void ChangeColor(Color color){
        if(panel != null) panel.color = color;
    }
    
    public void ChangeColor(Color color, float transitionTime){
        if(panel != null) panel.DOColor(color, transitionTime);
    }

    private void OnDestroy() {
        panel.DOKill();
    }

}
