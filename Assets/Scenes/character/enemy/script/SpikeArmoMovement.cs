using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeArmoMovement : MonoBehaviour
{
    Vector3 forwardDirection;
    public float speed = 1.0f;
    ParticleSystem fog;
    // Start is called before the first frame update
    void Start()
    {
        fog = GetComponentInChildren<ParticleSystem>();
        fog.Play();
        forwardDirection = GameObject.Find("Player").gameObject.transform.position - this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        this.transform.Translate(forwardDirection*Time.deltaTime*speed);
    }
    private void OnCollisionEnter(Collision other){
        if(other.gameObject.name != "Player" && other.gameObject.tag != "enemy" ){
            Destroy(this.gameObject);
        }
    }
}
