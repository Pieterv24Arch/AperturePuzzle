using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptationDisplay : MonoBehaviour
{
    public static CaptationDisplay instance;

    public CanvasGroup canvasGroup;
    public Text text;

    private bool isDiplayingText;

    void Awake()
    {
        instance = this;
        canvasGroup.alpha = 0;
    }

    public void SetText(string text)
    {
        this.text.text = text;
    }

    public void DisplayText(float visibleLength = 4, string text = "")
    {
        StopAllCoroutines();
        StartCoroutine(DisplayTextAnimation(visibleLength, text));
    }

    IEnumerator DisplayTextAnimation(float visibleLength, string text)
    {
        float startAlpha = canvasGroup.alpha;
        float startTime = Time.time;
        if (isDiplayingText)
        {
            while(Time.time < startTime + (0.5f + Time.deltaTime))
            {
                yield return new WaitForEndOfFrame();
                canvasGroup.alpha = Mathf.Lerp(startAlpha, 0, (Time.time - startTime) / 0.5f);
            }
        }

        isDiplayingText = true;
        startAlpha = canvasGroup.alpha;
        startTime = Time.time;
        this.text.text = text;
        while (Time.time < startTime + (0.5f + Time.deltaTime))
        {
            yield return new WaitForEndOfFrame();
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 1, (Time.time - startTime) / 0.5f);
        }

        yield return new WaitForSeconds(visibleLength);

        startAlpha = canvasGroup.alpha;
        startTime = Time.time;
        this.text.text = text;
        while (Time.time < startTime + (0.5f + Time.deltaTime) * startAlpha)
        {
            yield return new WaitForEndOfFrame();
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0, (Time.time - startTime) / 0.5f * startAlpha);
        }
        isDiplayingText = false;
    }
}