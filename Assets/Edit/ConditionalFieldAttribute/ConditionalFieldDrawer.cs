#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ConditionalFieldAttribute))]
public class ConditionalFieldDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ConditionalFieldAttribute conditional = (ConditionalFieldAttribute)attribute;
        SerializedProperty sourcePropertyValue = FindSourceProperty(property, conditional.ConditionalSourceField);

        if (sourcePropertyValue != null)
        {
            bool showField = sourcePropertyValue.boolValue;
            if (conditional.Inverse)
            {
                showField = !showField;
            }

            if (showField)
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }
        else
        {
            Debug.LogWarning($"Could not find a property named {conditional.ConditionalSourceField} in {property.serializedObject.targetObject.GetType()}");
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        ConditionalFieldAttribute conditional = (ConditionalFieldAttribute)attribute;
        SerializedProperty sourcePropertyValue = FindSourceProperty(property, conditional.ConditionalSourceField);

        if (sourcePropertyValue != null)
        {
            bool showField = sourcePropertyValue.boolValue;
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
        else
        {
            return -EditorGUIUtility.standardVerticalSpacing;
        }
    }

    private SerializedProperty FindSourceProperty(SerializedProperty property, string propertyName)
    {
        if (propertyName.Contains("."))
        {
            string[] path = propertyName.Split('.');
            SerializedProperty sourceProperty = property.serializedObject.FindProperty(path[0]);
            for (int i = 1; i < path.Length; i++)
            {
                if (sourceProperty != null)
                {
                    sourceProperty = sourceProperty.FindPropertyRelative(path[i]);
                }
            }
            return sourceProperty;
        }
        else
        {
            return property.serializedObject.FindProperty(propertyName);
        }
    }
}
#endif









