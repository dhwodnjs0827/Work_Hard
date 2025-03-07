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
        infoUI = UIManager.Instance.GetMainUI().InteractInfo;
    }

    public void OnInteract()
    {
        // UI 활성화
        infoUI.gameObject.SetActive(true);
        Debug.Log(itemData.itemDescription);
    }
}
