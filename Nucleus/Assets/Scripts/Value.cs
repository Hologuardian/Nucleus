using UnityEngine;
using System.Collections;
using System;

public class Value
{
    private Type type;
    public Type Type
    {
        get
        {
            return type;
        }
        set
        {
            type = value;
        }
    }

    private object value;
    public object V
    {
        get
        {
            return value;
        }
        set
        {
            this.value = value;
            this.type = value.GetType();
        }
    }

    public Value(object value)
    {
        type = value.GetType();
        this.value = value;
    }
}