using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EssentialsLoader : MonoBehaviour
{
    public GameObject UIScreen;
    public GameObject player;
    public GameManager gameManager;

    private void Start() 
    {
        if(UIFade.instance == null)
        {
            Instantiate(UIScreen);
        }

        if(PlayerController.playerInstance == null)
        {
            PlayerController clone = Instantiate(player).GetComponent<PlayerController>();
            PlayerController.playerInstance = clone;
        }

        if(GameManager.instance == null)
        {
            Instantiate(gameManager);

        }
        
    }


}
