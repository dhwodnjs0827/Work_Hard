using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput), typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    #region Field
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
    
    private bool isFirstPerson;
    private Vector3 firstPersonCamPos;
    private Vector3 thirdPersonCamPos;

    private bool isSlowMode;

    // TODO: 현재 바닥검사 시 레이어 사용 안함 -> 나중에 주석 해제
    //private LayerMask groundDetectLayer;

    private PlayerCondition condition;
    private PlayerAnimator animator;
    private CameraController cameraController;
    
    public bool IsSprint => canSprint && moveInputDir.magnitude > 0f;

    public Rigidbody Rigidbody { get; private set; }
    #endregion
    
    #region UnityMethod
    private void Awake()
    {
        moveSpeed = 5f;
        jumpPower = 5f;
        sprintSpeedMultiplier = 1.5f;
        
        mouseSensitivity = 10f;
        camContainer = transform.Find("CameraContainer");
        isFirstPerson = true;
        firstPersonCamPos = new Vector3(0f, 1.6f, 0f);
        thirdPersonCamPos = new Vector3(0.6f, 1.6f, -1.7f);

        isSlowMode = false;
        
        //groundDetectLayer = LayerMask.GetMask("Ground");
        
        Rigidbody = GetComponent<Rigidbody>();
        condition =  GetComponent<PlayerCondition>();
        animator =  GetComponent<PlayerAnimator>();
        if (Camera.main != null) cameraController = Camera.main.GetComponent<CameraController>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        Rotation();
        SlowMode();
    }

    private void LateUpdate()
    {
        Look();
    }
    #endregion
    
    /// <summary>
    /// 캐릭터 이동
    /// </summary>
    private void Move()
    {
        var velocity = Vector3.right * moveInputDir.x + Vector3.forward * moveInputDir.y;
        float speed;
        if (canSprint && moveInputDir.magnitude > 0.01f && condition.CanSprint)
        {
            speed = moveSpeed * sprintSpeedMultiplier;
            condition.UseStamina();
            animator.Sprint(true);
        }
        else
        {
            speed = moveSpeed;
            animator.Sprint(false);
        }
        velocity = transform.TransformDirection(velocity.normalized) * speed;
        animator.Move(velocity.magnitude > 0.01f);
        velocity.y = Rigidbody.velocity.y;
        Rigidbody.velocity = velocity;
    }

    /// <summary>
    /// 캐릭터 회전
    /// </summary>
    private void Rotation()
    {
        if (Cursor.lockState != CursorLockMode.Locked) return;
        
        var rotY = mouseInputDelta.x * mouseSensitivity * MOUSE_SENSITIVITY_MULTIPLIER;
        transform.eulerAngles += new Vector3(0f, rotY, 0f);
    }

    /// <summary>
    /// 카메라 컨트롤
    /// </summary>
    private void Look()
    {
        if (Cursor.lockState != CursorLockMode.Locked) return;
        
        curCamRotX += mouseInputDelta.y * mouseSensitivity * MOUSE_SENSITIVITY_MULTIPLIER;
        curCamRotX = Mathf.Clamp(curCamRotX, MIN_ROT_X, MAX_ROT_X);
        camContainer.localEulerAngles = new Vector3(-curCamRotX, 0f, 0f);

        if (isFirstPerson) return;
        // 3인칭
        var rotY = transform.eulerAngles.y;
        var rotation = Quaternion.Euler(-curCamRotX, rotY, 0f);
        var offsetPos = new Vector3(0.6f, 0f, -1.7f);
        // 쿼터니언을 벡터에 곱하면 해당 벡터가 회전됨
        var position = transform.position + Vector3.up * 1.6f + rotation * offsetPos;
        camContainer.position = position;
    }

    /// <summary>
    /// 바닥 검사 메서드
    /// </summary>
    /// <returns>True: 바닥 상태
    /// <para>False: 공중 상태</para></returns>
    private bool IsGround()
    {
        var groundRay = new[]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        //TODO: 나중에 주석처리 해제해야 함
        //return groundRay.Any(t => Physics.Raycast(t, 0.1f, groundDetectLayer));
        return groundRay.Any(t => Physics.Raycast(t, 0.1f));
    }

    /// <summary>
    /// 슬로우 모드 On/Off
    /// </summary>
    private void SlowMode()
    {
        if (isSlowMode && condition.CanSlowMode)
        {
            Time.timeScale = 0.5f;
            condition.HandleSlowTime();
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    #region InputSystemMethod
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
        if (context.started || context.performed)
        {
            canSprint = true;
        }
        else
        {
            canSprint = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && IsGround())
        {
            Rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseInputDelta = context.ReadValue<Vector2>();
    }

    public void OnSlowMode(InputAction.CallbackContext context)
    {
        if (context.started || context.performed)
        {
            isSlowMode = true;
        }
        else
        {
            isSlowMode = false;
        }
    }
    
    public void OnChangePOV(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isFirstPerson = !isFirstPerson;
            cameraController.SetPointOfView(isFirstPerson);
            camContainer.localPosition = isFirstPerson ? firstPersonCamPos : thirdPersonCamPos;
        }
    }
    #endregion
}