using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    
    enum Status{
        Lock,
        Open
    }
    Status status; 
    public string requiredKey;

    Material render;

    float dissolve;
    // Start is called before the first frame update
    void Start()
    {   
        dissolve = 0.0f;
        render = GetComponent<Renderer>().material;
        // render.material.shader = Shader.Find("StandardFleischDisolve");
        status = Status.Lock;

    }

    // Update is called once per frame
    void Update()
    {
        if(status == Status.Open){
            dissolve += Time.deltaTime * 0.5f;

            //do the open animation or shader or particle here
            render.SetFloat("_Cutoff",dissolve);

            Debug.Log("Opened Door");
            
            if(dissolve >= 1f){
                Destroy (this.gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision other){
        
        if(other.gameObject.name == "Player" ){
            KeyContainer container = other.gameObject.GetComponent<KeyContainer>();
                                        Debug.Log(container.ifOwnedKey(requiredKey));

            if(container.ifOwnedKey(requiredKey)){
                status = Status.Open;

            }else{
                //show the UI to warn player "must have key to open the door"
            }
           
        }
    }
}
