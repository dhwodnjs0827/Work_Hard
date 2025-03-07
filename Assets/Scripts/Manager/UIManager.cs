using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private MainUI mainUI;
    
    protected override void Awake()
    {
        base.Awake();
        Cursor.lockState = CursorLockMode.Locked;
        GetMainUI();
    }

    public MainUI GetMainUI()
    {
        if (mainUI != null) return mainUI;
        var go = GameObject.Find("MainUI");
        if (go != null) return mainUI;
        go = Resources.Load<GameObject>("Prefab/UI/MainUI");
        Instantiate(go).transform.SetParent(this.transform);
        mainUI = go.GetComponent<MainUI>();
        mainUI.Init(this);
        return  mainUI;
    }
}
