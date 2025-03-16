using System;
using UnityEngine;

public class Scene3 : MonoBehaviour
{
    public static Scene3 instance;

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
    [SerializeField] private Dialogue doorCloseDialogue;
    [SerializeField] private PlankSpot[] plankSpots;
    [SerializeField] private GameObject door;
    
    void Start()
    {
        GameManager.instance.player.GetComponent<Inventory>().AddItem(ItemType.Plank);
        DialogueManager.instance.StartDialogue(startDialogue);
        foreach (var spot in plankSpots)
        {
            spot.OnInteracted += OnInteracted;
        }
    }

    private void OnInteracted()
    {
        door.SetActive(true);   
        DialogueManager.instance.StartDialogue(doorCloseDialogue);
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
