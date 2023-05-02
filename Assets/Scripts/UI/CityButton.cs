using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CityButton : CustomButton
{
    [SerializeField] private new Image image;
    [SerializeField] private Sprite noon, night;

    /// <summary>
    /// Null: day, True: noon/dawn, False: night
    /// </summary>
    public bool? IsDay
    {
        set
        {
            if (value.HasValue)
            {
                image.overrideSprite = value.Value ? noon : night;
            }
            else
            {
                image.overrideSprite = null;
            }
        }
    }
}

#region Editor

[CustomEditor(typeof(CityButton))]
public class CityButtonEditor : CustomButtonEditor
{
    private SerializedProperty selected, unselected, noon, night, image;

    protected override void OnEnable()
    {
        base.OnEnable();
        noon = serializedObject.FindProperty("noon");
        night = serializedObject.FindProperty("night");
        image = serializedObject.FindProperty("image");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        CityButton button = (CityButton)target;
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Icons", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(noon);
        EditorGUILayout.PropertyField(night);
        EditorGUILayout.PropertyField(image);
        EditorGUILayout.Space();
        EditorGUI.indentLevel--;
        serializedObject.ApplyModifiedProperties();
    }
}

#endregion