using System;
using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private GameObject dialoguePanel;
    private DialogueManager dialogueManager;

    private void Awake()
    {
        dialogueManager = DialogueManager.instance;
    }

    private void OnEnable()
    {
        dialogueManager.OnDialogueStarted += ShowDialogue;
        dialogueManager.OnDialogueChanged += ChangeDialogue;
        dialogueManager.OnDialogueEnded += HideDialogue;
    }

    private void OnDisable()
    {
        dialogueManager.OnDialogueStarted -= ShowDialogue;
        dialogueManager.OnDialogueChanged -= ChangeDialogue;
        dialogueManager.OnDialogueEnded -= HideDialogue;
    }

    private void ShowDialogue()
    {
        dialoguePanel.SetActive(true);
    }
    
    private void HideDialogue()
    {
        dialoguePanel.SetActive(false);
    }

    private void ChangeDialogue(string name, string dialogue)
    {
        nameText.text = name;
        dialogueText.text = dialogue;
    }
}
