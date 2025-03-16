using System;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        DialogueManager.instance.StartDialogue(new Dialogue
        {
            sentences = new [] 
            {
                new DialogueData
                {
                    name = "Bela",
                    sentence = "You be evicted!"
                }
            }
        });
        DialogueManager.instance.OnDialogueEnded += OnDialogueEnded;
    }
    
    private void OnDialogueEnded()
    {
        GameManager.instance.Fin();
    }

    private void OnDestroy()
    {
        DialogueManager.instance.OnDialogueEnded -= OnDialogueEnded;
    }
}
