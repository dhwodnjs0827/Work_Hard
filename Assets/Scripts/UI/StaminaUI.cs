using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    [SerializeField] private Image guageBar;

    public void UpdateUIBar(float amount)
    {
        guageBar.fillAmount = amount;
    }
}
