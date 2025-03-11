using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera cam;
    
    private LayerMask firstPersonMask;
    private LayerMask thirdPersonMask;

    private void Awake()
    {
        cam = Camera.main;
        firstPersonMask = ~(1 << LayerMask.NameToLayer("Player"));
        thirdPersonMask = cam.cullingMask;
    }

    private void Start()
    {
        cam.cullingMask = firstPersonMask;
    }

    public void SetPointOfView(bool isFirstPerson)
    {
        cam.cullingMask = isFirstPerson ? firstPersonMask : thirdPersonMask;
    }
}
