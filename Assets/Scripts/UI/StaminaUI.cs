using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    [SerializeField] private Image gaugeBar;

    public void UpdateUIBar(float amount)
    {
        gaugeBar.fillAmount = amount;
    }
}
