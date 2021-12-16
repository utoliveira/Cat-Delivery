using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MarkerTimelineActivator : MonoBehaviour
{
    public List<FloorMarker> markers;
    public PlayableDirector director;

    private void Start() {
        if(markers == null) return;
        markers.ForEach(marker => marker.SetTimelineActivator(this));
    }


    public void UnregisterMarker(FloorMarker marker){
        if(markers == null) return;

        markers.Remove(marker);
        AudioManager.instance.Play(AudioCode.ITEM_DELIVERING);

        if(markers.Count < 1){
            director.Play();
            Destroy(this);
        }
    }
    
}
