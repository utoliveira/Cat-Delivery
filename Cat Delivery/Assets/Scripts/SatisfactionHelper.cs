using System;
public static class SatisFactionHelper {

    public static int GetPaymentValue(SatisfactionEnum satisfaction, Good good){
        int goodValue =  good.basePrice;
       
        switch(satisfaction){
            case SatisfactionEnum.SUPER_HAPPY:
                return goodValue * 3;
            
            case SatisfactionEnum.HAPPY:
                return goodValue * 2;
            
            default:
                return goodValue;
        }
    }

    public static SatisfactionEnum GetDeliverSatisfaction(int timeDelivered, int timeSettled, float timeToExpire){
        float percentOfTime = 1 - (timeDelivered - timeSettled) / timeToExpire;
        if(percentOfTime > 0.8f )
            return SatisfactionEnum.SUPER_HAPPY;
        
        if(percentOfTime > 0.35f)
            return SatisfactionEnum.HAPPY;

        return SatisfactionEnum.REGULAR;
    }

}