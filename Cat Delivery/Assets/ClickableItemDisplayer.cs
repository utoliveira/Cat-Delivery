using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ClickableItemDisplayer : ItemDisplayer
{
    private bool isSelected = false;
    [SerializeField] private Image panel;

    private Vector3 initialScale;
    private Vector3 selectedScale;

    [SerializeField] private Color initialColor;
    [SerializeField] private Color selectedColor;

    private void Start() {
        initialScale = transform.localScale;
        selectedScale = initialScale * 1.1f;
        panel.color = initialColor;
    }

    public bool IsSelected() {
        return this.isSelected;
    }

    public void OnClick(){
        isSelected = !isSelected;

        if(isSelected){
            DoScaleAnimation(selectedScale);
            ChangeColor(selectedColor);
        }else{
            DoScaleAnimation(initialScale);
            ChangeColor(initialColor);
        }
    }

    private void DoScaleAnimation(Vector3 finalScale){
        this.transform
            .DOScale(finalScale, 0.5f)
            .SetEase(Ease.OutBounce)
            .SetUpdate(true);
    
    }

    private void ChangeColor(Color color){
        panel.DOColor(color, 0.5f).SetUpdate(true);
    }

    public Item GetItem(){
        return this.item;
    }

    private void OnDestroy() {
        transform.DOKill();
        panel.DOKill();
    }

}
