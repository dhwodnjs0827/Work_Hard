using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    [SerializeField] private Image guageBar;

    private void Awake()
    {
        guageBar = transform.GetChild(0).GetComponent<Image>();
    }

    public void UpdateUIBar(float amount)
    {
        guageBar.fillAmount = amount;
    }
}
