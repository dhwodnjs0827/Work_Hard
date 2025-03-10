using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SignsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI contentText;
    [SerializeField] private Button exitButton;

    private void Awake()
    {
        exitButton.onClick.AddListener(() => gameObject.SetActive(false));
        exitButton.onClick.AddListener(() => UIManager.ActiveCursor(false));
    }

    public void SetText(string content)
    {
        contentText.text = content;
    }
}
