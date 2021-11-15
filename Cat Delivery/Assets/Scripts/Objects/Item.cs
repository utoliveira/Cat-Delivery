using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new Handable", menuName= "Handables/Handable")]
public class Item : ScriptableObject
{
    public new string name;
    public Sprite artwork;
    public float weight;
}
