using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.CompareTag(Tags.MERCHANT)){
            Merchant merchant = other.gameObject.GetComponent<Merchant>();
            //MerchantManager.instance.UnRegisterMerchant(merchant);
            Destroy(other.gameObject, 1f);
        }
    }
}
