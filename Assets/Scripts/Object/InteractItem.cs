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
            for (int i = 0; i < itemData.recoverable.Length; i++)
            {
                switch (itemData.recoverable[i].recoverableType)
                {
                    case ItemRecoverable.Stamina:
                        GameManager.Instance.Player.Condition.RecoverStamina(itemData.recoverable[i].value);
                        break;
                }
            }
        }
        Destroy(gameObject);
    }
}
