using UnityEngine;

public class Wheelchair : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform ejectPos;
    public void Interact(GameObject player)
    {
        player.transform.position = ejectPos.position;
        player.transform.rotation = transform.rotation;
        gameObject.transform.SetParent(player.transform);
        var playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.inWheelchair = true;
        playerMovement.wheelchair = this;
    }
}
