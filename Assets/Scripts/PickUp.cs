using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] Item item;

    public void PickUpItem()
    {
        Inventory.instance.Add(item);
        Destroy(gameObject);
    }
}
