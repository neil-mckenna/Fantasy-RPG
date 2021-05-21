using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CharStats[] playerStats;

    public bool gameMenuOpen, dialogueActive, fadingBetweenAreas, shopActive, battleActive;
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
        if(gameMenuOpen || dialogueActive || fadingBetweenAreas || shopActive || battleActive)
        {
            PlayerController.playerInstance.canMove = false;
        }
        else
        {
            PlayerController.playerInstance.canMove = true;
        }

        if(Input.GetKeyDown(KeyCode.O))
        {
            Debug.LogWarning("O was hit");
            SaveData();
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            Debug.LogWarning("P was hit");
            LoadData();
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
                
            }
        }

    }

    public int FindIndexOfString(string itemName)
    {
        int itemPosition = 0;

        for(int i = 0; i < itemsHeld.Length; i++)
        {
            if(itemsHeld[i] == itemName)
            {
                
                itemPosition = i;
                i = itemsHeld.Length;

                return itemPosition;
            }
        }

        return 0;

    }

    public void SaveData()
    {
        PlayerPrefs.SetString("Current_Scene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("Player_Position_x", PlayerController.playerInstance.transform.position.x);
        PlayerPrefs.SetFloat("Player_Position_y", PlayerController.playerInstance.transform.position.y);
        PlayerPrefs.SetFloat("Player_Position_z", PlayerController.playerInstance.transform.position.z);

        // save character stats
        for(int i = 0; i < playerStats.Length; i++)
        {
            if(playerStats[i].gameObject.activeInHierarchy)
            {
                PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_active", 1);
            }
            else
            {
                PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_active", 0);
            }

            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_Level", playerStats[i].playerLevel);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_currentExp", playerStats[i].currentExp);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_currentHP", playerStats[i].currentHP);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_maxHP", playerStats[i].maxHP);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_currentMP", playerStats[i].currentMP);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_maximumMP", playerStats[i].maximumMP);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_Strength", playerStats[i].strength);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_Defense", playerStats[i].defense);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_wpnPower", playerStats[i].wpnPower);
            PlayerPrefs.SetInt("Player_" + playerStats[i].charName + "_armorPower", playerStats[i].armorPower);
            PlayerPrefs.SetString("Player_" + playerStats[i].charName + "_equippedWpn", playerStats[i].equippedWpn);
            PlayerPrefs.SetString("Player_" + playerStats[i].charName + "_equippedArmor", playerStats[i].equippedArmor);

        }

        // store inventory data
        for(int i = 0; i < itemsHeld.Length; i++)
        {
            PlayerPrefs.SetString("ItemInInventory_" + i, itemsHeld[i]);
            PlayerPrefs.SetInt("ItemAmount_" + i, numOfItems[i]);
        }


    }

    public void LoadData()
    {
        // the scene
        SceneManager.LoadScene(PlayerPrefs.GetString("Current_Scene"));

        // player position in world
        PlayerController.playerInstance.transform.position = new Vector3(
            PlayerPrefs.GetFloat("Player_Position_x"),
            PlayerPrefs.GetFloat("Player_Position_y"),
            PlayerPrefs.GetFloat("Player_Position_z")
        );

        // character data
        for(int i = 0 ; i < playerStats.Length; i++)
        {
            if(PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_active") == 0)
            {
                playerStats[i].gameObject.SetActive(false);
            }
            else
            {
                playerStats[i].gameObject.SetActive(true);
            }

            playerStats[i].playerLevel = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_Level");
            playerStats[i].currentExp = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_currentExp");
            playerStats[i].currentHP = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_currentHP");
            playerStats[i].maxHP =PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_maxHP");
            playerStats[i].currentMP = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_currentMP");
            playerStats[i].maximumMP = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_maximumMP");
            playerStats[i].strength = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_Strength");
            playerStats[i].defense =PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_Defense");
            playerStats[i].wpnPower = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_wpnPower");
            playerStats[i].armorPower = PlayerPrefs.GetInt("Player_" + playerStats[i].charName + "_armorPower");
            playerStats[i].equippedWpn = PlayerPrefs.GetString("Player_" + playerStats[i].charName + "_equippedWpn");
            playerStats[i].equippedArmor = PlayerPrefs.GetString("Player_" + playerStats[i].charName + "_equippedArmor");

        }

        // Inventory data
        for(int i = 0; i < itemsHeld.Length; i++)
        {
            itemsHeld[i] = PlayerPrefs.GetString("ItemInInventory_" + i);
            numOfItems[i] = PlayerPrefs.GetInt("ItemAmount_" + i);
        }

    }

}
