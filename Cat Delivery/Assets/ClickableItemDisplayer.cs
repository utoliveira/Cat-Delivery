using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickableItemDisplayer : ItemDisplayer
{
    private bool isSelected = false;
    [SerializeField] private Image panel;

    public bool IsSelected() {
        return this.isSelected;
    }

    public void OnClick(){
        isSelected = !isSelected;
        panel.color = isSelected ? Color.yellow : Color.white;
    }

    public Item GetItem(){
        return this.item;
    }

}
