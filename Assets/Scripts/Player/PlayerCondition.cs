using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    private float maxStamina;
    private float curStamina;
    private float staminaRegenRate;
    private float staminaConsumptionRate;

    [SerializeField] private StaminaUI staminaUI;
    private PlayerController controller;

    public bool CanSprint { get; private set; }

    private void Awake()
    {
        maxStamina = 100f;
        curStamina = maxStamina;
        staminaRegenRate = 5f;
        staminaConsumptionRate = 20f;
        CanSprint = true;
        
        controller =  GetComponent<PlayerController>();
    }

    private void Update()
    {
        CanSprint = curStamina > 0f ? true : false;
        HandleStamina();
    }

    private void HandleStamina()
    {
        if (controller.IsSprint)
        {
            curStamina = Mathf.Max(curStamina - staminaConsumptionRate * Time.deltaTime, 0f);
            if (curStamina == 0f)
            {
                CanSprint = false;
            }
        }
        else
        {
            curStamina = Mathf.Min(curStamina + staminaRegenRate * Time.deltaTime, maxStamina);
        }

        staminaUI.UpdateUIBar(curStamina / maxStamina);
    }
}
