using System.Collections.Generic;
using UnityEngine;

public class Helper
{
    public static List<T> GetAllComponentsByTag<T>(string tag){
        GameObject[] costumers = GameObject.FindGameObjectsWithTag(tag);
        return new List<GameObject>(costumers).ConvertAll(gameObject => gameObject.GetComponent<T>());
    }

    public static T GetRandomized<T>(List<T> list){
        if(list == null || list.Count == 0) 
            return default(T);
        
        return list[Random.Range(0, list.Count)];
    }

    public static T GetRandomized<T>(HashSet<T> set){
        return GetRandomized<T>(new List<T>(set));
    }
    
}