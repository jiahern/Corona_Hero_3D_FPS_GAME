using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blankWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision){

        if(collision.gameObject.name == "Player"){
            Debug.Log("get it");
            string weaponName = getName(this.gameObject.name);
            Debug.Log(collision.gameObject.name);
            collision.gameObject.GetComponent<Weaponswitch>().addWeapon(weaponName);
            Destroy(this.gameObject);
            
        }
    }

    private string getName(string currName){
        if(currName == "blankPistol"){
            return "pistol";
        }else if(currName == "blankRifle"){
            return "rifle";
        }else if(currName == "blankPill"){
            return "pill";
        }
        return null;
    }
}
