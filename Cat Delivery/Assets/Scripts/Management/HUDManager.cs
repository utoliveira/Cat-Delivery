using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;
    [SerializeField] private Text WhiskasDisplay;

    private void Awake() {
        if(instance){
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }


    public void UpdateWhiskas(int whiskas){
        WhiskasDisplay.text = whiskas.ToString();
    }

}