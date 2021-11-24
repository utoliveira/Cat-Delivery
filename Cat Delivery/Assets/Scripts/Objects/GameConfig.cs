using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Game Config", menuName ="Config/Game Config")]
public class GameConfig : ScriptableObject {

    [Header("Merchant")]
    public List<GameObject> availableMerchants;

    [Header("Goods")]
    public List<Good> availableGoods;

    [Header("Costumers")]
    public List<GameObject> availableCostumers;

    public int initialWhiskas = 200;

}
