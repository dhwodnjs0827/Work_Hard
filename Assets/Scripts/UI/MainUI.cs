using DataDeclaration;
using TMPro;
using UnityEngine;

public class MainUI : BaseUI
{
    private Player player;

    private float playTimeSec;
    private int playTimeMin;

    [SerializeField] private ConditionUI conditionUI; // 플레이어 Condition 관리 UI
    [SerializeField] private TextMeshProUGUI playTimeText; // 플레이 시간 출력용
    [SerializeField] private TextMeshProUGUI heightInfoText; // 현재 플레이어 높이
    [SerializeField] private GameObject interactGuide; // 상호작용 아이템 검출 시 활성화 할 UI
    [SerializeField] private SignsUI signUI; // 상호작용 시 활성화 할 UI
    
    public ConditionUI ConditionUI => conditionUI;
    public GameObject InteractGuide => interactGuide;
    public SignsUI SignUI => signUI;

    private void Start()
    {
        player = GameManager.Instance.Player;
    }
    
    private void Update()
    {
        SetPlayTime();
    }
    
    public override void Init(UIManager instance)
    {
        base.Init(instance);
        
        uiState = UIState.Main;
    }

    private void SetPlayTime()
    {
        playTimeSec += Time.unscaledDeltaTime;
        if (playTimeSec >= 60f)
        {
            playTimeMin += 1;
            playTimeSec = 0;
        }

        playTimeText.text = $"진행 시간 {playTimeMin:D2}:{Mathf.FloorToInt(playTimeSec):D2}";
        heightInfoText.text = $"현재 높이 {player.transform.position.y:N0}m";
    }
}
