using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Animator animator;

    private string ANIMATOR_DISAPPEAR_TRIGGER = "Disappear";


    public void Configure(string text, float timeToDisappear){
        SetText(text);
        StartCoroutine(Disappear(timeToDisappear));
    }

    public void SetText(string text){
        this.text.text = text;
    }

    private IEnumerator Disappear(float timeToDisappear){
        yield return new WaitForSeconds(timeToDisappear);
        animator.SetTrigger(ANIMATOR_DISAPPEAR_TRIGGER);
        Destroy(this.gameObject, 0.5f);
    }
    

}
