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
                    name = "",
                    sentence = "You try to enter your home, the key doesnt work"
                },
                new DialogueData
                {
                    name = "",
                    sentence = "You try again and again to no avail"
                },
                new DialogueData
                {
                    name = "Landlord",
                    sentence = "Sorry lad, I had to change the locks, you haven't paid this month's rent"
                },
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
