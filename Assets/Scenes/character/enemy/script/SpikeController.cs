using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{   
    public GameObject shot1;
   

    Animator myAnimator;
    public float lookRadius = 10f;
    public float shotRadius = 100f;
    public float destroyTime = 5.0f;
    GameObject player;
    SpikeStat myStat;
    // Transform myTransform;
    Transform target;
    UnityEngine.AI.NavMeshAgent agent;
    float destroyTimer = 0.0f;
    bool attack1;
    
    
    bool fireDamage = false;
    float num;
    // Start is called before the first frame update
    void Start()
    {
        attack1 = false;
        
        player = GameObject.Find("Player").gameObject;
        target = player.transform;
        // myTransform = GetComponent<transform>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        myStat = GetComponent<SpikeStat>();
        myAnimator = GetComponent<Animator>();  
        num = Random.Range(0f,101f);  
    }

    // Update is called once per frame
    void Update()
    {  
        
        shot1.gameObject.transform.rotation = this.gameObject.transform.rotation;
        if (myStat.currentHealth <= 0) 
        {   
            destroyTimer += Time.deltaTime;
            if (destroyTimer >= destroyTime) DestroyObject(gameObject);
        } 
        else 
        {
            float distance = Vector3.Distance(target.position, transform.position);
            if(distance <= lookRadius)
            {
                
                agent.SetDestination(target.position);
                FaceTarget();
            }
            if(distance <= agent.stoppingDistance)
            {
                
                //attack target
                
                //face target
                FaceTarget();
            }
            if(distance < shotRadius){
                Attack();
            }
        }
        
            
        
    }
    
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime*5f);
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,lookRadius);
        
    }
    
    void OnCollisionEnter(UnityEngine.Collision other)
    {
        if(other.gameObject.name == "rifleArmo(Clone)") TakeDamage(other.gameObject.GetComponent<ArmoDamage>().damage);
        if(other.gameObject.name == "pistolArmo(Clone)")
        {
            TakeDamage(other.gameObject.GetComponent<ArmoDamage>().damage);
            myAnimator.SetTrigger("Defend");
        }
        if(other.gameObject.name == "pillArmo(Clone)") TakeDamage(other.gameObject.GetComponent<ArmoDamage>().damage);

        
    }

    void Attack()
    {
       
       
        // if (num > 0.5 && attack1 == false) {
        //     attack1 = true;
        //     myAnimator.SetTrigger("Stab Attack");
        // } 
        // if(!myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Stab Attack") && attack1 == true){
        //     attack1 = false;
        // }
        //Debug.Log(num);
        Debug.Log(num);
        
        if(attack1 == false) {
            myAnimator.SetTrigger("attack");
            
        }
        if(isPlaying(myAnimator, "spikeAttack")){
            attack1 = true;
            
        }else if(!myAnimator.GetCurrentAnimatorStateInfo(0).IsName("spikeAttack")  && attack1 == true){
            num = Random.Range(0f,101f);
            attack1 = false;
            shot1.gameObject.GetComponent<spikeArmoGenerator>().Shot();
            
            //player.gameObject.GetComponent<PlayerStat>().TakeDamage(GetComponent<PurpleEnemyStat>().stabDamage);
            
            
            
            //  yield return new WaitForSeconds(0.5f);   
        }
        
        
        
        

        
    }

    void TakeDamage(int damage)
    {
        //myAnimator.SetTrigger("Take Damage");
        myStat.TakeDamage(damage);
    }

    // void Die() 
    // {
    //     myAnimator.SetTrigger("Die");
    //     myAnimator.SetBool("isDead", true);
    // }
    bool isPlaying(Animator anim, string stateName)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            return true;
        else
            return false;
    }

}
