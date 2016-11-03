using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 target = Vector3.zero;
	
	// Update is called once per frame
	void Update ()
	{
        if (Input.GetKey(KeyCode.W))
        {
            if(Vector3.Angle(new Vector3(transform.position.x, 0, transform.position.z), transform.position) < 85)
                transform.RotateAround(target, transform.right, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            if(transform.position.y > 1)
                transform.RotateAround(target, transform.right, -1);
        }
        if (Input.GetKey(KeyCode.A))
	    {
            transform.RotateAround(target, Vector3.up, 1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.RotateAround(target, Vector3.up, -1);
        }
	    if (Input.GetKey(KeyCode.Q))
	    {
            if(Physics.Raycast(transform.position, transform.forward))
	        transform.Translate(transform.forward * 0.1F, Space.World);
	    }
	    if (Input.GetKey(KeyCode.E))
	    {
            transform.Translate(-transform.forward * 0.1F, Space.World);
        }
        transform.LookAt(target);
    }

    Vector3 CalcNewRotPosition(Vector3 center, Vector3 position)
    {

        return Vector3.zero;
    }
}
