using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;
    public Animator animator;

    private bool isMoving = false;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 position)
    {
        agent.SetDestination(position);
    }

    void FixedUpdate()
    {
        if(agent.remainingDistance < 0.1f && isMoving)
        {
            isMoving = false;
            animator.SetBool("isWalking", isMoving);
        }
        else if(agent.remainingDistance >= 0.1f && !isMoving)
        {
            isMoving = true;
            animator.SetBool("isWalking", isMoving);
        }
    }
}
