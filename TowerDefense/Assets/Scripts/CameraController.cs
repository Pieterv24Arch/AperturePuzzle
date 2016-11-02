using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.W))
	    {
	        
	    }
        if (Input.GetKey(KeyCode.S))
        {

        }
        if (Input.GetKey(KeyCode.A))
	    {
	        transform.Rotate(new Vector3(0,1,0), 1, Space.World);
	    }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, 1, 0), -1, Space.World);
        }
        //transform.LookAt(Vector3.zero);
    }

    Vector3 CalcNewRotPosition(Vector3 center, Vector3 position)
    {

        return Vector3.zero;
    }
}
