using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new Handable", menuName= "Handables/Handable")]
public class Item : ScriptableObject
{
    public string code;
    public new string name;
    public Sprite artwork;
    public float weight;

    public float desireTime = 8f;

    public override bool Equals(object obj){
        if (obj == null || GetType() != obj.GetType())
            return false;
        
        return ((Item)obj).code == this.code;
    }
    
    public override int GetHashCode(){
        return base.GetHashCode();
    }
}
