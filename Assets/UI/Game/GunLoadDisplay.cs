using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunLoadDisplay : MonoBehaviour
{   
    private int oneArmo;
    private int currentArmo;
    private int reloadArmo;
    private Text gunLoad;
    GameObject gun;

    // Start is called before the first frame update
    void Start()
    {   
        gunLoad = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {   
        gun = GameObject.FindWithTag("Weapon");
        if (gun != null) 
        {   
            oneArmo = gun.GetComponent<WeaponStat>().oneArmo;
            currentArmo = gun.GetComponent<WeaponStat>().currentArmo;
            reloadArmo = gun.GetComponent<WeaponStat>().reloadArmo;
        }

        if (oneArmo <= 0 || currentArmo <= reloadArmo * 0.2)
        {
            gunLoad.color = Color.red;
        } 
        else 
        {
            gunLoad.color = Color.white;
        }
        gunLoad.text = currentArmo + " / " + oneArmo;
    }
}

