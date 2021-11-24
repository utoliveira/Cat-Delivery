using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{

    [SerializeField] private Character2DMovement movementController;
    [SerializeField] private ItemStorage playerBag;
    [SerializeField] private float interactionRange = 0.5f;
    [SerializeField] private LayerMask interactableLayer;
    
    private bool jump;
    private bool moveDown;
    private float horizontalMove;
    private void Update() {
        CheckInteraction();
        CheckMovement();
    }

    private void FixedUpdate() {
        ApplyMovement();
    }
    
    private void CheckInteraction() {
        if(Input.GetButtonDown(PlayerConstants.INTERACTION_AXIS)) {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactionRange, interactableLayer);

            foreach(Collider2D collider in colliders){                
                switch(collider.gameObject.tag){
                    case Tags.VENDOR:
                        BuyItem(collider.gameObject.GetComponent<Vendor>());
                        break;
                    case Tags.COSTUMER:
                        DeliverItem(collider.gameObject.GetComponent<Costumer>());
                        break;
                    default:
                        Debug.Log("Jogador confuso");
                        break;
                }
            }
        }
    }

    private void BuyItem(Vendor vendor){
        if(vendor.GetGood().price <= LevelManager.instance.GetWhiskas()){
            Good good = vendor.GetGood();
            LevelManager.instance.RemoveWhiskas(good.price);
            playerBag.AddItem(good);
            vendor.OnSellingGood();
        }else{
            Debug.Log("Jogador não pode comprar");
        }
    }

    private void DeliverItem(Costumer costumer){

        if(playerBag.isEmpty()) return;
        
        Good costumerGood = (Good) costumer.GetDesiredItem();

        if(!costumerGood) return;
        
        if(playerBag.RemoveItem(costumerGood.name)){
            costumer.RemoveDesiredItem();
            LevelManager.instance.AddWhiskas(costumerGood.price * 2);
            Debug.Log("Item entregue");
            return;
        };

        Debug.Log("Jogador não tem item");
    }


    private void CheckMovement() {
        horizontalMove = Input.GetAxisRaw(PlayerConstants.HORIZONTAL_AXIS);

        if(Input.GetButtonDown(PlayerConstants.UP_AXIS)) 
            jump = true;
        
        if(Input.GetButtonDown(PlayerConstants.DOWN_AXIS))
            moveDown = true;
    }

    private void ApplyMovement() {
        movementController.Move(horizontalMove, jump, moveDown);
        jump = false;
        moveDown = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag(Tags.WATER)){
            Destroy(this.gameObject);
            LevelManager.instance.GameOver();
        }
    }
    
    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
