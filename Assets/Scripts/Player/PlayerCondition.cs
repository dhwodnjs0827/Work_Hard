using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCondition : MonoBehaviour
{
    private float maxStamina;
    private float curStamina;
    private float staminaRegenRate;
    private float staminaConsumptionRate;

    private bool isSprint;
    
    [SerializeField] private StaminaUI staminaUI;
    
    public bool  IsSprint => isSprint && curStamina > 0f;

    private void Awake()
    {
        maxStamina = 100f;
        curStamina = maxStamina;
        staminaRegenRate = 5f;
        staminaConsumptionRate = 20f;

        isSprint = false;
    }

    public void HandleStamina()
    {
        if (isSprint && curStamina > 0)
        {
            curStamina = Mathf.Max(curStamina - staminaConsumptionRate * Time.deltaTime, 0f);
            if (curStamina <= 0f)
            {
                isSprint = false;
            }
        }
        else
        {
            curStamina = Mathf.Min(curStamina + staminaRegenRate * Time.deltaTime, maxStamina);
        }
        
        staminaUI.UpdateUIBar(curStamina / maxStamina);
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isSprint = true;
        }
        else if (context.canceled)
        {
            isSprint = false;
        }
    }
}
