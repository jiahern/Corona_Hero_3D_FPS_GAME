using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager1 : MonoBehaviour
{
    #region Singleton
    public static playerManager1 instance;
    void Awake(){
        instance = this;
    }
    #endregion

    public GameObject player;
}
