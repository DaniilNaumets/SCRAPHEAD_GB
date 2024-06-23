#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ConditionalFieldAttribute))]
public class ConditionalFieldDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ConditionalFieldAttribute conditional = (ConditionalFieldAttribute)attribute;
        SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(conditional.ConditionalSourceField);

        bool showField = sourcePropertyValue != null && sourcePropertyValue.boolValue;
        if (conditional.Inverse)
        {
            showField = !showField;
        }

        if (showField)
        {
            EditorGUI.PropertyField(position, property, label, true);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        ConditionalFieldAttribute conditional = (ConditionalFieldAttribute)attribute;
        SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(conditional.ConditionalSourceField);

        bool showField = sourcePropertyValue != null && sourcePropertyValue.boolValue;
        if (conditional.Inverse)
        {
            showField = !showField;
        }

        if (showField)
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }
        else
        {
            return -EditorGUIUtility.standardVerticalSpacing;
        }
    }
}
#endif



