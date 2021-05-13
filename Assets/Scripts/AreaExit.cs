using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AreaExit : MonoBehaviour
{
    public string areaToLoad;
    public string areaTransitionGoToName;

    public AreaEntrance theEntrance;

    private void Start() 
    {
        theEntrance.transitionName = areaTransitionGoToName;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(areaToLoad);

            PlayerController.playerInstance.areaTransitionName = areaTransitionGoToName;
        }
        
    }




}
