using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Slot[] slots;

    private void Start()
    {
        Inventory.instance.pickUp += UpdateInventory;
        Inventory.instance.onRemove += ClearInventory;
    }

    public void UpdateInventory(Item item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].GetItem() == null)
            {
                slots[i].SetItem(item);
                slots[i].transform.GetChild(0).gameObject.SetActive(true);
                slots[i].transform.GetChild(0).GetComponentInChildren<Image>().sprite = item.imageItem;
                break;
            } 
        }
    }

    public void ClearInventory(Item item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].GetItem() == item)
            {
                slots[i].SetItem(null);
                slots[i].transform.GetChild(0).gameObject.SetActive(false);
                slots[i].transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                break;
            }
        }
    }
}
