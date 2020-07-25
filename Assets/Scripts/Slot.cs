using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] private Item item;

    [SerializeField] PlayerController player;

    private GameObject instantiate;

    //Wearing items form inventory to gameObjects
    public void Use()
    {
        if (item == null) return;

        Transform transform = FindObjectOfType<PlayerController>().transform;

        switch (item.index)
        {
            case (0):
                instantiate = Instantiate(item.perfabsObject);
                instantiate.transform.SetParent(transform);
                instantiate.transform.localPosition = new Vector3(0, 0, 0f);
                instantiate.transform.localScale = new Vector3(1f, 1f, 1f);
                instantiate.transform.eulerAngles = new Vector3(0f, -90f, 0f);
                instantiate.name = "Overalls_Using";
                instantiate.tag = "Using";
                Inventory.instance.Remove(item);
                player.indexItems = 1;
                break;
            case (1):
                if (player.indexItems == 1)
                {
                    instantiate = Instantiate(item.perfabsObject);
                    instantiate.transform.SetParent(transform);
                    instantiate.transform.localPosition = new Vector3(0, 0.7f, 0);
                    instantiate.transform.eulerAngles = transform.eulerAngles + new Vector3(-90f, -90f, 0f);
                    instantiate.transform.name = "Helmet_Using";
                    instantiate.tag = "Using";
                    Inventory.instance.Remove(item);
                    player.indexItems = 2;
                } else
                {
                    player.info.gameObject.SetActive(true);
                    player.info.text = "YOU NEED WEAR OVERALLS";
                }
                break;
            case (2):
                if (player.indexItems == 2)
                {
                    instantiate = Instantiate(item.perfabsObject);
                    instantiate.transform.SetParent(transform);
                    instantiate.transform.localPosition = new Vector3(-.5f, 0, .7f);
                    instantiate.transform.eulerAngles = transform.eulerAngles + new Vector3(0f, -90f, 0f);
                    instantiate.transform.name = "Extinguisher_Using";
                    FindObjectOfType<PlayerController>().SetExtinguisher(instantiate.GetComponent<Extinguisher>());
                    instantiate.tag = "Using";
                    Inventory.instance.Remove(item);
                    player.indexItems = 3;
                }
                else
                {
                    player.info.gameObject.SetActive(true);
                    player.info.text = "YOU NEED WEAR HELMET";
                }
                break;
        }
    }

    public Item GetItem()
    {
        return item;
    }

    public void SetItem(Item item)
    {
        this.item = item;
    }
}
