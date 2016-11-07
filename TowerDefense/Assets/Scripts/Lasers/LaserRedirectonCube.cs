using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class LaserRedirectonCube : MonoBehaviour {

    public Transform Laser;
    public Transform LaserOrigin;
    public LayerMask CollisionLayerMask;
    public LayerMask interactiveLayermask;

    private bool receivingLaser;
    private bool laserUpdated;
    private float lastInteractionUpdate = 0;
    private Transform interactionCube = null;
    private bool interactionMessageSend = false;


    void Update()
    {
        if (receivingLaser)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(LaserOrigin.position, LaserOrigin.forward, out hitInfo, 50, CollisionLayerMask))
            {
                Laser.transform.localPosition = Vector3.forward * (hitInfo.distance/ 2) + LaserOrigin.localPosition;
                Laser.transform.localScale = new Vector3(0.01F, 0.01F, hitInfo.distance);
                laserUpdated = false;
                if (interactiveLayermask == (interactiveLayermask | 1 << hitInfo.collider.gameObject.layer))
                {
                    if (Time.time > lastInteractionUpdate + 0.1F)
                    {
                        /*if (hit.transform.root.tag == "Player")
                            GameManager.instance.GetHit();*/
                        if (hitInfo.transform.root.tag == "Turret")
                        {
                            Debug.Log("Turret Detected");
                            hitInfo.transform.root.SendMessage("KillTurret", SendMessageOptions.DontRequireReceiver);
                        }

                    }
                    if (hitInfo.transform.root.tag == "Redirector")
                    {
                        if (interactionCube == null)
                            interactionCube = hitInfo.transform;
                        if (!interactionMessageSend)
                        {
                            interactionCube.SendMessageUpwards("SetLaserHitState", true, SendMessageOptions.DontRequireReceiver);
                            interactionMessageSend = true;
                        }
                    }
                    else
                    {
                        if (interactionCube != null)
                        {
                            interactionCube.SendMessageUpwards("SetLaserHitState", false, SendMessageOptions.DontRequireReceiver);
                            interactionCube = null;
                            interactionMessageSend = false;
                        }
                    }
                }
            }
            else if (!laserUpdated)
            {
                laserUpdated = true;
                Laser.transform.localPosition = Vector3.forward * 25f + LaserOrigin.localPosition;
                Laser.transform.localScale = new Vector3(0.01f, 0.01f, 50);
            }
        }
        else
        {
            laserUpdated = false;
            Laser.transform.localScale = new Vector3(0.01F, 0.01F, 0.01F);
            Laser.transform.localPosition = LaserOrigin.localPosition;
            if (interactionCube != null)
            {
                interactionCube.SendMessageUpwards("SetLaserHitState", false);
                interactionCube = null;
                interactionMessageSend = false;
            }
        }
    }

    //void OnMouseOver()
    //{
    //    if (Input.GetMouseButtonDown(1))
    //        transform.Rotate(Vector3.up, 45f);
    //}

    void SetLaserHitState(bool state)
    {
        Debug.Log("hit state is " + state);
        receivingLaser = state;
    }
}
