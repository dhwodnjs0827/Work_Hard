using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpPower;
    private Vector2 moveInputDir;

    [Space]
    [Header("Look")]
    [SerializeField] private float mouseSensitivity;
    private const float MOUSE_SENSITIVITY_MULTIPLIER = 0.01f;
    private Transform camContainer;
    private float curCamRotX;
    private const float MIN_ROT_X = -60f;
    private const float MAX_ROT_X = 60f;
    private Vector2 mouseInputDelta;
    
    private LayerMask groundLayer;

    private Rigidbody rb;

    private void Awake()
    {
        camContainer = transform.Find("CameraContainer");
        groundLayer = LayerMask.GetMask("Ground");
        
        InitRigdbody();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        Rotation();
    }

    private void LateUpdate()
    {
        Look();
    }

    private void InitRigdbody()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Move()
    {
        Vector3 velocity = Vector3.right * moveInputDir.x + Vector3.forward * moveInputDir.y;
        velocity = transform.TransformDirection(velocity.normalized) * moveSpeed;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
    }

    private void Rotation()
    {
        float rotY = mouseInputDelta.x * mouseSensitivity * MOUSE_SENSITIVITY_MULTIPLIER;
        transform.eulerAngles += new Vector3(0f, rotY, 0f);
    }

    private void Look()
    {
        curCamRotX += mouseInputDelta.y * mouseSensitivity * MOUSE_SENSITIVITY_MULTIPLIER;
        curCamRotX = Mathf.Clamp(curCamRotX, MIN_ROT_X, MAX_ROT_X);
        camContainer.localEulerAngles = new Vector3(-curCamRotX, 0f, 0f);
    }

    private bool IsGround()
    {
        Ray[] groundRay = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };
        
        for (int i = 0; i < groundRay.Length; i++)
        {
            if (Physics.Raycast(groundRay[i], 0.1f, groundLayer))
            {
                return true;
            }
        }
        return false;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moveInputDir = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            moveInputDir = Vector2.zero;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && IsGround())
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseInputDelta = context.ReadValue<Vector2>();
    }
}