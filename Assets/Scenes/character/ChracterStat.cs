using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChracterStat : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth = 100;
    public int damage = 20;
    public int cure = 20;
    
    // public int armor = 100;

    void Awake(){
        currentHealth = maxHealth;
    }

    // public void TakeDamage(int damage){

    //     // damage -= armor;
    //     // damage = Mathf.Clamp(damage,0,int.MaxValue);
    //     currentHealth -= damage;
    //     Debug.Log("AttackDamage");
    //     if(currentHealth <= 0){
    //         Die();
    //     }
    // }

    public virtual void Die()
    {
        //myAnimator.SetTrigger("Die");
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) Die();
    }

    public void Cure(int treatment)
    {
        currentHealth += treatment;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
    }

    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
    }
}
