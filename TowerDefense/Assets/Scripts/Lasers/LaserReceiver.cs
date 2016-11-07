using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReceiver : MonoBehaviour
{
    public GameObject[] receiverObjects;

    void ToggleTrigger(bool state)
    {
        Debug.Log("Receiver Hit");
        foreach (GameObject obj in receiverObjects)
            obj.SendMessage("SetTriggerState", state, SendMessageOptions.DontRequireReceiver);
    }

    void SetLaserHitState(bool state)
    {
        ToggleTrigger(state);
    }
}
