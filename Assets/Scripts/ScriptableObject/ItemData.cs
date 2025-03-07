using DataDeclaration;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public ItemType  itemType;
    public GameObject itemPrefab;
}
