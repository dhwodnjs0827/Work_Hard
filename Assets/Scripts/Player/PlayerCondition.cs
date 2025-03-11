using DataDeclaration;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    private float maxStamina; // 최대 스태미나
    private float curStamina; // 현재 스태미나
    private float staminaRegenRate; // 스태미나 소모량
    private float staminaConsumptionRate; // 스태미나 회복량
    private float lastSprintTime; // 마지막 스태미나 사용 시간
    private float staminaRegenDelay; // 회복 Delay 시간

    private float maxSlowTime; // 최대 슬로우 모드 시간
    private float curSlowTime; // 현재 슬로우 모드 시간
    private float slowTimeConsumptionRate; // 슬로우 모드 시간 소모량

    private ConditionUI conditionUI;
    private PlayerController controller;

    public bool CanSprint { get; private set; } // 스태미나가 충분하면 달리기 가능
    public bool CanSlowMode { get; private set; } // 슬로우 모드 시간이 충분하면 슬로우 모드 가능

    private void Awake()
    {
        maxStamina = 100f;
        curStamina = maxStamina;
        staminaRegenRate = 5f;
        staminaConsumptionRate = 20f;
        staminaRegenDelay = 2f;

        maxSlowTime = 10f;
        curSlowTime = maxSlowTime;
        slowTimeConsumptionRate = 5f;

        CanSprint = true;
        CanSlowMode = true;
        
        controller =  GetComponent<PlayerController>();
    }

    private void Start()
    {
        conditionUI = UIManager.Instance.MainUI.ConditionUI;
    }

    private void Update()
    {
        if (!controller.IsSprint)
        {
            lastSprintTime += Time.deltaTime;
            if (lastSprintTime < staminaRegenDelay) return;
            
            // 스태미나 회복
            curStamina = Mathf.Min(curStamina + staminaRegenRate * Time.deltaTime, maxStamina);
            CanSprint = curStamina != 0f;
            // UI 업데이트
            conditionUI.UpdateConditionUI(ConditionType.Stamina, curStamina / maxStamina);
        }
    }

    /// <summary>
    /// 스태미나 사용 시 호출
    /// </summary>
    public void UseStamina()
    {
        // 스태미나 감소
        curStamina = Mathf.Max(curStamina - staminaConsumptionRate * Time.deltaTime, 0f);
        CanSprint = curStamina != 0f;
        lastSprintTime = 0f;
        // UI 업데이트
        conditionUI.UpdateConditionUI(ConditionType.Stamina, curStamina / maxStamina);
    }

    /// <summary>
    /// 슬로우 모드 진입 시 슬로우 모드 시간 감소
    /// </summary>
    public void HandleSlowTime()
    {
        // 슬로우 모드 시간 감소
        curSlowTime = Mathf.Max(curSlowTime - slowTimeConsumptionRate * Time.deltaTime, 0f);
        CanSlowMode = curSlowTime != 0f;
        // UI 업데이트
        conditionUI.UpdateConditionUI(ConditionType.SlowTime, curSlowTime / maxSlowTime);
    }

    /// <summary>
    /// Condition 관련 수치 회복
    /// </summary>
    /// <param name="type">회복할 Condition 종류</param>
    /// <param name="amount">회복량</param>
    public void RecoverCurCondition(ConditionType type, float amount)
    {
        float value;
        // type에 맞는 Condition 회복
        switch (type)
        {
            case ConditionType.Stamina:
                curStamina = Mathf.Min(curStamina + amount, maxStamina);
                value = curStamina / maxStamina;
                break;
            case ConditionType.SlowTime:
                CanSlowMode = true;
                curSlowTime = Mathf.Min(curSlowTime + amount, maxSlowTime);
                value = curSlowTime / maxSlowTime;
                break;
            default:
                return;
        }
        // UI 업데이트
        conditionUI.UpdateConditionUI(type, value);
    }
}
