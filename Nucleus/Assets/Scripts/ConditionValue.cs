using UnityEngine;
using System.Collections;

public class ConditionValue
{
    public float value;

    public ConditionValue(float value)
    {
        this.value = value;
    }

    public static implicit operator float(ConditionValue value)
    {
        return value.value;
    }
}