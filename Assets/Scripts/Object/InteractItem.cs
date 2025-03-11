using DataDeclaration;
using UnityEngine;

/// <summary>
/// 상호 작용 Item 클래스
/// <para>TODO: 상속 구조로 변경</para>
/// </summary>
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
