using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weaponswitch : MonoBehaviour
{
    // Start is called before the first frame update
    public List<string> allOwnedWeapons = new List<string>();
    
    public GameObject pistol;
    public GameObject rifle;
    public GameObject pill; 
    void Start()
    {
        pistol = GameObject.Find("pistol").gameObject;
        rifle = GameObject.Find("rifle").gameObject;
        pill = GameObject.Find("pill").gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(allOwnedWeapons.Count == 0){
            var tempArray = GameObject.FindGameObjectsWithTag("Weapon");
            foreach(GameObject child in tempArray)
            {
               child.SetActive(false);
               child.GetComponent<WeaponAnumatorController>().switchOut = true;
            }
        }else{
            
            if(Input.GetKeyDown(KeyCode.Alpha1)){
                var tempArray = GameObject.FindGameObjectsWithTag("Weapon");
                foreach(GameObject child in tempArray)
                {
                    child.SetActive(false);
                    child.GetComponent<WeaponAnumatorController>().switchOut = true;
                }
                var actWeapon = getWeapon(allOwnedWeapons[0]);
                actWeapon.SetActive(true);
            }
            if(Input.GetKeyDown(KeyCode.Alpha2) && allOwnedWeapons.Count >1){
                var tempArray = GameObject.FindGameObjectsWithTag("Weapon");
                foreach(GameObject child in tempArray)
                {
                    child.SetActive(false);
                    child.GetComponent<WeaponAnumatorController>().switchOut = true;
                }
                var actWeapon = getWeapon(allOwnedWeapons[1]);
                actWeapon.SetActive(true);
            }
            if(Input.GetKeyDown(KeyCode.Alpha3) && allOwnedWeapons.Count > 2){
                var tempArray = GameObject.FindGameObjectsWithTag("Weapon");
                foreach(GameObject child in tempArray)
                {
                    child.SetActive(false);
                    child.GetComponent<WeaponAnumatorController>().switchOut = true;
                }
                var actWeapon = getWeapon(allOwnedWeapons[2]);
                actWeapon.SetActive(true);
            }
        }
            
        
        
    }

    

    private GameObject getWeapon(string name){
        if(name == "rifle"){
            return this.rifle;
        }else if(name == "pistol"){
            return this.pistol;
        }else if(name == "pill"){
            return this.pill;
        }
        return null;
    }
    public void addWeapon(string weapon){
        allOwnedWeapons.Add(weapon);
    }

    public void removeWeapon(string weapon){
        allOwnedWeapons.Remove(weapon);
    }
}
