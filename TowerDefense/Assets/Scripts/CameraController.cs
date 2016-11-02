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
	void Update () {
	    if (Input.GetKey(KeyCode.W))
	    {
	        
	    }
        if (Input.GetKey(KeyCode.S))
        {

        }
        if (Input.GetKey(KeyCode.A))
	    {
	        transform.Translate(Vector3.left);
	    }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right);
        }
        transform.LookAt(Vector3.zero);
    }

    Vector3 CalcNewRotPosition(Vector3 center, Vector3 position)
    {

        return Vector3.zero;
    }
}
