using UnityEngine;
using System.Collections;
using System;

public class ActionProduce : Action
{
    public ActionProduce(SimpleAgent self, Condition condition) : base(self, condition, StringLiterals.Produce)
    {

    }

    public override float Estimate()
    {
        return 0.0f;
    }

    protected override void Execute()
    {
        if (self.board.ContainsKey(StringLiterals.Energy))
        {
            self.board[StringLiterals.Energy].V = 10.0f + (float)self.board[StringLiterals.Energy].V;
        }
        else
        {
            self.board[StringLiterals.Energy].V = 0.0f;
        }
    }
}