﻿using UnityEngine;
using System.Collections;

public class ActionWander : Action
{
    private Vector2 origin;

    public ActionWander(SimpleAgent self, Condition condition) : base(self, condition, StringLiterals.Wander)
    {
        origin = self.parent.transform.position;
    }

    public override float Estimate()
    {
        return 1.0f;
    }

    protected override void Execute()
    {
        // Time to wander b
        if (self.parent != null)
        {
            if (self.board.ContainsKey(StringLiterals.TargetTransform))
            {
                self.board[StringLiterals.TargetTransform].V = UnityEngine.Random.insideUnitCircle * (self.parent.transform.localScale.x / 2) + origin;
            }
            else
            {
                self.board.Add(StringLiterals.TargetTransform, new Value(UnityEngine.Random.insideUnitCircle * (self.parent.transform.localScale.x / 2) + origin));
            }
        }
    }
}