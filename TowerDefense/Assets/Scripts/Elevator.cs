using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float speed = 1;

    public Vector3 bottomPosition;
    public Vector3 topPosition;

    private float lastTimeSwitch;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, topPosition, speed * Time.deltaTime);
    }
}
