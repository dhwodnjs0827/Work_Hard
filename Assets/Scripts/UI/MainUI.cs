using DataDeclaration;
using TMPro;
using UnityEngine;

public class MainUI : BaseUI
{
    [SerializeField] private StaminaUI staminaUI;
    [SerializeField] private TextMeshProUGUI playTimeText;
    [SerializeField] private GameObject interactGuide;
    [SerializeField] private GameObject interactInfo;
    
    public StaminaUI StaminaUI => staminaUI;
    public GameObject InteractGuide => interactGuide;
    public GameObject InteractInfo => interactInfo;

    public override void Init(UIManager instance)
    {
        base.Init(instance);
        uiState = UIState.Main;
    }

    private void Update()
    {
        playTimeText.text = Time.time.ToString("N2");
    }
}
