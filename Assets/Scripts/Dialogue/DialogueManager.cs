using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    
    public event Action<string> OnDialogueChanged;
    public event Action OnDialogueEnded;
    private Dialogue currentDialogue;
    private int currentSentenceIndex;
    
    public void StartDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        currentSentenceIndex = 0;
        ChangeDialogue(dialogue.sentences[0]);
    }
    
    public void AdvanceDialogue()
    {
        if (currentDialogue == null)
        {
            return;
        }
        
        currentSentenceIndex++;
        if (currentSentenceIndex < currentDialogue.sentences.Length)
        {
            ChangeDialogue(currentDialogue.sentences[currentSentenceIndex]);
            return;
        }
        EndDialogue();
    }
    
    public void ChangeDialogue(string dialogue)
    {
        OnDialogueChanged?.Invoke(dialogue);
    }
    
    public void EndDialogue()
    {
        OnDialogueEnded?.Invoke();
    }
}

[Serializable]
public class Dialogue
{
    public string name;
    [TextArea(3, 10)]
    public string[] sentences;
}