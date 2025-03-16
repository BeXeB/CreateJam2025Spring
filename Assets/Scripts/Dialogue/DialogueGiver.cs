using System;
using UnityEngine;

public class DialogueGiver : MonoBehaviour, IInteractable
{
    public Dialogue dialogue;
    
    public void Interact(GameObject player)
    {
        OnInteracted?.Invoke();
        DialogueManager.instance.StartDialogue(dialogue);
    }

    public event Action OnInteracted;
}
