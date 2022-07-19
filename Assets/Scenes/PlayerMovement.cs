using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{   
    public CharacterController controller;
    
    public float speed = 12f;
    public float gravity = -9.81f;
    Vector3 velocity;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public float jumpHeight = 3f;
    public LayerMask groundMask;
    private bool isGrounded;
    private bool jumpCount = true;

    void start(){
        
    }
    // Update is called once per frame
    void Update()
    {   
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
            jumpCount = true;
            
        }
        if(isGrounded){
            
        }
        

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpCount = false;
            Debug.Log(jumpCount);
        }
      
        
        controller.Move(velocity * Time.deltaTime);

        //Interactable interact = hit.collider.GetComponent<Interactable>();
        
    }
}
