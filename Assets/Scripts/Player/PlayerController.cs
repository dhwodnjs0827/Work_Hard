using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement"), SerializeField, Range(0f, 20f), Tooltip("기본 이동 속도")] private float moveSpeed;
    [SerializeField, Range(0f, 20f), Tooltip("점프력")] private float jumpPower;
    private Vector2 moveInputDir;
    [SerializeField, Range(1f, 3f), Tooltip("달리기 속도 배율")] private float sprintSpeedMultiplier;
    private bool canSprint;

    [Space]
    [Header("Look"), SerializeField, Range(0f, 100f), Tooltip("마우스 민감도")] private float mouseSensitivity;
    private const float MOUSE_SENSITIVITY_MULTIPLIER = 0.01f;   // 마우스 감도 배율
    private Transform camContainer;
    private float curCamRotX;
    private const float MIN_ROT_X = -60f;   // 최소 X축 회전값
    private const float MAX_ROT_X = 60f;    // 최대 X축 회전값
    private Vector2 mouseInputDelta;
    
    private LayerMask groundDetectLayer;
    
    private Rigidbody rb;
    private PlayerCondition condition;
    
    public bool IsSprint => canSprint && moveInputDir.magnitude > 0f && condition.CanSprint;

    private void Awake()
    {
        moveSpeed = 5f;
        jumpPower = 5f;
        sprintSpeedMultiplier = 1.5f;
        
        mouseSensitivity = 10f;
        camContainer = transform.Find("CameraContainer");
        
        groundDetectLayer = LayerMask.GetMask("Ground");
        
        rb = GetComponent<Rigidbody>();
        condition =  GetComponent<PlayerCondition>();
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
    
    private void Move()
    {
        var velocity = Vector3.right * moveInputDir.x + Vector3.forward * moveInputDir.y;
        var speed = canSprint && condition.CanSprint ? moveSpeed * sprintSpeedMultiplier : moveSpeed;
        velocity = transform.TransformDirection(velocity.normalized) * speed;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
    }

    private void Rotation()
    {
        if (Cursor.lockState != CursorLockMode.Locked) return;
        
        var rotY = mouseInputDelta.x * mouseSensitivity * MOUSE_SENSITIVITY_MULTIPLIER;
        transform.eulerAngles += new Vector3(0f, rotY, 0f);
    }

    private void Look()
    {
        if (Cursor.lockState != CursorLockMode.Locked) return;
        
        curCamRotX += mouseInputDelta.y * mouseSensitivity * MOUSE_SENSITIVITY_MULTIPLIER;
        curCamRotX = Mathf.Clamp(curCamRotX, MIN_ROT_X, MAX_ROT_X);
        camContainer.localEulerAngles = new Vector3(-curCamRotX, 0f, 0f);
    }

    /// <summary>
    /// 바닥 검사 메서드
    /// </summary>
    /// <returns>True: 바닥 상태
    /// <para>False: 공중 상태</para></returns>
    private bool IsGround()
    {
        var groundRay = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        //return groundRay.Any(t => Physics.Raycast(t, 0.1f, groundDetectLayer));
        return groundRay.Any(t => Physics.Raycast(t, 0.1f));
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

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            canSprint = true;
        }
        else if (context.canceled)
        {
            canSprint = false;
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