using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New GameConfig", menuName ="Config/GameConfig")]
public class GameConfig : ScriptableObject
{
    public int initialWhiskasCount;
    public List<Good> availableGoods;
    public List<GameObject> availableMerchants;

    //Available buildings

    //Merchant Spawn config
    //Building Spawn config

}
