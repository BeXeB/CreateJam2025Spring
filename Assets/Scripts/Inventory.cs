using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public event Action<ItemType> OnItemAdded;
    private List<ItemType> items = new ();
    
    public void AddItem(ItemType item)
    {
        OnItemAdded?.Invoke(item);
    }
    
    public bool HasItem(ItemType item)
    {
        return items.Contains(item);
    }
    
    public void RemoveItem(ItemType item)
    {
        items.Remove(item);
    }
}

public enum ItemType
{
    Plank,
}