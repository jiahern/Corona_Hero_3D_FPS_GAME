using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelMoveY : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float topTurnPosition;
    [SerializeField] private float bottomTurnPosition;
    private int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition += Vector3.up * Time.deltaTime * moveSpeed * direction;

        if (this.transform.localPosition.y > topTurnPosition) 
        {
            direction = -1;
        } 
        else if (this.transform.localPosition.y < bottomTurnPosition)
        {
            direction = 1;
        }
    }
}

