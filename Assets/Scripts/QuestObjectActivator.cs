using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjectActivator : MonoBehaviour
{
    public GameObject objectToActivate;
    public string questToCheck;
    public bool activeIfComplete;
    private bool initialCheckDone; 

    private void Update() 
    {
        if(!initialCheckDone)
        {
            initialCheckDone = true;

            CheckCompletion();
        }
        
    }


    public void CheckCompletion()
    {
        if(QuestManager.instance.CheckIfComplete(questToCheck))
        {
            objectToActivate.SetActive(activeIfComplete);

        }

    }



}
