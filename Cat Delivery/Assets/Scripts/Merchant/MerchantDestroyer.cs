using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantDestroyer : MonoBehaviour
{
    [SerializeField] private MerchantSpawner merchantSpawner;
    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.gameObject.CompareTag(Tags.MERCHANT)){
            Merchant merchant = other.gameObject.GetComponent<Merchant>();
            merchantSpawner.UnRegisterMerchant(merchant);
            Destroy(other.gameObject, 1f);
        }
    }
}
