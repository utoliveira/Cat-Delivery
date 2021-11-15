using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{

    [SerializeField] private Character2DMovement movementController;
    [SerializeField] private ItemStorage playerBag;
    [SerializeField] private float interactionRange = 0.5f;
    [SerializeField] private LayerMask interactableLayer;
    private int money = 200;

    private bool jump;
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
        if(!playerBag.IsFull() && vendor.GetGood().price <= money){
            Good good = vendor.GetGood();
            money -= good.price;
            playerBag.AddItem(good);
            vendor.OnSellingGood();
        }else{
            Debug.Log("Jogador não pode comprar");
        }
    }

    private void DeliverItem(Costumer costumer){
        Good good = (Good) costumer.GetDesiredItem();
        if(playerBag.RemoveItem(good.name)){
            costumer.OnItemDeliver();
            money += good.price * 2;
            Debug.Log("Item entregue");
            return;
        };

        Debug.Log("Jogador não tem item");
    }


    private void CheckMovement() {
        horizontalMove = Input.GetAxisRaw(PlayerConstants.HORIZONTAL_AXIS);
        //TODO: Change to Horizontal UP
        if(Input.GetButtonDown(PlayerConstants.JUMP_AXIS)) 
            jump = true;
    }

    private void ApplyMovement() {
        movementController.Move(horizontalMove, jump);
        jump = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag(Tags.WATER)){
            Destroy(this.gameObject);
        }
    }
    
    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
