using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerCondition))]
public class Player : MonoBehaviour
{
    private PlayerController controller;
    private PlayerCondition condition;
    
    private void Awake()
    {
        GameManager.Instance.InitPlayer(this);
        
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
        
        InitCapsuleCollider();
    }

    private void InitCapsuleCollider()
    {
        CapsuleCollider col = GetComponent<CapsuleCollider>();
        col.isTrigger = false;
        col.providesContacts = false;
        col.material = null;
        col.center = Vector3.up;
        col.radius = 0.5f;
        col.height = 2.0f;
    }
}
