
using UnityEngine;

[CreateAssetMenu(fileName = "Items", menuName = "Item")]
public class Item : ScriptableObject
{
    public int index;
    public string nameItem;
    public Sprite imageItem;
    public GameObject perfabsObject;
}