using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;

    public void PlayDialogue()
    {
        DialogueManager.instance.StartDialogue(dialogue);
        gameObject.SetActive(false);
    }
}
