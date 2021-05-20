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

    }

    public void OpenSellMenu()
    {
        buyMenu.SetActive(false);
        sellMenu.SetActive(true);

    }



}
