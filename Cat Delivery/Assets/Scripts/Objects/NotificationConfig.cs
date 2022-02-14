using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="Config/Notification Config", fileName ="New Notification Config")]
public class NotificationConfig : ScriptableObject
{
    public bool nextOnLoad;
    [TextArea]
    public string[] infos;

}
