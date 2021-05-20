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

    public Item selectedItem;
    public Text buyItemName, buyItemDesc, buyItemValue;
    public Text sellItemName, sellItemDesc, sellItemValue;

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
        buyItemsButtons[0].Press();

        buyMenu.SetActive(true);
        sellMenu.SetActive(false);

        ShowBuyItems();
    }

    private void ShowBuyItems()
    {
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

        ShowSellItems();

    }

    private void ShowSellItems()
    {
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

    public void SelectBuyItem(Item buyItem)
    {
        if(buyItem != null)
        {
            selectedItem = buyItem;
            buyItemName.text = selectedItem.itemName;
            buyItemDesc.text = selectedItem.description;
            buyItemValue.text = "Value: " + selectedItem.value.ToString() + "g";

        }
        
    }

    public void SelectSellItem(Item sellItem)
    {
        if(sellItem != null)
        {
            
            selectedItem = sellItem;
            sellItemName.text = selectedItem.itemName;
            sellItemDesc.text = selectedItem.description;
            sellItemValue.text = "Value: " + Mathf.FloorToInt(selectedItem.value * 0.6f).ToString() + "g";

        }
    }

    

    public void BuyItem()
    {
        if(selectedItem != null)
        {
            if(GameManager.instance.currentGold >= selectedItem.value)
            {
                GameManager.instance.currentGold -= selectedItem.value;
                GameManager.instance.AddItem(selectedItem.itemName);
            }

            goldText.text = GameManager.instance.currentGold.ToString() + "g";

        }
    }

    public void SellItem()
    {
        if(selectedItem != null)
        {
            GameManager.instance.currentGold += Mathf.FloorToInt(selectedItem.value * 0.6f);
            
            int index = GameManager.instance.FindIndexOfString(selectedItem.itemName);
            int amountAtIndex = GameManager.instance.numOfItems[index];

            Debug.LogWarning("index location " + index + " item amount " + amountAtIndex);
            
            if(amountAtIndex > 0)
            {
                GameManager.instance.RemoveItem(selectedItem.itemName);
            
                // add selling item to end of shopkeppers array
                if(itemsForSale.Length <= 40)
                {
                    // reverse forloop to pop sold item back into sell list
                    for(int i = 0; i < itemsForSale.Length; i++)
                    {
                        if(itemsForSale[i] == selectedItem.itemName) 
                        {
                            Debug.LogWarning("got 1 already thanks"); 
                            continue; 
                        }
                        else if(itemsForSale[i] == "")
                        {
                            itemsForSale[i] = selectedItem.itemName;
                            i = itemsForSale.Length;
                        }


                    }
                }

                goldText.text = GameManager.instance.currentGold.ToString() + "g";

            }
            else
            {
                
            }
            
        }
        
        

        ShowSellItems();
        

    }



}
