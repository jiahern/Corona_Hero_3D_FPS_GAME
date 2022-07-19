using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkFire : MonoBehaviour
{
    
    public GameObject armo;
    public GameObject beam;
    public GameObject generatedBeam;
    public Transform currentTarget;
    public int shot = 0;
    // Start is called before the first frame update
    public void Shot(){
        GameObject newArmo =  Instantiate(armo);
        newArmo.gameObject.transform.position = this.gameObject.transform.position;
    }
    void Update(){
        if(shot == 1){
            generatedBeam  = Instantiate(beam);
            generatedBeam.GetComponent<LineRenderer>().SetPosition(0,this.gameObject.transform.position);
            
            shot = 0;
        }
        if(generatedBeam != null && currentTarget != null){
            Vector3 point = new Vector3(0,0,0);
            Vector3 forwardDirection = currentTarget.position - this.gameObject.transform.position;
            RaycastHit hit;
            
            if (Physics.Raycast(this.gameObject.transform.position,forwardDirection, out hit)){
                point = hit.point;
                //moveDirec = (camera.transform.InverseTransformPoint(moveDirec) - this.gameObject.transform.localPosition).normalized;
            }
            generatedBeam.GetComponent<LineRenderer>().SetPosition(1,point);
        }
        
    }
    
}
