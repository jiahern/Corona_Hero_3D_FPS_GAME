using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{

[Header("Movement")]
[SerializeField] private Transform orientation;

[Header("Detection")]
[SerializeField] private float wallDistance = 0.5f;
[SerializeField] private float minimumJumpHeight = 1.5f;

[Header("Wall Running")]
[SerializeField] private float wallRunGravity = 1f;
[SerializeField] private float wallRunJumpForce = 6f;

[Header("Camera")]
[SerializeField] Camera cam;
[SerializeField] private float fov;
[SerializeField] private float wallRunfov;
[SerializeField] private float wallRunfovTime;
[SerializeField] private float camTilt;
[SerializeField] private float camTiltTime;

public float tilt { get; private set;}


private bool wallLeft = false;
private bool wallRight = false;

private bool wallLeftDrop = false;
private bool wallRightDrop = false;

RaycastHit leftWallHit;
RaycastHit rightWallHit;

RaycastHit leftWallHitDrop;
RaycastHit rightWallHitDrop;

Rigidbody rb;

private bool disableVerHor = false;

// public MainPlayerMovement2 Player;
MainPlayerMovement MainPlayerMovement;


bool CanWallRun(){

    return !Physics.Raycast(transform.position, Vector3.down, minimumJumpHeight);
}

private void Start(){

    rb = GetComponent<Rigidbody>();
    MainPlayerMovement = GetComponent<MainPlayerMovement>();

}

void CheckWall(){

    wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallDistance);
    wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallDistance);
    wallLeftDrop = Physics.Raycast(transform.position, -transform.right, out leftWallHitDrop, wallDistance);
    wallRightDrop = Physics.Raycast(transform.position, transform.right, out rightWallHitDrop, wallDistance);

}

private void Update(){

    CheckWall();

    if(CanWallRun() ){


        if (wallLeftDrop){
            StartWallRun();
            Debug.Log("wall running on the left");
            if(Input.GetKeyDown(KeyCode.RightArrow)){
            StopWallRun();
        }
        }

        else if(wallRightDrop){
            StartWallRun();
            Debug.Log("wall running on the right");
            if(Input.GetKeyDown(KeyCode.LeftArrow)){
            StopWallRun();
        }
        }
        else{
        StopWallRun();
        
      
    
    }
    }
    else{
        StopWallRun();
    }
}

void StartWallRun(){

    MainPlayerMovement.moveDirection = Vector3.zero;

    rb.useGravity = false;

    rb.AddForce(Vector3.down * wallRunGravity, ForceMode.Force);

    //Set camera FOV for WallRun
    cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, wallRunfov, wallRunfovTime * Time.deltaTime);

    if(wallLeft){
        tilt = Mathf.Lerp(tilt, -camTilt, camTiltTime * Time.deltaTime);

        
        if(Input.GetKeyDown(KeyCode.D)){
            Vector3 wallRunJumpDirection = transform.up + leftWallHit.normal;
            rb.velocity = new Vector3(rb.velocity.x,0,rb.velocity.z);
            rb.AddForce(wallRunJumpDirection * wallRunJumpForce*20, ForceMode.Force);}

    }else if(wallRight){

        tilt = Mathf.Lerp(tilt, camTilt, camTiltTime * Time.deltaTime);
             if(Input.GetKeyDown(KeyCode.A)){

            Vector3 wallRunJumpDirection = transform.up + rightWallHit.normal;
            rb.velocity = new Vector3(rb.velocity.x,0,rb.velocity.z);
            rb.AddForce(wallRunJumpDirection * wallRunJumpForce*20, ForceMode.Force);
        }
    }
    
    if(Input.GetKeyDown(KeyCode.Space)){

        if(wallLeft){

            Vector3 wallRunJumpDirection = transform.up + leftWallHit.normal;
            rb.velocity = new Vector3(rb.velocity.x,0,rb.velocity.z);
            rb.AddForce(wallRunJumpDirection * wallRunJumpForce*50, ForceMode.Force);
        }else if(wallRight){

            Vector3 wallRunJumpDirection = transform.up + rightWallHit.normal;
            rb.velocity = new Vector3(rb.velocity.x,0,rb.velocity.z);
            rb.AddForce(wallRunJumpDirection * wallRunJumpForce*50, ForceMode.Force);

        }
    }else{
    }

//Drop From the Wall
       

        // if(wallLeft){

        //     }
        // else if(wallRight){
        
        // }
    


}

void StopWallRun(){

    rb.useGravity = true;

//Set camera FOV for WallRun
    cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, wallRunfovTime * Time.deltaTime);
    tilt = Mathf.Lerp(tilt, 0, camTiltTime * Time.deltaTime);


}

}


