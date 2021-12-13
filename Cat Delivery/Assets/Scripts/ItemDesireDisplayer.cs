using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDesireDisplayer : ItemDisplayer{
    private float timeSpent = 0f;
    private float requiredTime = 8f;
    [SerializeField] private Gradient color;
    [SerializeField] private Image panel; 

    private Coroutine current;

    private void Start() {
        InitializeDisplay();
    }

    public override void ChangeItem(Item item){
        base.ChangeItem(item);
        InitializeDisplay();
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
        while(timeSpent < requiredTime){{
            this.timeSpent += Time.deltaTime;
            panel.color = color.Evaluate(1 - timeSpent/requiredTime);
            yield return null; //TODO colocar alguns segundos aqui só pra n ficar fazendo o tempo todo?
        }}
    }


}
