using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatCookiesCounter : MonoBehaviour
{
    //TODO: Make it reduce cookies too, now it only allows to add more;
    [SerializeField] private List<Image> cookies = new List<Image>();
    [SerializeField] private Canvas canvas;
    [SerializeField] private Color enabledColor;
    [SerializeField] private Color disabledColor;

    private void Start() {
        foreach(Image cookie in cookies){
            cookie.color = disabledColor;
        }
        canvas.enabled = false;
    }

    public void UpdateCounter(int cookiesAmount){
        if(cookiesAmount < 1){
            canvas.enabled = false;
            return;
        }

        canvas.enabled = true;

        for(int count = 0; count < cookies.Count && count < cookiesAmount; count++){
            cookies[count].color = Color.white;
        }
    }
}
