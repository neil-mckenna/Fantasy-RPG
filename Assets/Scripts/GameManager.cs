using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CharStats[] playerStats;

    public bool gameMenuOpen, dialogueActive, fadingBetweenAreas;    

    private void Awake() 
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
        
    }

    private void Update() 
    {
        if(gameMenuOpen || dialogueActive || fadingBetweenAreas)
        {
            PlayerController.playerInstance.canMove = false;
        }
        else
        {
            PlayerController.playerInstance.canMove = true;
        }


        
    }




}
