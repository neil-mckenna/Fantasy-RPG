using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMagicSelect : MonoBehaviour
{
    public string spellName;
    public int spellCost;
    public Text nameText;
    public Text costText;

    public void Press()
    {
        Debug.Log("Current mana " + BattleManager.instance.activeBattlers[BattleManager.instance.currentTurn].currentMP);


        if(BattleManager.instance.activeBattlers[BattleManager.instance.currentTurn].currentMP >= spellCost)
        {
            BattleManager.instance.magicMenu.SetActive(false);
            BattleManager.instance.OpenTargetMenu(spellName);
            BattleManager.instance.activeBattlers[BattleManager.instance.currentTurn].currentMP -= spellCost;

        }
        else
        {
            

            BattleManager.instance.battleNotification.theText.text = "Not Enough MP!";
            BattleManager.instance.battleNotification.Activate();
            BattleManager.instance.magicMenu.gameObject.SetActive(false);

        }


        



    }
}
