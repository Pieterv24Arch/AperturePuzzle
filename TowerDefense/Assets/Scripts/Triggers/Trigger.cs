using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    protected List<Rigidbody> allColliders = new List<Rigidbody>();

    public virtual void OnTriggerEnter(Collider col)
    {
        if (!allColliders.Contains(col.attachedRigidbody))
            allColliders.Add(col.attachedRigidbody);
    }

    public virtual void OnTriggerExit(Collider col)
    {
        if (allColliders.Contains(col.attachedRigidbody))
            allColliders.Remove(col.attachedRigidbody);
    }
}
