using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalPlatform : MonoBehaviour {

    [SerializeField] private Collider2D platformCollider;

    private void Awake() {
        platformCollider = this.GetComponent<Collider2D>();
    }

    public void DisableCollider() {
        StartCoroutine(PlayerFall());
    }

    private IEnumerator PlayerFall(){
        platformCollider.enabled = false;
        yield return new WaitForSeconds(0.5f);
        platformCollider.enabled = true;
    }
}
