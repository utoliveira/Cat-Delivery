using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NotificationManager : MonoBehaviour
{
    [SerializeField] TextNotification notificationComponent;
    [SerializeField] GameObject componentPrefab;
    Queue<string> notificationInfo;
    [SerializeField] UnityEvent onFinishNotificating;
    
    bool isNotificating;

    private void Update() {
        if(isNotificating && Input.GetButtonDown(PlayerConstants.INTERACTION_AXIS)){
            Next();
        };
    }

    public void Notificate(NotificationConfig config) {
        
        if(componentPrefab == null) {
            Debug.Log("Notification Manager without prefab");
            return;
        };

        if(notificationComponent == null){
            notificationComponent = Instantiate(componentPrefab, this.transform)
                .GetComponent<TextNotification>();
        }
        
        this.notificationInfo = new Queue<string>(config.infos);
        isNotificating = true;
        Next();
        
    }


    public void Next(){
        if(notificationInfo == null) return;

        if(notificationInfo.Count > 0) {
            notificationComponent.ShowText(notificationInfo.Dequeue());
            return;
        }

        notificationComponent.Disappear(true);
        isNotificating = false;
        if(onFinishNotificating != null)
            onFinishNotificating.Invoke();
    }

}
