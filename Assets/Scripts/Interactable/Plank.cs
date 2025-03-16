using System;
using UnityEngine;

public class Plank : MonoBehaviour, IInteractable
{
    public void Interact(GameObject player)
    {
        var inventory = player.GetComponent<Inventory>();
        OnInteracted?.Invoke();
        inventory.AddItem(ItemType.Plank);
        Destroy(gameObject);
    }

    public event Action OnInteracted;
}
