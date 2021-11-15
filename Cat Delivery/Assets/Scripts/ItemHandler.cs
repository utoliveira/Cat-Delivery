using UnityEngine;

public class ItemHandler : MonoBehaviour {

    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Item item;

    private void Start() {
        spriteRenderer.sprite = item.artwork;
    } 
    

}
