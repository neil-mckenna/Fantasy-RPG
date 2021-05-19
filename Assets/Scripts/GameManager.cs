using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CharStats[] playerStats;

    public bool gameMenuOpen, dialogueActive, fadingBetweenAreas;
    public string[] itemsHeld;
    public int[] numOfItems;
    public Item[] referenceItems;


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

    public Item GetItemDetails(string itemToGrab)
    {
        for(int i = 0; i < referenceItems.Length; i++)
        {
            if(referenceItems[i].itemName == itemToGrab)
            {
                Debug.LogWarning("Sucessful: ref id " + referenceItems[i] + "itemName" + referenceItems[i].itemName);
                return referenceItems[i];
                
            }
        }

        return null;
    }




}
