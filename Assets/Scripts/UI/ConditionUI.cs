using System.Collections.Generic;
using DataDeclaration;
using UnityEngine;
using UnityEngine.UI;

public class ConditionUI : MonoBehaviour
{
    private Dictionary<ConditionType, Image> conditionDict;
    
    [SerializeField] private Image staminaGauge;
    [SerializeField] private Image timeSlowGauge;

    private void Awake()
    {
        conditionDict = new Dictionary<ConditionType, Image>
        {
            { ConditionType.Stamina, staminaGauge },
            { ConditionType.SlowTime, timeSlowGauge }
        };
    }

    public void UpdateConditionUI(ConditionType condition, float amount)
    {
        if (conditionDict.TryGetValue(condition, out var value))
        {
            value.fillAmount = amount;
        }
    }
}
