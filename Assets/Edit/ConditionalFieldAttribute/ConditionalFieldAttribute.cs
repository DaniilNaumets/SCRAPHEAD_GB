using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
public class ConditionalFieldAttribute : PropertyAttribute
{
    public string ConditionalSourceField;
    public bool Inverse;

    public ConditionalFieldAttribute(string conditionalSourceField, bool inverse = false)
    {
        this.ConditionalSourceField = conditionalSourceField;
        this.Inverse = inverse;
    }
}




