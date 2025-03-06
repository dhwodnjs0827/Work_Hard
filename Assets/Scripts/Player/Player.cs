using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(CapsuleCollider))]
public class Player : MonoBehaviour
{
    private PlayerController controller;
    
    public PlayerController Controller => controller;
    
    private void Awake()
    {
        controller = GetComponent<PlayerController>();
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
