using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotate : MonoBehaviour
{
    public Vector3 force;
	void Update () {
        transform.Rotate(force * Time.deltaTime);
	}
}
