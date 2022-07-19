using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnumatorController : MonoBehaviour
{
    Animator animator;
    public AudioSource source;
    public AudioClip reload;
    public AudioClip shot;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundDistance = 0.2f;
    public WeaponStat weaponStat;
    public bool isGrounded;
    public bool switchOut;
    bool isFire = false;
    // Start is called before the first frame update
    void Start()
    {
        
        source = GetComponent<AudioSource>();
        weaponStat = GetComponent<WeaponStat>();
        animator = GetComponent<Animator>();
        animator.SetTrigger("selected");
        switchOut = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeSelf == true && switchOut == true){
            animator.SetTrigger("selected");
        }
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("selected")){
            switchOut = false;
        }
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("fire") && !isFire){
            isFire = true;
        }else if(!animator.GetCurrentAnimatorStateInfo(0).IsName("fire") && isFire){
            source.pitch = Random.Range(0.7f, 1.0f);
            source.Play();
            isFire = false;
        }
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(Input.GetKeyDown(KeyCode.R) && weaponStat.oneArmo > 0 && weaponStat.currentArmo < weaponStat.reloadArmo){
            animator.SetTrigger("reload");
            source.PlayOneShot(reload);
            animator.SetFloat("reloadSpeed",weaponStat.reloadSpeed);
        }
        if((gameObject.name == "rifle" ) && weaponStat.currentArmo > 0){
            if(Input.GetKey(KeyCode.Mouse0)){
                animator.SetTrigger("fire");
                
                
                
                animator.SetFloat("fireSpeed", weaponStat.fireSpeed);
            }
        }
        
        if((gameObject.name == "pistol" || gameObject.name == "pill") && weaponStat.currentArmo > 0){
            if(Input.GetKeyDown(KeyCode.Mouse0)){
                animator.SetTrigger("fire");
                source.pitch = Random.Range(0.7f, 1.0f);
                source.PlayOneShot(shot);
                animator.SetFloat("fireSpeed", weaponStat.fireSpeed);
            }
        }
        
        if(isGrounded){
            if(Input.GetKeyUp(KeyCode.W) ||Input.GetKeyUp(KeyCode.A)||Input.GetKeyUp(KeyCode.S)||Input.GetKeyUp(KeyCode.D) ){
                animator.SetFloat("speed",0);
            }else if(Input.GetKey(KeyCode.W) ||Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.D) ){
                
                if(Input.GetKey(KeyCode.LeftShift)){
                    animator.SetFloat("speed",2);
                }else{
                    animator.SetFloat("speed",1);
                }
                if(Input.GetKeyUp(KeyCode.LeftShift)){
                    animator.SetFloat("speed",1);
                }
            } 
            
        }
        if(!isGrounded){
            animator.SetFloat("speed",0);
        }
    }
}
