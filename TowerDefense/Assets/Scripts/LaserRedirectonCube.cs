using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class LaserRedirectonCube : MonoBehaviour {
    public enum Direction
    {
        X1, X2, Z1, Z2
    }

    public Direction LaserDirection;

    private bool receivingLaser;
    private Vector3[] Directions = new[] {Vector3.forward, Vector3.back, Vector3.left, Vector3.right};

    void Update()
    {
        if()
    }

    void SetLaserHitState(bool state)
    {
        Debug.Log("hit state is " + state);
        receivingLaser = state;
    }
}
