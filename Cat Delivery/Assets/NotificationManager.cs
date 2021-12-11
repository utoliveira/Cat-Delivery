using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NotificationTime {
    LONG,
    SHORT,
    MEDIUM,
}

public class NotificationManager : MonoBehaviour
{
    [SerializeField] private GameObject notificationPrefab;
    [SerializeField] private Transform notificationSpace;

    public static NotificationManager instance;

    private void Awake() {
        if(instance){
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    public void Notify(string text, NotificationTime notificationTime = NotificationTime.MEDIUM){
        Notification notification = Instantiate(notificationPrefab, notificationSpace).GetComponent<Notification>();
        notification.Configure(text, GetTimeByNotificationTime(notificationTime));
    }

    private float GetTimeByNotificationTime(NotificationTime notificationTime){
        switch(notificationTime){
            case NotificationTime.LONG:
                return 5f;
            case NotificationTime.MEDIUM:
                return 4f;
            case NotificationTime.SHORT:
                return 3f;
            default:
                return 3f;
        }
    }
}
