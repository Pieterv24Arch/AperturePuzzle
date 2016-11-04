﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEmitter : MonoBehaviour
{
    public LayerMask collisionLayerMask;
    public LayerMask interactiveLayerMask;
    public Transform emitterCore;
    public Transform laserObject;

    private bool laserUpdated = false;
    private float lastInteractionUpdate = 0;
    private GameObject interactionCube;
    private bool laserUpdateSend = false;
    
    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(emitterCore.position, Vector3.forward, out hit, 50, collisionLayerMask))
        {
            laserObject.localPosition = Vector3.forward * (hit.distance * 1.25f / 2) + emitterCore.localPosition;
            laserObject.localScale = new Vector3(0.01f, 0.01f, hit.distance * 1.25f);
            laserUpdated = false;

            if (interactiveLayerMask == (interactiveLayerMask | 1 << hit.collider.gameObject.layer))
            {
                if (Time.time > lastInteractionUpdate + 0.1F)
                {
                    /*if (hit.transform.root.tag == "Player")
                        GameManager.instance.GetHit();*/
                    
                }
                if (hit.transform.root.tag == "RedirectionCube")
                {
                    Debug.Log("Cube Detected");
                    if (interactionCube == null)
                        interactionCube = hit.transform.root.gameObject;
                    if (!laserUpdateSend)
                    {
                        interactionCube.transform.root.SendMessage("SetLaserHitState", true);
                        laserUpdateSend = true;
                    }
                }
            }
            if (interactionCube != null && laserUpdateSend)
            {
                interactionCube.transform.root.SendMessage("SetLaserHitState", false);
                interactionCube = null;
                laserUpdateSend = false;

            }
        }
        else if(!laserUpdated)
        {
            laserUpdated = true;
            laserObject.localPosition = Vector3.forward * 2.5f + emitterCore.localPosition;
            laserObject.localScale = new Vector3(0.01f, 0.01f, 5);
        }
    }
}