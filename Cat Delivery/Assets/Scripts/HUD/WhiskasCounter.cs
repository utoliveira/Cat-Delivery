using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhiskasCounter : MonoBehaviour
{
    [SerializeField] private Text currentWhiskas;
    [SerializeField] private Text limitWhiskas;

    private void Start() {
        UpdateCurrentWhiskas(0);
        UpdateLimit(0);  
    }

    public void UpdateLimit(int limit){
        limitWhiskas.text = "/ " + limit.ToString();
    }

    public void UpdateCurrentWhiskas(int whiskas){
        currentWhiskas.text = whiskas.ToString();
    }

};
