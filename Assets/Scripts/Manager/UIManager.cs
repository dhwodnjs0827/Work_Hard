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
        // 이미 데이터가 있을 경우 호출 종료
        if (mainUI != null) return;
        
        var go = GameObject.Find("MainUI");

        // Hierarchy 창에서 찾았는데 없을 경우 
        if (go != null)
        {
            mainUI = mainUI = go.GetComponent<MainUI>();
            mainUI.Init(this);
            return;
        }
        
        // 모든 경우에 해당하지 않으면 새로 생성 및 할당
        go = Resources.Load<GameObject>("Prefab/UI/MainUI");
        Instantiate(go).transform.SetParent(this.transform);
        mainUI = go.GetComponent<MainUI>();
        mainUI.Init(this);
    }

    /// <summary>
    /// 마우스 커서 On/Off 메서드
    /// </summary>
    /// <param name="active">True: 마우스 커서 활성화, False: 마우스 커서 비활성화</param>
    public static void ActiveCursor(bool active)
    {
        Cursor.lockState = active ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
