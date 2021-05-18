using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    public Enums.EntranceType entranceName;

    private void Start() 
    {
        Vector2 defaultStartPos = new Vector2(0,0); 


        if(PlayerController.playerInstance != null)
        {
            if(entranceName.ToString() == PlayerController.playerInstance.areaTransitionName)
            {
                PlayerController.playerInstance.transform.position = transform.position;
                Debug.Log("Spawned at entrance " + transform.position);
            }
            else
            {
                defaultStartPos = transform.position;
                Debug.Log("Spawned at default " + transform.position);
            }

        }

    } 

}
