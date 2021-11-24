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

    
    private void setDestination(TrajectoryInitialPoint trajectoryInitial){
        originPoint = this.transform.position;
        destinationPoint = trajectoryInitial.finalPoint.position;
    }

    public void Configure(TrajectoryInitialPoint trajectoryInitial, List<Good> availableGoods){
        setDestination(trajectoryInitial);
        ConfigureVendors(availableGoods);
        ConfigureMovement();
    }

    public void ConfigureMovement(){

        float speed = GetMerchantSpeed();

        if(originPoint.x > destinationPoint.x){
            this.transform.localScale = new Vector2(-1f, 1f);
            this.speed = -speed;
        }else{
           this.speed = speed;
        }
    }

    private float GetAverageSpeedVendorsBased () {
        
        List<Vendor> validVendors = vendors
            .FindAll( vendor => vendor.GetGood() != null && vendor.GetGood().speed > 0);
        
        if(validVendors.Count < 1) 
            return 0;

        float averageSpeed = 0;
        validVendors.ForEach(vendor => averageSpeed += vendor.GetGood().speed);
        return averageSpeed / validVendors.Count;
    }

    private float GetMerchantSpeed() {
        if(vendors == null) 
            return -10; //The object will travel in the opposite direction and destroy itself
            
        float averageSpeed = GetAverageSpeedVendorsBased();

        return averageSpeed  * LevelManager.instance.GetDifficulty().velocityFactor;
    }

    public float GetTraveledDistanceInPercent(){
        if(originPoint == null || destinationPoint == null) return 0;

        float distance = Vector3.Distance(originPoint, destinationPoint);
        float traveledDistance = Vector3.Distance(this.transform.position, destinationPoint);
        
        return 1 - (traveledDistance / distance);
    }

    private void ConfigureVendors(List<Good> goods){

        if(goods == null || goods.Count < 1) return;

        List<Good> availableGoods = new List<Good>(goods);
        
        vendors.ForEach( vendor => {
            //What if the amount of goods are lower than the vendors?
            Good good = Helper.GetRandomized<Good>(availableGoods);
            availableGoods.Remove(good);
            vendor.SetGood(good);
        });
    }

    public List<Good> GetGoods() {
        return vendors.FindAll(vendor => vendor.GetGood() != null ).ConvertAll(vendor => vendor.GetGood());
    }
}
