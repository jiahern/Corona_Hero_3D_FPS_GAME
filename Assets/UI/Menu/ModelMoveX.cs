using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelMoveX : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rightTurnPosition;
    [SerializeField] private float leftTurnPosition;
    private int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition += Vector3.right * Time.deltaTime * moveSpeed * direction;

        if (this.transform.localPosition.x > rightTurnPosition) 
        {
            direction = -1;
        } 
        else if (this.transform.localPosition.x < leftTurnPosition)
        {
            direction = 1;
        }
    }
}
