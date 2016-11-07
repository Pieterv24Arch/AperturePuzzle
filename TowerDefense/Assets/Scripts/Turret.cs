using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public LayerMask collisionLayerMask;
    public LayerMask hostileLayerMask;
    public Transform laserStartPoint;
    public Transform laserObject;
    public List<ParticleSystem> muzzles = new List<ParticleSystem>();
    public GameObject destructionParticle;

    public bool shouldFire = false;
    public float fireForce = 25;

    private bool hasLaserBeenUpdated = false;
    private int lastFiredMuzzle = 0;
    private float lastFiredTime = 0;

    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(laserStartPoint.position, transform.forward, out hit, 50, collisionLayerMask))
        {
            laserObject.localPosition = Vector3.forward * (hit.distance * 1.25f / 2) + laserStartPoint.localPosition;
            laserObject.localScale = new Vector3(0.01f, 0.01f, hit.distance*1.25f);
            hasLaserBeenUpdated = false;

            if (hostileLayerMask == (hostileLayerMask | 1 << hit.collider.gameObject.layer))
            {
                if (Time.time > lastFiredTime + 0.1f)
                {
                    Fire();
                    if (hit.collider.attachedRigidbody)
                        hit.collider.attachedRigidbody.AddForceAtPosition(transform.forward * fireForce, hit.point);
                    if (hit.transform.root.tag == "Player")
                        GameManager.instance.GetHit();
                }
            }
        }
        else if (!hasLaserBeenUpdated)
        {
            hasLaserBeenUpdated = true;
            laserObject.localPosition = Vector3.forward * 2.5f + laserStartPoint.localPosition;
            laserObject.localScale = new Vector3(0.01f, 0.01f, 5);
            shouldFire = false;
        }

        if (shouldFire)
        {
            if (Time.time > lastFiredTime + 0.1f)
            {
                Fire();
            }
        }
    }

    void Fire()
    {
        lastFiredMuzzle = (lastFiredMuzzle + 1) % 4;
        lastFiredTime = Time.time;
        muzzles[lastFiredMuzzle].Play();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 5)
        {
            Instantiate(destructionParticle, transform.position + (Vector3.up * 0.5f), Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void KillTurret()
    {
        Instantiate(destructionParticle, transform.position + (Vector3.up * 0.5f), Quaternion.identity);
        Destroy(gameObject);
    }
}
