using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    private float maxStamina;   // 최대 스태미나
    private float curStamina;   // 현재 스태미나
    private float staminaRegenRate; // 스태미나 소모량
    private float staminaConsumptionRate;   // 스태미나 회복량

    private StaminaUI staminaUI;
    private PlayerController controller;

    public bool CanSprint { get; private set; } // 스태미나가 충분하면 달리기 가능

    private void Awake()
    {
        maxStamina = 100f;
        curStamina = maxStamina;
        staminaRegenRate = 5f;
        staminaConsumptionRate = 20f;
        CanSprint = true;
        
        controller =  GetComponent<PlayerController>();
    }

    private void Start()
    {
        staminaUI = UIManager.Instance.MainUI.StaminaUI;
    }

    private void Update()
    {
        CanSprint = curStamina > 0f ? true : false;
        HandleStamina();
    }
    
    /// <summary>
    /// 달리기 여부에 따른 스태미나 소모/회복
    /// </summary>
    private void HandleStamina()
    {
        if (controller.IsSprint)
        {
            // 스태미나 소모
            curStamina = Mathf.Max(curStamina - staminaConsumptionRate * Time.deltaTime, 0f);
            if (curStamina == 0f)
            {
                CanSprint = false;
            }
        }
        else
        {
            // 스태미나 회복
            curStamina = Mathf.Min(curStamina + staminaRegenRate * Time.deltaTime, maxStamina);
        }

        // 스태미나 UI 업데이트
        staminaUI.UpdateUIBar(curStamina / maxStamina);
    }

    public void RecoverStamina(float amount)
    {
        curStamina = Mathf.Min(curStamina + amount, maxStamina);
    }
}
