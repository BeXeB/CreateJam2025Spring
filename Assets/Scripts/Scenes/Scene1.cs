using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scene1 : MonoBehaviour
{
    public static Scene1 instance;
    public Dialogue tutorialDialogue;
    public Dialogue nurseDialogue;
    private bool tutorial = true;
    [SerializeField] private GameObject nurseNPC;
    [SerializeField] private GameObject nurseDoor;
    [SerializeField] private GameObject[] hospitalDoors;
    [SerializeField] private Plank plank;
    
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        DialogueManager.instance.StartDialogue(tutorialDialogue);
        DialogueManager.instance.OnDialogueEnded += OnDialogueEnded;
        GameManager.instance.player.GetComponentInChildren<Animator>().SetBool("InChair", true);
        plank.OnInteracted += OpenDoors;
    }

    private void OnDestroy()
    {
        DialogueManager.instance.OnDialogueEnded -= OnDialogueEnded;
        plank.OnInteracted -= OpenDoors;
    }

    private void OnDialogueEnded()
    {
        if (tutorial)
        {
            InputManager.instance.playerInput.Player.Disable();
            nurseDoor.SetActive(false);
            tutorial = false;
            nurseNPC.SetActive(true);
            for (int i = 0; i < 4; i++)
            {
                InputManager.instance.playerInput.Dialogue.AdvanceDialogue.ChangeBinding(4).Erase();
            }
            StartCoroutine(NurseRoutine());
        }
    }

    public void OpenDoors()
    {
        foreach (var hospitalDoor in hospitalDoors)
        {
            hospitalDoor.SetActive(false);
        }
    }

    IEnumerator NurseRoutine()
    {
        yield return new WaitForSeconds(4);
        DialogueManager.instance.StartDialogue(nurseDialogue);
    }
}
