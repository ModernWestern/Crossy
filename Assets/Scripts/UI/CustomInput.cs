using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomInput : MonoBehaviour
{
    [SerializeField] private Image background;

    [SerializeField] private TMP_InputField inputField;

    [SerializeField] private Color success, fail;

    [SerializeField] private string text;

    private void Awake()
    {
        if (!string.IsNullOrEmpty(text))
        {
            inputField.text = text;
        }
    }

    public bool Success
    {
        set => background.color = value ? success : fail;
    }

    public bool Enable
    {
        set => inputField.interactable = value;
    }

    public bool HasText(out string text)
    {
        text = inputField.text;

        return !string.IsNullOrEmpty(inputField.text);
    }
}