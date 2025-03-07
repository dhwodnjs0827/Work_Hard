using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private MainUI mainUI;
    
    public MainUI MainUI => mainUI;
    
    protected override void Awake()
    {
        base.Awake();
        ActiveCursor(false);
        SetMainUI();
    }

    private void SetMainUI()
    {
        if (mainUI != null) return;
        
        var go = GameObject.Find("MainUI");

        if (go != null)
        {
            mainUI = mainUI = go.GetComponent<MainUI>();
            mainUI.Init(this);
            return;
        }
        
        go = Resources.Load<GameObject>("Prefab/UI/MainUI");
        Instantiate(go).transform.SetParent(this.transform);
        mainUI = go.GetComponent<MainUI>();
        mainUI.Init(this);
    }

    public static void ActiveCursor(bool active)
    {
        Cursor.lockState = active ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
