using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMarker : MonoBehaviour
{
    public string questToMark;
    public bool markComplete;
    public bool markOnEnter;

    public bool deactivateOnMarking;
    private bool canMark;


    private void Update() 
    {
        if(canMark && Input.GetButtonDown("Fire1"))
        {
            canMark = false;
            MarkQuest();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            if(markOnEnter)
            {
                MarkQuest();


            }
            else
            {
                canMark = true;

            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            canMark = false;
        }
        
    }

    public void MarkQuest()
    {
        if(markComplete)
        {
            QuestManager.instance.MarkQuestComplete(questToMark);
        }
        else
        {
            QuestManager.instance.MarkQuestIncomplete(questToMark);
        }

        gameObject.SetActive(!deactivateOnMarking);

    }

}
