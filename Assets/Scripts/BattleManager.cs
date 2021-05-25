using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    private bool battleActive;

    public GameObject battleScene;

    public Transform[] playerPositions;
    public Transform[] enemyPositions;

    public BattleChar[] playersPrefabs;
    public BattleChar[] enemyPrefabs;

    public List<BattleChar> activeBattlers = new List<BattleChar>();

    public int currentTurn;
    public bool turnWaiting;

    public GameObject uiButtonsHolder;

    public BattleMove[] movesList;
    public GameObject enemyAttackEffect;
    public DamageNumber theDamageNumber;

    public Text[] playerName, playerHP, playerMP;
    
    [Header("Normal Attacks")]
    public GameObject targetMenu;
    public BattleTargetButton[] targetButtons;

    [Header("Magic Attacks")]
    public GameObject magicMenu;
    public BattleMagicSelect[] magicButtons;

    [Header("Battle Notifcations")]
    public BattleNotification battleNotice;

    public int chanceToFlee = 35;


    // static
    public static BattleManager instance;

    private void Start() 
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
        
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            BattleStart(new string[] { "Eyeball", "Spider", "Skeleton" });
        }

        if(battleActive)
        {
            if(turnWaiting)
            {
                if(activeBattlers[currentTurn].isPlayer)
                {
                    uiButtonsHolder.SetActive(true);

                }
                else
                {
                    uiButtonsHolder.SetActive(false);

                    // enemy should attack
                    StartCoroutine(EnemyMoveCo());
                }
            }

            if(Input.GetKeyDown(KeyCode.N))
            {
                
                NextTurn();

            }

        }

    }

    public void BattleStart(string[] enemiesToSpawn)
    {
        if(!battleActive)
        {
            battleActive = true;

            GameManager.instance.battleActive = true;

            battleScene.transform.position = new Vector3(
                Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);
            battleScene.SetActive(true);

            AudioManager.instance.PlayBGM(0);

            // Instantiate player battle objects
            for(int i = 0; i < playerPositions.Length; i++)
            {
                if(GameManager.instance.playerStats[i].gameObject.activeInHierarchy)
                {
                    for(int j = 0; j < playersPrefabs.Length; j++)
                    {
                        if(playersPrefabs[j].charName == GameManager.instance.playerStats[i].charName)
                        {
                            BattleChar newPlayer = Instantiate(playersPrefabs[j],playerPositions[i].position, playerPositions[i].rotation);

                            newPlayer.transform.parent = playerPositions[i];

                            activeBattlers.Add(newPlayer);

                            CharStats thePlayer = GameManager.instance.playerStats[i];
                            activeBattlers[i].currentHp = thePlayer.currentHP;
                            activeBattlers[i].maxHP = thePlayer.maxHP;
                            activeBattlers[i].currentMP = thePlayer.currentMP;
                            activeBattlers[i].maximumMP = thePlayer.maximumMP;
                            activeBattlers[i].strength = thePlayer.strength;
                            activeBattlers[i].defense = thePlayer.defense;
                            activeBattlers[i].wpnPower = thePlayer.wpnPower;
                            activeBattlers[i].armorPower = thePlayer.armorPower;

                        }

                    }


                }
            }
            // player forloop end

            for(int i = 0; i < enemiesToSpawn.Length; i++)
            {
                if(enemiesToSpawn[i] != "")
                {
                    for(int j = 0; j < enemyPrefabs.Length; j++)
                    {
                        if(enemyPrefabs[j].charName == enemiesToSpawn[i])
                        {
                            BattleChar newEnemy = Instantiate(enemyPrefabs[j], enemyPositions[i].position, enemyPositions[i].rotation);

                            newEnemy.transform.parent = enemyPositions[i];

                            activeBattlers.Add(newEnemy);



                        }


                    }
                }


            }
            // enemy forloop end
            turnWaiting = true;
            currentTurn = Random.Range(0, activeBattlers.Count);

            UpdateUIStats();

        }// battle active

    }

    public void NextTurn()
    {
        currentTurn++;
        if(currentTurn >= activeBattlers.Count)
        {
            currentTurn = 0;
        }

        turnWaiting = true;

        UpdateBattle();
        UpdateUIStats();

    }

    public void UpdateBattle()
    {
        bool allEnemiesIsDead = true;
        bool allPlayersIsDead = true;

        for(int i = 0; i < activeBattlers.Count; i++)
        {
            if(activeBattlers[i].currentHp < 0)
            {
                activeBattlers[i].currentHp = 0;
            }

            if(activeBattlers[i].currentHp == 0)
            {
                // Handle dead battler
            }
            else  
            {// still alive
                if(activeBattlers[i].isPlayer)
                {
                    allPlayersIsDead = false;
                }
                else
                {
                    allEnemiesIsDead = false;

                }
            }

        }

        if(allEnemiesIsDead || allPlayersIsDead)
        {
            if(allEnemiesIsDead)
            {
                // end victory
            }
            else
            {
                // you dead game over ?
            }

            battleScene.SetActive(false);
            GameManager.instance.battleActive = false;
            battleActive = false;

        }
        else
        {
            while(activeBattlers[currentTurn].currentHp == 0)
            {
                
                if(currentTurn >= activeBattlers.Count)
                {
                    currentTurn = 0;
                }
                else
                {
                    currentTurn++;
                }
                
            }
        }

    }

    public IEnumerator EnemyMoveCo()
    {

        float waitTime = 2f;

        turnWaiting = false;

        yield return new WaitForSeconds(waitTime);

        EnemyAttack();

        yield return new WaitForSeconds(waitTime);

        NextTurn();


    }

    public void EnemyAttack()
    {
        List<int> players = new List<int>();

        for(int i = 0; i < activeBattlers.Count; i++)
        {
            if(activeBattlers[i].isPlayer && activeBattlers[i].currentHp > 0)
            {
                players.Add(i);
                
            }
        }

        int selectedTarget = players[Random.Range(0, players.Count)];
        
        int selectAttack = Random.Range(0, activeBattlers[currentTurn].movesAvailable.Length);
        int movePower = 0;
        for(int i = 0;i < movesList.Length; i++)
        {
            
            if(movesList[i].moveName == activeBattlers[currentTurn].movesAvailable[selectAttack])
            {
                
                Instantiate(movesList[i].theEffect, activeBattlers[selectedTarget].transform.position, activeBattlers[selectedTarget].transform.rotation);
                movePower = movesList[i].movePower;
            }
        }

        if(enemyAttackEffect != null)
        {
            Instantiate(enemyAttackEffect, activeBattlers[currentTurn].transform.position, activeBattlers[currentTurn].transform.rotation);
        }
        

        DealDamage(selectedTarget, movePower);
 
    }

    public void DealDamage(int target, int movePower)
    {
        float atkPower = activeBattlers[currentTurn].strength + activeBattlers[currentTurn].wpnPower;
        float defPower = activeBattlers[target].defense + activeBattlers[target].armorPower;

        float damageCalc = (atkPower / defPower) * movePower * Random.Range(0.9f, 1.1f);
        int damageToGive = Mathf.RoundToInt(damageCalc);

        //Debug.LogWarning(activeBattlers[currentTurn].charName + "is dealing (" + damageToGive + ") + damage to " + activeBattlers[target].charName);

        activeBattlers[target].currentHp -= damageToGive;

        Instantiate(theDamageNumber, activeBattlers[target].transform.position, activeBattlers[target].transform.rotation).SetDamage(damageToGive);

        UpdateUIStats();
    }

    public void UpdateUIStats()
    {
        for(int i = 0; i < playerName.Length; i++)
        {
            if(activeBattlers.Count > i)
            {
                if(activeBattlers[i].isPlayer)
                {
                    BattleChar playerData = activeBattlers[i];

                    playerName[i].gameObject.SetActive(true);
                    playerName[i].text = playerData.charName;

                    playerHP[i].text =  Mathf.Clamp(playerData.currentHp, 0 , int.MaxValue) + "/" + playerData.maxHP;
                    playerMP[i].text = Mathf.Clamp(playerData.currentMP, 0, int.MaxValue) + "/" + playerData.maximumMP;
                }
                else
                {
                    playerName[i].gameObject.SetActive(false);
                }

            }
            else
            {
                playerName[i].gameObject.SetActive(false);


            }

        }

    }

    public void PlayerAttack(string moveName , int selectedTarget)
    {

        //int selectedTarget = 2;

        int movePower = 0;
        for(int i = 0;i < movesList.Length; i++)
        {
            if(movesList[i].moveName == moveName)
            {
                Instantiate(movesList[i].theEffect, activeBattlers[selectedTarget].transform.position, activeBattlers[selectedTarget].transform.rotation);
                movePower = movesList[i].movePower;
            }
        }

        DealDamage(selectedTarget, movePower);

        if(enemyAttackEffect != null)
        {
            Instantiate(enemyAttackEffect, activeBattlers[currentTurn].transform.position, activeBattlers[currentTurn].transform.rotation);
        }

        uiButtonsHolder.SetActive(false);
        targetMenu.SetActive(false);

        NextTurn();

    }

    public void OpenTargetMenu(string moveName)
    {
        targetMenu.SetActive(true);
        
        List<int> enemies = new List<int>();

        for(int i = 0; i < activeBattlers.Count; i++)
        {
            if(!activeBattlers[i].isPlayer)
            {
                enemies.Add(i);
            }
        }

        for(int j = 0; j < targetButtons.Length; j++)
        {
            if(enemies.Count > j)
            {
                targetButtons[j].gameObject.SetActive(true);
                targetButtons[j].moveName = moveName;
                targetButtons[j].activeBattlerTarget = enemies[j];
                targetButtons[j].targetName.text = activeBattlers[enemies[j]].charName;

            }
            else
            {
                targetButtons[j].gameObject.SetActive(false);
            }

        }
    }

    public void OpenMagicMenu()
    {
        magicMenu.SetActive(true);

        for(int i = 0; i < magicButtons.Length; i++)
        {
            if(activeBattlers[currentTurn].movesAvailable.Length > i)
            {                
                magicButtons[i].gameObject.SetActive(true);

                magicButtons[i].spellName = activeBattlers[currentTurn].movesAvailable[i];
                magicButtons[i].nameText.text = magicButtons[i].spellName;


                for(int j = 0; j < movesList.Length; j++)
                {
                    if(movesList[j].moveName == magicButtons[i].spellName)
                    {
                        magicButtons[i].spellCost = movesList[j].moveCost;
                        magicButtons[i].costText.text = magicButtons[i].spellCost.ToString();

                    }


                }

            }
            else
            {
                magicButtons[i].gameObject.SetActive(false);
            }
            
        }
    }

    public void Flee()
    {
        int fleeSuccess = Random.Range(0,100);
        if(fleeSuccess < chanceToFlee)
        {
            // end battle
            battleActive = false;
            battleScene.SetActive(false);
        }
        else
        {
            NextTurn();
            battleNotice.theText.text = "Couldn't escape!";
            battleNotice.Activate();

        }
        


    }


}
