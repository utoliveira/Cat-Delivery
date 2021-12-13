using System.Collections;
using UnityEngine;

public class DashMove : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private float previousDirection;
    private float dashDirection;
    [SerializeField] private float dashCooldown;
    [SerializeField] private float dashForce;
    
    private bool isAbleToDash = true;

    private float timeBetweenInput = 0.17f;
    private float timeFirstInput;


    private void Awake() {
        this.rigidBody = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate() {
        if(dashDirection != 0){
            ApplyDash();
        }
    }

    private void Update() {
        if( isAbleToDash && Input.GetButtonDown(PlayerConstants.HORIZONTAL_AXIS)){
            
            float direction = Input.GetAxisRaw(PlayerConstants.HORIZONTAL_AXIS);

            if(direction == previousDirection && inTimeToDash()){
                dashDirection =  direction;
                return;
            }
            timeFirstInput = Time.time;
            previousDirection = direction;
        }
    }

    private bool inTimeToDash(){
        return Time.time - timeFirstInput <= timeBetweenInput;
    }

    private void ApplyDash() {
        //rigidBody.AddForce(new Vector2(dashDirection * dashForce, 0), ForceMode2D.Impulse);
        rigidBody.velocity = new Vector2(dashDirection * dashForce, 0);
        StartCoroutine(ApplyCooldown());
        dashDirection = 0;
        previousDirection = 0;
        AudioManager.instance.Play(AudioCode.PLAYER_DASH);
    }

    private IEnumerator ApplyCooldown(){
        isAbleToDash = false;
        yield return new WaitForSeconds(dashCooldown);
        isAbleToDash = true;
    }
    
}
