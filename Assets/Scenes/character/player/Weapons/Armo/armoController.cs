using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armoController : MonoBehaviour
{
    Animator parentAnimator;
    public GameObject armo;
    public string weaponName;
    private WeaponStat weapon;
    private bool isFire = true;
    private bool isReload = true;

    // Start is called before the first frame update
    void Start()
    {
        parentAnimator = GameObject.Find(weaponName).gameObject.GetComponentInParent<Animator>();
        weapon = GameObject.Find(weaponName).gameObject.GetComponent<WeaponStat>();
    }

    // Update is called once per frame
    void Update()
    {   
        if (weapon.currentArmo > 0) 
        {
            if(parentAnimator.GetCurrentAnimatorStateInfo(0).IsName("fire") && isFire)
            {
                var createdArmo =  Instantiate(armo);
                createdArmo.transform.localPosition = this.gameObject.transform.position;
                createdArmo.transform.localRotation = this.gameObject.transform.rotation;
                weapon.currentArmo -= 1;
                isFire = false;
            }
            if(!parentAnimator.GetCurrentAnimatorStateInfo(0).IsName("fire")){
                isFire = true;
            }
        } 
        
        if(parentAnimator.GetCurrentAnimatorStateInfo(0).IsName("reload") && isReload)
        {   
            if (weapon.currentArmo < weapon.reloadArmo)
            {   
                if (weapon.oneArmo >= weapon.reloadArmo) 
                {   
                    int temp = weapon.currentArmo;
                    weapon.currentArmo += (weapon.reloadArmo - temp);
                    weapon.oneArmo -= (weapon.reloadArmo - temp);
                } 
                else if (weapon.oneArmo < weapon.reloadArmo && weapon.oneArmo > 0)
                {
                    weapon.currentArmo += weapon.oneArmo;
                    if (weapon.currentArmo <= weapon.reloadArmo) 
                    {
                        weapon.oneArmo = 0;
                    }
                    else
                    {
                        int temp = weapon.currentArmo - weapon.reloadArmo;
                        weapon.currentArmo -= temp;
                        weapon.oneArmo = temp;
                    }
                }
                isReload = false;
            }
        }
        if (!parentAnimator.GetCurrentAnimatorStateInfo(0).IsName("reload"))
        {
            isReload = true;
        }
    }
}
