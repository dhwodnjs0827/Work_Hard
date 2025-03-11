using System;
using DataDeclaration;
using UnityEngine;

[Serializable]
public class Recoverable
{
    public ConditionType recoverableType;
    public float value;
}

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/Item")]
public class ItemData : ScriptableObject
{
    [Header("Item Info")]
    public int itemID;
    public string itemName;
    public string itemDescription;
    public ItemType itemType;

    [Header("Signs")]
    public string signContentText;
    
    [Header("Recoverable")]
    public Recoverable[] recoverable;
}
