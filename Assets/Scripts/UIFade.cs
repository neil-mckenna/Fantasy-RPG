using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{
    // static singleton
    public static UIFade instance;

    public Image fadeScreen = null;
    public float fadeSpeed = 5f;

    public bool shouldFadeOut = false;
    public bool shouldFadeIn = false;

    private void Start() 
    {
        instance = this;

        DontDestroyOnLoad(this);
    }

    private void Update() 
    {
        // to black
        if(shouldFadeOut)
        {
            FadeOut();
        }

        // to clear
        if(shouldFadeIn)
        {
            FadeIn();    
        }
        
    }

    // to black
    private void FadeOut()
    {
        fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
        
        if(fadeScreen.color.a == 1f)
        {
            shouldFadeOut = false;
        }
    }

    // to clear
    private void FadeIn()
    {
        fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

        if(fadeScreen.color.a == 0f)
        {
            shouldFadeIn = false;
        }
    }

    public void FadeToBlack()
    {
        shouldFadeIn = false;
        shouldFadeOut = true;
    }

    public void FadeFromBlack()
    {
        shouldFadeOut = false;
        shouldFadeIn = true;
    }

    
}
