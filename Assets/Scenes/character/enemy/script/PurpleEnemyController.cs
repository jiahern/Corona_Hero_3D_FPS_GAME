using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PurpleEnemyController : MonoBehaviour
{   
    Animator myAnimator;
    public float lookRadius = 10f;
    public float destroyTime = 5.0f;
    GameObject player;
    PurpleEnemyStat myStat;
    Transform target;
    NavMeshAgent agent;
    float destroyTimer = 0.0f;
    bool stabAttack = false;
    bool smashAttack;
    bool fireDamage = false;
    float num;
    // Start is called before the first frame update
    void Start()
    {
        smashAttack = false;
        player = GameObject.Find("Player").gameObject;
        target = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        myStat = GetComponent<PurpleEnemyStat>();
        myAnimator = GetComponent<Animator>();  
        num = Random.Range(0f,101f);  
    }

    // Update is called once per frame
    void Update()
    {   
        if (!myAnimator.GetBool("isDead")) 
        {
            if (myStat.currentHealth <= 0) 
            {   
                Die();
            } 
            else 
            {
                float distance = Vector3.Distance(target.position, transform.position);
                if(distance <= lookRadius && distance > agent.stoppingDistance)
                {
                    myAnimator.SetBool("Walk Forward",true);
                    agent.SetDestination(target.position);
                    
                }
                if(distance <= agent.stoppingDistance)
                {
                    myAnimator.SetBool("Walk Forward",false);
                    //attack target
                    Attack();
                    //face target
                    FaceTarget();
                }
            }
        } 
        else if (myAnimator.GetBool("isDead"))
        {
            destroyTimer += Time.deltaTime;
            if (destroyTimer >= destroyTime) DestroyObject(gameObject);
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

        if(other.gameObject.name == "Player"){
            // Debug.Log("atach player");
            // Debug.Log("smashbool"+smashAttack);
            // Debug.Log("smash"+myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Smash Attack"));
            // if(myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Stab Attack")&& stabAttack == false){
            //     // yield return new WaitForSeconds(2f);
            //     stabAttack = true;
            // }
            // if(!myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Stab Attack") && stabAttack == true){
            //     stabAttack = false;
            //     other.gameObject.GetComponent<PlayerStat>().TakeDamage(GetComponent<PurpleEnemyStat>().stabDamage);
            // }
            // if(myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Smash Attack")&& smashAttack == false){
                
            //     smashAttack = true;
            //     //Debug.Log(smashAttack);
                
            //     //yield return new WaitForSeconds(0.5f);
            // }
            // if(!myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Smash Attack") && smashAttack == true){
            //     Debug.Log("smash Attack done");
            //     other.gameObject.GetComponent<PlayerStat>().TakeDamage(GetComponent<PurpleEnemyStat>().smashDamage);
            // }
        }
    }

    void Attack()
    {
       
       
        // if (num > 0.5 && stabAttack == false) {
        //     stabAttack = true;
        //     myAnimator.SetTrigger("Stab Attack");
        // } 
        // if(!myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Stab Attack") && stabAttack == true){
        //     stabAttack = false;
        // }
        Debug.Log(num);
        
        if(num > 50){
            if(stabAttack == false) {
                myAnimator.SetTrigger("Stab Attack");
                Debug.Log("attacking" );
            }
            if(myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Stab Attack")){
                stabAttack = true;
            }else if(!myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Stab Attack")&& stabAttack == true){
                num = Random.Range(0f,101f);
                stabAttack = false;
                Debug.Log("stop");
                player.gameObject.GetComponent<PlayerStat>().TakeDamage(GetComponent<PurpleEnemyStat>().stabDamage);
                Debug.Log(myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Stab Attack") );
                //  yield return new WaitForSeconds(0.5f);   
        }
        }
        if(num < 50){
            if(smashAttack == false) {
                myAnimator.SetTrigger("Smash Attack");
                Debug.Log("attacking" );
            }
            if(myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Smash Attack")){
                smashAttack = true;
            }else if(!myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Smash Attack") && smashAttack == true){
                num = Random.Range(0f,101f);
                smashAttack = false;
                Debug.Log("stop");
                player.gameObject.GetComponent<PlayerStat>().TakeDamage(GetComponent<PurpleEnemyStat>().smashDamage);
                Debug.Log(myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Smash Attack") );
                //  yield return new WaitForSeconds(0.5f);   
            }
        }
        
        

        
    }

    void TakeDamage(int damage)
    {
        myAnimator.SetTrigger("Take Damage");
        myStat.TakeDamage(damage);
    }

    void Die() 
    {
        myAnimator.SetTrigger("Die");
        myAnimator.SetBool("isDead", true);
    }
    bool isPlaying(Animator anim, string stateName)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            return true;
        else
            return false;
    }


}
