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

    public void Evaluate()
    {
        if (condition)
        {
            Execute();
        }
    }

    protected abstract void Execute();
}