using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaithPlate : Trigger
{
    private Transform anchor;

    public Vector3 force;
    
    public float fireDelay = 3f;

    private float lastFireTime = 0;

    private float smoothBuffer;
    private float hardBuffer;

    void Awake()
    {
        anchor = transform.Find("anchor");
    }

    void FixedUpdate()
    {
        if(Time.time > lastFireTime + fireDelay && allColliders.Count > 0)
        {
            Fire();
        }

        smoothBuffer = Mathf.Lerp(smoothBuffer, hardBuffer, Time.deltaTime * 10);
        hardBuffer = Mathf.MoveTowards(hardBuffer, 0, Time.deltaTime * 20);
        anchor.localEulerAngles = new Vector3(smoothBuffer, 0, 0);
    }

    void Fire()
    {
        lastFireTime = Time.time;
        hardBuffer = 55;
        foreach (Rigidbody rig in allColliders)
        {
            if(rig.transform.root.tag == "Player")
            {
                GameManager.instance.playerController.SetFlyingMode(true);
            }

            rig.velocity = transform.rotation * force;
        }
    }
}
