using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Persistence;

public class CameraController : MonoBehaviour
{
    public float distance = 15;
    public Vector3 angle;
    public Transform targetObject;
    
	void Update ()
	{
	    angle.y += Input.GetKey(KeyCode.A) ? 1 : Input.GetKey(KeyCode.D) ? -1 : 0;
	    transform.eulerAngles = angle;
        transform.position = targetObject.position + (transform.rotation * Vector3.back * distance);
	}
}
