using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;

    public Transform leftArm;
    public Transform rightArm;

    public Item currentItem;

    public LayerMask groundLayer;

    public bool isFlying;

    private bool isMoving = false;
    private Transform itemTargetParent;

    private bool ignoreInputFrame;
    private float fallTimer;

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
        if (!agent.enabled)
            return;

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

        if (currentItem != null)
            if (Input.GetKeyDown(KeyCode.E))
                DropItem(false);
            else if (Input.GetKeyDown(KeyCode.G))
                DropItem(true);

        if(isFlying)
        {
            if (transform.position.y < 0)
                GameManager.instance.GetHit();

            if(GetComponent<Rigidbody>().velocity.y <= 0 && Physics.Raycast(transform.position, Vector3.down, 0.05f, groundLayer))
                SetFlyingMode(false);
        }
        /*if (!isFlying)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(transform.Find("RotationFix/ActorMesh").position, Vector3.down, out hitInfo, 50, groundLayer))
            {
                //Debug.Log("Distance is: " + hitInfo.distance);
                if(hitInfo.distance >= 1.4f)
                {
                    SetFlyingMode(true);
                    GameManager.instance.GetHit(typeDeath: 1);
                    if(hitInfo.distance >= 2.4f)
                        GameManager.instance.GetHit(100, 1);
                }
            }
        }*/
    }

    void DropItem(bool throwItem)
    {
        currentItem.transform.SetParent(transform.parent, true);
        currentItem.GetComponent<Rigidbody>().isKinematic = false;
        animator.SetBool("isHolding", false);

        if (currentItem.resetRotationOnDrop)
            currentItem.transform.rotation = new Quaternion(0, 0, 0, 0);

        if (throwItem && currentItem.thisRigidbody)
        {
            Vector3 force = GameManager.instance.currentAccurateMouseRayPoint - transform.position;
            force.y = 0;
            currentItem.thisRigidbody.AddForce(force.normalized * 5, ForceMode.Impulse);
        }
        currentItem.enabled = true;
        currentItem = null;
    }

    public void PickUpItem(Item item)
    {
        if (currentItem == null)
        {
            ignoreInputFrame = true;
            currentItem = item;
            item.transform.parent = itemTargetParent;
            item.transform.localPosition = item.localPositionWhenPicked;
            item.transform.localEulerAngles = item.localRotationWhenPicked;
            item.GetComponent<Rigidbody>().isKinematic = true;
            animator.SetBool("isHolding", true);
        }
    }

    public void KillPlayer(int type = 0)
    {
        agent.enabled = false;
        animator.CrossFade(type == 0 ? "die" : "crush", 0.1f);
    }

    public void SetFlyingMode(bool state)
    {
        agent.enabled = !state;
        GetComponent<Rigidbody>().isKinematic = !state;
        isFlying = state;
    }
}
