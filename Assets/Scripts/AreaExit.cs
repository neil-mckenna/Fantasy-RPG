using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AreaExit : MonoBehaviour
{
    public string areaToLoad;
    public string areaTransitionGoToName;

    public AreaEntrance theEntrance;

    private WaitForSeconds waitTime;
    public float waitToLoad = 1f;

    private void Start() 
    {
        theEntrance.transitionName = areaTransitionGoToName;
        waitTime = new WaitForSeconds(waitToLoad);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        { 
            StartCoroutine(FadeNLoad());  
        }
   
    }

    public IEnumerator FadeNLoad()
    {
        // Fade black
        UIFade.instance.FadeToBlack();

        // wait
        yield return waitTime;

        // load scene
        SceneManager.LoadScene(areaToLoad);

        // move player to entrance location
        PlayerController.playerInstance.areaTransitionName = areaTransitionGoToName;

        // fade back in
        UIFade.instance.FadeFromBlack();

    }


}
