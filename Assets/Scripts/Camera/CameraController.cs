using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    private Camera cam;
    
    private LayerMask firstPersonMask;
    private LayerMask thirdPersonMask;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        firstPersonMask = ~(1 << LayerMask.NameToLayer("Player"));
        thirdPersonMask = cam.cullingMask;
        cam.cullingMask = firstPersonMask;
    }

    /// <summary>
    /// 시점 변경에 따른 카메라 CullingMask 변경
    /// </summary>
    /// <param name="isFirstPerson">True: 1인칭   False: 3인칭</param>
    public void SetPointOfView(bool isFirstPerson)
    {
        cam.cullingMask = isFirstPerson ? firstPersonMask : thirdPersonMask;
    }
}
