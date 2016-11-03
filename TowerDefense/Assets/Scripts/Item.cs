using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool pickable = false;

    void Update() { }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.F) && other.transform.root.tag == "Player")
        {
            if (pickable)
                other.transform.root.GetComponent<PlayerController>().PickUpItem(this);

            enabled = false;
        }
    }
}
