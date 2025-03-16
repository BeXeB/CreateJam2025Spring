using System;
using UnityEngine;

public class Scene4 : MonoBehaviour
{
    public static Scene4 instance;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    [SerializeField] private Dialogue startDialogue;

    private void Start()
    {
        DialogueManager.instance.StartDialogue(startDialogue);
        DialogueManager.instance.OnDialogueEnded += InstanceOnOnDialogueEnded;
    }

    private void InstanceOnOnDialogueEnded()
    {
        GameManager.instance.LoadNext();
    }

    private void OnDestroy()
    {
        DialogueManager.instance.OnDialogueEnded -= InstanceOnOnDialogueEnded;
    }
}
