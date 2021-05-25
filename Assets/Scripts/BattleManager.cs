using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
        //activeBattlers[selectedTarget].currentHp -= 20;

        int selectAttack = Random.Range(0, activeBattlers[currentTurn].movesAvaiable.Length);
        
        for(int i = 0;i < movesList.Length; i++)
        {
            if(movesList[i].moveName == activeBattlers[currentTurn].movesAvaiable[selectAttack])
            {
                Instantiate(movesList[i].theEffect, activeBattlers[selectedTarget].transform.position, activeBattlers[selectedTarget].transform.rotation);
                


            }


        }


        
    }
    
}
