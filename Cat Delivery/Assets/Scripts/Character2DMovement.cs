using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2DMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float jumpForce = 20;
    [SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.05f;

    private bool isfacingRight = true; 

    public void Move(float horizontalMove, bool jump) {
        Vector2 targetVelocity = new Vector2(horizontalMove * 10f, rigidBody2D.velocity.y);
        rigidBody2D.velocity = Vector2.Lerp(rigidBody2D.velocity, targetVelocity, speed * Time.deltaTime );

        
        if(jump && isPlayerGrounded())
            rigidBody2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

    }

    private bool isPlayerGrounded(){
        return Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, groundLayer).Length > 0;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

}

