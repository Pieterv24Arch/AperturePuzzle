using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Image whiteScreen;
    public Image deathScreen;

    private float targetDamage = 0;
    private float currentDamage = 0;

    void Awake()
    {
        instance = this;
    }

    IEnumerator Start()
    {
        whiteScreen.enabled = true;
        whiteScreen.CrossFadeAlpha(1, 0, true);
        yield return new WaitForSeconds(0.5f);
        whiteScreen.CrossFadeAlpha(0, 1, true);
    }

    void Update()
    {
        if(!Mathf.Approximately(currentDamage + targetDamage, 0))
        {
            currentDamage = Mathf.Lerp(currentDamage, targetDamage, Time.deltaTime * 40f);
            targetDamage = Mathf.Lerp(targetDamage, 0, Time.deltaTime * 2f);

            deathScreen.color = new Color(1, 0.4f, 0, currentDamage);
        }
    }

    public void SetWhiteScreen(bool active, float fadeSpeed = 1f)
    {
        whiteScreen.CrossFadeAlpha(active ? 1 : 0, fadeSpeed, true);
    }

    public void GetHit()
    {
        targetDamage = 1;
    }
}
