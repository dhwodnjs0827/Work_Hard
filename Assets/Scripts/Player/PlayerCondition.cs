using DataDeclaration;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    private float maxStamina;   // 최대 스태미나
    private float curStamina;   // 현재 스태미나
    private float staminaRegenRate; // 스태미나 소모량
    private float staminaConsumptionRate;   // 스태미나 회복량

    private float maxSlowTime;  // 최대 슬로우 시간
    private float curSlowTime;  // 현재 슬로우 시간
    private float slowTimeConsumptionRate;  // 슬로우 시간 소모량

    private ConditionUI conditionUI;
    private PlayerController controller;

    public bool CanSprint { get; private set; } // 스태미나가 충분하면 달리기 가능
    public bool CanSlowMode { get; private set; }

    private void Awake()
    {
        maxStamina = 100f;
        curStamina = maxStamina;
        staminaRegenRate = 5f;
        staminaConsumptionRate = 20f;

        maxSlowTime = 10f;
        curSlowTime = maxSlowTime;
        slowTimeConsumptionRate = 5f;
        CanSlowMode = true;
        
        CanSprint = true;
        
        controller =  GetComponent<PlayerController>();
    }

    private void Start()
    {
        conditionUI = UIManager.Instance.MainUI.ConditionUI;
    }

    private void Update()
    {
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
            CanSprint = true;
        }

        // 스태미나 UI 업데이트
        conditionUI.UpdateConditionUI(ConditionType.Stamina, curStamina / maxStamina);
    }

    public void RecoverStamina(float amount)
    {
        curStamina = Mathf.Min(curStamina + amount, maxStamina);
    }

    public void HandleSlowTime()
    {
        curSlowTime = Mathf.Max(curSlowTime - slowTimeConsumptionRate * Time.deltaTime, 0f);
        CanSlowMode = curSlowTime != 0f;
        conditionUI.UpdateConditionUI(ConditionType.SlowTime, curSlowTime / maxSlowTime);
    }

    public void RecoverSlowTime(float amount)
    {
        CanSlowMode = true;
        curSlowTime = Mathf.Min(curSlowTime + amount, maxSlowTime);
    }
}
