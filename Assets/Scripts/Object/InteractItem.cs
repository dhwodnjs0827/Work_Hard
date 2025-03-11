using System;
using DataDeclaration;
using UnityEngine;

public class InteractItem : MonoBehaviour, IInteraction
{
    [SerializeField] private ItemData itemData;

    void IInteraction.OnInteract()
    {
        if (itemData.itemType == ItemType.Recoverable)
        {
            foreach (var t in itemData.recoverable)
            {
                GameManager.Instance.Player.Condition.RecoverCurCondition(t.recoverableType, t.value);
            }
        }
        Destroy(gameObject);
    }
}
