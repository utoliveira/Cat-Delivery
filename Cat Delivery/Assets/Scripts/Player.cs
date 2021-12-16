using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{

    [SerializeField] private Character2DMovement movementController;
    
    [SerializeField] private Animator animator;
    [SerializeField] private ItemStorage playerBag;
    [SerializeField] private float interactionRange = 0.5f;
    [SerializeField] private LayerMask interactableLayer;

    private bool jump;
    private bool moveDown;
    private float horizontalMove;
    private bool inputEnable = true;
    private void Update() {
        if(!inputEnable) return;
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
                    case Tags.COSTUMER: //CHANGE IT HERE TO logic be on Costumer's side
                        DeliverItem(collider.gameObject.GetComponent<Costumer>());
                        break;
                    case Tags.ITEM_MAKER:
                        InteractWithItemMaker(collider.gameObject.GetComponent<ItemMaker>());
                        break;
                    default:
                        Debug.Log("Jogador confuso");
                        break;
                }
            }
        }
    }

    private void BuyItem(Vendor vendor){
        if(vendor.GetGood().basePrice <= LevelManager.instance.GetWhiskas()){
            Good good = vendor.GetGood();
            LevelManager.instance.RemoveWhiskas(good.basePrice);
            playerBag.AddItem(good);
            vendor.OnSellingGood();
        }else{
            Debug.Log("Jogador não pode comprar");
        }
    }

    private void DeliverItem(Costumer costumer){

        if(playerBag.isEmpty()) return;
        
        Good costumerGood = (Good) costumer.GetDesiredGood();

        if(!costumerGood) return;
        
        if(playerBag.RemoveItem(costumerGood)){
            costumer.OnDesiredItemDeliver();
            return;
        };

        Debug.Log("Jogador não tem item");
    }

    private void InteractWithItemMaker(ItemMaker itemMaker){

        if(itemMaker.IsItemReady()){
            playerBag.AddItem(itemMaker.DeliverItem());
            return;
        }

        itemMaker.ReceiveItems(playerBag);
    }

    private void CheckMovement() {
        horizontalMove = Input.GetAxisRaw(PlayerConstants.HORIZONTAL_AXIS);
        animator.SetFloat(PlayerConstants.ANIM_SPEED_PARAM, Mathf.Abs(horizontalMove));

        if(Input.GetButtonDown(PlayerConstants.UP_AXIS)){
            jump = true;
            animator.SetBool(PlayerConstants.ANIM_JUMP_PARAM, true);
        }
        
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
            AudioManager.instance.Play(AudioCode.PLAYER_FALLS);
        }
    }
    
    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }

    public void OnLandEvent(){
        animator.SetBool(PlayerConstants.ANIM_JUMP_PARAM, false);
    }

    public void SetEnableInput(bool enable){
        inputEnable = enable;
    }
}
