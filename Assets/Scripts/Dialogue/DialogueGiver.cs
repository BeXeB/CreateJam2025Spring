using UnityEngine;

public class DialogueGiver : MonoBehaviour, IInteractable
{
    public Dialogue dialogue;
    
    public void Interact(GameObject player)
    {
        DialogueManager.instance.StartDialogue(dialogue);
    }
}
