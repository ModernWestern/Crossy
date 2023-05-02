using UnityEditor;
using UnityEngine;
using UnityEditor.UI;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CustomButton : Button
{
    [SerializeField] private Color selected, unselected;

    public UnityEvent onPointerEnter, onPointerExit;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        image.color = selected;

        onPointerEnter.Invoke();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        image.color = unselected;

        onPointerExit.Invoke();
    }
}

#region Editor

[CustomEditor(typeof(CustomButton))]
public class CustomButtonEditor : ButtonEditor
{
    private SerializedProperty onPointerEnterProp, onPointerExitProp, selected, unselected;

    protected override void OnEnable()
    {
        base.OnEnable();

        onPointerEnterProp = serializedObject.FindProperty("onPointerEnter");
        onPointerExitProp = serializedObject.FindProperty("onPointerExit");
        selected = serializedObject.FindProperty("selected");
        unselected = serializedObject.FindProperty("unselected");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();
        CustomButton button = (CustomButton)target;

        EditorGUILayout.Space();

        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(onPointerEnterProp);
        EditorGUILayout.PropertyField(onPointerExitProp);
        EditorGUILayout.Space();
        EditorGUI.indentLevel--;

        EditorGUILayout.LabelField("Button", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(selected);
        EditorGUILayout.PropertyField(unselected);
        EditorGUILayout.Space();
        EditorGUI.indentLevel--;

        serializedObject.ApplyModifiedProperties();
    }
}

#endregion