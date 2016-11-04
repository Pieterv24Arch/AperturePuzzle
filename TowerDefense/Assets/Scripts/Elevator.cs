using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Elevator : Trigger
{
    public float speed = 1;

    public Vector3 bottomPosition;
    public Vector3 topPosition;
    public bool isMovingUp;
    public enum Positions
    {
        Up, Down
    }
    public Positions DefaultPostition;

    private float lastTimeSwitch;
    private float endpointTime;
    private bool isMoving;
    private bool playerOnElevator;
    private 

    void Update()
    {
        if (isMovingUp && isMoving)
        {
            MoveUp();
        }
        else if (isMoving)
        {
            MoveDown();
        }
        if (!isMoving && transform.position != GetDefaultPosition())
        {
            if (DefaultPostition == Positions.Up)
                MoveUp();
            else
                MoveDown();
        }
    }

    public override void OnTriggerEnter(Collider col)
    {
        base.OnTriggerEnter(col);
        CheckPlayer();
    }

    public override void OnTriggerExit(Collider col)
    {
        base.OnTriggerExit(col);
        CheckPlayer();
    }

    void CheckPlayer()
    {
        if (allColliders.Count == 0)
        {
            playerOnElevator = false;
        }
        else if (allColliders.Count != 0)
        {
            bool playerFound = false;
            foreach (Rigidbody col in allColliders)
            {
                if (col.transform.root.tag == "Player")
                    playerFound = true;
            }
            playerOnElevator = playerFound;

        }
    }

    void MoveUp()
    {
        transform.position = Vector3.MoveTowards(transform.position, topPosition, speed * Time.deltaTime);
        if (transform.position == topPosition)
        {
            SetPlayerState(false);
            if (endpointTime == 0)
                endpointTime = Time.time;
            if (Time.time > endpointTime + 2)
            {
                SetPlayerState(true);
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
            SetPlayerState(false);
            if (endpointTime == 0)
                endpointTime = Time.time;
            if (Time.time > endpointTime + 2)
            {
                SetPlayerState(true);
                isMovingUp = true;
                endpointTime = 0;
            }
        }
    }

    void SetTriggerState(bool state)
    {
        isMoving = state;
    }

    Vector3 GetDefaultPosition()
    {
        if (DefaultPostition == Positions.Up)
            return topPosition;
        else
            return bottomPosition;
    }

    void SetPlayerState(bool add)
    {
        if (playerOnElevator)
        {
            Debug.Log("Player found");
            GameManager.instance.playerController.transform.parent = add ? transform : null;
            GameManager.instance.playerController.agent.enabled = !add;
        }
    }
}
