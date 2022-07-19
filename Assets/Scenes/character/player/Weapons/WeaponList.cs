using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponList : MonoBehaviour
{
    // Start is called before the first frame update
    public List<string> allOwnedWeapons = new List<string>();
    public void addKey(string weapon){
        allOwnedWeapons.Add(weapon);
    }

    public void removeKey(string weapon){
        allOwnedWeapons.Remove(weapon);
    }
}
