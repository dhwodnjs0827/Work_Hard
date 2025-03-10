using DataDeclaration;
using TMPro;
using UnityEngine;

public class ReadMeSign : MonoBehaviour, IInteraction
{
    [SerializeField] private ItemData itemData;
    
    private SignsUI signUI;  // 상호작용 시 활성화 할 UI

    private void Start()
    {
        signUI = UIManager.Instance.MainUI.SignUI;
    }

    void IInteraction.OnInteract()
    {
        signUI.SetText(itemData.signContentText);
        signUI.gameObject.SetActive(true);
        UIManager.ActiveCursor(true);
    }
}
