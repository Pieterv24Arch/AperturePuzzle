using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    protected List<Rigidbody> allColliders = new List<Rigidbody>();

    public virtual void OnTriggerEnter(Collider col)
    {
        allColliders.Add(col.attachedRigidbody);
    }

    public virtual void OnTriggerExit(Collider col)
    {
        allColliders.Remove(col.attachedRigidbody);
    }
}
