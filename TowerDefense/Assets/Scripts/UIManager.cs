using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Image whiteScreen;

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

    public void SetWhiteScreen(bool active, float fadeSpeed = 1f)
    {
        whiteScreen.CrossFadeAlpha(active ? 1 : 0, fadeSpeed, true);
    }
}
