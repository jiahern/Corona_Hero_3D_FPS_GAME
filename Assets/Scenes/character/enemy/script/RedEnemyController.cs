using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemyController : MonoBehaviour
{   
    Animator myAnimator;
    public float lookRadius = 10f;
    public float destroyTime = 5.0f;
    GameObject player;
    RedEnemyStat myStat;
    Transform target;
    UnityEngine.AI.NavMeshAgent agent;
    float destroyTimer = 0.0f;
    bool attack1;
    bool attack2;
    bool jumpAttack;
    float num;
    // Start is called before the first frame update
    void Start()
    {
        attack1 = false;
        attack2 = false;
        jumpAttack = false;
        player = GameObject.Find("Player").gameObject;
        target = GameObject.Find("Player").transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        myStat = GetComponent<RedEnemyStat>();
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
            if (destroyTimer >= destroyTime) Destroy(gameObject);
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
        Debug.Log(num);
        
        if(num > 70){
            if(attack1 == false) {
                myAnimator.SetTrigger("Attack01");
                
            }
            if(myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack01")){
                attack1 = true;
            }else if(!myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack01") && attack1 == true){
                num = Random.Range(0f,101f);
                attack1 = false;
                
                player.gameObject.GetComponent<PlayerStat>().TakeDamage(GetComponent<RedEnemyStat>().attack1Damage);
                //Debug.Log(myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Stab Attack") );
                //  yield return new WaitForSeconds(0.5f);   
        }
        }
        if(num < 70 && num >30){
            if(attack2 == false) {
                myAnimator.SetTrigger("Attack02");
                Debug.Log("attacking" );
            }
            if(myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack02")){
                attack2 = true;
            }else if(!myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack02") && attack2 == true){
                num = Random.Range(0f,101f);
                attack2 = false;
                Debug.Log("stop");
                player.gameObject.GetComponent<PlayerStat>().TakeDamage(GetComponent<RedEnemyStat>().attack2Damage);
                //Debug.Log(myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Smash Attack") );
                //  yield return new WaitForSeconds(0.5f);   
            }
        }
        
        if(num < 30){
            if(jumpAttack == false) {
                myAnimator.SetTrigger("Jump");
                Debug.Log("Jump" );
            }
            if(isPlaying(myAnimator,"JumpAttack")){
                jumpAttack = true;
            }else if(!isPlaying(myAnimator,"JumpAttack") && jumpAttack == true){
                num = Random.Range(0f,101f);
                jumpAttack = false;
                Debug.Log("stop");
                //player.gameObject.GetComponent<PlayerStat>().TakeDamage(GetComponent<RedEnemyStat>().attack2Damage);
                //Debug.Log(myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Smash Attack") );
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
