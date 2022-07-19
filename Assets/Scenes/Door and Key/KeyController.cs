using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public string keyName;
    
    void OnCollisionEnter(Collision other){
        if(other.gameObject.name == "Player"){

            other.gameObject.GetComponent<KeyContainer>().addKey(keyName);

            Destroy(this.gameObject);
            Debug.Log("Destroyed Key");
        }
    }
}
