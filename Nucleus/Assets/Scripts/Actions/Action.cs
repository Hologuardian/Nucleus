using UnityEngine;
using System.Collections;

public abstract class Action
{
    public string Label;
    Condition condition;

    public SimpleAgent self;

    public Action(SimpleAgent self, Condition condition, string Label)
    {
        this.self = self;
        this.condition = condition;
        this.Label = Label;
    }

    public abstract float Estimate();

    public bool Evaluate()
    {
        if (condition)
        {
            Execute();
            return true;
        }
        return false;
    }

    protected abstract void Execute();
}