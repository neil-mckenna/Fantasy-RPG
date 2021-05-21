using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText = null;
    public Text nameText = null;
    public GameObject dialogueBox;
    public GameObject nameBox;

    public string[] dialogueLines;

    public int currentLine = 0;
    private bool justStarted = false;

    private string questToMark;
    private bool markQuestComplete;
    private bool shouldMarkQuest;



    // static 
    public static DialogueManager dialogueInstance;

    private void Start() 
    {
        dialogueInstance = this;
        dialogueBox.SetActive(false);
        
    }    

    // Update is called once per frame
    void Update()
    {
        if(dialogueBox.activeInHierarchy)
        {
            if(Input.GetButtonUp("Fire1"))
            {

                if(!justStarted)
                {
                    currentLine++;
                    CheckIfName();


                    if(currentLine >= dialogueLines.Length)
                    {
                        dialogueBox.SetActive(false);
                        currentLine = 0;

                        GameManager.instance.dialogueActive = false;

                        if(shouldMarkQuest)
                        {
                            shouldMarkQuest = false;
                            if(markQuestComplete)
                            {
                                QuestManager.instance.MarkQuestComplete(questToMark);
                            }
                            else
                            {
                                QuestManager.instance.MarkQuestIncomplete(questToMark);
                            }
                        }
                    }
                    else
                    {
                        CheckIfName();
                        dialogueText.text = dialogueLines[currentLine];
                    }

                }
                else
                {
                    justStarted = false;
                }
                
            }

        }    
    }

    public void ShowDialogue(string[] newLines, bool isPerson)
    {

        dialogueLines = newLines;

        currentLine = 0;

        CheckIfName();

        if(currentLine >= dialogueLines.Length)
        {
            return;
        }

        dialogueText.text = dialogueLines[currentLine];

        dialogueBox.SetActive(true);

        justStarted = true;

        nameBox.SetActive(isPerson);

        GameManager.instance.dialogueActive = true;
    }

    public void CheckIfName()
    {
        if(currentLine >= dialogueLines.Length)
        {
            return;
        }

        if(dialogueLines[currentLine].StartsWith("n-"))
        {
            nameText.text = dialogueLines[currentLine].Replace("n-", "");
            
            currentLine++;
            
            
        }
    }

    public void ShouldActivateQuestAtEnd(string questName, bool markComplete)
    {
        questToMark = questName;
        markQuestComplete = markComplete;
        shouldMarkQuest = true;

    }


}
