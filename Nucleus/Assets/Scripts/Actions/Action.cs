using UnityEngine;
using System.Collections;

public abstract class Action
{
    Condition condition;

    public SimpleAgent self;

    public Action(SimpleAgent self, Condition condition)
    {
        this.self = self;
        this.condition = condition;
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