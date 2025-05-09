using UnityEngine;

[RequireComponent(typeof(PlayerController), typeof(PlayerCondition), typeof(PlayerInteraction))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(CapsuleCollider))]
public class Player : MonoBehaviour
{
    public PlayerController Controller { get; private set; }
    public PlayerCondition Condition { get; private set; }

    private void Awake()
    {
        GameManager.Instance.InitPlayer(this);
        
        Controller = GetComponent<PlayerController>();
        Condition = GetComponent<PlayerCondition>();
        
        InitCapsuleCollider();
    }

    /// <summary>
    /// CapsuleCollider 초기화
    /// </summary>
    private void InitCapsuleCollider()
    {
        var col = GetComponent<CapsuleCollider>();
        col.isTrigger = false;
        col.providesContacts = false;
        col.material = null;
        col.center = Vector3.up;
        col.radius = 0.5f;
        col.height = 2.0f;
    }
}
