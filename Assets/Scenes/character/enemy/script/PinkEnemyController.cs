using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkEnemyController : MonoBehaviour
{   
    public GameObject shot1;
    public GameObject shot2;
    public GameObject shot3;
    public GameObject shot4;
    public GameObject shot5;
    public GameObject shot6;
    Transform currentTarget;
    Animator myAnimator;
    public float lookRadius = 10f;
    public float shotRadius = 100f;
    public float destroyTime = 5.0f;
    GameObject player;
    PinkEnemyStat myStat;
    // Transform myTransform;
    Transform target;
    UnityEngine.AI.NavMeshAgent agent;
    float destroyTimer = 0.0f;
    bool attack1;
    bool attack2;
    
    bool fireDamage = false;
    float num;
    // Start is called before the first frame update
    void Start()
    {
        attack1 = false;
        attack2 = false;
        player = GameObject.Find("Player").gameObject;
        target = player.transform;
        // myTransform = GetComponent<transform>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        myStat = GetComponent<PinkEnemyStat>();
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
                    FaceTarget();
                }
                if(distance <= agent.stoppingDistance)
                {
                    myAnimator.SetBool("Walk Forward",false);
                    //attack target
                    
                    //face target
                    FaceTarget();
                }
                if(distance < shotRadius){
                    Attack();
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
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,direction.y,direction.z));
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
            // Debug.Log("smashbool"+attack2ST);
            // Debug.Log("smash"+myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Smash Attack"));
            // if(myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Stab Attack")&& attack1 == false){
            //     // yield return new WaitForSeconds(2f);
            //     attack1 = true;
            // }
            // if(!myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Stab Attack") && attack1 == true){
            //     attack1 = false;
            //     other.gameObject.GetComponent<PlayerStat>().TakeDamage(GetComponent<PurpleEnemyStat>().stabDamage);
            // }
            // if(myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Smash Attack")&& attack2ST == false){
                
            //     attack2ST = true;
            //     //Debug.Log(attack2ST);
                
            //     //yield return new WaitForSeconds(0.5f);
            // }
            // if(!myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Smash Attack") && attack2ST == true){
            //     Debug.Log("smash Attack done");
            //     other.gameObject.GetComponent<PlayerStat>().TakeDamage(GetComponent<PurpleEnemyStat>().smashDamage);
            // }
        }
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
        
        // if(num > 50){
        //     if(attack1 == false) {
        //         myAnimator.SetTrigger("Attack01");
                
        //     }
        //     if(myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack01")){
        //         attack1 = true;
        //     }else if(!myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack01") && attack1 == true){
        //         num = Random.Range(0f,101f);
        //         attack1 = false;
                
        //         //player.gameObject.GetComponent<PlayerStat>().TakeDamage(GetComponent<PurpleEnemyStat>().stabDamage);
                
        //         shot1.gameObject.GetComponent<PinkFire>().Shot();
        //         shot2.gameObject.GetComponent<PinkFire>().Shot();
        //         shot3.gameObject.GetComponent<PinkFire>().Shot();
        //         shot4.gameObject.GetComponent<PinkFire>().Shot();
        //         shot5.gameObject.GetComponent<PinkFire>().Shot();
        //         shot6.gameObject.GetComponent<PinkFire>().Shot();
        //         //  yield return new WaitForSeconds(0.5f);   
        //     }
        // }
        if(num < 1000){
            currentTarget = player.transform;
            if(attack2 == false) {
                    myAnimator.SetTrigger("Attack02RPT");
                    
                    
                }
            if(!attack2 && myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack02RPT")){
                shot1.gameObject.GetComponent<PinkFire>().shot = 1;
                shot2.gameObject.GetComponent<PinkFire>().shot = 1;
                shot3.gameObject.GetComponent<PinkFire>().shot = 1;
                shot4.gameObject.GetComponent<PinkFire>().shot = 1;
                shot5.gameObject.GetComponent<PinkFire>().shot = 1;
                shot6.gameObject.GetComponent<PinkFire>().shot = 1;
            }
            if(myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack02RPT")){
                attack2 = true;
                Vector3 point = new Vector3(0,0,0);
                Vector3 forwardDirection = currentTarget.position - this.gameObject.transform.position;
                RaycastHit hit;
                
                if (Physics.Raycast(this.gameObject.transform.position,forwardDirection, out hit)){
                    point = hit.point;
                    //moveDirec = (camera.transform.InverseTransformPoint(moveDirec) - this.gameObject.transform.localPosition).normalized;
                }
                
                shot1.gameObject.GetComponent<PinkFire>().currentTarget = currentTarget;
                // Debug.Log( shot1.gameObject.GetComponent<PinkFire>().beam.GetComponent<LineRenderer>().GetPosition(1));
                shot2.gameObject.GetComponent<PinkFire>().currentTarget = currentTarget;
                
                shot3.gameObject.GetComponent<PinkFire>().currentTarget = currentTarget;
                
                shot4.gameObject.GetComponent<PinkFire>().currentTarget = currentTarget;
                
                shot5.gameObject.GetComponent<PinkFire>().currentTarget = currentTarget;
                
                shot6.gameObject.GetComponent<PinkFire>().currentTarget = currentTarget;
                
            }else if(!myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack02RPT") && attack2 == true){
                num = Random.Range(0f,101f);
                attack2 = false;
                
                Destroy(shot1.gameObject.GetComponent<PinkFire>().generatedBeam);
                Destroy(shot2.gameObject.GetComponent<PinkFire>().generatedBeam);
                Destroy(shot3.gameObject.GetComponent<PinkFire>().generatedBeam);
                Destroy(shot4.gameObject.GetComponent<PinkFire>().generatedBeam);
                Destroy(shot5.gameObject.GetComponent<PinkFire>().generatedBeam);
                Destroy(shot6.gameObject.GetComponent<PinkFire>().generatedBeam);
                //player.gameObject.GetComponent<PlayerStat>().TakeDamage(GetComponent<PurpleEnemyStat>().stabDamage);
                //Debug.Log(myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack01") );
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
