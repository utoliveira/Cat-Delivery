using UnityEngine;
using DG.Tweening;
public class Notification : MonoBehaviour
{

    [SerializeField] private CanvasGroup canvasGroup;

    private void Start() {
        this.canvasGroup.alpha = 0;
    }

    private void FadeCanvas(float finalValue, float duration){
        DOTween.To(
            () => this.canvasGroup.alpha,
            x => this.canvasGroup.alpha = x, 
            finalValue, duration);
    }
    public void Appear(){
        this.transform.DOShakeScale(0.5f, 0.08f, 0);
        FadeCanvas(1, 0.4f);
    }

    public void Disappear(bool destroyAfter = false){
        float timeToDisappear = 0.3f;
        
        FadeCanvas(0, timeToDisappear - 0.1f);
        this.transform
            .DOShakeScale(timeToDisappear, 0.05f, 0)
            .OnComplete(() => this.gameObject.SetActive(false));  

        if(destroyAfter){
            Destroy(this.gameObject, timeToDisappear + 1f);
        }
    }

    private void OnEnable() {
        Appear();
    }

    private void OnDisable() {
        this.canvasGroup.alpha = 0;
    }

    private void OnDestroy() {
        this.transform.DOKill();
        this.canvasGroup.DOKill();
    }
}
