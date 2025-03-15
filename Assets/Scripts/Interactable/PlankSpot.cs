using UnityEngine;
using UnityEngine.Serialization;

public class PlankSpot : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject plankSprite;
    [SerializeField] private Transform plankSpot;
    [SerializeField] private GameObject barrier;
    private bool hasPlank = false;
    
    public void Interact(GameObject player)
    {
        var inventory = player.GetComponent<Inventory>();
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
}
