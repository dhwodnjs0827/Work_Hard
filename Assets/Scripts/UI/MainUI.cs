using DataDeclaration;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : BaseUI
{
    private Player player;

    private float sec;
    private int min;
    
    [SerializeField] private StaminaUI staminaUI;
    [SerializeField] private TextMeshProUGUI playTimeText;
    [SerializeField] private TextMeshProUGUI heightInfoText;
    [SerializeField] private GameObject interactGuide;
    [SerializeField] private GameObject interactInfo;
    [SerializeField] private Button interactInfoCloseBtn;
    
    public StaminaUI StaminaUI => staminaUI;
    public GameObject InteractGuide => interactGuide;
    public GameObject InteractInfo => interactInfo;

    public override void Init(UIManager instance)
    {
        base.Init(instance);
        
        uiState = UIState.Main;
        
        interactInfoCloseBtn.onClick.AddListener(() => interactInfo.SetActive(false));
        interactInfoCloseBtn.onClick.AddListener(() => UIManager.ActiveCursor(false));
    }

    private void Start()
    {
        player = GameManager.Instance.Player;
    }
    
    private void Update()
    {
        sec += Time.deltaTime;
        if (sec >= 60f)
        {
            min += 1;
            sec = 0;
        }

        playTimeText.text = $"진행 시간 {min:D2}:{Mathf.FloorToInt(sec):D2}";
        heightInfoText.text = $"현재 높이 {player.transform.position.y:N0}m";
    }
}
