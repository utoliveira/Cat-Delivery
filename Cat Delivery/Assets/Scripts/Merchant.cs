using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigidBody2D;
    [SerializeField] float speed;
    private void FixedUpdate() {
        Vector2 target = new Vector2(speed * Time.deltaTime, rigidBody2D.velocity.y);
        rigidBody2D.velocity = Vector2.MoveTowards(rigidBody2D.velocity, target, speed); 
        
    }
}
