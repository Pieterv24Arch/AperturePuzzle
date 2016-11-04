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

    void Update()
    {
        
    }

    void SetLaserHitState(bool state)
    {
        Debug.Log("hit state is " + state);
    }
}
