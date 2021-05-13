using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoader : MonoBehaviour
{
    public GameObject playerPrefab = null;

    private void Start() 
    {
        if(PlayerController.playerInstance == null)
        {
            Instantiate(playerPrefab);
        }
        
    }










}
