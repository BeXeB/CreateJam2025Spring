using System;
using UnityEngine;
using UnityEngine.InputSystem;

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

        playerInput = InputManager.instance.playerInput;
        instance = this;
    }
    
    public event Action<string, string> OnDialogueChanged;
    public event Action OnDialogueEnded;
    public event Action OnDialogueStarted;
    private Dialogue currentDialogue;
    private int currentSentenceIndex;
    private float cooldown = 0.1f;
    private float currentCooldown = 0;

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
        ChangeDialogue(dialogue.sentences[0].name, dialogue.sentences[0].sentence);
    }

    private void AdvanceDialogue(InputAction.CallbackContext context)
    {
        if (currentDialogue == null)
        {
            return;
        }

        if (currentCooldown > 0)
        {
            return;
        }

        currentSentenceIndex++;
        currentCooldown = cooldown;
        if (currentSentenceIndex < currentDialogue.sentences.Length)
        {
            var currentSentence = currentDialogue.sentences[currentSentenceIndex];
            ChangeDialogue(currentSentence.name, currentSentence.sentence);
            return;
        }

        EndDialogue();
    }

    private void Update()
    {
        currentCooldown -= Time.deltaTime;
    }

    private void ChangeDialogue(string name, string dialogue)
    {
        OnDialogueChanged?.Invoke(name, dialogue);
    }

    private void EndDialogue()
    {
        playerInput.Player.Enable();
        playerInput.Dialogue.Disable();
        currentDialogue = null;
        OnDialogueEnded?.Invoke();
    }
}

[Serializable]
public class Dialogue
{
    public DialogueData[] sentences;
}

[Serializable]
public struct DialogueData
{
    public string name;
    [TextArea(3, 10)]
    public string sentence;
}