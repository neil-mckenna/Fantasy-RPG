using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AreaExit : MonoBehaviour
{
    public Enums.SceneName SceneToLoad;
    public Enums.ExitType exitTo;
    private WaitForSeconds waitTime;
    public float waitToLoad = 1f;

    private void Start() 
    {
        // entrance = new Enums.EntranceType.Town1.ToString();
        // exitTo = new Enums.ExitType.Town1.ToString();
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
        //Disable player movement
        GameManager.instance.fadingBetweenAreas = true;

        // Fade black
        UIFade.instance.FadeToBlack();

        // wait
        yield return waitTime;

        // load scene
        SceneManager.LoadScene(SceneToLoad.ToString());

        // move player to entrance location
        PlayerController.playerInstance.areaTransitionName = exitTo.ToString();

        // fade back in
        UIFade.instance.FadeFromBlack();

        // Enable player movement
        GameManager.instance.fadingBetweenAreas = false;

    }


}
