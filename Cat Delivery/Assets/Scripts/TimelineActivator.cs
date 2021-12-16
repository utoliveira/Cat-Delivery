using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineActivator : MonoBehaviour {
    [SerializeField] private PlayableDirector director;

    private void OnTriggerEnter2D(Collider2D other) {    
        director.Play();
    }

}
