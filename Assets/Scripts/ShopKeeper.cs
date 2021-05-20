using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    private bool canOpen;
    public string[] itemsForSale = new string[40];

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            canOpen = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D other) 
    {

        if(other.CompareTag("Player"))
        {
            canOpen = false;
        }

    }

    private void Update() 
    {
        if(canOpen && Input.GetButtonDown("Fire1") && PlayerController.playerInstance.canMove && !Shop.instance.shopMenu.activeInHierarchy)
        {
            Shop.instance.itemsForSale = itemsForSale;

            Shop.instance.OpenShop();

        }



        
    }

}
