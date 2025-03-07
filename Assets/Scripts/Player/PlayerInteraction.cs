using DataDeclaration;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField, Range(0f, 5f)] private float maxDetectDistance;
    private Vector3 screenRayPos;
    private LayerMask interactionLayer;

    private GameObject interactGuide;
    private IInteraction interaction;
    
    private Camera mainCam;

    private void Awake()
    {
        maxDetectDistance = 3f;
        screenRayPos = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        interactionLayer = LayerMask.GetMask("Interaction");
    }

    private void Start()
    {
        interactGuide = UIManager.Instance.MainUI.InteractGuide;
        
        mainCam = Camera.main;
    }

    private void Update()
    {
        CheckInteractionObject();
    }

    private void CheckInteractionObject()
    {
        var ray = mainCam.ScreenPointToRay(screenRayPos);

        if (Physics.Raycast(ray, out var hit, maxDetectDistance, interactionLayer))
        {
            if (hit.collider.gameObject.TryGetComponent<IInteraction>(out interaction))
            {
                ActivateInteraction();
            }
        }
        else
        {
            interactGuide.SetActive(false);
            interaction = null;
        }
    }

    private void ActivateInteraction()
    {
        interactGuide.SetActive(true);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.started || interaction == null) return;
        interactGuide.SetActive(false);
        interaction.OnInteract();
    }
}
