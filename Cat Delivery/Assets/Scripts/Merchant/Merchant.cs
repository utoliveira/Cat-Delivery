using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private float speed;
    [SerializeField] private List<Vendor> vendors;
    private void FixedUpdate() {
        //Vector2 target = new Vector2(speed * Time.deltaTime, rigidBody2D.velocity.y);
        //rigidBody2D.velocity = Vector2.MoveTowards(rigidBody2D.velocity, target, speed);    
        rigidBody2D.velocity = new Vector2(speed * Time.deltaTime, rigidBody2D.velocity.y);    
    }

    public void SetSpeed(float speed){
        this.speed = speed;
    }

    public void ConfigureVendors(List<Good> goods){

        if(goods == null || goods.Count < 1) return;

        List<Good> availableGoods = getAvailableGoods(goods);

        vendors.ForEach( vendor => {
            //What if the amount of goods are lower than the vendors?
            Good good = availableGoods[Random.Range(0, availableGoods.Count)];
            availableGoods.Remove(good);
            vendor.setGood(good);
        });

    }

    private List<Good> getAvailableGoods(List<Good> goods){
        if(goods.Count < vendors.Count){
            throw new System.NotImplementedException();
        }
        return new List<Good>(goods);
    }

    public int getVendorsCount(){
        return vendors != null ? vendors.Count: 0;
    }
}