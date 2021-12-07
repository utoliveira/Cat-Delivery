using UnityEngine;

public class AlwaysOnCamera : MonoBehaviour {
    private Vector3 initialPosition;
    private RectTransform viewport;
    float limitX;
    float limitY;

    private void Awake() {
        this.viewport = GameObject.Find("Always On Screen Space").GetComponent<RectTransform>();
    }
    private void Start() {
        initialPosition = this.transform.position;
        limitX = viewport.rect.width /2 * viewport.transform.lossyScale.x;
        limitY = viewport.rect.height /2 * viewport.transform.lossyScale.y; 
    }
    
    private void LateUpdate() {
        Vector2 distanceToItem = initialPosition - viewport.position;

        Vector2 destination = initialPosition;
        
        if(Mathf.Abs(distanceToItem.x) > limitX)
            destination.x = distanceToItem.x > 0 ? viewport.position.x + limitX : viewport.position.x -limitX;
             
        if(Mathf.Abs(distanceToItem.y) > limitY)
            destination.y = distanceToItem.y > 0 ? viewport.position.y + limitY: viewport.position.y -limitY;
        

        //Change to two parts: inside circle and the canvas limit
        Debug.DrawLine(viewport.position, new Vector2(viewport.position.x + limitX, viewport.position.y + limitY), Color.yellow);
        Debug.DrawLine(viewport.position, destination, Color.green);
        this.transform.position = Vector2.Lerp(this.transform.position, destination, 15 * Time.deltaTime);
    }
    private void OnDrawGizmos() {
        if(!viewport) return;
        Gizmos.color = Color.magenta;
    }

}
