using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;
    public Animator animator;

    private bool isMoving = false;
    private Item currentItem;
    private Transform itemTargetParent;

    private bool ignoreInputFrame;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        itemTargetParent = transform.Find("RotationFix/ActorMesh");
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

    void Update()
    {
        if (ignoreInputFrame)
        {
            ignoreInputFrame = false;
            return;
        }

        if(Input.GetKeyDown(KeyCode.F) && currentItem != null)
        {
            currentItem.transform.SetParent(null);
            currentItem.transform.position = transform.position + new Vector3(0,0.5f,0);
            currentItem.GetComponent<Rigidbody>().isKinematic = false;
            currentItem = null;
        }
    }

    public void PickUpItem(Item item)
    {
        if (currentItem == null)
        {
            ignoreInputFrame = true;
            currentItem = item;
            item.transform.parent = itemTargetParent;
            item.transform.localPosition = new Vector3(0, 0.22f, 0);
            item.transform.localRotation = Quaternion.identity;
            item.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
