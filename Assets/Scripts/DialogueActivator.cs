using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivator : MonoBehaviour
{
    public string[] lines;

    private bool canActivate = false;

    public bool isPerson = true;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            canActivate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            canActivate = false;
        }
        
    }

    private void Update() 
    {
        if(canActivate && Input.GetButtonDown("Fire1") && !DialogueManager.dialogueInstance.dialogueBox.activeInHierarchy)
        {
            DialogueManager.dialogueInstance.ShowDialogue(lines, isPerson);

        }




        
    }



    
}
