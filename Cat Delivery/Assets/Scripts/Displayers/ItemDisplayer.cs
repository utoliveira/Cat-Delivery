using UnityEngine;
using UnityEngine.UI;
public class ItemDisplayer : MonoBehaviour
{
    
    [SerializeField] protected Image image;
    [SerializeField] protected Item item;

    private void Start() {
        UpdateImage();
    }
  
    public virtual void ChangeItem(Item item){
        this.item = item; 
        UpdateImage();
    }

    private void UpdateImage(){
        if(item  && image)
            image.sprite = item.artwork;
    }

    public bool isDisplaying(Item item){
        return this.item && item && this.item.name == item.name;
    }
    
}
