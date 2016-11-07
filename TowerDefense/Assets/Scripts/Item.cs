using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool pickable = false;
    public bool resetRotationOnDrop = false;
    public bool resetWhenOffLevel = true;
    public Vector3 localRotationWhenPicked;
    public Vector3 localPositionWhenPicked;

    public Rigidbody thisRigidbody;

    private Vector3 startPosition;
    private Vector3 startRotation;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.eulerAngles;
        if (thisRigidbody == null)
            thisRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (resetWhenOffLevel && transform.position.y < 0)
        {
            transform.position = startPosition;
            transform.eulerAngles = startRotation;
            if (thisRigidbody)
                thisRigidbody.velocity = Vector3.zero;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && other.transform.root.tag == "Player")
        {
            if (pickable)
                other.transform.root.GetComponent<PlayerController>().PickUpItem(this);

            enabled = false;
        }
    }
}
