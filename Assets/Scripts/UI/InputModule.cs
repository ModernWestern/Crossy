using System;
using UnityEngine;
using UnityEngine.UI;

public class InputModule : MonoBehaviour
{
    public event Action<string, Action<bool>> OnSet;

    public event Action OnSkip;

    [SerializeField] private CustomInput[] inputs;

    [SerializeField] private LayoutElement[] buttons;

    private bool isSet;

    public void Set()
    {
        if (!isSet && inputs[0].HasText(out var json))
        {
            OnSet?.Invoke(json, success =>
            {
                buttons[0].transform.parent.gameObject.SetActive(!success);

                inputs[0].Success = success;
                inputs[0].Enable = !success;

                isSet = success;
            });

            isSet = true;
        }
        else
        {
            inputs[0].Success = false;
        }
    }

    public void Skip()
    {
        OnSkip?.Invoke();

        gameObject.SetActive(false);
    }

    public void SetHovered()
    {
        buttons[0].layoutPriority = 0;
        buttons[1].layoutPriority = -1;
    }

    public void SkipHovered()
    {
        buttons[0].layoutPriority = -1;
        buttons[1].layoutPriority = 0;
    }

    public void PointerOut()
    {
        buttons[0].layoutPriority = 0;
        buttons[1].layoutPriority = 0;
    }
}