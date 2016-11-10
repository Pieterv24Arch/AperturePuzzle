using System.Collections;
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
    private Transform interactionCube = null;
    private bool interactionMessageSend = false;

    void Update()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(emitterCore.position, Vector3.forward, out hitInfo, 50, collisionLayerMask))
        {
            laserObject.localPosition = Vector3.forward*(hitInfo.distance/2) + emitterCore.localPosition;
            laserObject.localScale = new Vector3(0.01f, 0.01f, hitInfo.distance);
            laserUpdated = false;

            if (interactiveLayerMask == (interactiveLayerMask | 1 << hitInfo.collider.gameObject.layer))
            {
                if (Time.time > lastInteractionUpdate + 0.1F)
                {
                    /*if (hit.transform.tag == "Player")
                        GameManager.instance.GetHit();*/
                    if (hitInfo.transform.tag == "Turret")
                    {
                        hitInfo.transform.SendMessage("KillTurret", SendMessageOptions.DontRequireReceiver);
                    }

                }
                if (hitInfo.transform.tag == "Redirector")
                {
                    if (interactionCube == null)
                        interactionCube = hitInfo.transform;
                    if (!interactionMessageSend)
                    {
                        interactionCube.SendMessageUpwards("SetLaserHitState", true,
                            SendMessageOptions.DontRequireReceiver);
                        interactionMessageSend = true;
                    }
                }
                else
                {
                    ResetSignal();
                }
            }
        }
        else if (!laserUpdated)
        {
            laserUpdated = true;
            laserObject.localPosition = Vector3.forward*25f + emitterCore.localPosition;
            laserObject.localScale = new Vector3(0.01f, 0.01f, 50);
            ResetSignal();
        }
    }

    void ResetSignal()
    {
        if (interactionCube != null)
        {
            interactionCube.SendMessageUpwards("SetLaserHitState", false,
                            SendMessageOptions.DontRequireReceiver);
            interactionCube = null;
            interactionMessageSend = false;
        }
    }
}
