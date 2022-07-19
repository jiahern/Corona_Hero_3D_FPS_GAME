using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeArmoGenerator : MonoBehaviour
{
    public GameObject armo;
    // Start is called before the first frame update
    public void Shot(){
        Instantiate(armo);
        armo.gameObject.transform.position = this.gameObject.transform.position;
    }
}
