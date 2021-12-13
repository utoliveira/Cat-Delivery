using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item Maker Config", menuName ="Config/Item Maker")]
public class ItemMakerConfig : ScriptableObject
{
    public List<Item> requiredItems;
    public Item result;
    public int serviceValue;
    public GameObject colorableDisplayerPrefab;
}
