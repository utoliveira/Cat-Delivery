using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GradientDisplayer : ItemDisplayer{
    private float timeSpent = 0f;
    private float timeToChangeColor =8f;
    [SerializeField] private Gradient color;
    [SerializeField] private Image panel; 
    private bool applyGradient;

    private Coroutine current;

    private void Start() {
        InitializeDisplay();
    }

    public override void ChangeItem(Item item){
        base.ChangeItem(item);
        InitializeDisplay();
    }

    public void ChangeItem(Good good, bool applyGradient = true){
        ChangeItem((Item)good);
        timeToChangeColor = good.desireTime;
        this.applyGradient = applyGradient;
    }

    public void CleanDisplay(){
        StopCurrentCoroutine();
        base.image.enabled = false;
        panel.enabled = false;
    }

    public void InitializeDisplay(){
        if(base.item){
            base.image.enabled = true;
            panel.enabled = true;
            if(applyGradient)
                current = StartCoroutine(ChangeColorOverTime());
            return;
        }
        CleanDisplay();
    }

    private void StopCurrentCoroutine(){
        if(current != null){
            StopCoroutine(current);
            current = null;
        }
    }

    private IEnumerator ChangeColorOverTime(){
        timeSpent = 0;
        while(timeSpent < timeToChangeColor){{
            this.timeSpent += Time.deltaTime;
            panel.color = color.Evaluate(1 - timeSpent/timeToChangeColor);
            yield return null; //TODO colocar alguns segundos aqui só pra n ficar fazendo o tempo todo?
        }}
    }


}
