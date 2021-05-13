using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    public string transitionName;

    private void Start() 
    {
        Vector2 defaultStartPos = new Vector2(0,0); 


        if(PlayerController.playerInstance != null)
        {
            if(transitionName == PlayerController.playerInstance.areaTransitionName)
            {
                PlayerController.playerInstance.transform.position = transform.position;


            }
            else
            {
                
                defaultStartPos = transform.position;
            }

        }
        

        
    } 

}
