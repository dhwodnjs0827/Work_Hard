using System;
using DataDeclaration;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    private float maxStamina;   // 최대 스태미나
    private float curStamina;   // 현재 스태미나
    private float staminaRegenRate; // 스태미나 소모량
    private float staminaConsumptionRate;   // 스태미나 회복량
    private float lastSprintTime;
    private float staminaRegenDelay;

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
            if (!(lastSprintTime > staminaRegenDelay)) return;
            // 스태미나 회복
            curStamina = Mathf.Min(curStamina + staminaRegenRate * Time.deltaTime, maxStamina);
            CanSprint = curStamina != 0f;
            conditionUI.UpdateConditionUI(ConditionType.Stamina, curStamina / maxStamina);
        }
    }

    public void HandleStamina()
    {
        // 스태미나 소모
        curStamina = Mathf.Max(curStamina - staminaConsumptionRate * Time.deltaTime, 0f);
        CanSprint = curStamina != 0f;
        lastSprintTime = 0f;
        // 스태미나 UI 업데이트
        conditionUI.UpdateConditionUI(ConditionType.Stamina, curStamina / maxStamina);
    }

    public void RecoverStamina(float amount)
    {
        curStamina = Mathf.Min(curStamina + amount, maxStamina);
        conditionUI.UpdateConditionUI(ConditionType.SlowTime, curSlowTime / maxSlowTime);
    }

    /// <summary>
    /// 슬로우 모드 진입 시 작동 시간 감소
    /// </summary>
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
        conditionUI.UpdateConditionUI(ConditionType.SlowTime, curSlowTime / maxSlowTime);
    }

    public void RecoverCurCondition(ConditionType type, float amount)
    {
        switch (type)
        {
            case ConditionType.Stamina:
                curStamina = Mathf.Min(curStamina + amount, maxStamina);
                break;
            case ConditionType.SlowTime:
                CanSlowMode = true;
                curSlowTime = Mathf.Min(curSlowTime + amount, maxSlowTime);
                break;
        }
        conditionUI.UpdateConditionUI(type, amount);
    }
}
