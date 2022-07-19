using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyContainer : MonoBehaviour
{

    public List<string> allOwnedKeys = new List<string>();
    
 
    public bool ifOwnedKey(string key){
        if(allOwnedKeys.Contains(key)){
            return true;
        }
        return false;
    }

    public void addKey(string key){
        allOwnedKeys.Add(key);
    }

    public void removeKey(string key){
        allOwnedKeys.Remove(key);
    }
}
