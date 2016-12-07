using UnityEngine;
using System.Collections;
using Assets;
using System;

public class ActionEat : Action
{
    public ActionEat(SimpleAgent self, Condition condition) : base(self, condition, StringLiterals.EatNibble)
    {

    }

    public override float Estimate()
    {
        return 1.0f;
    }

    protected override void Execute()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(self.transform.position, 0.5f, Vector2.zero);
        Nibble hubby = null;
        float distance = float.PositiveInfinity;

        foreach (RaycastHit2D hit in hits)
        {
            Nibble binble = hit.collider.GetComponentInParent<Nibble>();
            if (binble != null)
            {
                float check = (binble.transform.position - self.transform.position).sqrMagnitude;
                if (check < distance)
                {
                    hubby = binble;
                    distance = check;
                }
            }
        }

        if (hubby != null)
        {
            hubby.Die();
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
}