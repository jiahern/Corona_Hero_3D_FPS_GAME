using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
[Header("Referneces")]
[SerializeField] WallRun WallRun;

[SerializeField] private float sensX;
[SerializeField] private float sensY;

[SerializeField] Transform cam;
[SerializeField] Transform orientation;

float mouseX;
float mouseY;

float multiplier = 0.01f;

float xRotation;
float yRotation;

private void Start(){
    
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false; 
}
private void LateUpdate(){
   
    mouseX = Input.GetAxisRaw("Mouse X");
    mouseY = Input.GetAxisRaw("Mouse Y");

    yRotation += mouseX * sensX * multiplier;
    xRotation -= mouseY * sensY * multiplier;

    xRotation = Mathf.Clamp(xRotation, -90f,90f);

    //Apply the Wallrun Camera Tilt in the third param
    cam.transform.localRotation = Quaternion.Euler(xRotation,yRotation,WallRun.tilt);
    orientation.transform.rotation = Quaternion.Euler(0,yRotation,0);
}
}

