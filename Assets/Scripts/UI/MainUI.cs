using DataDeclaration;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : BaseUI
{
    private Player player;

    private float playTimeSec;
    private int playTimeMin;
    
    [SerializeField] private StaminaUI staminaUI;
    [SerializeField] private TextMeshProUGUI playTimeText;
    [SerializeField] private TextMeshProUGUI heightInfoText;
    [SerializeField] private GameObject interactGuide;
    [SerializeField] private SignsUI signUI;
    
    public StaminaUI StaminaUI => staminaUI;
    public GameObject InteractGuide => interactGuide;
    public SignsUI SignUI => signUI;

    public override void Init(UIManager instance)
    {
        base.Init(instance);
        
        uiState = UIState.Main;
    }

    private void Start()
    {
        player = GameManager.Instance.Player;
    }
    
    private void Update()
    {
        playTimeSec += Time.deltaTime;
        if (playTimeSec >= 60f)
        {
            playTimeMin += 1;
            playTimeSec = 0;
        }

        playTimeText.text = $"진행 시간 {playTimeMin:D2}:{Mathf.FloorToInt(playTimeSec):D2}";
        heightInfoText.text = $"현재 높이 {player.transform.position.y:N0}m";
    }
}
