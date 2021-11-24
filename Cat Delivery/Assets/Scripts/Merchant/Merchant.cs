using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour
{
    //[SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private float speed;
    [SerializeField] private List<Vendor> vendors;

    private Vector3 originPoint;
    private Vector3 destinationPoint;
    private void FixedUpdate() {
        //Vector2 target = new Vector2(speed * Time.deltaTime, rigidBody2D.velocity.y);
        //rigidBody2D.velocity = Vector2.MoveTowards(rigidBody2D.velocity, destinationPoint, speed * Time.deltaTime);    
        //rigidBody2D.velocity = new Vector2(speed * Time.deltaTime, rigidBody2D.velocity.y);    
        this.transform.position = new Vector3(speed * Time.deltaTime, 0,0 ) + this.transform.position;
    }

    public void SetSpeed(float speed){
        this.speed = speed;
    }
    
    public void ConfigureTrajectory(TrajectoryInitialPoint trajectoryInitial, float speed){
        originPoint = this.transform.position;
        destinationPoint = trajectoryInitial.finalPoint.position;
        
        if(originPoint.x > destinationPoint.x){
            this.transform.localScale = new Vector2(-1f, 1f);
            SetSpeed(-speed);
        }else{
           SetSpeed(speed);
        }
    }

    public float GetTraveledDistanceInPercent(){
        if(originPoint == null || destinationPoint == null) return 0;
        
        float distance = Vector3.Distance(originPoint, destinationPoint);
        float traveledDistance = Vector3.Distance(this.transform.position, destinationPoint);
        
        return 1 - (traveledDistance / distance);
    }

    public void ConfigureVendors(List<Good> goods){

        if(goods == null || goods.Count < 1) return;

        List<Good> availableGoods = new List<Good>(goods);
    
        vendors.ForEach( vendor => {
            //What if the amount of goods are lower than the vendors?
            Good good = Helper.GetRandomized<Good>(availableGoods);
            availableGoods.Remove(good);
            vendor.setGood(good);
        });
    }

    public int getVendorsCount(){
        return vendors != null ? vendors.Count: 0;
    }

    public List<Good> getGoods() {
        return vendors.ConvertAll(vendor => vendor.GetGood());
    }
}
