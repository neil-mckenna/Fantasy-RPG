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

    public int currentGold;

    private void Awake() 
    {
        instance = this;

        DontDestroyOnLoad(gameObject);

        SortItems();
        
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

        if(Input.GetKeyDown(KeyCode.J))
        {
            AddItem("Iron Armor");
            AddItem("bla bla");
        }

        if(Input.GetKeyDown(KeyCode.K))
        {
            RemoveItem("Iron Armor");
            RemoveItem("bee bop");
        }



    }

    public Item GetItemDetails(string itemToGrab)
    {
        for(int i = 0; i < referenceItems.Length; i++)
        {
            if(referenceItems[i].itemName == itemToGrab)
            {
                //Debug.LogWarning("Sucessful: ref id " + referenceItems[i] + "itemName" + referenceItems[i].itemName);
                return referenceItems[i];
                
            }
        }

        return null;
    }

    public void SortItems()
    {
        bool itemAfterSpace = true;
        
        while(itemAfterSpace)
        {   
            // 1 loop
            itemAfterSpace = false;
            for(int i = 0; i < itemsHeld.Length - 1; i++)
            {
                if(itemsHeld[i] == "")
                {
                    itemsHeld[i] = itemsHeld[i + 1];
                    itemsHeld[i + 1] = "";

                    numOfItems[i] = numOfItems[i + 1];
                    numOfItems[i + 1] = 0;

                    if(itemsHeld[i] != "")
                    {
                        // keep the loop going
                        itemAfterSpace = true;
                    }
                }
            }
        }
    }

    public void AddItem(string itemToAdd)
    {
        int newItemPosition = 0;
        bool foundSpace = false;

        for(int i = 0; i < itemsHeld.Length; i++)
        {
            if(itemsHeld[i] == "" || itemsHeld[i] == itemToAdd)
            {
                newItemPosition = i;
                i = itemsHeld.Length;
                foundSpace = true; 
            }
        }

        if(foundSpace)
        {
            bool itemExists = false;
            for(int i =0; i < referenceItems.Length; i++)
            {
                if(referenceItems[i].itemName == itemToAdd)
                {
                    itemExists = true;
                    i = referenceItems.Length;
                }
            }

            if(itemExists)
            {
                itemsHeld[newItemPosition] = itemToAdd;
                numOfItems[newItemPosition]++;
            }
            else
            {
                Debug.LogError(itemToAdd = " Item does not exist!");
            }
        }

        GameMenu.instance.ShowItems();
    }

    public void RemoveItem(string itemToRemove)
    {
        bool foundItem = false;
        int itemPosition = 0;

        for(int i = 0; i < itemsHeld.Length; i++)
        {
            if(itemsHeld[i] == itemToRemove)
            {
                
                foundItem = true;
                itemPosition = i;
                i = itemsHeld.Length;
            }

            if(foundItem)
            {

                numOfItems[itemPosition]--;

                if(numOfItems[itemPosition] <= 0)
                {
                    itemsHeld[itemPosition] = "";
                }
                GameMenu.instance.ShowItems();
            }
            // ignoring 0 as was mana potion was fine but nothing else would pass without error
            else if(itemPosition != 0)
            {
                Debug.LogError("Couldn't find " + itemToRemove);
            }
            else
            {
                // the first run check at position 0 then rechecks through correctly because not a nested loop so get a false positive
                Debug.Log("itemPosition at itemsHeld[i] " + itemPosition + " Couldn't find item " + itemToRemove + " 0 is usally mana potion");
            }




        }





    }




}
