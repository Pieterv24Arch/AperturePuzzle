using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Elevator : MonoBehaviour
{
    public float speed = 1;

    public Vector3 bottomPosition;
    public Vector3 topPosition;
    public bool isMovingUp;
    public bool isMoving;
    public enum Positions
    {
        Up, Down
    }
    public Positions DefaultPostition;

    private float lastTimeSwitch;
    private float endpointTime;

    void Update()
    {
        if (isMovingUp && isMoving)
        {
            MoveUp();
        }
        else if(isMoving)
        {
            MoveDown();
        }
        if (!isMoving && transform.position != GetDefaultPosition())
        {
            if(DefaultPostition == Positions.Up)
                MoveUp();
            else
                MoveDown();
        }
    }

    void MoveUp()
    {
        transform.position = Vector3.MoveTowards(transform.position, topPosition, speed * Time.deltaTime);
        if (transform.position == topPosition)
        {
            if (endpointTime == 0)
                endpointTime = Time.time;
            if (Time.time > endpointTime + 2)
            {
                isMovingUp = false;
                endpointTime = 0;
            }
        }
    }

    void MoveDown()
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

    void Activate()
    {
        isMoving = true;
    }

    void Deactivate()
    {
        isMoving = false;
    }

    Vector3 GetDefaultPosition()
    {
        if (DefaultPostition == Positions.Up)
            return topPosition;
        else
            return bottomPosition;
    }
}
