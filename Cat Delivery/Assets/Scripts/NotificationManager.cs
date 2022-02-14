using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NotificationManager : MonoBehaviour
{
    TextNotification notificationComponent;
    [SerializeField] GameObject componentPrefab;
    Queue<string> notificationInfo;
    bool isNotificating;

    private void Update() {
        if(Input.GetButtonDown(PlayerConstants.INTERACTION_AXIS)){
            Next();
        };
    }

    public void Notificate(NotificationConfig config) {
        
        if(componentPrefab == null) {
            Debug.Log("Notification Manager without prefab");
            return;
        };
        
        this.notificationInfo = new Queue<string>(config.infos);
        notificationComponent = Instantiate(componentPrefab, this.transform)
            .GetComponent<TextNotification>();
            
        Next();
        OnStartNotificating();
    }


    public void Next(){
        if(notificationInfo == null) return;

        if(notificationInfo.Count > 0) {
            notificationComponent.ShowText(notificationInfo.Dequeue());
            return;
        }

        OnFinishNotification();
    }

    private void OnFinishNotification() {
        notificationComponent.Disappear(true);
        LevelManager.instance.StartManager();
        isNotificating = false;
    }

    
    private void OnStartNotificating() {
        LevelManager.instance.StopManager();
        isNotificating = true;
        
    }
}
