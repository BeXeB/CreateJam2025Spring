using UnityEngine;

public class Wheelchair : MonoBehaviour, IInteractable
{
    [SerializeField] public Transform ejectPos;
    public void Interact(GameObject player)
    {
        player.transform.position = transform.position;
        player.transform.rotation = transform.rotation;
        gameObject.transform.SetParent(player.transform);
        var playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.SetInWheelChair(true);
        playerMovement.wheelchair = this;
    }
}
