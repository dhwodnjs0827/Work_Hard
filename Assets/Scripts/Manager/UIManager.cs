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

    /// <summary>
    /// mainUI에 데이터 저장
    /// </summary>
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

    /// <summary>
    /// 마우스 커서 On/Off 메서드
    /// </summary>
    /// <param name="active">True: 마우스 커서 활성화
    /// <para>False: 마우스 커서 비활성화</para></param>
    public static void ActiveCursor(bool active)
    {
        Cursor.lockState = active ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
