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
        return 1.0f;
    }

    protected override void Execute()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(self.transform.position, 0.5f, Vector2.zero);
        Glow hubby = null;
        float distance = float.PositiveInfinity;

        foreach (RaycastHit2D hit in hits)
        {
            Glow binble = hit.collider.GetComponent<Glow>();
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
            if (self.board.ContainsKey(StringLiterals.Energy))
            {
                self.board[StringLiterals.Energy].V = hubby.intensity * Time.deltaTime + (float)self.board[StringLiterals.Energy].V;
            }
            else
            {
                self.board[StringLiterals.Energy].V = 0.0f;
            }
        }
    }
}