using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    private PlayerInput playerInput;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        playerInput = new PlayerInput();
        instance = this;
    }

    public event Action<string, string> OnDialogueChanged;
    public event Action OnDialogueEnded;
    public event Action OnDialogueStarted;
    private Dialogue currentDialogue;
    private int currentSentenceIndex;

    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.Dialogue.AdvanceDialogue.performed += AdvanceDialogue;
    }

    private void OnDisable()
    {
        playerInput.Dialogue.AdvanceDialogue.performed -= AdvanceDialogue;
        playerInput.Disable();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        playerInput.Player.Disable();
        playerInput.Dialogue.Enable();
        currentDialogue = dialogue;
        currentSentenceIndex = 0;
        OnDialogueStarted?.Invoke();
        ChangeDialogue(dialogue.name, dialogue.sentences[0]);
    }

    private void AdvanceDialogue(InputAction.CallbackContext context)
    {
        if (currentDialogue == null)
        {
            return;
        }

        currentSentenceIndex++;
        if (currentSentenceIndex < currentDialogue.sentences.Length)
        {
            ChangeDialogue(currentDialogue.name, currentDialogue.sentences[currentSentenceIndex]);
            return;
        }

        EndDialogue();
    }

    private void ChangeDialogue(string name, string dialogue)
    {
        OnDialogueChanged?.Invoke(name, dialogue);
    }

    private void EndDialogue()
    {
        playerInput.Player.Enable();
        playerInput.Dialogue.Disable();
        OnDialogueEnded?.Invoke();
    }
}

[Serializable]
public class Dialogue
{
    public string name;
    [TextArea(3, 10)] public string[] sentences;
}