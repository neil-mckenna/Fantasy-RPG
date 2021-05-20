using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject shopMenu;
    public GameObject buyMenu;
    public GameObject sellMenu;

    public Text goldText;

    public string[] itemsForSale;

    public ItemButton[] buyItemsButtons;
    public ItemButton[] sellItemsButtons;

    // static instance
    public static Shop instance;

    private void Start() 
    {
        instance = this;
        
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.F) && !shopMenu.activeInHierarchy)
        {
            Debug.Log("f was hit");
            OpenShop();

        }
        
    }

    public void OpenShop()
    {
        shopMenu.SetActive(true);
        OpenBuyMenu();

        GameManager.instance.shopActive = true;

        goldText.text = GameManager.instance.currentGold.ToString() + "g";
    }

    public void CloseShop()
    {
        shopMenu.SetActive(false);
        GameManager.instance.shopActive = false;
    }

    public void OpenBuyMenu()
    {
        buyMenu.SetActive(true);
        sellMenu.SetActive(false);

        for(int i = 0; i < buyItemsButtons.Length; i++)
        {
            buyItemsButtons[i].buttonValue = i;

            if(itemsForSale[i] != "")
            {
                buyItemsButtons[i].buttonImage.gameObject.SetActive(true);
                buyItemsButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(itemsForSale[i]).itemSprite;
                buyItemsButtons[i].amountText.text = "";
            }
            else
            {
                buyItemsButtons[i].buttonImage.gameObject.SetActive(false);
                buyItemsButtons[i].buttonImage.sprite = null ;
                buyItemsButtons[i].amountText.text = "";
            }

        }

    }

    public void OpenSellMenu()
    {
        buyMenu.SetActive(false);
        sellMenu.SetActive(true);

        GameManager.instance.SortItems();

        for(int i = 0; i < sellItemsButtons.Length; i++)
        {
            sellItemsButtons[i].buttonValue = i;

            if(GameManager.instance.itemsHeld[i] != "")
            {
                sellItemsButtons[i].buttonImage.gameObject.SetActive(true);
                sellItemsButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
                sellItemsButtons[i].amountText.text = GameManager.instance.numOfItems[i].ToString();
            }
            else
            {
                sellItemsButtons[i].buttonImage.gameObject.SetActive(false);
                sellItemsButtons[i].amountText.text = "";
            }

        }

    }



}
