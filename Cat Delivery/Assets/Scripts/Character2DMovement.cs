using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character2DMovement : MonoBehaviour
{
    private Vector2 velocity = Vector2.zero;
    [SerializeField] private float velocity_smoothing = 0.5f;
    [SerializeField] private float speed = 20f;
    [SerializeField] private float jumpForce = 20;
    [SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.05f;

    public UnityEvent onLandEvent;

    private bool isfacingRight = true; 

    private bool isGrounded = false;
    private bool jumpedTwice = false;

    private bool canJumpInTheAir = true; 


    private void FixedUpdate() {
        bool wasPlayerGrounded = isGrounded;
        isGrounded = CheckPlayerGrounded();

        if(isGrounded && !wasPlayerGrounded){
            onLandEvent.Invoke();
            jumpedTwice = false;
        }
    }

    private bool CheckPlayerGrounded (){
        return Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, groundLayer).Length > 0;
    } 

    public void Move(float horizontalMove, bool jump, bool moveDown) {
        Vector2 targetVelocity = new Vector2(horizontalMove * 10f * speed * Time.deltaTime, rigidBody2D.velocity.y);
        rigidBody2D.velocity = Vector2.SmoothDamp(rigidBody2D.velocity, targetVelocity, ref velocity, velocity_smoothing );
        
        if(jump && isGrounded){
            Jump();

        }
    
        if(moveDown && isGrounded)
            DeactivateColliderBelow();
        
        if(jump && isAbleToJumpInTheAir()){
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, 0);
            Jump();
            jumpedTwice = true;
        }

        if(isfacingRight && horizontalMove < 0)
            Flip();

        else if(!isfacingRight &&  horizontalMove > 0)
            Flip();
        
    }

    private void Jump(){
        rigidBody2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        AudioManager.instance.Play(AudioCode.PLAYER_JUMP);
    }

    private bool isAbleToJumpInTheAir(){
        return canJumpInTheAir && !isGrounded && !jumpedTwice;
    }

    private void DeactivateColliderBelow(){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, groundLayer);
        foreach(Collider2D collider in colliders){
            HorizontalPlatform horizontalPlatform = collider.GetComponent<HorizontalPlatform>();
            if(horizontalPlatform) horizontalPlatform.DisableCollider();
        }
    }

    private void Flip(){
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        isfacingRight = !isfacingRight;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

}

