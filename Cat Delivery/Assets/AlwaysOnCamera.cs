using UnityEngine;

public class AlwaysOnCamera : MonoBehaviour {
    private Vector3 initialPosition;
    private RectTransform viewport;
    private Transform playerTransform;
    float viewportLimitX;
    float viewportLimitY;

    float limitDistance = 6;
    [SerializeField] float movementSpeed = 15;
    

    private void Awake() {
        this.viewport = GameObject.Find("Always On Screen Space").GetComponent<RectTransform>();
        playerTransform = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
    }
    private void Start() {
        initialPosition = this.transform.position;
        viewportLimitX = viewport.rect.width /2 * viewport.transform.lossyScale.x;
        viewportLimitY = viewport.rect.height /2 * viewport.transform.lossyScale.y; 
    }
    
    private void LateUpdate() {

        Vector2 destination = initialPosition;

        if(!IsObjectInsideViewport() && playerTransform){
            destination = GetNewPointCloserToPlayer();
            Debug.DrawLine(playerTransform.position, destination, Color.green);
        }

        this.transform.position = Vector2.Lerp(this.transform.position, destination, movementSpeed * Time.deltaTime);
    }

    private bool IsObjectInsideViewport(){
        Vector2 distanceToItem = initialPosition - viewport.position;
        
        return Mathf.Abs(distanceToItem.x) <= viewportLimitX 
                && Mathf.Abs(distanceToItem.y) <= viewportLimitY;
    }
    private void OnDrawGizmos() {
        if(!playerTransform) return;
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(playerTransform.position, limitDistance);
    }

    private Vector2 GetNewPointCloserToPlayer(){
        float distanceFromObject = Vector2.Distance(playerTransform.position, initialPosition);
        
        float coefDistance = limitDistance/distanceFromObject;

        float x = GetNewPointValueBetween(coefDistance, playerTransform.position.x, initialPosition.x);
        float y = GetNewPointValueBetween(coefDistance, playerTransform.position.y,initialPosition.y);
        return new Vector2(x, y);
    }

    private float GetNewPointValueBetween(float coefDistance, float playerPoint, float initialPoint){
        return (1 - coefDistance) * playerPoint + coefDistance * initialPoint;
    }

}
