using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private Text WhiskasDisplay;

    public void UpdateWhiskas(int whiskas){
        WhiskasDisplay.text = whiskas.ToString();
    }

}
