using System;
using UnityEngine;

public interface IInteractable
{
    void Interact(GameObject player);
    
    public event Action OnInteracted;
}
