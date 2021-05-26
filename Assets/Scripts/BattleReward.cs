using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleReward : MonoBehaviour
{
    public static BattleReward instance;
    public Text xpText, itemText;

    public GameObject rewardScreen;

    public string[] rewardsItems;
    public int xpEarned;

    public bool markQuestComplete;
    public string questToMark;



    // Start is called before the first frame update
    void Start()
    {
        instance = this;    
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            OpenRewardScreen(54, new string[] { "iron sword", "leather armor" });
        }
        
    }

    public void OpenRewardScreen(int xp, string[] rewards)
    {
        xpEarned = xp;
        rewardsItems = rewards;

        xpText.text = "Everyone earned " + xpEarned + "xp!";
        itemText.text = "";

        for(int i = 0; i < rewardsItems.Length; i++)
        {
            itemText.text += rewards[i] + "\n"; 
        }

        rewardScreen.SetActive(true);

    }

    public void CloseRewardScreen()
    {
        for(int i = 0; i < GameManager.instance.playerStats.Length; i++)
        {
            if(GameManager.instance.playerStats[i].gameObject.activeInHierarchy)
            {
                GameManager.instance.playerStats[i].AddExp(xpEarned);
            }
        }

        for(int i = 0; i < rewardsItems.Length; i++)
        {
            GameManager.instance.AddItem(rewardsItems[i]);
        }

        

        rewardScreen.SetActive(false);
        GameManager.instance.battleActive = false;

        if(markQuestComplete)
        {
            QuestManager.instance.MarkQuestComplete(questToMark);
        }

    }

}
