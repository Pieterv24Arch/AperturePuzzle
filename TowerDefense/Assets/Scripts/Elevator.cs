using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float speed = 1;

    public Vector3 bottomPosition;
    public Vector3 topPosition;
    public bool isMovingUp;

    private float lastTimeSwitch;
    private float endpointTime; 

    void Update()
    {
        if (isMovingUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, topPosition, speed * Time.deltaTime);
            if (transform.position == topPosition)
            {
                if (endpointTime == 0)
                    endpointTime = Time.time;
                if(Time.time > endpointTime + 2)
                {
                    isMovingUp = false;
                    endpointTime = 0; 
                }
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, bottomPosition, speed * Time.deltaTime);
            if (transform.position == bottomPosition)
            {
                if (endpointTime == 0)
                    endpointTime = Time.time;
                if (Time.time > endpointTime + 2)
                {
                    isMovingUp = true;
                    endpointTime = 0;
                }
            }
        }
    }
}
