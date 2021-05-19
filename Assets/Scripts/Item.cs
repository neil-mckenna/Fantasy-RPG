using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item Type")]
    public bool isItem;
    public bool isWeapon;
    public bool isArmor;
    [Header("Item Details")]
    public string itemName;
    public string description;
    public int value;
    public Sprite itemSprite;
    [Header("Item Details")]
    public int amountToChange;
    public bool affectHP, affectMP, affectStr;
    [Header("Weapon/Armor Details")]
    public int weaponStrength;
    public int armorStrength;










}
