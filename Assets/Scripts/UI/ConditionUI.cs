using System.Collections.Generic;
using DataDeclaration;
using UnityEngine;
using UnityEngine.UI;

public class ConditionUI : MonoBehaviour
{
    private Dictionary<ConditionType, Image> conditionDict;
    
    [SerializeField] private Image staminaGauge;
    [SerializeField] private Image slowModeGauge;

    private void Awake()
    {
        conditionDict = new Dictionary<ConditionType, Image>
        {
            { ConditionType.Stamina, staminaGauge },
            { ConditionType.SlowTime, slowModeGauge }
        };
    }

    /// <summary>
    /// Condition 관련 게이지 바 조절
    /// </summary>
    /// <param name="condition">조절할 Condition 종류</param>
    /// <param name="amount">curValue / maxValue</param>
    public void UpdateConditionUI(ConditionType condition, float amount)
    {
        if (conditionDict.TryGetValue(condition, out var value))
        {
            value.fillAmount = amount;
        }
    }
}
