using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] private int whiskas = 5;
    private void Awake() {
        if(instance){
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    private void Start() {
        HUDManager.instance.UpdateWhiskas(whiskas);
    }
    public void AddWhiskas(int whiskas){
        this.whiskas += Mathf.Abs(whiskas);
        HUDManager.instance.UpdateWhiskas(this.whiskas);
    }

    public void RemoveWhiskas(int whiskas){
        if(whiskas <= this.whiskas){
            this.whiskas -= whiskas;
            HUDManager.instance.UpdateWhiskas(this.whiskas);
        } 
    }

    public int GetWhiskas(){
        return whiskas;
    }



    //Spawn new building

    // New Desired item
    //  Spawn new building or get an boat that is existed -> 

    //Event -> OnFinishedDeliver => add free costumer to a list 
}
