using System;
using UnityEngine;

public class Scene5 : MonoBehaviour
{
    public static Scene5 instance;

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
    [SerializeField] private Dialogue ticketDialogue;
    [SerializeField] private GameObject wheelchair;
    [SerializeField] private Transform spwan;
    private Wheelchair asd;
    
    private void Start()
    {
        GameManager.instance.player.GetComponent<PlayerMovement>().GetOutOfChairImplementationWithDelete();
        asd = Instantiate(wheelchair, spwan).GetComponent<Wheelchair>();
        DialogueManager.instance.StartDialogue(startDialogue);
        asd.OnInteracted += AsdOnOnInteracted;
    }

    private void AsdOnOnInteracted()
    {
        DialogueManager.instance.StartDialogue(ticketDialogue);
    }

    private void OnDestroy()
    {
        asd.OnInteracted -= AsdOnOnInteracted;
    }
}
