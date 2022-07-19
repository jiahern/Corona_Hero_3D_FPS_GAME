using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerMovement : MonoBehaviour
{

float playerHeight = 2f;

[SerializeField] Transform orientation;

[Header("Movement")]
public float moveSpeed = 6f;
float movementMultiplier = 10f;
[SerializeField] public float airMultiplier = 0.1f;

[Header("Sprinting")]
[SerializeField] float walkSpeed = 4f;
[SerializeField] float sprintSpeed = 8f;
[SerializeField] float acceleration = 10f;

[Header("Jumping")]
public float jumpForce = 5f;
private const int MAX_JUMP = 2;
private int currentJump = 0;


[Header("Keybinds")]
[SerializeField] KeyCode jumpKey = KeyCode.Space;
[SerializeField] KeyCode sprintKey = KeyCode.LeftShift;


[Header("Drag")]
float groundDrag = 6f;
float airDrag = 0.01f;

public float horizontalMovement;
public float verticalMovement;


[Header("Ground Detection")]
[SerializeField] Transform groundCheck;
[SerializeField] LayerMask groundMask;
[SerializeField] float groundDistance = 0.2f;
public bool isGrounded;


public Vector3 moveDirection;

Vector3 slopeMoveDirection;

Rigidbody rb;

RaycastHit slopeHit;


private bool OnSlope(){

if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f)){

    if(slopeHit.normal != Vector3.up){
        return true;
    }else{
        return false;
    }
}
    return false;

}


private void Start(){

    rb = GetComponent<Rigidbody>();
    
    rb.freezeRotation = true;
}

private void Update(){

    isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

    MyInput();
    ControlDrag();
    ControlSpeed();

    //Single Jump + Double Jump
    if(Input.GetKeyDown(jumpKey) && (isGrounded || MAX_JUMP > currentJump)){

        Jump();
        isGrounded = false;
        currentJump++;
    }


    slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
}

void MyInput(){
horizontalMovement = Input.GetAxisRaw("Horizontal");
verticalMovement = Input.GetAxisRaw("Vertical");

moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;

}

private void Jump(){
    //Dont individually modify the x,y
    rb.velocity = new Vector3(rb.velocity.x, 0 , rb.velocity.z);
    rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
}

void ControlSpeed(){

    if (Input.GetKey(sprintKey) && isGrounded){

        moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
    }
    else{

        moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
    }

} 

void ControlDrag(){

if(isGrounded){
    rb.drag = groundDrag;

}else{

    rb.drag = airDrag;
}
}

private void OnCollisionEnter(Collision collision) {
    isGrounded = true;
    currentJump = 0;
}

private void FixedUpdate(){
    MovePlayer();
}

void MovePlayer(){

//Not on Slope
    if(isGrounded && !OnSlope()){
    rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);

//When is on Slope
    }else if(isGrounded && OnSlope()){

        rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
    }
    
    else if(!isGrounded){

            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);

    }
}
}
