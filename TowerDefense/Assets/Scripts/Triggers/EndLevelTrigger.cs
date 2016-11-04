using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTrigger : Trigger
{
    private bool hasActivated = false;
    void FixedUpdate()
    {
        if (hasActivated)
            return;

        if(allColliders.Count > 0)
        {
            foreach(Rigidbody rig in allColliders)
            {
                if(rig.transform.tag == "Player")
                {
                    hasActivated = true;
                    GameManager.instance.EndLevel();
                }
            }
        }
    }
}
