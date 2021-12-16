using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMarker : MonoBehaviour
{
    private MarkerTimelineActivator timelineActivator;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag(Tags.PLAYER)){
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy() {
        timelineActivator.UnregisterMarker(this);
    }
    
    public void SetTimelineActivator(MarkerTimelineActivator activator){
        this.timelineActivator = activator; 
    }
}
