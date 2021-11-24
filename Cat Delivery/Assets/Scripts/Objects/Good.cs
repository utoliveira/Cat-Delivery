using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new Good", menuName= "Handables/Goods")]
public class Good : Item{
   public int basePrice;

   [Range(1, 8)]
   public int speed;
}
