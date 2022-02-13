using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Notification : MonoBehaviour
{

    [SerializeField] private CanvasGroup canvasGroup;

    private void Start() {
        this.canvasGroup.alpha = 0;
    }
    public void Appear(){
        this.transform.DOShakeScale(0.5f, 0.08f, 0);
        this.canvasGroup.DOFade(1, 0.4f);
    }

    public void Disappear(){
        this.transform.DOShakeScale(0.3f, 0.05f, 0);
        this.canvasGroup.DOFade(0,0.2f);
    }

    private void OnEnable() {
        Appear();
    }

    private void OnDisable() {
        this.canvasGroup.alpha = 0;
    }

    private void OnDestroy() {
        this.DOKill();
    }
}
