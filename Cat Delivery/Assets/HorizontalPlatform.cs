using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalPlatform : MonoBehaviour {

    [SerializeField] private Collider2D collider;

    //TODO: melhorar aqui pelamor

    private void Update() {
        if(Input.GetButtonDown(PlayerConstants.DOWN_AXIS)){
            StartCoroutine(PlayerFall());
        }
    }

    private IEnumerator PlayerFall(){
        collider.enabled = false;
        yield return new WaitForSeconds(0.5f);
        collider.enabled = true;
    }
}
