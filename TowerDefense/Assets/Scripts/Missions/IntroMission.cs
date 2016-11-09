using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;

public class IntroMission : MonoBehaviour
{
    IEnumerator Start()
    {
        Camera cam = Camera.allCameras[0];
        VignetteAndChromaticAberration vig = cam.GetComponent<VignetteAndChromaticAberration>();
        vig.intensity = 0;

        yield return new WaitForSeconds(1);

        CaptationDisplay.instance.DisplayText(5, "Welcome testsub...     <b>*cough*</b>     Contestant 55");

        float startTime = Time.time;
        while (Time.time < startTime + 1)
        {
            vig.intensity = Mathf.Lerp(0, 0.25f, Time.time - startTime);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(5);

        CaptationDisplay.instance.DisplayText(5, "This is you.. ofcourse you already know who you are... but anyways");
        Transform target1 = GameObject.Find("PlayerObject").transform;

        startTime = Time.time;
        while (Time.time < startTime + 6)
        {
            target1.position = Vector3.Lerp(target1.position, new Vector3(0.5f, 0, 1.89f), Time.deltaTime * 5);
            yield return new WaitForEndOfFrame();
        }

        //yield return new WaitForSeconds(7); already waited 7 seconds

        CaptationDisplay.instance.DisplayText(5, "Your objective will be to complete the upcoming tests!");
        Transform target2 = GameObject.Find("StorageCube").transform;
        Transform target3 = GameObject.Find("Turret").transform;
        startTime = Time.time;
        while (Time.time < startTime + 6)
        {
            target1.position = Vector3.Lerp(target1.position, new Vector3(0.5f, 3, 1.89f), Time.deltaTime);
            target2.position = Vector3.Lerp(target2.position, new Vector3(0.5f, 0, 1.89f), Time.deltaTime * 5);
            target3.position = Vector3.Lerp(target3.position, new Vector3(-0.5f, -0.3f, 1.89f), Time.deltaTime * 5);
            yield return new WaitForEndOfFrame();
        }
        

        CaptationDisplay.instance.DisplayText(3, "Only then will you be able to 'win' the 60 bucks, good luck!");
        startTime = Time.time;
        while (Time.time < startTime + 5)
        {
            target2.position = Vector3.Lerp(target2.position, new Vector3(0.5f, 3, 1.89f), Time.deltaTime);
            target3.position = Vector3.Lerp(target3.position, new Vector3(-0.5f, 3, 1.89f), Time.deltaTime);
            vig.intensity = Mathf.Lerp(0.25f, 0, Time.time - startTime);
            yield return new WaitForEndOfFrame();
        }

        AsyncOperation async = SceneManager.LoadSceneAsync((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }
}
