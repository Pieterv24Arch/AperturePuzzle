using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButton : Trigger
{
    public Transform button;
    public GameObject TriggerReceiver;

    private bool isActivated = true;

    void Start()
    {
        CheckChange();
    }
    
    public override void OnTriggerEnter(Collider col)
    {
        base.OnTriggerEnter(col);
        CheckChange();
    }

    public override void OnTriggerExit(Collider col)
    {
        base.OnTriggerExit(col);
        CheckChange();
    }

    void CheckChange()
    {
        if(isActivated && allColliders.Count == 0)
        {
            StopAllCoroutines();
            StartCoroutine(LerpButton(new Vector3(0,0.01f,0)));
            isActivated = false;
            TriggerReceiver.SendMessage("SetTriggerState", false);
        }
        else if (!isActivated && allColliders.Count != 0)
        {
            StopAllCoroutines();
            StartCoroutine(LerpButton(new Vector3(0, -0.03f, 0)));
            isActivated = true;
            TriggerReceiver.SendMessage("SetTriggerState", true);
        }
    }

    IEnumerator LerpButton(Vector3 to)
    {
        float endTime = Time.time + 1;

        while(Time.time <= endTime)
        {
            yield return new WaitForFixedUpdate();
            button.localPosition = Vector3.Lerp(button.localPosition, to, Time.fixedDeltaTime * 10);
        }
    }
}
