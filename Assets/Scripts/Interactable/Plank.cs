using UnityEngine;

public class Plank : MonoBehaviour, IInteractable
{
    public void Interact(GameObject player)
    {
        var inventory = player.GetComponent<Inventory>();
        inventory.AddItem(ItemType.Plank);
        Destroy(gameObject);
    }
}
