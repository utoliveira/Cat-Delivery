using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TextNotification : Notification
{
    [SerializeField] Text text;

    public void ShowText(string newText) {
        text.text = "";
        text.DOText(newText, 1f);
    }

    public void HideText(){
        text.DOFade(0, 0.2f);
    }

    private void OnDestroy() {
        DOTween.Kill(this);
    }
}
