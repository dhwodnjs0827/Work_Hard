using DataDeclaration;
using UnityEngine;

public class ReadMeSign : MonoBehaviour, IInteraction
{
    private GameObject infoUI;  // 상호작용 시 활성화 할 UI 게임 오브젝트

    private void Start()
    {
        infoUI = UIManager.Instance.MainUI.InteractInfo;
    }

    void IInteraction.OnInteract()
    {
        infoUI.gameObject.SetActive(true);
        UIManager.ActiveCursor(true);
    }
}
