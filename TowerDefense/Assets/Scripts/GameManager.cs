﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform SelectionCube;
    public Camera SceneCamera;

    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(SceneCamera.ScreenPointToRay(Input.mousePosition), out hit, 50))
        {
            if (!SelectionCube.gameObject.activeSelf)
                SelectionCube.gameObject.SetActive(true);

            Vector3 target = hit.point;
            target.x = Mathf.Round(target.x+0.5f)-0.5f;
            target.z = Mathf.Round(target.z+0.5f)-0.5f;
            SelectionCube.position = target + new Vector3(0,0.025f,0);

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {

            }
        }
        else if (SelectionCube.gameObject.activeSelf)
            SelectionCube.gameObject.SetActive(false);
    }
}