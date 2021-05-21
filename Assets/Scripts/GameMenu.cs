using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public GameObject theMenu;
    public GameObject[] windows;
    public GameObject[] statusButtons;
    public Text statusName, statusHP, statusMP, statusStrength, statusDefence, statusEquippedWpn, statusWpnPower, statusEquippedArmor, statusArmorPower, statusExpToNextLevel;
    public Image statusImage;
    private CharStats[] playerStats;
    [SerializeField] Text[] nameText, hpText, mpText, lvlText, expText;
    public Slider[] expSlider;
    public Image[] charImage;
    public GameObject[] chatStatHolder;
    public ItemButton[] itemButtons;
    public string selectedItem;
    public Item activeItem;
    public Text itemName, itemDescription, useButtonText;

    public GameObject itemCharChoiceMenu;
    public Text[] itemCharChoiceNames;

    public Text goldText;

    // static instance
    public static GameMenu instance;
    

    
    private void Start() 
    {
        instance = this;



        theMenu.SetActive(false);
        
    }

    private void Update() 
    {
        if(Input.GetButtonDown("Fire2"))
        {
            if(theMenu.activeInHierarchy)
            {
                CloseMenu();
            }
            else
            {
                theMenu.SetActive(true);
                UpdateMainStats();
                GameManager.instance.gameMenuOpen = true;
            }

            AudioManager.instance.PlaySFX(5);
        }
    }

    public void UpdateMainStats()
    {
        playerStats = GameManager.instance.playerStats;

        for(int i = 0; i < playerStats.Length; i++)
        {
            if(playerStats[i].gameObject.activeInHierarchy)
            {
                chatStatHolder[i].SetActive(true);

                nameText[i].text = playerStats[i].charName;
                hpText[i].text = "HP: " + playerStats[i].currentHP + "/" + playerStats[i].maxHP;
                mpText[i].text = "MP: " + playerStats[i].currentMP + "/" + playerStats[i].maximumMP;
                lvlText[i].text = "Lvl: " + playerStats[i].playerLevel;
                expText[i].text = "" + playerStats[i].currentExp + "/" + playerStats[i].expToNextLevel[playerStats[i].playerLevel];
                expSlider[i].maxValue = playerStats[i].expToNextLevel[playerStats[i].playerLevel];
                expSlider[i].value = playerStats[i].currentExp;
                charImage[i].sprite = playerStats[i].charImage;

            }
            else
            {
                chatStatHolder[i].SetActive(false);
            }
        }

        goldText.text = GameManager.instance.currentGold.ToString() + "g";



    }

    public void ToggleWindow(int windowNum)
    {
        UpdateMainStats();

        for(int i = 0; i < windows.Length; i++)
        {
            if(i == windowNum)
            {
                windows[i].SetActive(!windows[i].activeInHierarchy);
            }
            else
            {
                windows[i].SetActive(false);
            }
        }

        itemCharChoiceMenu.SetActive(false);
    }

    public void CloseMenu()
    {
        for(int i = 0; i < windows.Length; i++)
        {
            windows[i].SetActive(false);
        }

        theMenu.SetActive(false);
        GameManager.instance.gameMenuOpen = false;

        itemCharChoiceMenu.SetActive(false);

    }

    public void OpenStatus()
    {
        UpdateMainStats();

        // update information that is shown
        StatusCharacter(0);

        for(int i = 0; i < statusButtons.Length; i++)
        {
            statusButtons[i].SetActive(playerStats[i].gameObject.activeInHierarchy);
            statusButtons[i].GetComponentInChildren<Text>().text = playerStats[i].charName;
        }

    }

    public void StatusCharacter(int selected)
    {
        // Name
        statusName.text = playerStats[selected].charName;
        
        //health and mana
        statusHP.text = "" + playerStats[selected].currentHP + "/" + playerStats[selected].maxHP;
        statusMP.text = "" + playerStats[selected].currentMP + "/" + playerStats[selected].maximumMP;
        
        // Attributes
        statusStrength.text = playerStats[selected].strength.ToString();
        statusDefence.text = playerStats[selected].defense.ToString();

        // Weapon
        if(playerStats[selected].equippedWpn != "")
        {
            statusEquippedWpn.text = playerStats[selected].equippedWpn.ToString();
        }
        else
        {
            statusEquippedWpn.text = "None";
        }
        statusWpnPower.text = playerStats[selected].wpnPower.ToString();

        // Armour
        if(playerStats[selected].equippedArmor != "")
        {
            statusEquippedArmor.text = playerStats[selected].equippedArmor.ToString();
        }
        else
        {
            statusEquippedArmor.text = "None";
        }

        statusArmorPower.text = playerStats[selected].armorPower.ToString();

        // xp
        statusExpToNextLevel.text = (playerStats[selected].expToNextLevel[playerStats[selected].playerLevel] - playerStats[selected].currentExp).ToString();

        // character image
        statusImage.sprite = playerStats[selected].charImage;

    }

    public void ShowItems()
    {
        GameManager.instance.SortItems();
        
        for(int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].buttonValue = i;

            if(GameManager.instance.itemsHeld[i] != "")
            {

                itemButtons[i].buttonImage.gameObject.SetActive(true);
                itemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
                itemButtons[i].amountText.text = GameManager.instance.numOfItems[i].ToString();  
            }
            else
            {
                itemButtons[i].buttonImage.gameObject.SetActive(false);
                itemButtons[i].buttonImage.sprite = null;
                itemButtons[i].amountText.text = "";        
            }

        }

    }

    public void SelectItem(Item newItem)
    {
        activeItem = newItem;

        if(activeItem.isItem)
        {
            useButtonText.text = "Use";
        }

        if(activeItem.isWeapon || activeItem.isArmor)
        {
            useButtonText.text = "Equip";
        }

        itemName.text = activeItem.itemName;
        itemDescription.text = activeItem.description;

    }

    public void DiscardItem()
    {
        if(activeItem != null)
        {
            GameManager.instance.RemoveItem(activeItem.itemName);

        }
    }

    public void OpenItemCharChoice()
    {
        itemCharChoiceMenu.SetActive(true);

        for(int i = 0; i < itemCharChoiceNames.Length; i++)
        {
            itemCharChoiceNames[i].text = GameManager.instance.playerStats[i].charName;
            itemCharChoiceNames[i].transform.parent.gameObject.SetActive(GameManager.instance.playerStats[i].gameObject.activeInHierarchy);
        }

    }

    public void CloseItemCharChoice()
    {
        itemCharChoiceMenu.SetActive(false);

    }

    public void UseItem(int selectChar)
    {
        activeItem.Use(selectChar);
        CloseItemCharChoice();
    }

    public void SaveGame()
    {
        GameManager.instance.SaveData();
        QuestManager.instance.SaveQuestData();
    }

    // public void LoadGame()
    // {
    //     GameManager.instance.LoadData();
    //     QuestManager.instance.LoadQuestData();

    // }

    public void PlayButtonSound()
    {
        AudioManager.instance.PlaySFX(4);
    }

}
