using System;
using UnityEngine;

public class PlankSpot : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject plankSprite;
    [SerializeField] private GameObject barrier;
    private bool hasPlank = false;
    
    public void Interact(GameObject player)
    {
        var inventory = player.GetComponent<Inventory>();
        OnInteracted?.Invoke();
        if (hasPlank)
        {
            inventory.AddItem(ItemType.Plank);
            plankSprite.SetActive(false);
            barrier.SetActive(true);
            hasPlank = false;
            return;
        }
        
        if (!inventory.HasItem(ItemType.Plank))
        {
            return;
        }
        plankSprite.SetActive(true);
        barrier.SetActive(false);
        hasPlank = true;
        inventory.RemoveItem(ItemType.Plank);
    }

    public event Action OnInteracted;
}
