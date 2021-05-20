using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    [Header("These Settings vary the auto generation for stats")]    
    [SerializeField] [Range(1.01f, 1.9f)]float playerExpToLevelModifier = 1.2f;
    [SerializeField] [Range(1.01f, 1.9f)]float playerMPToLevelModifier = 1.2f;

    public string charName;
    public int playerLevel = 1;
    public int currentExp;
    public int[] expToNextLevel; 
    public int maxLevel = 99;
    public int baseExp = 1000;
    public int currentHP;
    public int maxHP = 100;
    public int currentMP;
    public int baseMP = 10;
    public int maximumMP = 50;
    public int[] mpLevelBonus;
    public int strength;
    public int defense;
    public int wpnPower;
    public int armorPower;
    public string equippedWpn;
    public string equippedArmor;
    public Sprite charImage;

    private void Start() 
    {
        expToNextLevel = new int[maxLevel];
        expToNextLevel[1] = baseExp;

        // Autogenerate xp stats for 99 levels 
        for (int i = 2; i < maxLevel; i++)
        {
            expToNextLevel[i] =  expToNextLevel[i - 1] + ((int)(baseExp * Mathf.Pow(1.05f, playerExpToLevelModifier)));
            
        } 

        // Autogenerate mp stats for 99 levels
        mpLevelBonus = new int[maxLevel]; 
        mpLevelBonus[0] = baseMP;
        mpLevelBonus[1] = baseMP + 2;
        
        for (int i = 2; i < maxLevel; i++)
        {
            mpLevelBonus[i] += mpLevelBonus[i - 1] + Mathf.FloorToInt(baseMP * playerMPToLevelModifier);
        }            
    }

    private void Update() 
    {
        if(Input.GetKey(KeyCode.L))
        {
            if(playerLevel < maxLevel)
            {
                AddExp(2000);
            }
            else
            {
                Debug.LogWarning("Experience has exceeded the maximum level");
            }       
        }
        
    }

    public void AddExp(int expToAdd)
    {
        currentExp += expToAdd;

        if(playerLevel <= maxLevel)
        {
            if(currentExp >= expToNextLevel[playerLevel])
            {
                // Leveling up
                currentExp -= expToNextLevel[playerLevel];
                
                playerLevel++;

                // determine whether to add str or def based on odd or even
                if(playerLevel % 2 == 0)
                {
                    strength++;
                }
                else
                {
                    defense++;
                }
                
                maxHP = Mathf.FloorToInt(maxHP * 1.05f);
                currentHP = maxHP;

                Debug.LogWarning(playerLevel);
                maximumMP = mpLevelBonus[playerLevel - 1];
                currentMP = maximumMP;

            }
        }
        else if(playerLevel >= maxLevel)
        {
            playerLevel = maxLevel;
            currentExp = 0;
            maximumMP = mpLevelBonus[maxLevel];

        }

    }



}
