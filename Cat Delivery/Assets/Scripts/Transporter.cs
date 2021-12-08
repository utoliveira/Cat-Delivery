using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transporter : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other) { 
        //TODO: 
        //Make only one boat at time, so they can never hit each other and then change this to enter,
        if(!isPlayer(other) || isBeingTransportedByAnyone(other)) return;
        other.transform.parent = this.transform;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(isBeingTransportedByMe(other)){
            other.transform.parent = null;
        }
    }

    public void RemoveAllBeingTransported() {
        this.transform.DetachChildren();
    }

    private bool isPlayer(Collider2D other){
        return other.gameObject.CompareTag(Tags.PLAYER);
    }

    private bool isBeingTransportedByAnyone(Collider2D other){
        return other.transform.parent != null && other.transform.parent.CompareTag(Tags.TRANSPORTER);
    }

    private bool isBeingTransportedByMe(Collider2D other){
        return other.transform.parent != null && other.transform.parent == this.transform;
    }
}
