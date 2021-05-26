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


        rewardScreen.SetActive(false);

    }

}
