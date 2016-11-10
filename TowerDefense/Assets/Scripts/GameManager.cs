using System.Collections;
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

    public Vector3 currentAccurateMouseRayPoint = Vector3.zero;

    private bool isRotatingPlacement;
    private Vector3 previousTarget = Vector3.zero;
    private bool targetIsInProximity = false;

    private bool skipInputFrame = false;

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

            currentAccurateMouseRayPoint = hit.point;
            Vector3 target = hit.point;
            target.x = Mathf.Round(target.x + 0.5f) - 0.5f;
            target.z = Mathf.Round(target.z + 0.5f) - 0.5f;
            SelectionCube.position = target + new Vector3(0, 0.025f, 0);

            if (Input.GetKeyDown(KeyCode.Mouse0))
                playerController.MoveTo(target);

            if (previousTarget != target)
            {
                previousTarget = target;
                Vector3 sub = playerController.transform.position - target;
                if (Mathf.Abs(sub.x) < 1.5f && Mathf.Abs(sub.z) < 1.5f && playerController.currentItem != null && Mathf.Abs(sub.y) < 0.5F)
                {
                    SelectionCube.GetComponent<Renderer>().material.color = Color.white;
                    targetIsInProximity = true;
                }
                else
                {
                    SelectionCube.GetComponent<Renderer>().material.color = Color.black;
                    targetIsInProximity = false;
                }
            }

            if (targetIsInProximity && !isRotatingPlacement && Input.GetKeyDown(KeyCode.Mouse1))
            {
                Time.timeScale = 0.35f;
                isRotatingPlacement = true;
                playerController.currentItem.transform.SetParent(null, true);
                playerController.currentItem.transform.position = previousTarget + playerController.currentItem.positionOffsetWhenPlaced;
                skipInputFrame = true;
                playerController.agent.enabled = false;
            }
            if (isRotatingPlacement && !skipInputFrame)
            {
                Vector3 pos = currentAccurateMouseRayPoint;
                pos.y = playerController.currentItem.transform.position.y;

                playerController.currentItem.transform.LookAt(pos);

                if(Input.GetKeyDown(KeyCode.Mouse1))
                {
                    isRotatingPlacement = false;
                    Time.timeScale = 1;
                    
                    playerController.currentItem.GetComponent<Rigidbody>().isKinematic = false;
                    playerController.animator.SetBool("isHolding", false);
                    playerController.currentItem.enabled = true;
                    playerController.currentItem = null;
                    playerController.agent.enabled = true;
                }
            }
        }
        else if (SelectionCube.gameObject.activeSelf)
            SelectionCube.gameObject.SetActive(false);

        if (Input.GetKeyDown(KeyCode.R) && !isResseting)
            StartCoroutine(SetLevel(SceneManager.GetActiveScene().buildIndex));
        if (Input.GetKeyDown(KeyCode.Escape) && !isResseting)
            StartCoroutine(SetLevel(0));

        skipInputFrame = false;
    }

    bool isResseting = false;
    IEnumerator SetLevel(int level = 2)
    {
        isResseting = true;
        AsyncOperation async = SceneManager.LoadSceneAsync(level);
        async.allowSceneActivation = false;
        UIManager.instance.SetWhiteScreen(true, 0.5f);
        yield return new WaitForSeconds(0.5f);
        async.allowSceneActivation = true;
    }

    public void GetHit(int hitMagnitude = 10, int typeDeath = 0)
    {
        if (isAlive && playerHealth > 0)
        {
            playerHealth -= hitMagnitude;
            UIManager.instance.GetHit();
        }
        else if(isAlive || hitMagnitude >= 100)
        {
            playerHealth = 0;
            isAlive = false;
            playerController.KillPlayer(typeDeath);
            StartCoroutine(KillPlayer());
        }
    }

    IEnumerator KillPlayer()
    {
        isResseting = true;
        yield return new WaitForSeconds(2f);
        StartCoroutine(SetLevel(SceneManager.GetActiveScene().buildIndex));
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
