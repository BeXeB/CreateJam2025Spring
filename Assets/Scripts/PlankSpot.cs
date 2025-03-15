using UnityEngine;

public class PlankSpot : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject plankPrefab;
    [SerializeField] private Transform plankSpot;
    [SerializeField] private GameObject barrier;
    
    public void Interact(GameObject player)
    {
        var inventory = player.GetComponent<Inventory>();
        if (!inventory.HasItem(ItemType.Plank))
        {
            return;
        }
        var plank = Instantiate(plankPrefab, plankSpot.position, plankSpot.rotation);
        barrier.SetActive(false);
        inventory.RemoveItem(ItemType.Plank);
    }
}
