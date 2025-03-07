using DataDeclaration;
using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    protected UIManager uiManager;
    protected UIState uiState;

    public virtual void Init(UIManager instance)
    {
        uiManager = instance;
    }

    public void ActiveUI(UIState state)
    {
        gameObject.SetActive(uiState == state);
    }
}
