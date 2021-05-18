using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    public string charName;
    public int playerLevel = 1;
    public int currentExp;
    public int[] expToNextLevel; 
    public int maxLevel = 99;
    public int baseExp = 1000;
    public float playerExpToLevelModifier = 1.2f;




    public int currentHP;
    public int maxHP = 100;
    public int currentMP;
    public int maximumMP = 50;
    public int strength;
    public int defense;
    public int wpnPower;
    public int armourPower;
    public string equippedWpn;
    public string equippedPower;
    public Sprite charImage;

    private void Start() 
    {
        expToNextLevel = new int[maxLevel];
        expToNextLevel[1] = baseExp;

        for (int i = 2; i < maxLevel; i++)
        {
            expToNextLevel[i] =  expToNextLevel[i - 1] + ((int)(baseExp * Mathf.Pow(1.05f, playerExpToLevelModifier)));
            
        }        
    }

    private void Update() 
    {
        if(Input.GetKey(KeyCode.K))
        {
            if(playerLevel <= maxLevel)
            {
                AddExp(2000);
            }

            
        }

        Debug.LogWarning("currentExp / " + currentExp + "   playerLevel " + playerLevel);
        
    }

    public void AddExp(int expToAdd)
    {
        currentExp += expToAdd;

        if(playerLevel < maxLevel)
        {
            if(currentExp > expToNextLevel[playerLevel])
            {
                currentExp -= expToNextLevel[playerLevel];
                
                playerLevel++;
            }
        }

    }



}
