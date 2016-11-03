using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class MainMenu : MonoBehaviour
{
    public string currentMenu = "enter";
    public AnimationCurve curve;

    IEnumerator Start()
    {
        CanvasGroup cg = transform.Find("EnterMenu").GetComponent<CanvasGroup>();
        cg.alpha = 0;

        Camera cam = Camera.allCameras[0];
        VignetteAndChromaticAberration vig = cam.GetComponent<VignetteAndChromaticAberration>();
        vig.intensity = 0;

        yield return new WaitForSeconds(1);

        float startTime = Time.time;
        while (Time.time - 2 - Time.deltaTime < startTime)
        {
            cg.alpha = Mathf.Lerp(0, 1, curve.Evaluate((Time.time - startTime) / 3));
            vig.intensity = Mathf.Lerp(0, 0.25f, curve.Evaluate((Time.time - startTime) / 3));
            yield return new WaitForEndOfFrame();
        }

        while (!Input.GetKey(KeyCode.Return))
            yield return new WaitForEndOfFrame();

        startTime = Time.time;
        while (Time.time - 5 < startTime)
        {
            cg.alpha = Mathf.Lerp(1, 0, (Time.time - startTime));
            cam.backgroundColor = Color.Lerp(Color.white, new Color(0.2f, 0.2f, 0.2f), (Time.time - startTime));
            cam.transform.position = Vector3.Lerp(Vector3.zero, new Vector3(0, -10, 0), curve.Evaluate((Time.time - startTime) / 4));
            yield return new WaitForEndOfFrame();
        }
    }

    public void StartGame()
    {
        if (!isSwitching)
            StartCoroutine(SwitchLevelAnimation());
    }

    bool isSwitching = false;
    IEnumerator SwitchLevelAnimation()
    {
        isSwitching = true;
        Image white = transform.Find("white").GetComponent<Image>();
        white.enabled = true;
        white.CrossFadeAlpha(0, 0, true);
        white.CrossFadeAlpha(1, 1, true);

        AsyncOperation async = SceneManager.LoadSceneAsync(1);
        async.allowSceneActivation = false;

        Camera cam = Camera.allCameras[0];
        VignetteAndChromaticAberration vig = cam.GetComponent<VignetteAndChromaticAberration>();
        float startTime = Time.time;
        while (Time.time - 3f - Time.deltaTime < startTime)
        {
            vig.intensity = Mathf.Lerp(0, 0.25f, curve.Evaluate(1-(Time.time - startTime) / 2.5f));
            cam.transform.eulerAngles = Vector3.Lerp(new Vector3(10, 20, 0), new Vector3(0, 0, 0), curve.Evaluate(1-(Time.time - startTime) / 2.5f));
            cam.transform.position = Vector3.Lerp(new Vector3(0, -12, -2), new Vector3(0, -10, 0), curve.Evaluate(1 - (Time.time - startTime) / 2.5f));
            RenderSettings.ambientLight = Color.Lerp(RenderSettings.ambientLight, Color.white, Time.deltaTime*5);
            yield return new WaitForEndOfFrame();
        }
        async.allowSceneActivation = true;
    }
}
