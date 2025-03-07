using System.Collections;
using System.Collections.Generic;
using DataDeclaration;
using UnityEngine;

public class ReadMeSign : MonoBehaviour, IInteraction
{
    private ItemData itemData;
    private GameObject infoUI;

    private void Start()
    {
        infoUI = UIManager.Instance.MainUI.InteractInfo;
    }

    public void OnInteract()
    {
        infoUI.gameObject.SetActive(true);
        UIManager.ActiveCursor(true);
    }
}
