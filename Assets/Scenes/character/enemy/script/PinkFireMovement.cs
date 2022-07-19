using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkFireMovement : MonoBehaviour
{
    Vector3 forwardDirection;
    public float speed = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        forwardDirection = GameObject.Find("Player").gameObject.transform.position - this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(forwardDirection*Time.deltaTime*speed);
    }
    private void OnCollisionEnter(Collision other){
        if(other.gameObject.name != "Player" && other.gameObject.tag != "enemy"){
            Destroy(this.gameObject);
        }
    }
}
