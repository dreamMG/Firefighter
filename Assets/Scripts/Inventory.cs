using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    private void Awake()
    {
        instance = this;
    }

    public List<Item> items = new List<Item>();
    public int space = 3;

    #region Delegates
    public delegate void OnPickUp(Item item);
    public OnPickUp pickUp;

    public delegate void OnRemove(Item item);
    public OnRemove onRemove;
    #endregion

    public bool Add(Item item)
    {
        if (items.Count >= space)
        {
            Debug.Log("Not enough room.");
            return false;
        }
        items.Add(item);
        pickUp.Invoke(item);
        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        onRemove.Invoke(item);
    }
}
