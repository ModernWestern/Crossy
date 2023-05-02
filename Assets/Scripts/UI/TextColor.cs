using TMPro;
using UnityEngine;

public class TextColor : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    
    [SerializeField] private Color enter, exit;

    public void OnEnter() => text.color = enter;

    public void OnExit() => text.color = exit;
}