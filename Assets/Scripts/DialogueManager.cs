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

    public static DialogueManager dialogueInstance;
    private bool justStarted = false;

    private void Start() 
    {
        dialogueInstance = this;
        
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

                        PlayerController.playerInstance.canMove = true;
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

        PlayerController.playerInstance.canMove = false;
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


}
