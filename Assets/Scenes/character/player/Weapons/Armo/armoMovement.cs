using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armoMovement : MonoBehaviour
{
    ParticleSystem flash;
    bool afterPlay = false;
    //Vector3 moveDirec;
    Vector3 originPosition;
    Rigidbody body;
    Vector3 point;
    public float recoil = 0.05f;
    
    public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {   
        body = GetComponent<Rigidbody>();
        flash = GetComponentInChildren<ParticleSystem>();
        originPosition = this.gameObject.transform.position;
        Camera camera = GameObject.Find("Camera").GetComponent<Camera>();
        Ray ray = new Ray(camera.transform.position,camera.transform.forward);
        //Vector3 normal = new Vector3(0,0,0);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)){
            point = hit.point;
            //moveDirec = (camera.transform.InverseTransformPoint(moveDirec) - this.gameObject.transform.localPosition).normalized;
        }
       

        // Vector3 cam = new Vector3(0,16,0) ;
        // direction = cam;
    }

    // Update is called once per frame
    void Update()
    {
        float destinationDistance = Vector3.Distance(point, originPosition);
        float randomX = Random.Range(-recoil*destinationDistance,recoil*destinationDistance);
        float randomY = Random.Range(-recoil*destinationDistance,recoil*destinationDistance);
        float randomZ = Random.Range(-recoil*destinationDistance,recoil*destinationDistance);
        float distance = Vector3.Distance(this.gameObject.transform.position, originPosition);
        
        if(distance > 100){
            Destroy(this.gameObject);
        }
        //this.transform.Translate(moveDirec * Time.deltaTime*speed, Space.World);
        //body.velocity = moveDirec * speed;
        Vector3 offset = new Vector3(randomX,randomY,randomZ);
        body.MovePosition(point+offset);
        //this.transform.position = body.position;
        if(afterPlay == true && flash.isStopped){
            //Debug.Log("!!!!!!!!!!!");
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision other){
        ContactPoint contact = other.GetContact(0);
        flash.transform.forward = -contact.normal;
        flash.Play(); 
        GetComponent<Renderer>().enabled = false;
        body.isKinematic = true;
        if (flash.isPlaying){
            //Debug.Log("!!!!!!!!!!!");
            afterPlay = true;
        }
    }
}


 
