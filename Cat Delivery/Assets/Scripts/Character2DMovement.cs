using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character2DMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float jumpForce = 20;
    [SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.05f;

    public UnityEvent onLandEvent;

    private bool isfacingRight = true; 

    private bool isGrounded = false;

    private void FixedUpdate() {
        bool wasPlayerGrounded = isGrounded;
        isGrounded = CheckPlayerGrounded();

        if(isGrounded && !wasPlayerGrounded)
            onLandEvent.Invoke();
    }

    private bool CheckPlayerGrounded (){
        return Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, groundLayer).Length > 0;
    } 

    public void Move(float horizontalMove, bool jump, bool moveDown) {
        Vector2 targetVelocity = new Vector2(horizontalMove * 10f, rigidBody2D.velocity.y);
        rigidBody2D.velocity = Vector2.Lerp(rigidBody2D.velocity, targetVelocity, speed * Time.deltaTime );
        
        if(jump && isGrounded){
            rigidBody2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        if(moveDown && isGrounded){
            DeactivateColliderBelow();
        }

        if(isfacingRight && horizontalMove < 0){
            Flip();
        }else if(!isfacingRight &&  horizontalMove > 0){
            Flip();
        }
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

