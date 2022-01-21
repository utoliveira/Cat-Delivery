using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiItemDeliverHUD : MonoBehaviour
{

    List<ClickableItemDisplayer> displayers = new List<ClickableItemDisplayer>();
    [SerializeField] private GameObject displayerPrefab;
    private IItemDelivearable origin;
    private ItemStorage itemStorage; //TODO N TÁ LEGAL AQUI EIN

    private bool isOpen = false;

    public void Open(IItemDelivearable origin, ItemStorage storage){

        if(isOpen) return;

        Time.timeScale = 0.1f; //Put this with disabled game controllers
        this.origin = origin;
        this.itemStorage = storage;
        this.gameObject.SetActive(true);
        storage.GetItems().ForEach(item =>  AddNewDisplayer(item));
        isOpen = true;    
    }
    private void AddNewDisplayer(Item item){
        ClickableItemDisplayer displayer = Instantiate(displayerPrefab, this.transform)
            .GetComponent<ClickableItemDisplayer>();
        displayer.ChangeItem(item);
        displayers.Add(displayer);
    }

    public void DeliverItems(){
        List<Item> selectedItems = displayers
            .FindAll(displayer => displayer.IsSelected())
            .ConvertAll(displayer => displayer.GetItem());

        origin.ReceiveItems(selectedItems, itemStorage);
        Close();
    }

    public void Close(){
        Time.timeScale = 1f;  //Put this with disabled game controllers

        displayers.Clear();
        origin = null;
        this.gameObject.SetActive(false);

        foreach(Transform child in this.transform){
            Destroy(child.gameObject);
        }

        isOpen = false;
    }

    private void Update() {
        if(Input.GetButtonDown(PlayerConstants.CANCEL)){
            Close();
            return;
        }

        if(Input.GetButtonDown(PlayerConstants.INTERACTION_AXIS)){
            DeliverItems();
            return;
        }
    }
}
