﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Transform SelectionCube;
    public Camera SceneCamera;
    public PlayerController playerController;

    public LayerMask selectionBlockLayerMask;

    public float playerHealth = 100;
    public bool isAlive = true;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(SceneCamera.ScreenPointToRay(Input.mousePosition), out hit, 50, selectionBlockLayerMask))
        {
            if (!SelectionCube.gameObject.activeSelf)
                SelectionCube.gameObject.SetActive(true);

            Vector3 target = hit.point;
            target.x = Mathf.Round(target.x + 0.5f) - 0.5f;
            target.z = Mathf.Round(target.z + 0.5f) - 0.5f;
            SelectionCube.position = target + new Vector3(0, 0.025f, 0);

            if (Input.GetKeyDown(KeyCode.Mouse0))
                playerController.MoveTo(target);
        }
        else if (SelectionCube.gameObject.activeSelf)
            SelectionCube.gameObject.SetActive(false);

        if(Input.GetKeyDown(KeyCode.R) && !isResseting)
            StartCoroutine(SetLevel());
    }

    bool isResseting = false;
    IEnumerator SetLevel(int level = 1)
    {
        isResseting = true;
        AsyncOperation async = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        async.allowSceneActivation = false;
        UIManager.instance.SetWhiteScreen(true, 0.5f);
        yield return new WaitForSeconds(0.5f);
        async.allowSceneActivation = true;
    }

    public void GetHit()
    {
        if (isAlive && playerHealth > 0)
        {
            playerHealth -= 10;
            UIManager.instance.GetHit();
        }
        else if(isAlive)
        {
            playerHealth = 0;
            isAlive = false;
            playerController.KillPlayer();
            StartCoroutine(KillPlayer());
        }
    }

    IEnumerator KillPlayer()
    {
        isResseting = true;
        yield return new WaitForSeconds(2f);
        StartCoroutine(SetLevel());
    }

    private bool levelHasEnded = false;
    public void EndLevel()
    {
        if (!levelHasEnded && !isResseting)
        {
            levelHasEnded = true;
            StartCoroutine(SetLevel((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings));
        }
    }
}
